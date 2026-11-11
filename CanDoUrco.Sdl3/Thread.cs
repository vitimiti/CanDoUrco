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
    public static ReadOnlySpan<byte> PropThreadCreateEntryFunctionPointer =>
        "SDL.thread.create.entry_function\0"u8;
    public static ReadOnlySpan<byte> PropThreadCreateNameString => "SDL.thread.create.name\0"u8;
    public static ReadOnlySpan<byte> PropThreadCreateUserdataPointer =>
        "SDL.thread.create.userdata\0"u8;
    public static ReadOnlySpan<byte> PropThreadCreateStackSizeNumber =>
        "SDL.thread.create.stacksize\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_CreateThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Thread _CreateThread(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        byte* name,
        void* data
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateThreadWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Thread _CreateThreadWithProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateThreadRuntime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Thread _CreateThreadRuntime(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        byte* name,
        void* data,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateThreadWithPropertiesRuntime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Thread _CreateThreadWithPropertiesRuntime(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetThreadName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetThreadName(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentThreadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadID _GetCurrentThreadID();

    [LibraryImport(DllName, EntryPoint = "SDL_GetThreadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadID _GetThreadID(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_SetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetCurrentThreadPriority(ThreadPriority priority);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _WaitThread(Thread thread, out int status);

    [LibraryImport(DllName, EntryPoint = "SDL_GetThreadState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadState _GetThreadState(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_DetachThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DetachThread(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTLS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetTls(ref TlsID id);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTLS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTls(
        ref TlsID id,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void> destructor
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CleanupTLS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CleanupTls();

    public static Thread CreateThread(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        byte* name,
        void* data
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThread(fn, name, data);
    }

    public static Thread CreateThreadWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThreadWithProperties(props);
    }

    public static Thread CreateThreadRuntime(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        byte* name,
        void* data,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThreadRuntime(fn, name, data, pfnBeginThread, pfnEndThread);
    }

    public static Thread CreateThreadWithPropertiesRuntime(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThreadWithPropertiesRuntime(props, pfnBeginThread, pfnEndThread);
    }

    public static byte* GetThreadName(Thread thread)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetThreadName(thread);
    }

    public static ThreadID GetCurrentThreadID()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentThreadID();
    }

    public static ThreadID GetThreadID(Thread thread)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetThreadID(thread);
    }

    public static Bool SetCurrentThreadPriority(ThreadPriority priority)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetCurrentThreadPriority(priority);
    }

    public static void WaitThread(Thread thread, out int status)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _WaitThread(thread, out status);
    }

    public static ThreadState GetThreadState(Thread thread)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetThreadState(thread);
    }

    public static void DetachThread(Thread thread)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DetachThread(thread);
    }

    public static void* GetTls(ref TlsID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTls(ref id);
    }

    public static Bool SetTls(
        ref TlsID id,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void> destructor
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTls(ref id, value, destructor);
    }

    public static void CleanupTls()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CleanupTls();
    }

    public enum ThreadPriority
    {
        Low,
        Normal,
        High,
        TimeCritical,
    }

    public enum ThreadState
    {
        Unknown,
        Alive,
        Detached,
        Complete,
    }

    public readonly record struct Thread(nint Value)
    {
        public static implicit operator Thread(nint value) => new(value);

        public static implicit operator nint(Thread thread) => thread.Value;

        public static implicit operator bool(Thread thread) => thread.Value != 0;
    }

    public readonly record struct ThreadID(ulong Value)
    {
        public static implicit operator ThreadID(ulong value) => new(value);

        public static implicit operator ulong(ThreadID threadID) => threadID.Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TlsID
    {
        public int Value;

        public static implicit operator TlsID(AtomicInt atomicInt) =>
            new() { Value = atomicInt.Value };

        public static implicit operator AtomicInt(TlsID tlsID) => new() { Value = tlsID.Value };
    }
}
