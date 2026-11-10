// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static MouseID PenMouseID => uint.MaxValue - 1;
    public static TouchID PenTouchID => ulong.MaxValue - 1;

    public static PenInputFlags PenInputDown => 1U << 0;
    public static PenInputFlags PenInputButton1 => 1U << 1;
    public static PenInputFlags PenInputButton2 => 1U << 2;
    public static PenInputFlags PenInputButton3 => 1U << 3;
    public static PenInputFlags PenInputButton4 => 1U << 4;
    public static PenInputFlags PenInputButton5 => 1U << 5;
    public static PenInputFlags PenInputEraserTip => 1U << 30;
    public static PenInputFlags PenInputInProximity => 1U << 31;

    [LibraryImport(DllName, EntryPoint = "SDL_GetPenDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PenDeviceType _GetPenDeviceType(PenID penID);

    public static PenDeviceType GetPenDeviceType(PenID penID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPenDeviceType(penID);
    }

    public enum PenAxis
    {
        Pressure,
        XTilt,
        YTilt,
        Distance,
        Rotation,
        Slider,
        TangentialPressure,
    }

    public enum PenDeviceType
    {
        Invalid = -1,
        Unknown,
        Direct,
        Indirect,
    }

    public readonly record struct PenID(uint Value)
    {
        public static implicit operator uint(PenID penID) => penID.Value;

        public static implicit operator PenID(uint value) => new(value);
    }

    public readonly record struct PenInputFlags(uint Value)
    {
        public static PenInputFlags operator |(PenInputFlags left, PenInputFlags right) =>
            new(left.Value | right.Value);

        public static PenInputFlags operator &(PenInputFlags left, PenInputFlags right) =>
            new(left.Value & right.Value);

        public static PenInputFlags operator ^(PenInputFlags left, PenInputFlags right) =>
            new(left.Value ^ right.Value);

        public static PenInputFlags operator ~(PenInputFlags flags) => new(~flags.Value);

        public static implicit operator uint(PenInputFlags flags) => flags.Value;

        public static implicit operator PenInputFlags(uint value) => new(value);
    }
}
