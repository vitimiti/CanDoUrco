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
    public const ulong MSPerSecond = 1_000;
    public const ulong USPerSecond = 1_000_000;
    public const ulong NSPerSecond = 1_000_000_000;
    public const ulong NSPerMS = 1_000_000;
    public const ulong NSPerUS = 1_000;

    public static ulong SecondsToNS(ulong s) => s * NSPerSecond;

    public static ulong NSToSeconds(ulong ns) => ns / NSPerSecond;

    public static ulong MSToNS(ulong ms) => ms * NSPerMS;

    public static ulong NSToMS(ulong ns) => ns / NSPerMS;

    public static ulong USToNS(ulong us) => us * NSPerUS;

    public static ulong NSToUS(ulong ns) => ns / NSPerUS;

    [LibraryImport(DllName, EntryPoint = "SDL_GetTicks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetTicks();

    [LibraryImport(DllName, EntryPoint = "SDL_GetTicksNS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetTicksNS();

    [LibraryImport(DllName, EntryPoint = "SDL_GetPerformanceCounter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetPerformanceCounter();

    [LibraryImport(DllName, EntryPoint = "SDL_GetPerformanceFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetPerformanceFrequency();

    [LibraryImport(DllName, EntryPoint = "SDL_Delay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _Delay(uint ms);

    [LibraryImport(DllName, EntryPoint = "SDL_DelayNS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DelayNS(ulong ns);

    [LibraryImport(DllName, EntryPoint = "SDL_DelayPrecise")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DelayPrecise(ulong ns);

    [LibraryImport(DllName, EntryPoint = "SDL_AddTimer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial TimerID _AddTimer(
        uint interval,
        delegate* unmanaged[Cdecl]<void*, TimerID, uint, uint> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_AddTimerNS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial TimerID _AddTimerNS(
        ulong interval,
        delegate* unmanaged[Cdecl]<void*, TimerID, ulong, ulong> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveTimer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RemoveTimer(TimerID id);

    public static ulong GetTicks()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTicks();
    }

    public static ulong GetTicksNS()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTicksNS();
    }

    public static ulong GetPerformanceCounter()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPerformanceCounter();
    }

    public static ulong GetPerformanceFrequency()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPerformanceFrequency();
    }

    public static void Delay(uint ms)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _Delay(ms);
    }

    public static void DelayNS(ulong ns)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DelayNS(ns);
    }

    public static void DelayPrecise(ulong ns)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DelayPrecise(ns);
    }

    public static unsafe TimerID AddTimer(
        uint interval,
        delegate* unmanaged[Cdecl]<void*, TimerID, uint, uint> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddTimer(interval, callback, userdata);
    }

    public static unsafe TimerID AddTimerNS(
        ulong interval,
        delegate* unmanaged[Cdecl]<void*, TimerID, ulong, ulong> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddTimerNS(interval, callback, userdata);
    }

    public static bool RemoveTimer(TimerID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RemoveTimer(id);
    }

    public readonly record struct TimerID(uint Value)
    {
        public bool IsInvalid => Value == 0;

        public static implicit operator TimerID(uint value) => new TimerID(value);

        public static implicit operator uint(TimerID timerID) => timerID.Value;
    }
}
