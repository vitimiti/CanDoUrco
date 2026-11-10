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
    public static ReadOnlySpan<byte> PropHidApiLibUsbDeviceHandlePointer =>
        "SDL.hidapi.libusb.device.handle\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_hid_init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidInit();

    [LibraryImport(DllName, EntryPoint = "SDL_hid_exit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _HidExit();

    [LibraryImport(DllName, EntryPoint = "SDL_hid_device_change_count")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _HidDeviceChangeCount();

    [LibraryImport(DllName, EntryPoint = "SDL_hid_enumerate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDeviceInfo* _HidEnumerate(ushort vendorID, ushort productID);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_free_enumeration")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _HidFreeEnumeration(HidDeviceInfo* devs);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_open")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDevice _HidOpen(
        ushort vendorID,
        ushort productID,
        char* serialNumber
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_open_path")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDevice _HidOpenPath(byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_properties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _HidGetProperties(HidDevice device);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_write")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidWrite(HidDevice dev, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_write_timeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidWriteTimeout(
        HidDevice dev,
        byte* data,
        nuint length,
        int milliseconds
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_read")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidRead(HidDevice dev, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_set_nonblocking")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidSetNonBlocking(HidDevice dev, Bool nonblock);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_send_feature_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidSendFeatureReport(HidDevice dev, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_feature_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidGetFeatureReport(HidDevice dev, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_input_report")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidGetInputReport(HidDevice dev, byte* data, nuint length);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_close")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidClose(HidDevice dev);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_manufacturer_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidGetManufacturerString(
        HidDevice dev,
        char* @string,
        nuint maxLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_product_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidGetProductString(
        HidDevice dev,
        char* @string,
        nuint maxLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_serial_number_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidGetSerialNumberString(
        HidDevice dev,
        char* @string,
        nuint maxLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_indexed_string")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ErrorCodeBool _HidGetIndexedString(
        HidDevice dev,
        int stringIndex,
        char* @string,
        nuint maxLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_device_info")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial HidDeviceInfo* _HidGetDeviceInfo(HidDevice dev);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_get_report_descriptor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _HidGetReportDescriptor(HidDevice dev, byte* buf, nuint bufSize);

    [LibraryImport(DllName, EntryPoint = "SDL_hid_ble_scan")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _HidBleScan(Bool active);

    public static ErrorCodeBool HidInit()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidInit();
    }

    public static void HidExit()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _HidExit();
    }

    public static uint HidDeviceChangeCount()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidDeviceChangeCount();
    }

    public static HidDeviceInfo* HidEnumerate(ushort vendorID, ushort productID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidEnumerate(vendorID, productID);
    }

    public static void HidFreeEnumeration(HidDeviceInfo* devs)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _HidFreeEnumeration(devs);
    }

    public static HidDevice HidOpen(ushort vendorID, ushort productID, char* serialNumber)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidOpen(vendorID, productID, serialNumber);
    }

    public static HidDevice HidOpenPath(byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidOpenPath(path);
    }

    public static PropertiesID HidGetProperties(HidDevice device)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetProperties(device);
    }

    public static int HidWrite(HidDevice dev, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidWrite(dev, data, length);
    }

    public static int HidWriteTimeout(HidDevice dev, byte* data, nuint length, int milliseconds)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidWriteTimeout(dev, data, length, milliseconds);
    }

    public static int HidRead(HidDevice dev, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidRead(dev, data, length);
    }

    public static ErrorCodeBool HidSetNonBlocking(HidDevice dev, Bool nonblock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidSetNonBlocking(dev, nonblock);
    }

    public static int HidSendFeatureReport(HidDevice dev, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidSendFeatureReport(dev, data, length);
    }

    public static int HidGetFeatureReport(HidDevice dev, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetFeatureReport(dev, data, length);
    }

    public static int HidGetInputReport(HidDevice dev, byte* data, nuint length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetInputReport(dev, data, length);
    }

    public static ErrorCodeBool HidClose(HidDevice dev)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidClose(dev);
    }

    public static ErrorCodeBool HidGetManufacturerString(HidDevice dev, char* @string, nuint maxLen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetManufacturerString(dev, @string, maxLen);
    }

    public static ErrorCodeBool HidGetProductString(HidDevice dev, char* @string, nuint maxLen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetProductString(dev, @string, maxLen);
    }

    public static ErrorCodeBool HidGetSerialNumberString(HidDevice dev, char* @string, nuint maxLen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetSerialNumberString(dev, @string, maxLen);
    }

    public static ErrorCodeBool HidGetIndexedString(
        HidDevice dev,
        int stringIndex,
        char* @string,
        nuint maxLen
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetIndexedString(dev, stringIndex, @string, maxLen);
    }

    public static HidDeviceInfo* HidGetDeviceInfo(HidDevice dev)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetDeviceInfo(dev);
    }

    public static int HidGetReportDescriptor(HidDevice dev, byte* buf, nuint bufSize)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HidGetReportDescriptor(dev, buf, bufSize);
    }

    public static void HidBleScan(Bool active)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _HidBleScan(active);
    }

    public enum HidBusType
    {
        Unknown,
        Usb,
        Bluetooth,
        I2C,
        Spi,
    }

    public readonly record struct HidDevice(nint Value)
    {
        public static implicit operator nint(HidDevice device) => device.Value;

        public static implicit operator HidDevice(nint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HidDeviceInfo
    {
        public byte* Path;
        public ushort VendorID;
        public ushort ProductID;
        public char* SerialNumber;
        public ushort ReleaseNumber;
        public char* ManufacturerString;
        public char* ProductString;
        public ushort UsagePage;
        public ushort Usage;
        public int InterfaceNumber;
        public int InterfaceClass;
        public int InterfaceSubclass;
        public int InterfaceProtocol;
        public HidBusType BusType;
        public HidDeviceInfo* Next;
    }
}
