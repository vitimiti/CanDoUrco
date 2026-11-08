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
    public const float StandardGravity = 9.80665F;

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorID* _GetSensors(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetSensorNameForID(SensorID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType _GetSensorTypeForID(SensorID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorNonPortableTypeForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetSensorNonPortableTypeForID(SensorID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sensor _OpenSensor(SensorID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Sensor _GetSensorFromID(SensorID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetSensorProperties(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetSensorName(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorType _GetSensorType(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorNonPortableType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetSensorNonPortableType(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SensorID _GetSensorID(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSensorData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSensorData(Sensor sensor, float* data, int numValues);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseSensor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseSensor(Sensor sensor);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateSensors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UpdateSensors();

    public static SensorID* GetSensors(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensors(out count);
    }

    public static byte* GetSensorNameForID(SensorID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorNameForID(instanceID);
    }

    public static SensorType GetSensorTypeForID(SensorID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorTypeForID(instanceID);
    }

    public static int GetSensorNonPortableTypeForID(SensorID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorNonPortableTypeForID(instanceID);
    }

    public static Sensor OpenSensor(SensorID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenSensor(instanceID);
    }

    public static Sensor GetSensorFromID(SensorID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorFromID(instanceID);
    }

    public static PropertiesID GetSensorProperties(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorProperties(sensor);
    }

    public static byte* GetSensorName(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorName(sensor);
    }

    public static SensorType GetSensorType(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorType(sensor);
    }

    public static int GetSensorNonPortableType(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorNonPortableType(sensor);
    }

    public static SensorID GetSensorID(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorID(sensor);
    }

    public static Bool GetSensorData(Sensor sensor, float* data, int numValues)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSensorData(sensor, data, numValues);
    }

    public static void CloseSensor(Sensor sensor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseSensor(sensor);
    }

    public static void UpdateSensors()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UpdateSensors();
    }

    public enum SensorType
    {
        Invalid = -1,
        Unknown,
        Accel,
        Gyro,
        AccelL,
        GyroL,
        AccelR,
        GyroR,
    }

    public readonly record struct Sensor(nint Value)
    {
        public static implicit operator nint(Sensor sensor) => sensor.Value;

        public static implicit operator Sensor(nint value) => new(value);
    }

    public readonly record struct SensorID(uint Value)
    {
        public static implicit operator uint(SensorID sensorID) => sensorID.Value;

        public static implicit operator SensorID(uint value) => new(value);
    }
}
