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
    public static MouseID TouchMouseID => uint.MaxValue;
    public static TouchID MouseTouchID => ulong.MaxValue;

    [LibraryImport(DllName, EntryPoint = "SDL_GetTouchDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchID* _GetTouchDevices(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTouchDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetTouchDeviceName(TouchID id);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTouchDeviceType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TouchDeviceType _GetTouchDeviceType(TouchID id);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTouchFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Finger** _GetTouchFingers(TouchID id, out int count);

    public static TouchID* GetTouchDevices(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTouchDevices(out count);
    }

    public static byte* GetTouchDeviceName(TouchID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTouchDeviceName(id);
    }

    public static TouchDeviceType GetTouchDeviceType(TouchID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTouchDeviceType(id);
    }

    public static Finger** GetTouchFingers(TouchID id, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTouchFingers(id, out count);
    }

    public enum TouchDeviceType
    {
        Invalid = -1,
        Direct,
        IndirectAbsolute,
        IndirectRelative,
    }

    public readonly record struct TouchID(ulong Value)
    {
        public static implicit operator ulong(TouchID id) => id.Value;

        public static implicit operator TouchID(ulong value) => new(value);
    }

    public readonly record struct FingerID(ulong Value)
    {
        public static implicit operator ulong(FingerID id) => id.Value;

        public static implicit operator FingerID(ulong value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Finger
    {
        public FingerID ID;
        public float X;
        public float Y;
        public float Pressure;
    }
}
