// This is a direct mode, unsafe import of the SDL3 library.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Native.Sdl3;

public static unsafe partial class Sdl
{
    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_SetAppMetadataProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean SetAppMetadataProperty(byte* name, byte* value);

    public static ReadOnlySpan<byte> PropAppMetadataNameString => "SDL.app.metadata.name"u8;
    public static ReadOnlySpan<byte> PropAppMetadataVersionString => "SDL.app.metadata.version"u8;
    public static ReadOnlySpan<byte> PropAppMetadataIdentifierString =>
        "SDL.app.metadata.identifier"u8;

    public static ReadOnlySpan<byte> PropAppMetadataCreatorString => "SDL.app.metadata.creator"u8;
    public static ReadOnlySpan<byte> PropAppMetadataCopyrightString =>
        "SDL.app.metadata.copyright"u8;

    public static ReadOnlySpan<byte> PropAppMetadataUrlString => "SDL.app.metadata.url"u8;
    public static ReadOnlySpan<byte> PropAppMetadataTypeString => "SDL.app.metadata.type"u8;
}
