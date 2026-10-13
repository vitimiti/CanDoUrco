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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl3.Internals;

internal static partial class Sdl
{
    public const string PropAppMetadataNameString = "SDL.app.metadata.name";
    public const string PropAppMetadataVersionString = "SDL.app.metadata.version";
    public const string PropAppMetadataIdentifierString = "SDL.app.metadata.identifier";
    public const string PropAppMetadataCreatorString = "SDL.app.metadata.creator";
    public const string PropAppMetadataCopyrightString = "SDL.app.metadata.copyright";
    public const string PropAppMetadataUrlString = "SDL.app.metadata.url";
    public const string PropAppMetadataTypeString = "SDL.app.metadata.type";

    [LibraryImport(DllName, EntryPoint = "SDL_IsMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool IsMainThread();

    [LibraryImport(DllName, EntryPoint = "SDL_RunOnMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool RunOnMainThread(
        delegate* unmanaged[Cdecl]<nint, void> callback,
        nint userdata,
        [MarshalAs(UnmanagedType.I1)] bool waitComplete
    );

    [LibraryImport(DllName, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool Init(uint flags);

    [LibraryImport(DllName, EntryPoint = "SDL_QuitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void QuitSubSystem(uint flags);

    [LibraryImport(DllName, EntryPoint = "SDL_WasInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint WasInit(uint flags);

    [LibraryImport(DllName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetAppMetadataProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetAppMetadataProperty(string name, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetAppMetadataProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* GetAppMetadataProperty(string name);
}
