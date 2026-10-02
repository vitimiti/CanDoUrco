// A generator for loading OpenGL libraries.
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

using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Text;

namespace CanDoUrco.OpenGl.Generator;

[Generator]
public sealed class GlGenerator : IIncrementalGenerator
{
    private static readonly DiagnosticDescriptor s_versionProblem = Descriptor(
        "OGL001",
        "Invalid OpenGL version"
    );
    private static readonly DiagnosticDescriptor s_unknownExtension = Descriptor(
        "OGL002",
        "Unknown OpenGL extension"
    );
    private static readonly DiagnosticDescriptor s_unsupportedExtension = Descriptor(
        "OGL003",
        "Unsupported OpenGL extension"
    );

    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(ctx =>
        {
            ctx.AddSource("OpenGlAttribute.g.cs", SourceText.From(Attributes.Generate(), Encoding.UTF8));
            ctx.AddSource("OpenGlLibraryLoader.g.cs", SourceText.From(LibraryLoader.Generate(), Encoding.UTF8));
        });

        var targets = context.SyntaxProvider.ForAttributeWithMetadataName(
            Attributes.AttributeMetadataName,
            static (node, _) => node is ClassDeclarationSyntax,
            static (ctx, _) => CreateTarget(ctx)
        );

        context.RegisterSourceOutput(targets, static (ctx, target) => Generate(ctx, target));
    }

    private static DiagnosticDescriptor Descriptor(string id, string title) =>
        new(id, title, "{0}", "CanDoUrco.OpenGl", DiagnosticSeverity.Warning, true);

    private static GlTarget? CreateTarget(GeneratorAttributeSyntaxContext ctx)
    {
        if (ctx.TargetSymbol is not INamedTypeSymbol symbol)
        {
            return null;
        }

        string? version = null;
        var profile = 0;
        var profileSet = false;
        var extensions = new List<string>();
        string? loaderMethod = null;
        foreach (var attribute in ctx.Attributes)
        {
            if (attribute.ConstructorArguments.Length != 1 || attribute.ConstructorArguments[0].Value is not string text)
            {
                continue;
            }

            var attributeProfile = 0;
            foreach (var named in attribute.NamedArguments)
            {
                if (named.Key == "Profile" && named.Value.Value is int value)
                {
                    attributeProfile = value;
                }
                else if (
                    named.Key == "LoaderMethod"
                    && named.Value.Value is string method
                    && method.Length > 0
                    && method.All(c => char.IsLetterOrDigit(c) || c is '_' or '.' or ':')
                )
                {
                    loaderMethod = method;
                }
            }

            if (GlSelection.IsVersion(text))
            {
                version = text;
                profile = attributeProfile;
                profileSet = true;
            }
            else
            {
                extensions.Add(text);
                if (!profileSet)
                {
                    profile = attributeProfile;
                }
            }
        }

        if (loaderMethod is null)
        {
            var glfw = ctx.SemanticModel.Compilation.GetTypeByMetadataName(
                "CanDoUrco.Glfw.Utilities.GlfwContextUtilities"
            );
            if (
                glfw is { DeclaredAccessibility: Accessibility.Public }
                && glfw.GetMembers("GetProcAddress").OfType<IMethodSymbol>().Any(m => m.IsStatic)
            )
            {
                loaderMethod = "global::CanDoUrco.Glfw.Utilities.GlfwContextUtilities.GetProcAddress";
            }
        }

        var containers = new List<string>();
        for (var parent = symbol.ContainingType; parent is not null; parent = parent.ContainingType)
        {
            containers.Insert(0, (parent.IsStatic ? "static " : "") + "partial class " + parent.Name);
        }

        var ns = symbol.ContainingNamespace is { IsGlobalNamespace: false } n ? n.ToDisplayString() : null;
        return new GlTarget(
            ns,
            symbol.Name,
            AccessibilityText(symbol.DeclaredAccessibility),
            symbol.IsStatic,
            containers.ToArray(),
            version,
            profile,
            extensions.ToArray(),
            loaderMethod
        );
    }

    private static string AccessibilityText(Accessibility accessibility) =>
        accessibility switch
        {
            Accessibility.Public => "public",
            Accessibility.Protected => "protected",
            Accessibility.ProtectedOrInternal => "protected internal",
            Accessibility.ProtectedAndInternal => "private protected",
            Accessibility.Private => "private",
            _ => "internal",
        };

    private static void Generate(SourceProductionContext ctx, GlTarget? target)
    {
        if (target is null)
        {
            return;
        }

        var registry = GlRegistry.Instance;
        var selection = GlSelection.Create(registry, target.Version, target.Profile, target.Extensions);
        foreach (var (id, message) in selection.Problems)
        {
            var descriptor = id switch
            {
                "OGL001" => s_versionProblem,
                "OGL002" => s_unknownExtension,
                _ => s_unsupportedExtension,
            };
            ctx.ReportDiagnostic(Diagnostic.Create(descriptor, Location.None, message));
        }

        var source = GlEmitter.Emit(target, registry, selection);
        var hint = string.Join(".", new[] { target.Namespace }.Concat(target.Containers.Select(c => c.Split(' ').Last())).Concat(new[] { target.Name }).Where(x => !string.IsNullOrEmpty(x))) + ".g.cs";
        ctx.AddSource(hint, SourceText.From(source, Encoding.UTF8));
    }
}
