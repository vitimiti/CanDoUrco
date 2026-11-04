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
    [LibraryImport(DllName, EntryPoint = "SDL_GetPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState _GetPowerInfo(out int seconds, out int percent);

    public static PowerState GetPowerInfo(out int seconds, out int percent)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPowerInfo(out seconds, out percent);
    }

    public enum PowerState
    {
        Error = -1,
        Unknown,
        OnBattery,
        NoBattery,
        Charging,
        Charged,
    }
}
