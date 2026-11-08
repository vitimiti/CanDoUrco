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
    public const uint HapticInfinity = 4294967295U;
    public const uint HapticGain = 1 << 16;
    public const uint HapticAutocenter = 1 << 17;
    public const uint HapticStatus = 1 << 18;
    public const uint HapticPause = 1 << 19;

    public static HapticEffectType HapticConstantType => 1 << 0;
    public static HapticEffectType HapticSine => 1 << 1;
    public static HapticEffectType HapticSquare => 1 << 2;
    public static HapticEffectType HapticTriangle => 1 << 3;
    public static HapticEffectType HapticSawtoothUp => 1 << 4;
    public static HapticEffectType HapticSawtoothDown => 1 << 5;
    public static HapticEffectType HapticRampType => 1 << 6;
    public static HapticEffectType HapticSpring => 1 << 7;
    public static HapticEffectType HapticDamper => 1 << 8;
    public static HapticEffectType HapticInertia => 1 << 9;
    public static HapticEffectType HapticFriction => 1 << 10;
    public static HapticEffectType HapticLeftRightType => 1 << 11;
    public static HapticEffectType HapticReserved1 => 1 << 12;
    public static HapticEffectType HapticReserved2 => 1 << 13;
    public static HapticEffectType HapticReserved3 => 1 << 14;
    public static HapticEffectType HapticCustomType => 1 << 15;

    public static HapticDirectionType HapticPolar => 0;
    public static HapticDirectionType HapticCartesian => 1;
    public static HapticDirectionType HapticSpherical => 2;
    public static HapticDirectionType HapticSteeringAxis => 3;

    [LibraryImport(DllName, EntryPoint = "SDL_GetHaptics")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HapticID* _GetHaptics(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetHapticNameForID(HapticID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Haptic _OpenHaptic(HapticID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Haptic _GetHapticFromID(HapticID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HapticID _GetHapticID(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetHapticName(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_IsMouseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsMouseHaptic();

    [LibraryImport(DllName, EntryPoint = "SDL_OpenHapticFromMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Haptic _OpenHapticFromMouse();

    [LibraryImport(DllName, EntryPoint = "SDL_IsJoystickHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsJoystickHaptic(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenHapticFromJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Haptic _OpenHapticFromJoystick(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseHaptic(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMaxHapticEffects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetMaxHapticEffects(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMaxHapticEffectsPlaying")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetMaxHapticEffectsPlaying(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticFeatures")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HapticEffectType _GetHapticFeatures(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumHapticAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumHapticAxes(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_HapticEffectSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HapticEffectSupported(Haptic haptic, in HapticEffect effect);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HapticEffectID _CreateHapticEffect(
        Haptic haptic,
        in HapticEffect effect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateHapticEffect(
        Haptic haptic,
        HapticEffectID effect,
        in HapticEffect data
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RunHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RunHapticEffect(
        Haptic haptic,
        HapticEffectID effect,
        uint iterations
    );

    [LibraryImport(DllName, EntryPoint = "SDL_StopHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StopHapticEffect(Haptic haptic, HapticEffectID effect);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyHapticEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyHapticEffect(Haptic haptic, HapticEffectID effect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHapticEffectStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetHapticEffectStatus(Haptic haptic, HapticEffectID effect);

    [LibraryImport(DllName, EntryPoint = "SDL_SetHapticGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetHapticGain(Haptic haptic, int gain);

    [LibraryImport(DllName, EntryPoint = "SDL_SetHapticAutocenter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetHapticAutocenter(Haptic haptic, int autocenter);

    [LibraryImport(DllName, EntryPoint = "SDL_PauseHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PauseHaptic(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_ResumeHaptic")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ResumeHaptic(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_StopHapticEffects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StopHapticEffects(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_HapticRumbleSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HapticRumbleSupported(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_InitHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _InitHapticRumble(Haptic haptic);

    [LibraryImport(DllName, EntryPoint = "SDL_PlayHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PlayHapticRumble(Haptic haptic, float strength, uint length);

    [LibraryImport(DllName, EntryPoint = "SDL_StopHapticRumble")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StopHapticRumble(Haptic haptic);

    public static HapticID* GetHaptics(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHaptics(out count);
    }

    public static byte* GetHapticNameForID(HapticID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticNameForID(instanceID);
    }

    public static Haptic OpenHaptic(HapticID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenHaptic(instanceID);
    }

    public static Haptic GetHapticFromID(HapticID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticFromID(instanceID);
    }

    public static HapticID GetHapticID(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticID(haptic);
    }

    public static byte* GetHapticName(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticName(haptic);
    }

    public static Bool IsMouseHaptic()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsMouseHaptic();
    }

    public static Haptic OpenHapticFromMouse()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenHapticFromMouse();
    }

    public static Bool IsJoystickHaptic(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsJoystickHaptic(joystick);
    }

    public static Haptic OpenHapticFromJoystick(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenHapticFromJoystick(joystick);
    }

    public static void CloseHaptic(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseHaptic(haptic);
    }

    public static int GetMaxHapticEffects(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMaxHapticEffects(haptic);
    }

    public static int GetMaxHapticEffectsPlaying(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMaxHapticEffectsPlaying(haptic);
    }

    public static HapticEffectType GetHapticFeatures(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticFeatures(haptic);
    }

    public static int GetNumHapticAxes(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumHapticAxes(haptic);
    }

    public static Bool HapticEffectSupported(Haptic haptic, in HapticEffect effect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HapticEffectSupported(haptic, in effect);
    }

    public static HapticEffectID CreateHapticEffect(Haptic haptic, in HapticEffect effect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateHapticEffect(haptic, in effect);
    }

    public static Bool UpdateHapticEffect(
        Haptic haptic,
        HapticEffectID effect,
        in HapticEffect data
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateHapticEffect(haptic, effect, in data);
    }

    public static Bool RunHapticEffect(Haptic haptic, HapticEffectID effect, uint iterations)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RunHapticEffect(haptic, effect, iterations);
    }

    public static Bool StopHapticEffect(Haptic haptic, HapticEffectID effect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StopHapticEffect(haptic, effect);
    }

    public static void DestroyHapticEffect(Haptic haptic, HapticEffectID effect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyHapticEffect(haptic, effect);
    }

    public static Bool GetHapticEffectStatus(Haptic haptic, HapticEffectID effect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHapticEffectStatus(haptic, effect);
    }

    public static Bool SetHapticGain(Haptic haptic, int gain)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetHapticGain(haptic, gain);
    }

    public static Bool SetHapticAutocenter(Haptic haptic, int autocenter)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetHapticAutocenter(haptic, autocenter);
    }

    public static Bool PauseHaptic(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PauseHaptic(haptic);
    }

    public static Bool ResumeHaptic(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ResumeHaptic(haptic);
    }

    public static Bool StopHapticEffects(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StopHapticEffects(haptic);
    }

    public static Bool HapticRumbleSupported(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HapticRumbleSupported(haptic);
    }

    public static Bool InitHapticRumble(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _InitHapticRumble(haptic);
    }

    public static Bool PlayHapticRumble(Haptic haptic, float strength, uint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PlayHapticRumble(haptic, strength, length);
    }

    public static Bool StopHapticRumble(Haptic haptic)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StopHapticRumble(haptic);
    }

    public readonly record struct Haptic(nint Value)
    {
        public static implicit operator nint(Haptic haptic) => haptic.Value;

        public static implicit operator Haptic(nint value) => new(value);
    }

    public readonly record struct HapticEffectType(uint Value)
    {
        public static implicit operator uint(HapticEffectType hapticEffect) => hapticEffect.Value;

        public static implicit operator HapticEffectType(uint value) => new(value);
    }

    public readonly record struct HapticDirectionType(byte Value)
    {
        public static implicit operator byte(HapticDirectionType hapticDirectionType) =>
            hapticDirectionType.Value;

        public static implicit operator HapticDirectionType(byte value) => new(value);
    }

    public readonly record struct HapticEffectID(int Value)
    {
        public static implicit operator int(HapticEffectID hapticEffectID) => hapticEffectID.Value;

        public static implicit operator HapticEffectID(int value) => new(value);
    }

    public readonly record struct HapticID(uint Value)
    {
        public static implicit operator uint(HapticID hapticID) => hapticID.Value;

        public static implicit operator HapticID(uint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticDirection
    {
        public HapticDirectionType Type;
        public fixed int Dir[3];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticConstant
    {
        public HapticEffectType Type;
        public HapticDirection Direction;
        public uint Length;
        public ushort Delay;
        public ushort Button;
        public ushort Interval;
        public short Level;
        public ushort AttackLength;
        public ushort AttackLevel;
        public ushort FadeLength;
        public ushort FadeLevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticPeriodic
    {
        public HapticEffectType Type;
        public HapticDirection Direction;
        public uint Length;
        public ushort Delay;
        public ushort Button;
        public ushort Interval;
        public ushort Period;
        public short Magnitude;
        public short Offset;
        public ushort Phase;
        public ushort AttackLength;
        public ushort AttackLevel;
        public ushort FadeLength;
        public ushort FadeLevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticCondition
    {
        public HapticEffectType Type;
        public HapticDirection Direction;
        public uint Length;
        public ushort Delay;
        public ushort Button;
        public ushort Interval;
        public fixed ushort RightSat[3];
        public fixed ushort LeftSat[3];
        public fixed short RightCoeff[3];
        public fixed short LeftCoeff[3];
        public fixed ushort Deadband[3];
        public fixed short Center[3];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticRamp
    {
        public HapticEffectType Type;
        public HapticDirection Direction;
        public uint Length;
        public ushort Delay;
        public ushort Button;
        public ushort Interval;
        public short Start;
        public short End;
        public ushort AttackLength;
        public ushort AttackLevel;
        public ushort FadeLength;
        public ushort FadeLevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticLeftRight
    {
        public HapticEffectType Type;
        public uint Length;
        public ushort LargeMagnitude;
        public ushort SmallMagnitude;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticCustom
    {
        public HapticEffectType Type;
        public HapticDirection Direction;
        public uint Length;
        public ushort Delay;
        public ushort Button;
        public ushort Interval;
        public byte Channels;
        public ushort Period;
        public ushort Samples;
        public ushort* Data;
        public ushort AttackLength;
        public ushort AttackLevel;
        public ushort FadeLength;
        public ushort FadeLevel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HapticEffect
    {
        public HapticEffectType Type;
        public HapticConstant Constant;
        public HapticPeriodic Periodic;
        public HapticCondition Condition;
        public HapticRamp Ramp;
        public HapticLeftRight LeftRight;
        public HapticCustom Custom;
    }
}
