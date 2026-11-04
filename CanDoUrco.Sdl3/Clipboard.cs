// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

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
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
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
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
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
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(text);
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
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(text);
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
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(mimeTypes);
        return _SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
    }

    public static bool ClearClipboardData()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearClipboardData();
    }

    public static unsafe void* GetClipboardData(string mimeType, out uint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(mimeType);
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
