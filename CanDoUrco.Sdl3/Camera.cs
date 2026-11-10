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
    [LibraryImport(DllName, EntryPoint = "SDL_GetNumCameraDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumCameraDrivers();

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCameraDriver(int index);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentCameraDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCurrentCameraDriver();

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameras")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraID* _GetCameras(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraSupportedFormats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraSpec** _GetCameraSupportedFormats(
        CameraID instanceID,
        out int count
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCameraName(CameraID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraPosition _GetCameraPosition(CameraID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Camera _OpenCamera(CameraID instanceID, CameraSpec* spec);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraPermissionState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraPermissionState _GetCameraPermissionState(Camera camera);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial CameraID _GetCameraID(Camera camera);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetCameraProperties(Camera camera);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCameraFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetCameraFormat(Camera camera, out CameraSpec spec);

    [LibraryImport(DllName, EntryPoint = "SDL_AcquireCameraFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _AcquireCameraFrame(Camera camera, out ulong timestampNS);

    [LibraryImport(DllName, EntryPoint = "SDL_ReleaseCameraFrame")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ReleaseCameraFrame(Camera camera, Surface* frame);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseCamera")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseCamera(Camera camera);

    public static int GetNumCameraDrivers()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumCameraDrivers();
    }

    public static byte* GetCameraDriver(int index)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraDriver(index);
    }

    public static byte* GetCurrentCameraDriver()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentCameraDriver();
    }

    public static CameraID* GetCameras(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameras(out count);
    }

    public static CameraSpec** GetCameraSupportedFormats(CameraID instanceID, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraSupportedFormats(instanceID, out count);
    }

    public static byte* GetCameraName(CameraID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraName(instanceID);
    }

    public static CameraPosition GetCameraPosition(CameraID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraPosition(instanceID);
    }

    public static Camera OpenCamera(CameraID instanceID, CameraSpec* spec)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenCamera(instanceID, spec);
    }

    public static Bool GetCameraFormat(Camera camera, out CameraSpec spec)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraFormat(camera, out spec);
    }

    public static CameraPermissionState GetCameraPermissionState(Camera camera)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraPermissionState(camera);
    }

    public static CameraID GetCameraID(Camera camera)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraID(camera);
    }

    public static PropertiesID GetCameraProperties(Camera camera)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCameraProperties(camera);
    }

    public static Surface* AcquireCameraFrame(Camera camera, out ulong timestampNS)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AcquireCameraFrame(camera, out timestampNS);
    }

    public static void ReleaseCameraFrame(Camera camera, Surface* frame)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ReleaseCameraFrame(camera, frame);
    }

    public static void CloseCamera(Camera camera)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseCamera(camera);
    }

    public enum CameraPosition
    {
        Unknown,
        FrontFacing,
        BackFacing,
    }

    public enum CameraPermissionState
    {
        Denied = -1,
        Pending,
        Approved,
    }

    public readonly record struct CameraID(uint Value)
    {
        public static implicit operator uint(CameraID instanceID) => instanceID.Value;

        public static implicit operator CameraID(uint value) => new(value);
    }

    public readonly record struct Camera(nint Value)
    {
        public static implicit operator nint(Camera camera) => camera.Value;

        public static implicit operator Camera(nint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CameraSpec
    {
        public PixelFormat Format;
        public Colorspace Colorspace;
        public int Width;
        public int Height;
        public int FramerateNumerator;
        public int FramerateDenominator;
    }
}
