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
    private static partial Bool _HasAltiVec();

    [LibraryImport(DllName, EntryPoint = "SDL_HasMMX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasMmx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasSse();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasSse2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasSse3();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE41")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasSse41();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE42")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasSse42();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasAvx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasAvx2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX512F")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasAvx512F();

    [LibraryImport(DllName, EntryPoint = "SDL_HasARMSIMD")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasArmSimd();

    [LibraryImport(DllName, EntryPoint = "SDL_HasNEON")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasNeon();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLSX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasLsx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLASX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasLasx();

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

    public static Bool HasAltiVec()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAltiVec();
    }

    public static Bool HasMmx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasMmx();
    }

    public static Bool HasSse()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse();
    }

    public static Bool HasSse2()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse2();
    }

    public static Bool HasSse3()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse3();
    }

    public static Bool HasSse41()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse41();
    }

    public static Bool HasSse42()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasSse42();
    }

    public static Bool HasAvx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx();
    }

    public static Bool HasAvx2()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx2();
    }

    public static Bool HasAvx512F()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasAvx512F();
    }

    public static Bool HasArmSimd()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasArmSimd();
    }

    public static Bool HasNeon()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasNeon();
    }

    public static Bool HasLsx()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasLsx();
    }

    public static Bool HasLasx()
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
