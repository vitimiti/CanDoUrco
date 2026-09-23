// A generator to import required OpenGL and its extensions.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Collections.Immutable;
using System.Text;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CanDourco.OpenGl.Generator;

[Generator]
public sealed class OpenGlGenerator : IIncrementalGenerator
{
    private const string SpecificationAttribute =
        "CanDoUrco.OpenGl.Common.Attributes.OpenGlSpecificationAttribute";

    private const string ExtensionAttribute =
        "CanDoUrco.OpenGl.Common.Attributes.OpenGlExtensionAttribute";

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        var specifications = context
            .SyntaxProvider.ForAttributeWithMetadataName(
                SpecificationAttribute,
                static (_, _) => true,
                static (context, _) => ReadSpecification(context)
            )
            .Where(static specification => specification is not null);

        var extensions = context
            .SyntaxProvider.ForAttributeWithMetadataName(
                ExtensionAttribute,
                static (_, _) => true,
                static (context, _) => ReadExtensions(context)
            )
            .SelectMany(static (extensions, _) => extensions)
            .Collect();

        var registries = context
            .AdditionalTextsProvider.Where(static file =>
                Path.GetFileName(file.Path) is "gl.xml" or "glx.xml" or "wgl.xml"
            )
            .Select(
                static (file, cancellationToken) =>
                    XDocument.Parse(
                        file.GetText(cancellationToken)!.ToString(),
                        LoadOptions.PreserveWhitespace
                    )
            )
            .Collect();

        var input = specifications.Combine(extensions).Combine(registries);

        context.RegisterSourceOutput(
            input,
            (productionContext, input) =>
                GenerateSources(productionContext, input.Left.Left!, input.Left.Right, input.Right)
        );
    }

    private static Specification? ReadSpecification(GeneratorAttributeSyntaxContext context)
    {
        if (context.TargetSymbol is not INamedTypeSymbol symbol)
        {
            return null;
        }

        var attribute = context.Attributes.Single();
        var version = attribute.ConstructorArguments.FirstOrDefault().Value as string;
        if (string.IsNullOrWhiteSpace(version))
        {
            return null;
        }

        var profile = attribute.ConstructorArguments.Skip(1).FirstOrDefault().Value as int? ?? 0;
        return new Specification(
            @namespace: symbol.ContainingNamespace.IsGlobalNamespace
                ? null
                : symbol.ContainingNamespace.ToDisplayString(),
            className: symbol.Name,
            accessibility: SyntaxFacts.GetText(symbol.DeclaredAccessibility),
            version: version!,
            profile: profile
        );
    }

    private static ImmutableArray<string> ReadExtensions(GeneratorAttributeSyntaxContext context) =>
        [
            .. context
                .Attributes.Select(attribute =>
                    attribute.ConstructorArguments.FirstOrDefault().Value as string
                )
                .Where(extension => !string.IsNullOrWhiteSpace(extension))
                .Cast<string>()
                .Distinct(StringComparer.Ordinal),
        ];

    private static void GenerateSources(
        SourceProductionContext context,
        Specification specification,
        ImmutableArray<string> extensions,
        ImmutableArray<XDocument> registries
    )
    {
        var registry = registries.FirstOrDefault(document =>
            document.Root?.Element("commands")?.Attribute("namespace")?.Value == "GL"
        );

        if (registry?.Root is null)
        {
            return;
        }

        if (!Version.TryParse(specification.Version, out var targetVersion))
        {
            return;
        }

        // Matches CanDoUrco.OpenGl.Attributes.OpenGlProfile.Core's underlying value.
        var isCoreProfile = specification.Profile == 1;
        var profileName = isCoreProfile ? "core" : "compatibility";
        var requiredEnums = new HashSet<string>(StringComparer.Ordinal);
        var requiredCommands = new HashSet<string>(StringComparer.Ordinal);
        var features = registry
            .Root.Elements("feature")
            .Where(element =>
                element.Attribute("api")?.Value == "gl"
                && Version.TryParse(element.Attribute("number")?.Value, out var featureVersion)
                && featureVersion <= targetVersion
            )
            .OrderBy(element => Version.Parse(element.Attribute("number")!.Value));

        foreach (var feature in features)
        {
            ApplyRequires(feature, profileName, requiredEnums, requiredCommands);
            if (isCoreProfile)
            {
                ApplyRemoves(feature, requiredEnums, requiredCommands);
            }
        }

        foreach (var extension in extensions)
        {
            var extensionElement = registry
                .Root.Element("extensions")
                ?.Elements("extension")
                .FirstOrDefault(element => element.Attribute("name")?.Value == extension);

            if (extensionElement is not null)
            {
                ApplyRequires(extensionElement, profileName, requiredEnums, requiredCommands);
            }
        }

        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("#nullable enable");
        source.AppendLine("using System;");
        if (specification.Namespace is not null)
        {
            source.AppendLine($"namespace {specification.Namespace};");
        }

        source.AppendLine();
        source.AppendLine(
            $"{specification.Accessibility} static unsafe partial class {specification.ClassName}"
        );

        source.AppendLine("{");

        EmitFunctionLoader(source);
        EmitEnums(source, registry.Root, requiredEnums);
        EmitCommands(source, registry.Root, requiredCommands);

        source.AppendLine("}");
        source.AppendLine("#nullable restore");

        context.AddSource($"{specification.ClassName}.g.cs", source.ToString());
    }

    private static void EmitFunctionLoader(StringBuilder source)
    {
        source.AppendLine("    private static Func<string, nint>? _getProcAddress;");
        source.AppendLine();
        source.AppendLine(
            "    /// <summary>Modern OpenGL entry points are context-dependent; call this once after"
        );
        source.AppendLine("    /// a context is current (e.g. glfwGetProcAddress).</summary>");
        source.AppendLine(
            "    public static void LoadFunctions(Func<string, nint> getProcAddress)"
        );
        source.AppendLine("    {");
        source.AppendLine("        _getProcAddress = getProcAddress;");
        source.AppendLine("    }");
        source.AppendLine();
        source.AppendLine("    private static nint LoadFunctionPointer(string name)");
        source.AppendLine("    {");
        source.AppendLine("        if (_getProcAddress is null)");
        source.AppendLine("        {");
        source.AppendLine(
            "            throw new InvalidOperationException(\"Call LoadFunctions with a current OpenGL context before invoking OpenGL functions.\");"
        );
        source.AppendLine("        }");
        source.AppendLine();
        source.AppendLine("        var pointer = _getProcAddress(name);");
        source.AppendLine("        if (pointer == 0)");
        source.AppendLine("        {");
        source.AppendLine(
            "            throw new EntryPointNotFoundException($\"Unable to resolve OpenGL function '{name}'.\");"
        );
        source.AppendLine("        }");
        source.AppendLine();
        source.AppendLine("        return pointer;");
        source.AppendLine("    }");
        source.AppendLine();
    }

    private static void ApplyRequires(
        XElement owner,
        string profileName,
        HashSet<string> enums,
        HashSet<string> commands
    )
    {
        foreach (var require in owner.Elements("require"))
        {
            var requireProfile = require.Attribute("profile")?.Value;
            if (
                requireProfile is not null
                && !string.Equals(requireProfile, profileName, StringComparison.Ordinal)
            )
            {
                continue;
            }

            foreach (var enumElement in require.Elements("enum"))
            {
                var name = enumElement.Attribute("name")?.Value;
                if (name is not null)
                {
                    enums.Add(name);
                }
            }

            foreach (var commandElement in require.Elements("command"))
            {
                var name = commandElement.Attribute("name")?.Value;
                if (name is not null)
                {
                    commands.Add(name);
                }
            }
        }
    }

    private static void ApplyRemoves(
        XElement owner,
        HashSet<string> enums,
        HashSet<string> commands
    )
    {
        foreach (var remove in owner.Elements("remove"))
        {
            if (remove.Attribute("profile")?.Value != "core")
            {
                continue;
            }

            foreach (var enumElement in remove.Elements("enum"))
            {
                var name = enumElement.Attribute("name")?.Value;
                if (name is not null)
                {
                    enums.Remove(name);
                }
            }

            foreach (var commandElement in remove.Elements("command"))
            {
                var name = commandElement.Attribute("name")?.Value;
                if (name is not null)
                {
                    commands.Remove(name);
                }
            }
        }
    }

    private static void EmitEnums(
        StringBuilder source,
        XElement registry,
        HashSet<string> requiredEnums
    )
    {
        var values = registry
            .Descendants("enum")
            .Where(element =>
                element.Attribute("name")?.Value is string name && requiredEnums.Contains(name)
            )
            .GroupBy(element => element.Attribute("name")!.Value)
            .Select(group => group.First())
            .ToArray();

        if (values.Length == 0)
        {
            return;
        }

        // Nested to avoid collisions with commands sharing the same name
        // (e.g. GL_VIEWPORT vs. glViewport()).
        source.AppendLine("    public static class Enums");
        source.AppendLine("    {");

        foreach (var value in values)
        {
            var name = value.Attribute("name")!.Value;
            var strippedName = name.StartsWith("GL_", StringComparison.Ordinal)
                ? name.Substring(3)
                : name;
            var enumName = ToPascalCase(strippedName);

            var rawValue = value.Attribute("value")?.Value;
            if (rawValue is null)
            {
                continue;
            }

            var enumType = MapEnumType(value.Attribute("type")?.Value);
            source.AppendLine(
                $"        public const {enumType} {enumName} = {NormalizeEnumValue(rawValue, enumType)};"
            );
        }

        source.AppendLine("    }");
        source.AppendLine();
    }

    private static string ToPascalCase(string name)
    {
        var builder = new StringBuilder();

        foreach (var segment in name.Split('_'))
        {
            if (segment.Length == 0)
            {
                continue;
            }

            builder.Append(char.ToUpperInvariant(segment[0]));

            for (var i = 1; i < segment.Length; i++)
            {
                builder.Append(char.ToLowerInvariant(segment[i]));
            }
        }

        var result = builder.ToString();

        // Identifiers can't start with a digit (e.g. GL_2_BYTES).
        return result.Length > 0 && char.IsDigit(result[0]) ? "_" + result : result;
    }

    private static void EmitCommands(
        StringBuilder source,
        XElement registry,
        HashSet<string> requiredCommands
    )
    {
        var commands =
            registry
                .Element("commands")
                ?.Elements("command")
                .Where(element =>
                    element.Element("proto")?.Element("name")?.Value is string name
                    && requiredCommands.Contains(name)
                )
            ?? [];

        foreach (var command in commands)
        {
            var proto = command.Element("proto");
            if (proto?.Element("name") is null)
            {
                continue;
            }

            var nativeName = proto.Element("name")!.Value;
            var managedName = StripGlPrefix(nativeName);
            var returnType = GetReturnType(proto);
            var parameters = command
                .Elements("param")
                .Select(parameter =>
                    (
                        Name: EscapeIdentifier(parameter.Element("name")?.Value ?? "value"),
                        Type: GetParameterType(parameter)
                    )
                )
                .ToArray();

            var parameterList = string.Join(
                ", ",
                parameters.Select(parameter => $"{parameter.Type} {parameter.Name}")
            );

            var argumentList = string.Join(", ", parameters.Select(parameter => parameter.Name));
            var fieldName = "_" + char.ToLowerInvariant(managedName[0]) + managedName.Substring(1);
            var pointerSignature = string.Join(
                ", ",
                parameters.Select(parameter => parameter.Type).Append(returnType)
            );

            var pointerType = $"delegate* unmanaged[Cdecl]<{pointerSignature}>";
            source.AppendLine($"    private static {pointerType} {fieldName};");
            source.AppendLine();
            source.AppendLine($"    public static {returnType} {managedName}({parameterList})");
            source.AppendLine("    {");
            source.AppendLine($"        if ({fieldName} == default)");
            source.AppendLine("        {");
            source.AppendLine(
                $"            {fieldName} = ({pointerType})LoadFunctionPointer(\"{nativeName}\");"
            );
            source.AppendLine("        }");
            source.AppendLine();

            source.AppendLine(
                returnType == "void"
                    ? $"        {fieldName}({argumentList});"
                    : $"        return {fieldName}({argumentList});"
            );

            source.AppendLine("    }");
            source.AppendLine();
        }
    }

    private static string StripGlPrefix(string nativeName) =>
        nativeName.StartsWith("gl", StringComparison.Ordinal) && nativeName.Length > 2
            ? nativeName.Substring(2)
            : nativeName;

    private static string EscapeIdentifier(string identifier) =>
        SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(identifier) != SyntaxKind.None
            ? $"@{identifier}"
            : identifier;

    private static string GetReturnType(XElement proto)
    {
        var depth = GetPointerDepth(proto);
        if (depth == 0)
        {
            return MapType(GetScalarType(proto));
        }

        var scalar = GetScalarType(proto);

        // Pointer-to-pointer and untyped void* results stay raw void pointers.
        return depth == 1 && scalar != "void" ? $"{MapType(scalar)}*" : "void*";
    }

    private static string GetParameterType(XElement parameter)
    {
        var depth = GetPointerDepth(parameter);
        var scalar = GetScalarType(parameter);
        if (depth == 0)
        {
            return MapType(scalar);
        }

        // Pointer-to-pointer and untyped void* buffers stay raw void pointers.
        return depth == 1 && scalar != "void" ? $"{MapType(scalar)}*" : "void*";
    }

    private static int GetPointerDepth(XElement element) =>
        element.Nodes().OfType<XText>().Sum(text => text.Value.Count(c => c == '*'));

    private static string GetScalarType(XElement element)
    {
        var ptype = element.Element("ptype")?.Value;
        if (!string.IsNullOrWhiteSpace(ptype))
        {
            return ptype!;
        }

        var text = element.Value;
        return text.Contains("void", StringComparison.Ordinal) ? "void"
            : text.Contains("double", StringComparison.Ordinal) ? "double"
            : text.Contains("float", StringComparison.Ordinal) ? "float"
            : text.Contains("unsigned", StringComparison.Ordinal) ? "uint"
            : "int";
    }

    private static string MapType(string type) =>
        type switch
        {
            "void" => "void",
            "GLenum" => "uint",
            "GLbitfield" => "uint",
            "GLboolean" => "byte",
            "GLbyte" => "sbyte",
            "GLubyte" => "byte",
            "GLshort" => "short",
            "GLushort" => "ushort",
            "GLint" => "int",
            "GLuint" => "uint",
            "GLsizei" => "int",
            "GLfloat" => "float",
            "GLdouble" => "double",
            "GLchar" => "byte",
            _ => "nint",
        };

    private static string MapEnumType(string? type) =>
        type switch
        {
            "ull" => "ulong",
            _ => "uint",
        };

    private static string NormalizeEnumValue(string value, string enumType)
    {
        if (!value.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            return value;
        }

        var suffix = enumType == "ulong" ? "ul" : "u";
        return value.TrimEnd('u', 'U', 'l', 'L') + suffix;
    }

    private sealed class Specification(
        string? @namespace,
        string className,
        string accessibility,
        string version,
        int profile
    )
    {
        public string? Namespace { get; set; } = @namespace;
        public string ClassName { get; set; } = className;
        public string Accessibility { get; set; } = accessibility;
        public string Version { get; set; } = version;
        public int Profile { get; set; } = profile;
    }
}
