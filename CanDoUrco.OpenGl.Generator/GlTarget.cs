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

namespace CanDoUrco.OpenGl.Generator;

internal sealed class GlTarget(
    string? ns,
    string name,
    string accessibility,
    bool isStatic,
    string[] containers,
    string? version,
    int profile,
    string[] extensions,
    string? loaderMethod
) : IEquatable<GlTarget>
{
    public string? Namespace { get; } = ns;
    public string Name { get; } = name;
    public string Accessibility { get; } = accessibility;
    public bool IsStatic { get; } = isStatic;
    public string[] Containers { get; } = containers;
    public string? Version { get; } = version;
    public int Profile { get; } = profile;
    public string[] Extensions { get; } = extensions;
    public string? LoaderMethod { get; } = loaderMethod;

    private string Key =>
        string.Join(
            "|",
            Namespace,
            Name,
            Accessibility,
            IsStatic,
            string.Join(",", Containers),
            Version,
            Profile,
            string.Join(",", Extensions),
            LoaderMethod
        );

    public bool Equals(GlTarget? other) => other is not null && Key == other.Key;

    public override bool Equals(object? obj) => Equals(obj as GlTarget);

    public override int GetHashCode() => Key.GetHashCode();
}
