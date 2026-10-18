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

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const int CacheLineSize = 128;

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumLogicalCPUCores")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetNumLogicalCpuCores();

    [LibraryImport(DllName, EntryPoint = "SDL_GetCPUCacheLineSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetCpuCacheLineSize();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAltiVec")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAltiVec();

    [LibraryImport(DllName, EntryPoint = "SDL_HasMMX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasMmx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSse();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSse2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE3")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSse3();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE41")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSse41();

    [LibraryImport(DllName, EntryPoint = "SDL_HasSSE42")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasSse42();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAvx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX2")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAvx2();

    [LibraryImport(DllName, EntryPoint = "SDL_HasAVX512F")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasAvx512F();

    [LibraryImport(DllName, EntryPoint = "SDL_HasARMSIMD")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasArmSimd();

    [LibraryImport(DllName, EntryPoint = "SDL_HasNEON")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasNeon();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLSX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasLsx();

    [LibraryImport(DllName, EntryPoint = "SDL_HasLASX")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool HasLasx();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSystemRAM")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSystemRam();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSIMDAlignment")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nuint GetSimdAlignment();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSystemPageSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetSystemPageSize();
}
