// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_SetMainReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetMainReady();

    [LibraryImport(DllName, EntryPoint = "SDL_RunApp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _RunApp(
        int argc,
        byte** argv,
        delegate* unmanaged[Cdecl]<int, byte**, int> mainFunction,
        void* reserved
    );

    [LibraryImport(DllName, EntryPoint = "SDL_EnterAppMainCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _EnterAppMainCallbacks(
        int argc,
        byte** argv,
        delegate* unmanaged[Cdecl]<void**, int, byte**, AppResult> appInit,
        delegate* unmanaged[Cdecl]<void*, AppResult> appIter,
        delegate* unmanaged[Cdecl]<void*, Event*, AppResult> appEvent,
        delegate* unmanaged[Cdecl]<void*, AppResult, void> appQuit
    );

    [SupportedOSPlatform("windows")]
    [LibraryImport(DllName, EntryPoint = "SDL_RegisterApp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RegisterApp(byte* name, uint style, void* hInst);

    [SupportedOSPlatform("windows")]
    [LibraryImport(DllName, EntryPoint = "SDL_UnregisterApp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnregisterApp();

    [LibraryImport(DllName, EntryPoint = "SDL_GDKSuspendComplete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GDKSuspendComplete();

    public static void SetMainReady()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetMainReady();
    }

    public static void RunApp(
        int argc,
        byte** argv,
        delegate* unmanaged[Cdecl]<int, byte**, int> mainFunction
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RunApp(argc, argv, mainFunction, null);
    }

    public static int EnterAppMainCallbacks(
        int argc,
        byte** argv,
        delegate* unmanaged[Cdecl]<void**, int, byte**, AppResult> appInit,
        delegate* unmanaged[Cdecl]<void*, AppResult> appIter,
        delegate* unmanaged[Cdecl]<void*, Event*, AppResult> appEvent,
        delegate* unmanaged[Cdecl]<void*, AppResult, void> appQuit
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnterAppMainCallbacks(argc, argv, appInit, appIter, appEvent, appQuit);
    }

    [SupportedOSPlatform("windows")]
    public static bool RegisterApp(byte* name, uint style, void* hInst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RegisterApp(name, style, hInst);
    }

    [SupportedOSPlatform("windows")]
    public static void UnregisterApp()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnregisterApp();
    }

    public static void GdkSuspendComplete()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GDKSuspendComplete();
    }
}
