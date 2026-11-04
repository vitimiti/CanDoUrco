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

public static partial class Sdl
{
    public const int CacheLineSize = 128;

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumLogicalCPUCores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumLogicalCpuCores();

    [LibraryImport(DllName, EntryPoint = "SDL_GetCPUCacheLineSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetCpuCacheLineSize();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAltiVec")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasAltiVec();

    [LibraryImport(DllName, EntryPoint = "SDL_HasMMX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasMmx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasSse();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasSse2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasSse3();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE41")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasSse41();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE42")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasSse42();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasAvx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasAvx2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX512F")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasAvx512F();

    [LibraryImport(DllName, EntryPoint = "SDL_HasARMSIMD")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasArmSimd();

    [LibraryImport(DllName, EntryPoint = "SDL_HasNEON")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasNeon();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLSX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasLsx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLASX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasLasx();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSystemRAM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetSystemRam();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSimdAlignment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _GetSimdAlignment();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSystemPageSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetSystemPageSize();

    public static int GetNumLogicalCpuCores()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumLogicalCpuCores();
    }

    public static int GetCpuCacheLineSize()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCpuCacheLineSize();
    }

    public static bool HasAltiVec()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAltiVec();
    }

    public static bool HasMmx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasMmx();
    }

    public static bool HasSse()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse();
    }

    public static bool HasSse2()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse2();
    }

    public static bool HasSse3()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse3();
    }

    public static bool HasSse41()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse41();
    }

    public static bool HasSse42()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse42();
    }

    public static bool HasAvx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx();
    }

    public static bool HasAvx2()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx2();
    }

    public static bool HasAvx512F()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx512F();
    }

    public static bool HasArmSimd()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasArmSimd();
    }

    public static bool HasNeon()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasNeon();
    }

    public static bool HasLsx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasLsx();
    }

    public static bool HasLasx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasLasx();
    }

    public static int GetSystemRam()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSystemRam();
    }

    public static nuint GetSimdAlignment()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSimdAlignment();
    }

    public static int GetSystemPageSize()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSystemPageSize();
    }
}
