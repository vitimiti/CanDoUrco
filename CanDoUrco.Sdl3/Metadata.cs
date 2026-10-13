// An SDL3 interop library with dotnet safety and idioms.
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

using System.Runtime.InteropServices;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3;

public static class Metadata
{
    public static void Configure(Action<ApplicationMetadata> configure)
    {
        ArgumentNullException.ThrowIfNull(configure);
        var metadata = new ApplicationMetadata();
        configure(metadata);

        Configure(Sdl.PropAppMetadataNameString, metadata.Name);
        Configure(Sdl.PropAppMetadataVersionString, metadata.Version?.ToString());
        Configure(Sdl.PropAppMetadataIdentifierString, metadata.Identifier);
        Configure(Sdl.PropAppMetadataCreatorString, metadata.Creator);
        Configure(Sdl.PropAppMetadataCopyrightString, metadata.Copyright);
        Configure(Sdl.PropAppMetadataUrlString, metadata.Url?.ToString());
        Configure(Sdl.PropAppMetadataTypeString, metadata.AppType);
    }

    public static ApplicationMetadata Get()
    {
        var metadata = new ApplicationMetadata
        {
            Name = GetProperty(Sdl.PropAppMetadataNameString),
            Version = Version.TryParse(
                GetProperty(Sdl.PropAppMetadataVersionString),
                out var version
            )
                ? version
                : null,
            Identifier = GetProperty(Sdl.PropAppMetadataIdentifierString),
            Creator = GetProperty(Sdl.PropAppMetadataCreatorString),
            Copyright = GetProperty(Sdl.PropAppMetadataCopyrightString),
            Url = Uri.TryCreate(
                GetProperty(Sdl.PropAppMetadataUrlString),
                UriKind.Absolute,
                out var url
            )
                ? url
                : null,
            AppType = GetProperty(Sdl.PropAppMetadataTypeString),
        };

        return metadata;
    }

    private static void Configure(string name, string? value)
    {
        if (!Sdl.SetAppMetadataProperty(name, value))
        {
            throw new InvalidOperationException(
                $"Failed to set SDL app metadata property '{name}' to '{value}'."
            );
        }
    }

    private static unsafe string? GetProperty(string name)
    {
        var valuePtr = Sdl.GetAppMetadataProperty(name);
        return valuePtr is null ? null : Marshal.PtrToStringUTF8((nint)valuePtr);
    }
}
