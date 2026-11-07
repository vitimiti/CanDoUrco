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

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropIOStreamWindowsHandlePointer =>
        "SDL.iostream.windows.handle"u8;
    public static ReadOnlySpan<byte> PropIOStreamStdIOFilePointer => "SDL.iostream.stdio.file"u8;
    public static ReadOnlySpan<byte> PropIOStreamFileDescriptorNumber =>
        "SDL.iostream.file_descriptor"u8;
    public static ReadOnlySpan<byte> PropIOStreamAndroidAAssetPointer =>
        "SDL.iostream.android.aasset"u8;
    public static ReadOnlySpan<byte> PropIOStreamMemoryPointer => "SDL.iostream.memory.base"u8;
    public static ReadOnlySpan<byte> PropIOStreamMemorySizeNumber => "SDL.iostream.memory.size"u8;
    public static ReadOnlySpan<byte> PropIOStreamMemoryFreeFuncPointer =>
        "SDL.iostream.memory.free"u8;
    public static ReadOnlySpan<byte> PropIOStreamDynamicMemoryPointer =>
        "SDL.iostream.dynamic.memory"u8;
    public static ReadOnlySpan<byte> PropIOStreamDynamicChunkSizeNumber =>
        "SDL.iostream.dynamic.chunksize"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromFile(byte* file, byte* mode);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromMem(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromConstMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromConstMem(void* mem, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_IOFromDynamicMem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _IOFromDynamicMem();

    [LibraryImport(DllName, EntryPoint = "SDL_OpenIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _OpenIO(in IOStreamInterface iface, void* userdata);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _CloseIO(IOStream context);

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
    private static partial nuint _ReadIO(IOStream context, void* buffer, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _WriteIO(IOStream context, void* buffer, nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_IOprintf")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nuint _IOprintf(IOStream context, byte* fmt, byte* message);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushIO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FlushIO(IOStream context);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadFile_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _LoadFileIO(IOStream src, out nuint dataSize, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _LoadFile(byte* file, out nuint dataSize);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveFile_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SaveFileIO(IOStream src, void* data, nuint dataSize, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SaveFile(byte* file, void* data, nuint dataSize);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU8(IOStream context, out byte value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS8(IOStream context, out sbyte value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU16LE(IOStream context, out ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS16LE(IOStream context, out short value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU16BE(IOStream context, out ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS16BE(IOStream context, out short value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU32LE(IOStream context, out uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS32LE(IOStream context, out int value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU32BE(IOStream context, out uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS32BE(IOStream context, out int value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU64LE(IOStream context, out ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS64LE(IOStream context, out long value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadU64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadU64BE(IOStream context, out ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadS64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadS64BE(IOStream context, out long value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU8(IOStream context, byte value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS8")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS8(IOStream context, sbyte value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU16LE(IOStream context, ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS16LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS16LE(IOStream context, short value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU16BE(IOStream context, ushort value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS16BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS16BE(IOStream context, short value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU32LE(IOStream context, uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS32LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS32LE(IOStream context, int value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU32BE(IOStream context, uint value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS32BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS32BE(IOStream context, int value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU64LE(IOStream context, ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS64LE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS64LE(IOStream context, long value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteU64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteU64BE(IOStream context, ulong value);

    [LibraryImport(DllName, EntryPoint = "SDL_WriteS64BE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteS64BE(IOStream context, long value);

    public static IOStream IOFromFile(byte* file, byte* mode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromFile(file, mode);
    }

    public static IOStream IOFromMem(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromMem(mem, size);
    }

    public static IOStream IOFromConstMem(void* mem, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromConstMem(mem, size);
    }

    public static IOStream IOFromDynamicMem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOFromDynamicMem();
    }

    public static IOStream OpenIO(in IOStreamInterface iface, void* userdata)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenIO(in iface, userdata);
    }

    public static Bool CloseIO(IOStream context)
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

    public static nuint ReadIO(IOStream context, void* buffer, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadIO(context, buffer, size);
    }

    public static nuint WriteIO(IOStream context, void* buffer, nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteIO(context, buffer, size);
    }

    public static nuint IOPrintF(IOStream context, byte* fmt, byte* message)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IOprintf(context, fmt, message);
    }

    public static Bool FlushIO(IOStream context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlushIO(context);
    }

    public static void* LoadFileIO(IOStream src, out nuint dataSize, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadFileIO(src, out dataSize, closeIO);
    }

    public static void* LoadFile(byte* file, out nuint dataSize)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadFile(file, out dataSize);
    }

    public static Bool SaveFileIO(IOStream src, void* data, nuint dataSize, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SaveFileIO(src, data, dataSize, closeIO);
    }

    public static Bool SaveFile(byte* file, void* data, nuint dataSize)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SaveFile(file, data, dataSize);
    }

    public static Bool ReadU8(IOStream context, out byte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU8(context, out value);
    }

    public static Bool ReadS8(IOStream context, out sbyte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS8(context, out value);
    }

    public static Bool ReadU16LE(IOStream context, out ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU16LE(context, out value);
    }

    public static Bool ReadS16LE(IOStream context, out short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS16LE(context, out value);
    }

    public static Bool ReadU16BE(IOStream context, out ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU16BE(context, out value);
    }

    public static Bool ReadS16BE(IOStream context, out short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS16BE(context, out value);
    }

    public static Bool ReadU32LE(IOStream context, out uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU32LE(context, out value);
    }

    public static Bool ReadS32LE(IOStream context, out int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS32LE(context, out value);
    }

    public static Bool ReadU32BE(IOStream context, out uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU32BE(context, out value);
    }

    public static Bool ReadS32BE(IOStream context, out int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS32BE(context, out value);
    }

    public static Bool ReadU64LE(IOStream context, out ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU64LE(context, out value);
    }

    public static Bool ReadS64LE(IOStream context, out long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS64LE(context, out value);
    }

    public static Bool ReadU64BE(IOStream context, out ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadU64BE(context, out value);
    }

    public static Bool ReadS64BE(IOStream context, out long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadS64BE(context, out value);
    }

    public static Bool WriteU8(IOStream context, byte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU8(context, value);
    }

    public static Bool WriteS8(IOStream context, sbyte value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS8(context, value);
    }

    public static Bool WriteU16LE(IOStream context, ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU16LE(context, value);
    }

    public static Bool WriteS16LE(IOStream context, short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS16LE(context, value);
    }

    public static Bool WriteU16BE(IOStream context, ushort value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU16BE(context, value);
    }

    public static Bool WriteS16BE(IOStream context, short value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS16BE(context, value);
    }

    public static Bool WriteU32LE(IOStream context, uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU32LE(context, value);
    }

    public static Bool WriteS32LE(IOStream context, int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS32LE(context, value);
    }

    public static Bool WriteU32BE(IOStream context, uint value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU32BE(context, value);
    }

    public static Bool WriteS32BE(IOStream context, int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS32BE(context, value);
    }

    public static Bool WriteU64LE(IOStream context, ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU64LE(context, value);
    }

    public static Bool WriteS64LE(IOStream context, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS64LE(context, value);
    }

    public static Bool WriteU64BE(IOStream context, ulong value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteU64BE(context, value);
    }

    public static Bool WriteS64BE(IOStream context, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteS64BE(context, value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct IOStreamInterface
    {
        public uint Version;
        public delegate* unmanaged[Cdecl]<void*, long> Size;
        public delegate* unmanaged[Cdecl]<void*, long, IOWhence, long> Seek;
        public delegate* unmanaged[Cdecl]<void*, void*, nuint, IOStatus*, nuint> Read;
        public delegate* unmanaged[Cdecl]<void*, void*, nuint, IOStatus*, nuint> Write;
        public delegate* unmanaged[Cdecl]<void*, IOStatus*, Bool> Flush;
        public delegate* unmanaged[Cdecl]<void*, Bool> Close;
    }

    public readonly record struct IOStream(nint Value)
    {
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
