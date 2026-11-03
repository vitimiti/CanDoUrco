// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) <year>  <name of author>
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
using CanDoUrco.Logging;
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
    private static partial bool _SetClipboardText(string text);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetClipboardText",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetClipboardText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasClipboardText();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetPrimarySelectionText(string text);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPrimarySelectionText",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlOwnedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetPrimarySelectionText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasPrimarySelectionText();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetClipboardData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetClipboardData(
        delegate* unmanaged[Cdecl]<void*, byte*, nuint*, void*> callback,
        delegate* unmanaged[Cdecl]<void*, void> cleanup,
        void* userdata,
        byte** mimeTypes,
        nuint numMimeTypes
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetClipboardData",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _GetClipboardData(string mimeType, out uint size);

    [LibraryImport(DllName, EntryPoint = "SDL_HasClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasClipboardData();

    [LibraryImport(DllName, EntryPoint = "SDL_ClearClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ClearClipboardData();

    [LibraryImport(DllName, EntryPoint = "SDL_GetClipboardMimeTypes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial byte** _GetClipboardMimeTypes(out nuint numMimeTypes);

    public static bool SetClipboardText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetClipboardText(text);
    }

    public static string? GetClipboardText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardText();
    }

    public static bool HasClipboardText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasClipboardText();
    }

    public static bool SetPrimarySelectionText(string text)
    {
        ArgumentNullException.ThrowIfNull(text);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPrimarySelectionText(text);
    }

    public static string? GetPrimarySelectionText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPrimarySelectionText();
    }

    public static bool HasPrimarySelectionText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasPrimarySelectionText();
    }

    public static unsafe bool SetClipboardData(
        delegate* unmanaged[Cdecl]<void*, byte*, nuint*, void*> callback,
        delegate* unmanaged[Cdecl]<void*, void> cleanup,
        void* userdata,
        byte** mimeTypes,
        nuint numMimeTypes
    )
    {
        ArgumentNullException.ThrowIfNull(mimeTypes);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
    }

    public static bool ClearClipboardData()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearClipboardData();
    }

    public static unsafe void* GetClipboardData(string mimeType, out uint size)
    {
        ArgumentNullException.ThrowIfNull(mimeType);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardData(mimeType, out size);
    }

    public static bool HasClipboardData()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasClipboardData();
    }

    public static unsafe byte** GetClipboardMimeTypes(out nuint numMimeTypes)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardMimeTypes(out numMimeTypes);
    }
}
