// CanDoUrco.Sdl3 - SDL3 bindings for Can Do Urco
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
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetClipboardText",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetClipboardText(string text);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetClipboardText",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetClipboardText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasClipboardText();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPrimarySelectionText(string text);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetPrimarySelectionText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasPrimarySelectionText();

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate nint ClipboardDataCallback(
        nint userdata,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string? mimeType,
        out nuint size
    );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void ClipboardCleanupCallback(nint userdata);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetClipboardData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetClipboardData(
        ClipboardDataCallback callback,
        ClipboardCleanupCallback cleanup,
        nint userdata,
        [In]
        [MarshalUsing(
            typeof(ArrayMarshaller<string, nint>),
            CountElementName = nameof(numMimeTypes)
        )]
            string[] mimeTypes,
        nuint numMimeTypes
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ClearClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearClipboardData();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetClipboardData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<byte, byte>),
        CountElementName = nameof(size)
    )]
    public static partial byte[]? GetClipboardData(string mimeType, out nuint size);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_HasClipboardData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasClipboardData(string mimeType);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetClipboardMimeTypes",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<byte, byte>),
        CountElementName = nameof(numMimeTypes)
    )]
    public static partial byte[]? GetClipboardMimeTypes(out nuint numMimeTypes);
}
