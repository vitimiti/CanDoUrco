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

namespace CanDoUrco.OpenGL.Generator;

internal sealed class GLTarget(
    string? ns,
    string name,
    string accessibility,
    bool isStatic,
    string[] containers,
    string? version,
    int profile,
    string?[] extensions,
    string? loaderMethod
) : IEquatable<GLTarget>
{
    public string? Namespace { get; } = ns;
    public string Name { get; } = name;
    public string Accessibility { get; } = accessibility;
    public bool IsStatic { get; } = isStatic;
    public string[] Containers { get; } = containers;
    public string? Version { get; } = version;
    public int Profile { get; } = profile;
    public string?[] Extensions { get; } = extensions;
    public string? LoaderMethod { get; } = loaderMethod;

    public bool Equals(GLTarget? other) =>
        other is not null
        && Namespace == other.Namespace
        && Name == other.Name
        && Accessibility == other.Accessibility
        && IsStatic == other.IsStatic
        && Containers.SequenceEqual(other.Containers)
        && Version == other.Version
        && Profile == other.Profile
        && Extensions.SequenceEqual(other.Extensions)
        && LoaderMethod == other.LoaderMethod;

    public override bool Equals(object? obj) => Equals(obj as GLTarget);

    public override int GetHashCode()
    {
        unchecked
        {
            var hash = 17;
            hash = hash * 31 + (Namespace?.GetHashCode() ?? 0);
            hash = hash * 31 + Name.GetHashCode();
            hash = hash * 31 + Accessibility.GetHashCode();
            hash = hash * 31 + IsStatic.GetHashCode();
            foreach (var container in Containers)
            {
                hash = hash * 31 + container.GetHashCode();
            }
            hash = hash * 31 + (Version?.GetHashCode() ?? 0);
            hash = hash * 31 + Profile;
            foreach (var extension in Extensions)
            {
                hash = hash * 31 + (extension?.GetHashCode() ?? 0);
            }
            return hash * 31 + (LoaderMethod?.GetHashCode() ?? 0);
        }
    }
}
