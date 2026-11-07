// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_SetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetClipboardText(byte* text);

    [LibraryImport(DllName, EntryPoint = "SDL_GetClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetClipboardText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasClipboardText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasClipboardText();

    [LibraryImport(DllName, EntryPoint = "SDL_SetPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetPrimarySelectionText(byte* text);

    [LibraryImport(DllName, EntryPoint = "SDL_GetPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetPrimarySelectionText();

    [LibraryImport(DllName, EntryPoint = "SDL_HasPrimarySelectionText")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasPrimarySelectionText();

    [LibraryImport(DllName, EntryPoint = "SDL_SetClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetClipboardData(
        delegate* unmanaged[Cdecl]<void*, byte*, nuint*, void*> callback,
        delegate* unmanaged[Cdecl]<void*, void> cleanup,
        void* userdata,
        byte** mimeTypes,
        nuint numMimeTypes
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetClipboardData(byte* mimeType, out uint size);

    [LibraryImport(DllName, EntryPoint = "SDL_HasClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasClipboardData();

    [LibraryImport(DllName, EntryPoint = "SDL_ClearClipboardData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClearClipboardData();

    [LibraryImport(DllName, EntryPoint = "SDL_GetClipboardMimeTypes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte** _GetClipboardMimeTypes(out nuint numMimeTypes);

    public static Bool SetClipboardText(byte* text)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetClipboardText(text);
    }

    public static byte* GetClipboardText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardText();
    }

    public static Bool HasClipboardText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasClipboardText();
    }

    public static Bool SetPrimarySelectionText(byte* text)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPrimarySelectionText(text);
    }

    public static byte* GetPrimarySelectionText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPrimarySelectionText();
    }

    public static Bool HasPrimarySelectionText()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasPrimarySelectionText();
    }

    public static Bool SetClipboardData(
        delegate* unmanaged[Cdecl]<void*, byte*, nuint*, void*> callback,
        delegate* unmanaged[Cdecl]<void*, void> cleanup,
        void* userdata,
        byte** mimeTypes,
        nuint numMimeTypes
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetClipboardData(callback, cleanup, userdata, mimeTypes, numMimeTypes);
    }

    public static Bool ClearClipboardData()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearClipboardData();
    }

    public static void* GetClipboardData(byte* mimeType, out uint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardData(mimeType, out size);
    }

    public static Bool HasClipboardData()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasClipboardData();
    }

    public static byte** GetClipboardMimeTypes(out nuint numMimeTypes)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClipboardMimeTypes(out numMimeTypes);
    }
}
