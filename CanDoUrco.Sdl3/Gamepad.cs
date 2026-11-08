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
    public static ReadOnlySpan<byte> PropGamepadCapMonoLedBoolean => PropJoystickCapMonoLedBoolean;
    public static ReadOnlySpan<byte> PropGamepadCapRgbLedBoolean => PropJoystickCapRgbLedBoolean;
    public static ReadOnlySpan<byte> PropGamepadCapPlayerLedBoolean =>
        PropJoystickCapPlayerLedBoolean;
    public static ReadOnlySpan<byte> PropGamepadCapRumbleBoolean => PropJoystickCapRumbleBoolean;
    public static ReadOnlySpan<byte> PropGamepadCapTriggerRumbleBoolean =>
        PropJoystickCapTriggerRumbleBoolean;

    [LibraryImport(DllName, EntryPoint = "SDL_AddGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _AddGamepadMapping(byte* mapping);

    [LibraryImport(DllName, EntryPoint = "SDL_AddGamepadMappingsFromIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _AddGamepadMappingsFromIO(IOStream src, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_AddGamepadMappingsFromFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _AddGamepadMappingsFromFile(byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_ReloadGamepadMappings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReloadGamepadMappings();

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadMappings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte** _GetGamepadMappings(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadMappingForGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadMappingForGuid(Guid guid);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadMapping(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGamepadMapping")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGamepadMapping(JoystickID instanceID, byte* mapping);

    [LibraryImport(DllName, EntryPoint = "SDL_HasGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasGamepad();

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickID* _GetGamepads(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_IsGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsGamepad(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadNameForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadPathForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadPathForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadPlayerIndexForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetGamepadPlayerIndexForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadGUIDForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Guid _GetGamepadGuidForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadVendorForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadVendorForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadProductForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadProductForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadProductVersionForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadProductVersionForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType _GetGamepadTypeForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRealGamepadTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType _GetRealGamepadTypeForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadMappingForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadMappingForID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Gamepad _OpenGamepad(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Gamepad _GetGamepadFromID(JoystickID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadFromPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Gamepad _GetGamepadFromPlayerIndex(int playerIndex);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetGamepadProperties(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickID _GetGamepadID(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadName(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadPath(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType _GetGamepadType(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRealGamepadType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType _GetRealGamepadType(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetGamepadPlayerIndex(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGamepadPlayerIndex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGamepadPlayerIndex(Gamepad gamepad, int playerIndex);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadVendor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadVendor(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadProduct")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadProduct(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadProductVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadProductVersion(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadFirmwareVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ushort _GetGamepadFirmwareVersion(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadSerial")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadSerial(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadSteamHandle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetGamepadSteamHandle(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadConnectionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial JoystickConnectionState _GetGamepadConnectionState(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadPowerInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PowerState _GetGamepadPowerInfo(Gamepad gamepad, out int percent);

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadConnected")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadConnected(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadJoystick")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Joystick _GetGamepadJoystick(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGamepadEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetGamepadEventsEnabled(Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadEventsEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadEventsEnabled();

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadBindings")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadBinding** _GetGamepadBindings(Gamepad gamepad, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateGamepads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UpdateGamepads();

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadTypeFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadType _GetGamepadTypeFromString(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadStringForType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadStringForType(GamepadType type);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadAxisFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadAxis _GetGamepadAxisFromString(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadStringForAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadStringForAxis(GamepadAxis axis);

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadHasAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadHasAxis(Gamepad gamepad, GamepadAxis axis);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial short _GetGamepadAxis(Gamepad gamepad, GamepadAxis axis);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadButtonFromString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButton _GetGamepadButtonFromString(byte* str);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadStringForButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadStringForButton(GamepadButton button);

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadHasButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadHasButton(Gamepad gamepad, GamepadButton button);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetGamepadButton(Gamepad gamepad, GamepadButton button);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadButtonLabelForType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel _GetGamepadButtonLabelForType(
        GamepadType type,
        GamepadButton button
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadButtonLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GamepadButtonLabel _GetGamepadButtonLabel(
        Gamepad gamepad,
        GamepadButton button
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumGamepadTouchpads")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumGamepadTouchpads(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumGamepadTouchpadFingers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumGamepadTouchpadFingers(Gamepad gamepad, int touchpad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadTouchpadFinger")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetGamepadTouchpadFinger(
        Gamepad gamepad,
        int touchpad,
        int finger,
        out Bool down,
        out float x,
        out float y,
        out float pressure
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadHasSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadHasSensor(Gamepad gamepad, SensorType type);

    [LibraryImport(DllName, EntryPoint = "SDL_SetGamepadSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGamepadSensorEnabled(
        Gamepad gamepad,
        SensorType type,
        Bool enabled
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GamepadSensorEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GamepadSensorEnabled(Gamepad gamepad, SensorType type);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadSensorDataRate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetGamepadSensorDataRate(Gamepad gamepad, SensorType type);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetGamepadSensorData(
        Gamepad gamepad,
        SensorType type,
        float* data,
        int numValues
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RumbleGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RumbleGamepad(
        Gamepad gamepad,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationMs
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RumbleGamepadTriggers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RumbleGamepadTriggers(
        Gamepad gamepad,
        ushort leftRumble,
        ushort rightRumble,
        uint durationMs
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetGamepadLED")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetGamepadLed(Gamepad gamepad, byte red, byte green, byte blue);

    [LibraryImport(DllName, EntryPoint = "SDL_SendGamepadEffect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SendGamepadEffect(Gamepad gamepad, void* data, int size);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseGamepad(Gamepad gamepad);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadAppleSFSymbolsNameForButton(
        Gamepad gamepad,
        GamepadButton button
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetGamepadAppleSFSymbolsNameForAxis")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetGamepadAppleSFSymbolsNameForAxis(
        Gamepad gamepad,
        GamepadAxis axis
    );

    public static int AddGamepadMapping(byte* mapping)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddGamepadMapping(mapping);
    }

    public static int AddGamepadMappingsFromIO(IOStream src, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddGamepadMappingsFromIO(src, closeIO);
    }

    public static int AddGamepadMappingsFromFile(byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddGamepadMappingsFromFile(file);
    }

    public static Bool ReloadGamepadMappings()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReloadGamepadMappings();
    }

    public static byte** GetGamepadMappings(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadMappings(out count);
    }

    public static byte* GetGamepadMappingForGuid(Guid guid)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadMappingForGuid(guid);
    }

    public static byte* GetGamepadMapping(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadMapping(gamepad);
    }

    public static Bool SetGamepadMapping(JoystickID instanceID, byte* mapping)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGamepadMapping(instanceID, mapping);
    }

    public static Bool HasGamepad()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasGamepad();
    }

    public static JoystickID* GetGamepads(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepads(out count);
    }

    public static Bool IsGamepad(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsGamepad(instanceID);
    }

    public static byte* GetGamepadNameForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadNameForID(instanceID);
    }

    public static byte* GetGamepadPathForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadPathForID(instanceID);
    }

    public static int GetGamepadPlayerIndexForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadPlayerIndexForID(instanceID);
    }

    public static Guid GetGamepadGuidForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadGuidForID(instanceID);
    }

    public static ushort GetGamepadVendorForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadVendorForID(instanceID);
    }

    public static ushort GetGamepadProductForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadProductForID(instanceID);
    }

    public static ushort GetGamepadProductVersionForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadProductVersionForID(instanceID);
    }

    public static GamepadType GetGamepadTypeForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadTypeForID(instanceID);
    }

    public static GamepadType GetRealGamepadTypeForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRealGamepadTypeForID(instanceID);
    }

    public static byte* GetGamepadMappingForID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadMappingForID(instanceID);
    }

    public static Gamepad OpenGamepad(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenGamepad(instanceID);
    }

    public static Gamepad GetGamepadFromID(JoystickID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadFromID(instanceID);
    }

    public static Gamepad GetGamepadFromPlayerIndex(int playerIndex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadFromPlayerIndex(playerIndex);
    }

    public static PropertiesID GetGamepadProperties(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadProperties(gamepad);
    }

    public static JoystickID GetGamepadID(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadID(gamepad);
    }

    public static byte* GetGamepadName(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadName(gamepad);
    }

    public static byte* GetGamepadPath(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadPath(gamepad);
    }

    public static GamepadType GetGamepadType(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadType(gamepad);
    }

    public static GamepadType GetRealGamepadType(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRealGamepadType(gamepad);
    }

    public static int GetGamepadPlayerIndex(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadPlayerIndex(gamepad);
    }

    public static Bool SetGamepadPlayerIndex(Gamepad gamepad, int playerIndex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGamepadPlayerIndex(gamepad, playerIndex);
    }

    public static ushort GetGamepadVendor(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadVendor(gamepad);
    }

    public static ushort GetGamepadProduct(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadProduct(gamepad);
    }

    public static ushort GetGamepadProductVersion(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadProductVersion(gamepad);
    }

    public static ushort GetGamepadFirmwareVersion(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadFirmwareVersion(gamepad);
    }

    public static byte* GetGamepadSerial(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadSerial(gamepad);
    }

    public static ulong GetGamepadSteamHandle(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadSteamHandle(gamepad);
    }

    public static JoystickConnectionState GetGamepadConnectionState(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadConnectionState(gamepad);
    }

    public static PowerState GetGamepadPowerInfo(Gamepad gamepad, out int percent)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadPowerInfo(gamepad, out percent);
    }

    public static Bool GamepadConnected(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadConnected(gamepad);
    }

    public static Joystick GetGamepadJoystick(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadJoystick(gamepad);
    }

    public static void SetGamepadEventsEnabled(Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetGamepadEventsEnabled(enabled);
    }

    public static Bool GamepadEventsEnabled()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadEventsEnabled();
    }

    public static GamepadBinding** GetGamepadBindings(Gamepad gamepad, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadBindings(gamepad, out count);
    }

    public static void UpdateGamepads()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UpdateGamepads();
    }

    public static GamepadType GetGamepadTypeFromString(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadTypeFromString(str);
    }

    public static byte* GetGamepadStringForType(GamepadType type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadStringForType(type);
    }

    public static GamepadAxis GetGamepadAxisFromString(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadAxisFromString(str);
    }

    public static byte* GetGamepadStringForAxis(GamepadAxis axis)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadStringForAxis(axis);
    }

    public static Bool GamepadHasAxis(Gamepad gamepad, GamepadAxis axis)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadHasAxis(gamepad, axis);
    }

    public static short GetGamepadAxis(Gamepad gamepad, GamepadAxis axis)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadAxis(gamepad, axis);
    }

    public static GamepadButton GetGamepadButtonFromString(byte* str)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadButtonFromString(str);
    }

    public static byte* GetGamepadStringForButton(GamepadButton button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadStringForButton(button);
    }

    public static Bool GamepadHasButton(Gamepad gamepad, GamepadButton button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadHasButton(gamepad, button);
    }

    public static Bool GetGamepadButton(Gamepad gamepad, GamepadButton button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadButton(gamepad, button);
    }

    public static GamepadButtonLabel GetGamepadButtonLabelForType(
        GamepadType type,
        GamepadButton button
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadButtonLabelForType(type, button);
    }

    public static GamepadButtonLabel GetGamepadButtonLabel(Gamepad gamepad, GamepadButton button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadButtonLabel(gamepad, button);
    }

    public static int GetNumGamepadTouchpads(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumGamepadTouchpads(gamepad);
    }

    public static int GetNumGamepadTouchpadFingers(Gamepad gamepad, int touchpad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumGamepadTouchpadFingers(gamepad, touchpad);
    }

    public static Bool GetGamepadTouchpadFinger(
        Gamepad gamepad,
        int touchpad,
        int finger,
        out Bool down,
        out float x,
        out float y,
        out float pressure
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadTouchpadFinger(
            gamepad,
            touchpad,
            finger,
            out down,
            out x,
            out y,
            out pressure
        );
    }

    public static Bool GamepadHasSensor(Gamepad gamepad, SensorType type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadHasSensor(gamepad, type);
    }

    public static Bool SetGamepadSensorEnabled(Gamepad gamepad, SensorType type, Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGamepadSensorEnabled(gamepad, type, enabled);
    }

    public static Bool GamepadSensorEnabled(Gamepad gamepad, SensorType type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GamepadSensorEnabled(gamepad, type);
    }

    public static float GetGamepadSensorDataRate(Gamepad gamepad, SensorType type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadSensorDataRate(gamepad, type);
    }

    public static Bool GetGamepadSensorData(
        Gamepad gamepad,
        SensorType type,
        float* data,
        int numValues
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadSensorData(gamepad, type, data, numValues);
    }

    public static Bool RumbleGamepad(
        Gamepad gamepad,
        ushort lowFrequencyRumble,
        ushort highFrequencyRumble,
        uint durationMs
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RumbleGamepad(gamepad, lowFrequencyRumble, highFrequencyRumble, durationMs);
    }

    public static Bool RumbleGamepadTriggers(
        Gamepad gamepad,
        ushort leftRumble,
        ushort rightRumble,
        uint durationMs
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RumbleGamepadTriggers(gamepad, leftRumble, rightRumble, durationMs);
    }

    public static Bool SetGamepadLed(Gamepad gamepad, byte red, byte green, byte blue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetGamepadLed(gamepad, red, green, blue);
    }

    public static Bool SendGamepadEffect(Gamepad gamepad, void* data, int size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SendGamepadEffect(gamepad, data, size);
    }

    public static void CloseGamepad(Gamepad gamepad)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseGamepad(gamepad);
    }

    public static byte* GetGamepadAppleSFSymbolsNameForButton(Gamepad gamepad, GamepadButton button)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadAppleSFSymbolsNameForButton(gamepad, button);
    }

    public static byte* GetGamepadAppleSFSymbolsNameForAxis(Gamepad gamepad, GamepadAxis axis)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGamepadAppleSFSymbolsNameForAxis(gamepad, axis);
    }

    public enum GamepadType
    {
        Unknown,
        Standard,
        Xbox360,
        XboxOne,
        PS3,
        PS4,
        PS5,
        NintendoSwitchPro,
        NintendoSwitchJoyconLeft,
        NintendoSwitchJoyconRight,
        NintendoSwitchJoyconPair,
        GameCube,
        Steam,
    }

    public enum GamepadButton
    {
        Invalid = -1,
        South,
        East,
        West,
        North,
        Back,
        Guide,
        Start,
        LeftStick,
        RightStick,
        LeftShoulder,
        RightShoulder,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        Misc1,
        RightPaddle1,
        LeftPaddle1,
        RightPaddle2,
        LeftPaddle2,
        Touchpad,
        Misc2,
        Misc3,
        Misc4,
        Misc5,
        Misc6,
    }

    public enum GamepadButtonLabel
    {
        Unknown,
        A,
        B,
        X,
        Y,
        Cross,
        Circle,
        Square,
        Triangle,
    }

    public enum GamepadAxis
    {
        Invalid = -1,
        LeftX,
        LeftY,
        RightX,
        RightY,
        LeftTrigger,
        RightTrigger,
    }

    public enum GamepadBindingType
    {
        None,
        Button,
        Axis,
        Hat,
    }

    public readonly record struct Gamepad(nint Value)
    {
        public static implicit operator nint(Gamepad gamepad) => gamepad.Value;

        public static implicit operator Gamepad(nint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadBindingInputAxis
    {
        public int Axis;
        public int AxisMin;
        public int AxisMax;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadBindingInputHat
    {
        public int Hat;
        public int HatMask;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GamepadBindingInput
    {
        [FieldOffset(0)]
        public int Button;

        [FieldOffset(0)]
        public GamepadBindingInputAxis Axis;

        [FieldOffset(0)]
        public GamepadBindingInputHat Hat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadBindingOutputAxis
    {
        public GamepadAxis Axis;
        public int AxisMin;
        public int AxisMax;
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct GamepadBindingOutput
    {
        [FieldOffset(0)]
        public GamepadButton Button;

        [FieldOffset(0)]
        public GamepadBindingOutputAxis Axis;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadBinding
    {
        public GamepadBindingType InputType;
        public GamepadBindingInput Input;
        public GamepadBindingType OutputType;
        public GamepadBindingOutput Output;
    }
}
