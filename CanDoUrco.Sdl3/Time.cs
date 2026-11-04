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
    [LibraryImport(DllName, EntryPoint = "SDL_GetDateTimeLocalePreferences")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetDateTimeLocalePreferences(
        out DateFormat dateFormat,
        out TimeFormat timeFormat
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetCurrentTime(out Time ticks);

    [LibraryImport(DllName, EntryPoint = "SDL_TimeToDateTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TimeToDateTime(
        Time ticks,
        out DateTime dateTime,
        [MarshalAs(UnmanagedType.I1)] bool localTime
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DateTimeToTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _DateTimeToTime(in DateTime dateTime, out Time ticks);

    [LibraryImport(DllName, EntryPoint = "SDL_TimeToWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _TimeToWindows(
        Time ticks,
        out uint dwLowDateTime,
        out uint dwHighDateTime
    );

    [LibraryImport(DllName, EntryPoint = "SDL_TimeFromWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Time _TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDaysInMonth")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetDaysInMonth(int year, int month);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDayOfYear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetDayOfYear(int year, int month, int day);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDayOfWeek")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetDayOfWeek(int year, int month, int day);

    public static bool GetDateTimeLocalePreferences(
        out DateFormat dateFormat,
        out TimeFormat timeFormat
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDateTimeLocalePreferences(out dateFormat, out timeFormat);
    }

    public static bool GetCurrentTime(out Time ticks)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentTime(out ticks);
    }

    public static bool TimeToDateTime(Time ticks, out DateTime dateTime, bool localTime)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TimeToDateTime(ticks, out dateTime, localTime);
    }

    public static bool DateTimeToTime(in DateTime dateTime, out Time ticks)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DateTimeToTime(in dateTime, out ticks);
    }

    public static void TimeToWindows(Time ticks, out uint dwLowDateTime, out uint dwHighDateTime)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _TimeToWindows(ticks, out dwLowDateTime, out dwHighDateTime);
    }

    public static Time TimeFromWindows(uint dwLowDateTime, uint dwHighDateTime)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TimeFromWindows(dwLowDateTime, dwHighDateTime);
    }

    public static int GetDaysInMonth(int year, int month)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDaysInMonth(year, month);
    }

    public static int GetDayOfYear(int year, int month, int day)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDayOfYear(year, month, day);
    }

    public static int GetDayOfWeek(int year, int month, int day)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDayOfWeek(year, month, day);
    }

    public enum DateFormat
    {
        YYYYMMDD,
        DDMMYYYY,
        MMDDYYYY,
    }

    public enum TimeFormat
    {
        Format24H,
        Format12H,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DateTime
    {
        public int Year;
        public int Month;
        public int Day;
        public int Hour;
        public int Minute;
        public int Second;
        public int Nanosecond;
        public int DayOfWeek;
        public int UtcOffset;
    }
}
