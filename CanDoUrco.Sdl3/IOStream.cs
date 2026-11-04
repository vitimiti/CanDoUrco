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
    public const string PropIOStreamWindowsHandlePointer = "SDL.iostream.windows.handle";
    public const string PropIOStreamStdIOFilePointer = "SDL.iostream.stdio.file";
    public const string PropIOStreamFileDescriptorNumber = "SDL.iostream.file_descriptor";
    public const string PropIOStreamAndroidAAssetPointer = "SDL.iostream.android.aasset";
    public const string PropIOStreamMemoryPointer = "SDL.iostream.memory.base";
    public const string PropIOStreamMemorySizeNumber = "SDL.iostream.memory.size";
    public const string PropIOStreamMemoryFreeFuncPointer = "SDL.iostream.memory.free";
    public const string PropIOStreamDynamicMemoryPointer = "SDL.iostream.dynamic.memory";
    public const string PropIOStreamDynamicChunkSizeNumber = "SDL.iostream.dynamic.chunksize";

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_IOFromFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromFile(string file, string mode);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IOStream _IOFromMem(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromConstMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IOStream _IOFromConstMem(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromDynamicMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromDynamicMem();

    [LibraryImport(DllName, EntryPoint = "SDL_OpenIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial IOStream _OpenIO(in IOStreamInterface iface, void* userdata);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _CloseIO(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_GetIOProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetIOProperties(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_GetIOStatus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStatus _GetIOStatus(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_GetIOSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _GetIOSize(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_SeekIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _SeekIO(IOStream context, long offset, IOWhence whence);

    [LibraryImport(DllName, EntryPoint = "SDL_TellIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _TellIO(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _ReadIO(IOStream context, void* buffer, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nuint _WriteIO(IOStream context, void* buffer, nuint size);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_IOprintf",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _IOprintf(IOStream context, string fmt, string message);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _FlushIO(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadFile_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _LoadFileIO(
        IOStream src,
        out nuint dataSize,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_LoadFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _LoadFile(string file, out nuint dataSize);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveFile_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SaveFileIO(
        IOStream src,
        void* data,
        nuint dataSize,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SaveFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SaveFile(string file, void* data, nuint dataSize);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU8(IOStream context, out byte value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS8(IOStream context, out sbyte value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU16LE(IOStream context, out ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS16LE(IOStream context, out short value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU16BE(IOStream context, out ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS16BE(IOStream context, out short value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU32LE(IOStream context, out uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS32LE(IOStream context, out int value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU32BE(IOStream context, out uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS32BE(IOStream context, out int value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU64LE(IOStream context, out ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS64LE(IOStream context, out long value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadU64BE(IOStream context, out ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadS64BE(IOStream context, out long value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU8(IOStream context, byte value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS8(IOStream context, sbyte value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU16LE(IOStream context, ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS16LE(IOStream context, short value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU16BE(IOStream context, ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS16BE(IOStream context, short value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU32LE(IOStream context, uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS32LE(IOStream context, int value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU32BE(IOStream context, uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS32BE(IOStream context, int value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU64LE(IOStream context, ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS64LE(IOStream context, long value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteU64BE(IOStream context, ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteS64BE(IOStream context, long value);

    public static IOStream IOFromFile(string file, string mode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(mode);
        return _IOFromFile(file, mode);
    }

    public static unsafe IOStream IOFromMem(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromMem(mem, size);
    }

    public static unsafe IOStream IOFromConstMem(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromConstMem(mem, size);
    }

    public static IOStream IOFromDynamicMem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromDynamicMem();
    }

    public static unsafe IOStream OpenIO(in IOStreamInterface iface, void* userdata)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenIO(in iface, userdata);
    }

    public static bool CloseIO(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CloseIO(context);
    }

    public static PropertiesID GetIOProperties(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetIOProperties(context);
    }

    public static IOStatus GetIOStatus(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetIOStatus(context);
    }

    public static long GetIOSize(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetIOSize(context);
    }

    public static long SeekIO(IOStream context, long offset, IOWhence whence)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SeekIO(context, offset, whence);
    }

    public static long TellIO(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TellIO(context);
    }

    public static unsafe nuint ReadIO(IOStream context, void* buffer, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(buffer);
        return _ReadIO(context, buffer, size);
    }

    public static unsafe nuint WriteIO(IOStream context, void* buffer, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(buffer);
        return _WriteIO(context, buffer, size);
    }

    public static nuint IOPrintF(IOStream context, string message)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOprintf(context, "%s", message ?? string.Empty);
    }

    public static bool FlushIO(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlushIO(context);
    }

    public static unsafe void* LoadFileIO(IOStream src, out nuint dataSize, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadFileIO(src, out dataSize, closeIO);
    }

    public static unsafe void* LoadFile(string file, out nuint dataSize)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _LoadFile(file, out dataSize);
    }

    public static unsafe bool SaveFileIO(IOStream src, void* data, nuint dataSize, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(data);
        return _SaveFileIO(src, data, dataSize, closeIO);
    }

    public static unsafe bool SaveFile(string file, void* data, nuint dataSize)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        ArgumentNullException.ThrowIfNull(data);
        return _SaveFile(file, data, dataSize);
    }

    public static bool ReadU8(IOStream context, out byte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU8(context, out value);
    }

    public static bool ReadS8(IOStream context, out sbyte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS8(context, out value);
    }

    public static bool ReadU16LE(IOStream context, out ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU16LE(context, out value);
    }

    public static bool ReadS16LE(IOStream context, out short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS16LE(context, out value);
    }

    public static bool ReadU16BE(IOStream context, out ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU16BE(context, out value);
    }

    public static bool ReadS16BE(IOStream context, out short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS16BE(context, out value);
    }

    public static bool ReadU32LE(IOStream context, out uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU32LE(context, out value);
    }

    public static bool ReadS32LE(IOStream context, out int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS32LE(context, out value);
    }

    public static bool ReadU32BE(IOStream context, out uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU32BE(context, out value);
    }

    public static bool ReadS32BE(IOStream context, out int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS32BE(context, out value);
    }

    public static bool ReadU64LE(IOStream context, out ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU64LE(context, out value);
    }

    public static bool ReadS64LE(IOStream context, out long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS64LE(context, out value);
    }

    public static bool ReadU64BE(IOStream context, out ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU64BE(context, out value);
    }

    public static bool ReadS64BE(IOStream context, out long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS64BE(context, out value);
    }

    public static bool WriteU8(IOStream context, byte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU8(context, value);
    }

    public static bool WriteS8(IOStream context, sbyte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS8(context, value);
    }

    public static bool WriteU16LE(IOStream context, ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU16LE(context, value);
    }

    public static bool WriteS16LE(IOStream context, short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS16LE(context, value);
    }

    public static bool WriteU16BE(IOStream context, ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU16BE(context, value);
    }

    public static bool WriteS16BE(IOStream context, short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS16BE(context, value);
    }

    public static bool WriteU32LE(IOStream context, uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU32LE(context, value);
    }

    public static bool WriteS32LE(IOStream context, int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS32LE(context, value);
    }

    public static bool WriteU32BE(IOStream context, uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU32BE(context, value);
    }

    public static bool WriteS32BE(IOStream context, int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS32BE(context, value);
    }

    public static bool WriteU64LE(IOStream context, ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU64LE(context, value);
    }

    public static bool WriteS64LE(IOStream context, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS64LE(context, value);
    }

    public static bool WriteU64BE(IOStream context, ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU64BE(context, value);
    }

    public static bool WriteS64BE(IOStream context, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS64BE(context, value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IOStreamInterface
    {
        public uint Version;
        public unsafe delegate* unmanaged[Cdecl]<void*, long> Size;
        public unsafe delegate* unmanaged[Cdecl]<void*, long, IOWhence, long> Seek;
        public unsafe delegate* unmanaged[Cdecl]<void*, void*, nuint, IOStatus*, nuint> Read;
        public unsafe delegate* unmanaged[Cdecl]<void*, void*, nuint, IOStatus*, nuint> Write;
        public unsafe delegate* unmanaged[Cdecl]<void*, IOStatus*, sbyte> Flush; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, sbyte> Close; // sbyte is bool
    }

    public readonly record struct IOStream(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator IOStream(nint value) => new(value);

        public static implicit operator nint(IOStream value) => value.Value;
    }

    public enum IOStatus
    {
        Ready,
        Error,
        Eof,
        NotReady,
        ReadOnly,
        WriteOnly,
    }

    public enum IOWhence
    {
        Set,
        Cur,
        End,
    }
}
