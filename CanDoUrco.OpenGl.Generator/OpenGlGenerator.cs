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
        source.AppendLine("using System.Runtime.InteropServices;");
        if (specification.Namespace is not null)
        {
            source.AppendLine($"namespace {specification.Namespace};");
        }

        source.AppendLine();
        source.AppendLine(
            $"{specification.Accessibility} static partial class {specification.ClassName}"
        );

        source.AppendLine("{");
        source.AppendLine("    internal static class Native");
        source.AppendLine("    {");

        EmitEnums(source, registry.Root, requiredEnums);
        EmitCommands(source, registry.Root, requiredCommands);

        source.AppendLine("    }");
        source.AppendLine("}");
        source.AppendLine("#nullable restore");

        context.AddSource($"{specification.ClassName}.g.cs", source.ToString());
        context.AddSource(
            $"{specification.ClassName}.Api.g.cs",
            GeneratePublicApi(specification, registry.Root, requiredEnums, requiredCommands)
        );
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

    private static string GeneratePublicApi(
        Specification specification,
        XElement registry,
        HashSet<string> requiredEnums,
        HashSet<string> requiredCommands
    )
    {
        var apiNamespace = specification.Namespace;
        var source = new StringBuilder();
        source.AppendLine("// <auto-generated />");
        source.AppendLine("#nullable enable");
        source.AppendLine("using System;");
        source.AppendLine("using System.Collections.Generic;");
        source.AppendLine("using System.Runtime.InteropServices;");
        if (specification.Namespace is not null)
        {
            source.AppendLine($"using {specification.Namespace};");
        }

        if (apiNamespace is not null)
        {
            source.AppendLine($"namespace {apiNamespace};");
        }

        var rawClassName = specification.Namespace is null
            ? $"global::{specification.ClassName}.Native"
            : $"global::{specification.Namespace}.{specification.ClassName}.Native";

        source.AppendLine();
        source.AppendLine(
            $"{specification.Accessibility} static partial class {specification.ClassName}"
        );
        source.AppendLine("{");

        source.AppendLine("    public static class Enums");
        source.AppendLine("    {");
        EmitPublicEnums(source, registry, requiredEnums);
        source.AppendLine("    }");
        source.AppendLine();

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
            EmitPublicCommand(source, command, rawClassName, registry);
        }

        EmitNativeLoader(source);
        source.AppendLine("}");
        source.AppendLine("#nullable restore");
        return source.ToString();
    }

    private static void EmitPublicEnums(
        StringBuilder source,
        XElement registry,
        HashSet<string> requiredEnums
    )
    {
        var bitmaskGroups = new HashSet<string>(
            registry
                .Elements("enums")
                .Where(element => element.Attribute("type")?.Value == "bitmask")
                .Select(element => element.Attribute("group")?.Value)
                .Where(group => !string.IsNullOrWhiteSpace(group))
                .Select(group => group!),
            StringComparer.Ordinal
        );
        var groups = new Dictionary<string, List<XElement>>(StringComparer.Ordinal);

        foreach (var value in registry.Descendants("enum"))
        {
            var name = value.Attribute("name")?.Value;
            if (name is null || !requiredEnums.Contains(name))
            {
                continue;
            }

            foreach (var group in (value.Attribute("group")?.Value ?? string.Empty).Split(','))
            {
                var groupName = group.Trim();
                if (groupName.Length == 0)
                {
                    continue;
                }

                if (!groups.TryGetValue(groupName, out var values))
                {
                    values = [];
                    groups.Add(groupName, values);
                }

                if (!values.Any(existing => existing.Attribute("name")?.Value == name))
                {
                    values.Add(value);
                }
            }
        }

        foreach (var pair in groups.OrderBy(pair => pair.Key, StringComparer.Ordinal))
        {
            var groupName = pair.Key;
            var values = pair.Value;
            var enumName = EscapeIdentifier(groupName);
            var enumType = values.Any(value =>
                value.Attribute("type")?.Value == "ull"
                || int.TryParse(value.Attribute("bitpos")?.Value, out var bit) && bit >= 32
            )
                ? "ulong"
                : "uint";
            if (bitmaskGroups.Contains(groupName))
            {
                source.AppendLine("        [Flags]");
            }

            source.AppendLine($"        public enum {enumName} : {enumType}");
            source.AppendLine("        {");

            foreach (var value in values)
            {
                var rawValue = ResolveEnumValue(
                    value,
                    registry,
                    new HashSet<string>(StringComparer.Ordinal)
                );
                if (rawValue is null)
                {
                    continue;
                }

                var nativeName = value.Attribute("name")!.Value;
                var memberName = nativeName.StartsWith("GL_", StringComparison.Ordinal)
                    ? nativeName.Substring(3)
                    : nativeName;
                var suffix = enumType == "ulong" ? "ul" : "u";
                source.AppendLine(
                    $"            {EscapeIdentifier(ToPascalCase(memberName))} = {NormalizeEnumValue(rawValue, enumType)}{(rawValue.StartsWith("0x", StringComparison.OrdinalIgnoreCase) ? string.Empty : suffix)},"
                );
            }

            source.AppendLine("        }");
            source.AppendLine();
        }
    }

    private static string? ResolveEnumValue(
        XElement value,
        XElement registry,
        HashSet<string> visited
    )
    {
        var name = value.Attribute("name")?.Value;
        if (name is not null && !visited.Add(name))
        {
            return null;
        }

        var literal = value.Attribute("value")?.Value;
        if (literal is not null)
        {
            return literal;
        }

        var alias = value.Attribute("alias")?.Value;
        if (alias is not null)
        {
            var target = registry
                .Descendants("enum")
                .FirstOrDefault(element => element.Attribute("name")?.Value == alias);
            return target is null ? null : ResolveEnumValue(target, registry, visited);
        }

        var bitPosition = value.Attribute("bitpos")?.Value;
        if (int.TryParse(bitPosition, out var bit))
        {
            return (1UL << bit).ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        if (
            int.TryParse(value.Attribute("offset")?.Value, out var offset)
            && int.TryParse(value.Attribute("extnumber")?.Value, out var extensionNumber)
        )
        {
            var extensionValue = 1_000_000_000L + (extensionNumber - 1L) * 1_000L + offset;
            if (value.Attribute("dir")?.Value == "-")
            {
                extensionValue = -extensionValue;
            }

            return extensionValue.ToString(System.Globalization.CultureInfo.InvariantCulture);
        }

        return null;
    }

    private static void EmitPublicCommand(
        StringBuilder source,
        XElement command,
        string rawClassName,
        XElement registry
    )
    {
        var proto = command.Element("proto");
        var nativeName = proto?.Element("name")?.Value;
        if (proto is null || nativeName is null)
        {
            return;
        }

        var managedName = StripGlPrefix(nativeName);
        var nativeReturnType = GetReturnType(proto);
        var returnGroup = proto.Attribute("group")?.Value?.Split(',').FirstOrDefault()?.Trim();
        var publicReturnType = returnGroup is not null
            ? GetGroupTypeName(returnGroup)
            : nativeReturnType;
        var parameters = command
            .Elements("param")
            .Select((parameter, index) => CreateApiParameter(parameter, index, command, registry))
            .ToArray();
        var callbackParameters = parameters
            .Select((parameter, index) => (Parameter: parameter, Index: index))
            .Where(item => item.Parameter.Callback is not null)
            .ToArray();
        foreach (var item in callbackParameters)
        {
            source.AppendLine(
                $"    private static {rawClassName}.{item.Parameter.Callback!.NativeDelegateName}? {GetCallbackRootName(managedName, item.Index)};"
            );
        }

        var signature = string.Join(
            ", ",
            parameters
                .Where(parameter => !parameter.IsOmitted)
                .Select(parameter =>
                    $"{(parameter.Modifier.Length == 0 ? string.Empty : parameter.Modifier + " ")}{parameter.PublicType} {parameter.Name}"
                )
        );

        source.AppendLine($"    public static {publicReturnType} {managedName}({signature})");
        source.AppendLine("    {");

        foreach (var item in callbackParameters)
        {
            var callback = item.Parameter.Callback!;
            var localCallbackName = $"__nativeCallback{item.Index}";
            var managedCallbackName = $"__managedCallback{item.Index}";
            var nativeArguments = string.Join(
                ", ",
                callback.Parameters.Select(parameter => EscapeIdentifier(parameter.Name))
            );
            var publicArguments = string.Join(
                ", ",
                callback
                    .Parameters.Where(parameter => !parameter.IsUserData)
                    .Select(parameter => GetPublicCallbackArgument(callback, parameter))
            );
            var callbackInvocation =
                callback.ReturnType == "void"
                    ? $"{managedCallbackName}!.Invoke({publicArguments})"
                    : $"{managedCallbackName}!.Invoke({publicArguments})";

            source.AppendLine($"        var {managedCallbackName} = {item.Parameter.Name};");
            source.AppendLine(
                $"        {rawClassName}.{callback.NativeDelegateName}? {localCallbackName} = {managedCallbackName} is null ? null : new {rawClassName}.{callback.NativeDelegateName}(({nativeArguments}) => {callbackInvocation});"
            );
        }

        foreach (var parameter in parameters)
        {
            if (parameter.IsOutValue)
            {
                source.AppendLine($"        {parameter.Name} = default;");
            }
            else if (parameter.IsSpan && parameter.LengthParameter is not null)
            {
                source.AppendLine(
                    $"        if ({parameter.Name}.Length < checked((int)({parameter.LengthParameter}))) throw new ArgumentException(\"Span is shorter than the OpenGL parameter length.\", nameof({parameter.Name}));"
                );
            }
        }

        source.AppendLine(
            $"        {rawClassName}.{managedName} ??= NativeLoader.GetDelegate<{rawClassName}.{managedName}Delegate>(\"{nativeName}\");"
        );

        if (nativeReturnType != "void")
        {
            source.AppendLine($"        {nativeReturnType} result;");
        }

        var fixedParameters = parameters.Where(parameter => parameter.NeedsFixed).ToArray();
        if (fixedParameters.Length > 0)
        {
            source.AppendLine("        unsafe");
            source.AppendLine("        {");
        }

        for (var index = 0; index < fixedParameters.Length; index++)
        {
            var parameter = fixedParameters[index];
            var target = parameter.IsOutValue ? $"&{parameter.Name}" : parameter.Name;
            source.AppendLine(
                $"            fixed ({parameter.ElementType}* __pointer{index} = {target})"
            );
            source.AppendLine("            {");
        }

        var callArguments = string.Join(
            ", ",
            parameters.Select(
                (parameter, index) =>
                {
                    if (parameter.IsOmitted)
                    {
                        return "(nint)0";
                    }

                    if (parameter.Callback is not null)
                    {
                        var callback = $"__nativeCallback{index}";
                        return $"{callback} is null ? (nint)0 : Marshal.GetFunctionPointerForDelegate({callback})";
                    }

                    if (parameter.NeedsFixed)
                    {
                        var pointerIndex = Array.IndexOf(fixedParameters, parameter);
                        return $"(nint)__pointer{pointerIndex}";
                    }

                    return parameter.GroupName is null
                        ? parameter.Name
                        : $"({parameter.NativeType}){parameter.Name}";
                }
            )
        );

        var call = $"{rawClassName}.{managedName}({callArguments})";
        var callIndent = fixedParameters.Length > 0 ? "                " : "        ";
        if (nativeReturnType == "void")
        {
            source.AppendLine($"{callIndent}{call};");
        }
        else
        {
            source.AppendLine($"{callIndent}result = {call};");
        }

        for (var index = fixedParameters.Length - 1; index >= 0; index--)
        {
            source.AppendLine("            }");
        }

        if (fixedParameters.Length > 0)
        {
            source.AppendLine("        }");
        }

        foreach (var item in callbackParameters)
        {
            source.AppendLine(
                $"        {GetCallbackRootName(managedName, item.Index)} = __nativeCallback{item.Index};"
            );
        }

        if (nativeReturnType != "void")
        {
            source.AppendLine(
                returnGroup is null
                    ? "        return result;"
                    : $"        return ({publicReturnType})result;"
            );
        }

        source.AppendLine("    }");
        source.AppendLine();
    }

    private static ApiParameter CreateApiParameter(
        XElement parameter,
        int index,
        XElement command,
        XElement registry
    )
    {
        var rawName = parameter.Element("name")?.Value ?? $"value{index}";
        var name = EscapeIdentifier(rawName);
        var nativeType =
            GetPointerDepth(parameter) == 0 ? MapType(GetScalarType(parameter)) : "nint";
        var groupName = parameter.Attribute("group")?.Value?.Split(',').FirstOrDefault()?.Trim();
        var depth = GetPointerDepth(parameter);
        var length = parameter.Attribute("len")?.Value;

        var callback =
            depth == 0 ? ReadCallbackSignature(registry, GetScalarType(parameter)) : null;
        if (callback is not null)
        {
            return new ApiParameter(
                name,
                nativeType,
                $"{callback.PublicDelegateType}?",
                string.Empty,
                null,
                nativeType,
                null,
                false,
                false,
                false,
                callback
            );
        }

        var commandHasCallback = command
            .Elements("param")
            .Any(candidate =>
                GetPointerDepth(candidate) == 0
                && ReadCallbackSignature(registry, GetScalarType(candidate)) is not null
            );
        var isOmittedCallbackUserData =
            commandHasCallback && rawName.Equals("userParam", StringComparison.OrdinalIgnoreCase);

        if (isOmittedCallbackUserData)
        {
            return new ApiParameter(
                name,
                "nint",
                "nint",
                string.Empty,
                null,
                "nint",
                null,
                false,
                false,
                false,
                null,
                true
            );
        }

        if (depth == 0)
        {
            return new ApiParameter(
                name,
                nativeType,
                groupName is null ? nativeType : GetGroupTypeName(groupName),
                string.Empty,
                groupName,
                nativeType,
                null,
                false,
                false,
                false,
                null,
                isOmittedCallbackUserData
            );
        }

        var pointerText = string.Concat(
            parameter.Nodes().OfType<XText>().Select(text => text.Value)
        );
        var isConst = pointerText.Contains("const", StringComparison.Ordinal);
        var elementType = depth > 1 ? "nint" : MapType(GetScalarType(parameter));
        if (elementType == "void")
        {
            elementType = "byte";
        }

        var lengthParameter =
            length is not null && IsIdentifier(length)
                ? command
                    .Elements("param")
                    .FirstOrDefault(candidate =>
                        candidate.Element("name")?.Value == length
                        && GetPointerDepth(candidate) == 0
                    )
                : null;
        var outputValue = !isConst && length == "1";
        var isSpan = length is not null && !outputValue;
        var publicType =
            outputValue ? elementType
            : isSpan ? $"{(isConst ? "ReadOnlySpan" : "Span")}<{elementType}>"
            : "nint";

        return new ApiParameter(
            name,
            nativeType,
            publicType,
            outputValue ? "out" : string.Empty,
            null,
            elementType,
            lengthParameter is null ? null : EscapeIdentifier(length!),
            isSpan,
            outputValue,
            isSpan || outputValue,
            null,
            isOmittedCallbackUserData
        );
    }

    private static string GetCallbackRootName(string methodName, int parameterIndex) =>
        $"__{methodName}{parameterIndex}Callback";

    private static string GetPublicCallbackArgument(
        CallbackSignature callback,
        CallbackParameter parameter
    )
    {
        var name = EscapeIdentifier(parameter.Name);
        if (!parameter.IsPointer || parameter.PublicType != "string?")
        {
            return name;
        }

        var length = callback.Parameters.FirstOrDefault(argument =>
            argument.Name.Equals("length", StringComparison.OrdinalIgnoreCase)
        );
        return length is null
            ? $"Marshal.PtrToStringUTF8((nint){name})"
            : $"({EscapeIdentifier(length.Name)} < 0 ? Marshal.PtrToStringUTF8((nint){name}) : Marshal.PtrToStringUTF8((nint){name}, {EscapeIdentifier(length.Name)}))";
    }

    private static bool IsIdentifier(string value) =>
        value.Length > 0
        && (char.IsLetter(value[0]) || value[0] == '_')
        && value.Skip(1).All(character => char.IsLetterOrDigit(character) || character == '_');

    private static string GetGroupTypeName(string groupName) =>
        $"Enums.{EscapeIdentifier(groupName)}";

    private static void EmitNativeLoader(StringBuilder source)
    {
        source.AppendLine("    private static class NativeLoader");
        source.AppendLine("    {");
        source.AppendLine(
            "        internal static T GetDelegate<T>(string functionName) where T : Delegate"
        );
        source.AppendLine("        {");
        source.AppendLine("            foreach (var libraryName in GetLibraryNames())");
        source.AppendLine("            {");
        source.AppendLine(
            "                if (!NativeLibrary.TryLoad(libraryName, out var handle)) continue;"
        );
        source.AppendLine(
            "                if (NativeLibrary.TryGetExport(handle, functionName, out var pointer))"
        );
        source.AppendLine(
            "                    return Marshal.GetDelegateForFunctionPointer<T>(pointer);"
        );
        source.AppendLine("                NativeLibrary.Free(handle);");
        source.AppendLine("            }");
        source.AppendLine(
            "            throw new EntryPointNotFoundException($\"Function '{functionName}' not found in any OpenGL library.\");"
        );
        source.AppendLine("        }");
        source.AppendLine();
        source.AppendLine("        private static IEnumerable<string> GetLibraryNames()");
        source.AppendLine("        {");
        source.AppendLine(
            "            if (OperatingSystem.IsWindows()) yield return \"opengl32.dll\";"
        );
        source.AppendLine("            else if (OperatingSystem.IsLinux())");
        source.AppendLine("            {");
        source.AppendLine("                yield return \"libOpenGL.so.0\";");
        source.AppendLine("                yield return \"libOpenGL.so\";");
        source.AppendLine("                yield return \"libEGL.so.1\";");
        source.AppendLine("                yield return \"libEGL.so\";");
        source.AppendLine("                yield return \"libGL.so.1\";");
        source.AppendLine("                yield return \"libGL.so\";");
        source.AppendLine("            }");
        source.AppendLine(
            "            else if (OperatingSystem.IsMacOS()) yield return \"/System/Library/Frameworks/OpenGL.framework/OpenGL\";"
        );
        source.AppendLine(
            "            else throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);"
        );
        source.AppendLine("        }");
        source.AppendLine("    }");
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
        source.AppendLine("    internal static class Enums");
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
                $"        internal const {enumType} {enumName} = {NormalizeEnumValue(rawValue, enumType)};"
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
                .ToArray()
            ?? [];

        var callbackSignatures = new Dictionary<string, CallbackSignature>(StringComparer.Ordinal);
        foreach (var parameter in commands.SelectMany(command => command.Elements("param")))
        {
            var callback = ReadCallbackSignature(registry, GetScalarType(parameter));
            if (callback is not null && !callbackSignatures.ContainsKey(callback.TypeName))
            {
                callbackSignatures.Add(callback.TypeName, callback);
            }
        }

        EmitCallbackDelegates(source, callbackSignatures.Values);

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

            var delegateName = $"{managedName}Delegate";
            source.AppendLine("    [UnmanagedFunctionPointer(CallingConvention.Winapi)]");
            source.AppendLine(
                $"    internal delegate {returnType} {delegateName}({parameterList});"
            );
            source.AppendLine();
            source.AppendLine($"    internal static {delegateName}? {managedName};");
            source.AppendLine();
        }
    }

    private static CallbackSignature? ReadCallbackSignature(XElement registry, string typeName)
    {
        var callbackType = registry
            .Element("types")
            ?.Elements("type")
            .FirstOrDefault(type => type.Element("name")?.Value == typeName);
        var nameElement = callbackType?.Element("name");
        if (callbackType is null || nameElement is null)
        {
            return null;
        }

        var prefix = string.Concat(
            callbackType
                .Nodes()
                .TakeWhile(node => node != nameElement)
                .OfType<XText>()
                .Select(text => text.Value)
        );
        var suffix = string.Concat(
            callbackType
                .Nodes()
                .SkipWhile(node => node != nameElement)
                .Skip(1)
                .OfType<XText>()
                .Select(text => text.Value)
        );
        var argumentsStart = suffix.IndexOf('(');
        var argumentsEnd = suffix.LastIndexOf(");", StringComparison.Ordinal);
        if (argumentsStart < 0 || argumentsEnd < argumentsStart)
        {
            return null;
        }

        var returnType = prefix
            .Replace("typedef", string.Empty)
            .Replace("*", string.Empty)
            .Replace("(", string.Empty)
            .Trim()
            .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
            .LastOrDefault();
        if (string.IsNullOrWhiteSpace(returnType))
        {
            return null;
        }

        var argumentText = suffix.Substring(argumentsStart + 1, argumentsEnd - argumentsStart - 1);
        var arguments = argumentText.Trim() is "" or "void"
            ? []
            : argumentText.Split(',').Select(ParseCallbackParameter).ToArray();

        return new CallbackSignature(typeName, MapType(returnType!), arguments);
    }

    private static CallbackParameter ParseCallbackParameter(string declaration)
    {
        var trimmed = declaration.Trim();
        var lastWhitespace = trimmed.LastIndexOfAny([' ', '\t', '\r', '\n']);
        var rawName = (lastWhitespace < 0 ? trimmed : trimmed.Substring(lastWhitespace + 1))
            .TrimStart('*', '&')
            .TrimEnd('[', ']');
        if (rawName.Length == 0)
        {
            rawName = "value";
        }

        var typeDeclaration = trimmed.Substring(
            0,
            trimmed.LastIndexOf(rawName, StringComparison.Ordinal)
        );
        var isPointer =
            typeDeclaration.Contains("*", StringComparison.Ordinal)
            || declaration
                .Substring(lastWhitespace < 0 ? 0 : lastWhitespace + 1)
                .Contains("*", StringComparison.Ordinal);
        var scalarType =
            typeDeclaration
                .Replace("const", string.Empty)
                .Replace("volatile", string.Empty)
                .Replace("*", string.Empty)
                .Trim()
                .Split((char[]?)null, StringSplitOptions.RemoveEmptyEntries)
                .LastOrDefault()
            ?? "void";
        var publicType = isPointer
            ? scalarType is "GLchar" or "GLcharARB" or "GLubyte"
                ? "string?"
                : "nint"
            : MapType(scalarType);

        return new CallbackParameter(
            rawName,
            isPointer ? "nint" : MapType(scalarType),
            publicType,
            isPointer,
            scalarType
        );
    }

    private static void EmitCallbackDelegates(
        StringBuilder source,
        IEnumerable<CallbackSignature> callbacks
    )
    {
        foreach (var callback in callbacks)
        {
            var parameters = string.Join(
                ", ",
                callback.Parameters.Select(parameter =>
                    $"{parameter.NativeType} {EscapeIdentifier(parameter.Name)}"
                )
            );
            source.AppendLine("    [UnmanagedFunctionPointer(CallingConvention.Winapi)]");
            source.AppendLine(
                $"    internal delegate {callback.ReturnType} {callback.NativeDelegateName}({parameters});"
            );
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

        // Pointers and arrays of any kind are passed through as raw handles.
        return depth == 0 ? MapType(GetScalarType(proto)) : "nint";
    }

    private static string GetParameterType(XElement parameter)
    {
        var depth = GetPointerDepth(parameter);

        // Pointers and arrays of any kind are passed through as raw handles.
        return depth == 0 ? MapType(GetScalarType(parameter)) : "nint";
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
            "GLcharARB" => "byte",
            "GLclampd" => "double",
            "GLclampf" => "float",
            "GLclampx" => "int",
            "GLfixed" => "int",
            "GLhalf" => "ushort",
            "GLhalfARB" => "ushort",
            "GLhalfNV" => "ushort",
            "GLint64" => "long",
            "GLint64EXT" => "long",
            "GLintptr" => "nint",
            "GLintptrARB" => "nint",
            "GLsizeiptr" => "nint",
            "GLsizeiptrARB" => "nint",
            "GLuint64" => "ulong",
            "GLuint64EXT" => "ulong",
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

    private sealed class ApiParameter(
        string name,
        string nativeType,
        string publicType,
        string modifier,
        string? groupName,
        string elementType,
        string? lengthParameter,
        bool isSpan,
        bool isOutValue,
        bool needsFixed,
        CallbackSignature? callback = null,
        bool isOmitted = false
    )
    {
        public string Name { get; } = name;
        public string NativeType { get; } = nativeType;
        public string PublicType { get; } = publicType;
        public string Modifier { get; } = modifier;
        public string? GroupName { get; } = groupName;
        public string ElementType { get; } = elementType;
        public string? LengthParameter { get; } = lengthParameter;
        public bool IsSpan { get; } = isSpan;
        public bool IsOutValue { get; } = isOutValue;
        public bool NeedsFixed { get; } = needsFixed;
        public CallbackSignature? Callback { get; } = callback;
        public bool IsOmitted { get; } = isOmitted;
    }

    private sealed class CallbackSignature(
        string typeName,
        string returnType,
        CallbackParameter[] parameters
    )
    {
        public string TypeName { get; } = typeName;
        public string ReturnType { get; } = returnType;
        public CallbackParameter[] Parameters { get; } = parameters;
        public string NativeDelegateName { get; } = $"{typeName}CallbackDelegate";

        public string PublicDelegateType
        {
            get
            {
                var parameterTypes = Parameters
                    .Where(parameter => !parameter.IsUserData)
                    .Select(parameter => parameter.PublicType)
                    .ToArray();
                if (ReturnType == "void")
                {
                    return parameterTypes.Length == 0
                        ? "Action"
                        : $"Action<{string.Join(", ", parameterTypes)}>";
                }

                return $"Func<{string.Join(", ", parameterTypes.Append(ReturnType))}>";
            }
        }
    }

    private sealed class CallbackParameter(
        string name,
        string nativeType,
        string publicType,
        bool isPointer,
        string scalarType
    )
    {
        public string Name { get; } = name;
        public string NativeType { get; } = nativeType;
        public string PublicType { get; } = publicType;
        public bool IsPointer { get; } = isPointer;
        public string ScalarType { get; } = scalarType;
        public bool IsUserData { get; } =
            name.Equals("userParam", StringComparison.OrdinalIgnoreCase);
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
