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

using System.Text.RegularExpressions;

namespace CanDoUrco.OpenGl.Generator;

internal sealed class GlSelection
{
    public string Api { get; private set; } = "gl";
    public List<string> Enums { get; } = [];
    public List<string> Commands { get; } = [];
    public List<(string Id, string Message)> Problems { get; } = [];

    public static bool IsVersion(string text) => Regex.IsMatch(text, @"^\d+\.\d+$");

    public static GlSelection Create(GlRegistry registry, string? version, int profile, string[] extensions)
    {
        var selection = new GlSelection();
        var profileName = profile switch
        {
            0 => "core",
            1 => "compatibility",
            _ => null,
        };

        var enums = new HashSet<string>();
        var commands = new HashSet<string>();

        if (version is not null && Version.TryParse(version, out var number))
        {
            selection.Api = profile == 2 ? (number.Major == 1 ? "gles1" : "gles2") : "gl";
            var features = registry.Features.Where(f => f.Api == selection.Api).ToList();
            if (!features.Any(f => f.Number == number))
            {
                selection.Problems.Add(
                    (
                        "OGL001",
                        $"OpenGL version '{version}' is not known for the selected profile; the registry has versions up to '{features.LastOrDefault()?.Number}'."
                    )
                );
            }

            foreach (var feature in features.Where(f => f.Number <= number))
            {
                Apply(feature.Blocks, selection.Api, profileName, enums, commands);
            }
        }
        else if (version is not null)
        {
            selection.Problems.Add(("OGL001", $"'{version}' is not a valid OpenGL version."));
        }
        else
        {
            selection.Api = profile == 2 ? "gles2" : "gl";
        }

        var supportKey = profile switch
        {
            0 => "glcore",
            1 => "gl",
            _ => selection.Api,
        };

        foreach (var requested in extensions)
        {
            var name = requested.StartsWith("GL_", StringComparison.OrdinalIgnoreCase)
                ? "GL_" + requested.Substring(3)
                : "GL_" + requested;
            if (!registry.Extensions.TryGetValue(name, out var extension))
            {
                selection.Problems.Add(("OGL002", $"OpenGL extension '{requested}' is not known."));
                continue;
            }

            if (!extension.Supported.Contains(supportKey))
            {
                selection.Problems.Add(
                    ("OGL003", $"OpenGL extension '{name}' is not supported by the selected API/profile.")
                );
                continue;
            }

            Apply(extension.Blocks, selection.Api, profileName, enums, commands);
        }

        selection.Enums.AddRange(enums);
        selection.Commands.AddRange(commands);
        return selection;
    }

    private static void Apply(
        List<RequireBlock> blocks,
        string api,
        string? profile,
        HashSet<string> enums,
        HashSet<string> commands
    )
    {
        foreach (var block in blocks)
        {
            if (block.Api is not null && block.Api != api)
            {
                continue;
            }

            if (block.IsRemove)
            {
                if (block.Profile is not null && block.Profile != profile)
                {
                    continue;
                }

                enums.ExceptWith(block.Enums);
                commands.ExceptWith(block.Commands);
                continue;
            }

            if (block.Profile is not null && block.Profile != "common" && block.Profile != profile)
            {
                continue;
            }

            enums.UnionWith(block.Enums);
            commands.UnionWith(block.Commands);
        }
    }
}
