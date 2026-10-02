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

using System.Globalization;
using System.Reflection;
using System.Text;
using System.Xml.Linq;

namespace CanDoUrco.OpenGL.Generator;

internal sealed class EnumDef(string name, decimal value, string? api, string[] groups)
{
    public string Name { get; } = name;
    public decimal Value { get; } = value;
    public string? Api { get; } = api;
    public string[] Groups { get; } = groups;
}

internal sealed class ParamDef(
    string name,
    string baseType,
    bool isConst,
    int depth,
    string? len,
    string? group
)
{
    public string Name { get; } = name;
    public string BaseType { get; } = baseType;
    public bool IsConst { get; } = isConst;
    public int Depth { get; } = depth;
    public string? Len { get; } = len;
    public string? Group { get; } = group;
}

internal sealed class CommandDef(string name, ParamDef returnType, List<ParamDef> parameters)
{
    public string Name { get; } = name;
    public ParamDef Return { get; } = returnType;
    public List<ParamDef> Parameters { get; } = parameters;
}

internal sealed class RequireBlock(string? api, string? profile, bool isRemove)
{
    public string? Api { get; } = api;
    public string? Profile { get; } = profile;
    public bool IsRemove { get; } = isRemove;
    public List<string> Enums { get; } = [];
    public List<string> Commands { get; } = [];
}

internal sealed class FeatureDef(string api, Version number)
{
    public string Api { get; } = api;
    public Version Number { get; } = number;
    public List<RequireBlock> Blocks { get; } = [];
}

internal sealed class ExtensionDef(string name, HashSet<string> supported)
{
    public string Name { get; } = name;
    public HashSet<string> Supported { get; } = supported;
    public List<RequireBlock> Blocks { get; } = [];
}

internal sealed class GLRegistry
{
    private const string ResourceName = "OpenGLGenerator.Resources.GL.xml";

    private static readonly Lazy<GLRegistry> LazyInstance = new(Load);

    private GLRegistry() { }

    public static GLRegistry Instance => LazyInstance.Value;

    public Dictionary<string, List<EnumDef>> Enums { get; } = [];
    public HashSet<string> BitmaskGroups { get; } = [];
    public Dictionary<string, CommandDef> Commands { get; } = [];
    public List<FeatureDef> Features { get; } = [];
    public Dictionary<string, ExtensionDef> Extensions { get; } = [];

    private static GLRegistry Load()
    {
        using var stream =
            Assembly.GetExecutingAssembly().GetManifestResourceStream(ResourceName)
            ?? throw new InvalidOperationException($"Missing embedded resource {ResourceName}.");

        var root = XDocument.Load(stream).Root!;
        var registry = new GLRegistry();

        foreach (var block in root.Elements("enums"))
        {
            var isBitmask = (string?)block.Attribute("type") == "bitmask";
            foreach (var e in block.Elements("enum"))
            {
                var name = (string?)e.Attribute("name");
                var valueText = (string?)e.Attribute("value");
                if (name is null || valueText is null || !TryParseValue(valueText, out var value))
                {
                    continue;
                }

                var groupText = (string?)e.Attribute("group");
                var groups = groupText is null
                    ? []
                    : groupText
                        .Split([','], StringSplitOptions.RemoveEmptyEntries)
                        .Select(g => g.Trim())
                        .ToArray();

                if (isBitmask)
                {
                    foreach (var g in groups)
                    {
                        registry.BitmaskGroups.Add(g);
                    }
                }

                if (!registry.Enums.TryGetValue(name, out var list))
                {
                    registry.Enums[name] = list = [];
                }

                list.Add(new EnumDef(name, value, (string?)e.Attribute("api"), groups));
            }

            var blockGroup = (string?)block.Attribute("group");
            if (isBitmask && blockGroup is not null)
            {
                registry.BitmaskGroups.Add(blockGroup);
            }
        }

        foreach (var c in root.Elements("commands").Elements("command"))
        {
            var command = ParseCommand(c);
            if (command is not null)
            {
                registry.Commands[command.Name] = command;
            }
        }

        foreach (var f in root.Elements("feature"))
        {
            var api = (string?)f.Attribute("api");
            if (api is null || !Version.TryParse((string?)f.Attribute("number"), out var number))
            {
                continue;
            }

            var feature = new FeatureDef(api, number);
            feature.Blocks.AddRange(f.Elements().Select(ParseBlock).OfType<RequireBlock>());
            registry.Features.Add(feature);
        }

        foreach (var x in root.Elements("extensions").Elements("extension"))
        {
            var name = (string?)x.Attribute("name");
            if (name is null)
            {
                continue;
            }

            var supported = new HashSet<string>(
                ((string?)x.Attribute("supported") ?? "").Split('|')
            );

            var extension = new ExtensionDef(name, supported);
            extension.Blocks.AddRange(x.Elements().Select(ParseBlock).OfType<RequireBlock>());
            registry.Extensions[name] = extension;
        }

        return registry;
    }

    private static RequireBlock? ParseBlock(XElement element)
    {
        if (element.Name != "require" && element.Name != "remove")
        {
            return null;
        }

        var block = new RequireBlock(
            (string?)element.Attribute("api"),
            (string?)element.Attribute("profile"),
            element.Name == "remove"
        );

        foreach (var item in element.Elements())
        {
            var name = (string?)item.Attribute("name");
            if (name is null)
            {
                continue;
            }

            if (item.Name == "enum")
            {
                block.Enums.Add(name);
            }
            else if (item.Name == "command")
            {
                block.Commands.Add(name);
            }
        }

        return block;
    }

    private static bool TryParseValue(string text, out decimal value)
    {
        value = 0;
        text = text.Trim();
        if (text.StartsWith("0x", StringComparison.OrdinalIgnoreCase))
        {
            if (
                !ulong.TryParse(
                    text.Substring(2),
                    NumberStyles.AllowHexSpecifier,
                    CultureInfo.InvariantCulture,
                    out var u
                )
            )
            {
                return false;
            }

            value = u;
            return true;
        }

        return decimal.TryParse(
            text,
            NumberStyles.AllowLeadingSign,
            CultureInfo.InvariantCulture,
            out value
        );
    }

    private static CommandDef? ParseCommand(XElement command)
    {
        var proto = command.Element("proto");
        var protoName = proto?.Element("name")?.Value;
        if (proto is null || protoName is null)
        {
            return null;
        }

        var returnType = ParseType("", proto, (string?)proto.Attribute("group"), null);
        var parameters = new List<ParamDef>();
        foreach (var p in command.Elements("param"))
        {
            var name = p.Element("name")?.Value;
            if (name is null)
            {
                return null;
            }

            parameters.Add(
                ParseType(name, p, (string?)p.Attribute("group"), (string?)p.Attribute("len"))
            );
        }

        return new CommandDef(protoName, returnType, parameters);
    }

    private static ParamDef ParseType(string name, XElement element, string? group, string? len)
    {
        var text = new StringBuilder();
        string? baseType = null;
        foreach (var node in element.Nodes())
        {
            if (node is XElement { Name.LocalName: "name" })
            {
                break;
            }

            if (node is XElement { Name.LocalName: "ptype" } ptype)
            {
                baseType = ptype.Value;
                text.Append(" @ ");
            }
            else
            {
                text.Append(node is XText t ? t.Value : null);
            }
        }

        var full = text.ToString();
        var marker = full.IndexOf('@');
        var before = marker >= 0 ? full.Substring(0, marker) : full;
        baseType ??= "void";

        var firstStar = full.IndexOf('*');
        var constRegion = firstStar >= 0 ? full.Substring(0, firstStar) : full;
        var isConst = constRegion.Contains("const") || before.Contains("const");
        return new ParamDef(name, baseType, isConst, full.Count(c => c == '*'), len, group);
    }
}
