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
    public const short JoystickAxisMax = 32767;
    public const short JoystickAxisMin = -32768;

    public static ReadOnlySpan<byte> PropJoystickCapMonoLedBoolean =>
        "SDL.joystick.cap.mono_led\0"u8;
    public static ReadOnlySpan<byte> PropJoystickCapRgbLedBoolean => "SDL.joystick.cap.rgb_led\0"u8;
    public static ReadOnlySpan<byte> PropJoystickCapPlayerLedBoolean =>
        "SDL.joystick.cap.player_led\0"u8;
    public static ReadOnlySpan<byte> PropJoystickCapRumbleBoolean => "SDL.joystick.cap.rumble\0"u8;
    public static ReadOnlySpan<byte> PropJoystickCapTriggerRumbleBoolean =>
        "SDL.joystick.cap.trigger_rumble\0"u8;

    public static Hat HatCentered => 0x00;
    public static Hat HatUp => 0x01;
    public static Hat HatRight => 0x02;
    public static Hat HatDown => 0x04;
    public static Hat HatLeft => 0x08;
    public static Hat HatRightUp => HatRight | HatUp;
    public static Hat HatRightDown => HatRight | HatDown;
    public static Hat HatLeftUp => HatLeft | HatUp;
    public static Hat HatLeftDown => HatLeft | HatDown;

    [LibraryImport(DllName, EntryPoint = "SDL_LockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _LockJoysticks();

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockJoysticks();

    [LibraryImport(DllName, EntryPoint = "SDL_HasJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasJoystick();

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickID* _GetJoysticks(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetJoystickNameForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickPlayerIndexForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetJoystickPlayerIndexForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickGUIDForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Guid _GetJoystickGuidForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickVendorForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickVendorForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickProductForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickProductForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickProductVersionForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickProductVersionForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType _GetJoystickTypeForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Joystick _OpenJoystick(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Joystick _GetJoystickFromID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Joystick _GetJoystickFromPlayerIndex(int playerIndex);

    [LibraryImport(DllName, EntryPoint = "SDL_AttachVirtualJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickID _AttachVirtualJoystick(in VirtualJoystickDesc desc);

    [LibraryImport(DllName, EntryPoint = "SDL_DetachVirtualJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _DetachVirtualJoystick(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_IsJoystickVirtual")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsJoystickVirtual(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickVirtualAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickVirtualAxis(Joystick joystick, int axis, short value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickVirtualBall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickVirtualBall(
        Joystick joystick,
        int ball,
        short xRel,
        short yRel
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickVirtualButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickVirtualButton(Joystick joystick, int button, Bool down);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickVirtualHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickVirtualHat(Joystick joystick, int hat, Hat value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickVirtualTouchpad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickVirtualTouchpad(
        Joystick joystick,
        int touchpad,
        int finger,
        Bool down,
        float x,
        float y,
        float pressure
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SendJoystickVirtualSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SendJoystickVirtualSensorData(
        Joystick joystick,
        SensorType type,
        ulong sensorTimestamp,
        float* data,
        int numValues
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetJoystickProperties(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetJoystickName(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetJoystickPath(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetJoystickPlayerIndex(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickPlayerIndex(Joystick joystick, int playerIndex);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Guid _GetJoystickGuid(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickVendor(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickProduct(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickProductVersion(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetJoystickFirmwareVersion(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetJoystickSerial(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickType _GetJoystickType(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickGUIDInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetJoystickGuidInfo(
        Guid guid,
        out ushort vendor,
        out ushort product,
        out ushort version,
        out ushort crc16
    );

    [LibraryImport(DllName, EntryPoint = "SDL_JoystickConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _JoystickConnected(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickID _GetJoystickID(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumJoystickAxes(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumJoystickBalls")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumJoystickBalls(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumJoystickHats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumJoystickHats(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumJoystickButtons(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetJoystickEventsEnabled(Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_JoystickEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _JoystickEventsEnabled();

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateJoysticks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UpdateJoysticks();

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short _GetJoystickAxis(Joystick joystick, int axis);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickAxisInitialState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetJoystickAxisInitialState(
        Joystick joystick,
        int axis,
        out short state
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickBall")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetJoystickBall(
        Joystick joystick,
        int ball,
        out int dx,
        out int dy
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickHat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Hat _GetJoystickHat(Joystick joystick, int hat);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetJoystickButton(Joystick joystick, int button);

    [LibraryImport(DllName, EntryPoint = "SDL_RumbleJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RumbleJoystick(
        Joystick joystick,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationMS
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RumbleJoystickTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RumbleJoystickTriggers(
        Joystick joystick,
        ushort leftRumble,
        ushort rightRumble,
        uint durationMS
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetJoystickLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetJoystickLED(Joystick joystick, byte red, byte green, byte blue);

    [LibraryImport(DllName, EntryPoint = "SDL_SendJoystickEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SendJoystickEffect(Joystick joystick, void* data, int size);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseJoystick(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickConnectionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState _GetJoystickConnectionState(Joystick joystick);

    [LibraryImport(DllName, EntryPoint = "SDL_GetJoystickPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState _GetJoystickPowerInfo(Joystick joystick, out int percent);

    public static void LockJoysticks()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _LockJoysticks();
    }

    public static void UnlockJoysticks()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockJoysticks();
    }

    public static Bool HasJoystick()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasJoystick();
    }

    public static JoystickID* GetJoysticks(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoysticks(out count);
    }

    public static byte* GetJoystickNameForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickNameForID(instanceID);
    }

    public static int GetJoystickPlayerIndexForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickPlayerIndexForID(instanceID);
    }

    public static Guid GetJoystickGuidForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickGuidForID(instanceID);
    }

    public static ushort GetJoystickVendorForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickVendorForID(instanceID);
    }

    public static ushort GetJoystickProductForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickProductForID(instanceID);
    }

    public static ushort GetJoystickProductVersionForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickProductVersionForID(instanceID);
    }

    public static JoystickType GetJoystickTypeForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickTypeForID(instanceID);
    }

    public static Joystick OpenJoystick(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenJoystick(instanceID);
    }

    public static Joystick GetJoystickFromID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickFromID(instanceID);
    }

    public static Joystick GetJoystickFromPlayerIndex(int playerIndex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickFromPlayerIndex(playerIndex);
    }

    public static JoystickID AttachVirtualJoystick(in VirtualJoystickDesc desc)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AttachVirtualJoystick(desc);
    }

    public static Bool DetachVirtualJoystick(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DetachVirtualJoystick(instanceID);
    }

    public static Bool IsJoystickVirtual(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsJoystickVirtual(instanceID);
    }

    public static Bool SetJoystickVirtualAxis(Joystick joystick, int axis, short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickVirtualAxis(joystick, axis, value);
    }

    public static Bool SetJoystickVirtualBall(Joystick joystick, int ball, short xRel, short yRel)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickVirtualBall(joystick, ball, xRel, yRel);
    }

    public static Bool SetJoystickVirtualButton(Joystick joystick, int button, Bool down)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickVirtualButton(joystick, button, down);
    }

    public static Bool SetJoystickVirtualHat(Joystick joystick, int hat, Hat value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickVirtualHat(joystick, hat, value);
    }

    public static Bool SetJoystickVirtualTouchpad(
        Joystick joystick,
        int touchpad,
        int finger,
        Bool down,
        float x,
        float y,
        float pressure
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickVirtualTouchpad(joystick, touchpad, finger, down, x, y, pressure);
    }

    public static Bool SendJoystickVirtualSensorData(
        Joystick joystick,
        SensorType type,
        ulong sensorTimestamp,
        float* data,
        int numValues
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SendJoystickVirtualSensorData(joystick, type, sensorTimestamp, data, numValues);
    }

    public static PropertiesID GetJoystickProperties(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickProperties(joystick);
    }

    public static byte* GetJoystickName(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickName(joystick);
    }

    public static byte* GetJoystickPath(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickPath(joystick);
    }

    public static int GetJoystickPlayerIndex(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickPlayerIndex(joystick);
    }

    public static Bool SetJoystickPlayerIndex(Joystick joystick, int playerIndex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickPlayerIndex(joystick, playerIndex);
    }

    public static Guid GetJoystickGuid(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickGuid(joystick);
    }

    public static ushort GetJoystickVendor(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickVendor(joystick);
    }

    public static ushort GetJoystickProduct(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickProduct(joystick);
    }

    public static ushort GetJoystickProductVersion(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickProductVersion(joystick);
    }

    public static ushort GetJoystickFirmwareVersion(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickFirmwareVersion(joystick);
    }

    public static byte* GetJoystickSerial(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickSerial(joystick);
    }

    public static JoystickType GetJoystickType(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickType(joystick);
    }

    public static void GetJoystickGuidInfo(
        Guid guid,
        out ushort vendor,
        out ushort product,
        out ushort version,
        out ushort crc16
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetJoystickGuidInfo(guid, out vendor, out product, out version, out crc16);
    }

    public static Bool JoystickConnected(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _JoystickConnected(instanceID);
    }

    public static JoystickID GetJoystickID(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickID(joystick);
    }

    public static int GetNumJoystickAxes(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumJoystickAxes(joystick);
    }

    public static int GetNumJoystickBalls(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumJoystickBalls(joystick);
    }

    public static int GetNumJoystickHats(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumJoystickHats(joystick);
    }

    public static int GetNumJoystickButtons(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumJoystickButtons(joystick);
    }

    public static void SetJoystickEventsEnabled(Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetJoystickEventsEnabled(enabled);
    }

    public static Bool JoystickEventsEnabled()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _JoystickEventsEnabled();
    }

    public static void UpdateJoysticks()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UpdateJoysticks();
    }

    public static short GetJoystickAxis(Joystick joystick, int axis)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickAxis(joystick, axis);
    }

    public static Bool GetJoystickAxisInitialState(Joystick joystick, int axis, out short state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickAxisInitialState(joystick, axis, out state);
    }

    public static Bool GetJoystickBall(Joystick joystick, int ball, out int dx, out int dy)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickBall(joystick, ball, out dx, out dy);
    }

    public static Hat GetJoystickHat(Joystick joystick, int hat)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickHat(joystick, hat);
    }

    public static Bool GetJoystickButton(Joystick joystick, int button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickButton(joystick, button);
    }

    public static Bool RumbleJoystick(
        Joystick joystick,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationMS
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RumbleJoystick(joystick, lowFrequencyRumble, highFrequencyRumble, durationMS);
    }

    public static Bool RumbleJoystickTriggers(
        Joystick joystick,
        ushort leftRumble,
        ushort rightRumble,
        uint durationMS
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RumbleJoystickTriggers(joystick, leftRumble, rightRumble, durationMS);
    }

    public static Bool SetJoystickLED(Joystick joystick, byte red, byte green, byte blue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetJoystickLED(joystick, red, green, blue);
    }

    public static Bool SendJoystickEffect(Joystick joystick, void* data, int size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SendJoystickEffect(joystick, data, size);
    }

    public static void CloseJoystick(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseJoystick(joystick);
    }

    public static JoystickConnectionState GetJoystickConnectionState(Joystick joystick)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickConnectionState(joystick);
    }

    public static PowerState GetJoystickPowerInfo(Joystick joystick, out int percent)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetJoystickPowerInfo(joystick, out percent);
    }

    public enum JoystickType : ushort
    {
        Unknown,
        Gamepad,
        Wheel,
        ArcadeStick,
        FLightStick,
        DancePad,
        Guitar,
        DrumKit,
        ArcadePad,
        Throttle,
        Count,
    }

    public enum JoystickConnectionState
    {
        Invalid = -1,
        Unknown,
        Wired,
        Wireless,
    }

    public readonly record struct Joystick(nint Value)
    {
        public static implicit operator nint(Joystick joystick) => joystick.Value;

        public static implicit operator Joystick(nint value) => new(value);
    }

    public readonly record struct JoystickID(uint Value)
    {
        public static implicit operator uint(JoystickID joystickID) => joystickID.Value;

        public static implicit operator JoystickID(uint value) => new(value);
    }

    public readonly record struct Hat(byte Value)
    {
        public static Hat operator |(Hat left, Hat right) => new((byte)(left.Value | right.Value));

        public static Hat operator &(Hat left, Hat right) => new((byte)(left.Value & right.Value));

        public static Hat operator ^(Hat left, Hat right) => new((byte)(left.Value ^ right.Value));

        public static Hat operator ~(Hat hat) => new((byte)(~hat.Value));

        public static implicit operator byte(Hat hat) => hat.Value;

        public static implicit operator Hat(byte value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VirtualJoystickTouchpadDesc
    {
        public ushort NFingers;
        private fixed ushort _padding[3];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VirtualJoystickSensorDesc
    {
        public SensorType Type;
        public float Rate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VirtualJoystickDesc
    {
        public uint Version;
        public JoystickType Type;
        private readonly ushort _padding;
        public ushort VendorID;
        public ushort ProductID;
        public ushort NAxes;
        public ushort NButtons;
        public ushort NBalls;
        public ushort NHats;
        public ushort NTouchpads;
        public ushort NSensors;
        private fixed ushort _padding2[2];
        public uint ButtonMask;
        public uint AxisMask;
        public byte* Name;
        public VirtualJoystickTouchpadDesc* Touchpads;
        public VirtualJoystickSensorDesc* Sensors;
        public void* Userdata;
        public delegate* unmanaged[Cdecl]<void*, void> Update;
        public delegate* unmanaged[Cdecl]<void*, int, void> SetPlayerIndex;
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, Bool> Rumble;
        public delegate* unmanaged[Cdecl]<void*, ushort, ushort, Bool> RumbleTriggers;
        public delegate* unmanaged[Cdecl]<void*, byte, byte, byte, Bool> SetLED;
        public delegate* unmanaged[Cdecl]<void*, void*, int, Bool> SendEffect;
        public delegate* unmanaged[Cdecl]<void*, Bool, Bool> SetSensorsEnabled;
        public delegate* unmanaged[Cdecl]<void*, void> Cleanup;
    }
}
