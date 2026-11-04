// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const string PropThreadCreateEntryFunctionPointer = "SDL.thread.create.entry_function";
    public const string PropThreadCreateNameString = "SDL.thread.create.name";
    public const string PropThreadCreateUserdataPointer = "SDL.thread.create.userdata";
    public const string PropThreadCreateStackSizeNumber = "SDL.thread.create.stacksize";

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateThread",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Thread _CreateThread(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        string name,
        void* data
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateThreadWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Thread _CreateThreadWithProperties(PropertiesID props);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateThreadRuntime",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Thread _CreateThreadRuntime(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        string name,
        void* data,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateThreadWithPropertiesRuntime",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Thread _CreateThreadWithPropertiesRuntime(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetThreadName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetThreadName(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentThreadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadID _GetCurrentThreadID();

    [LibraryImport(DllName, EntryPoint = "SDL_GetThreadID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ThreadID _GetThreadID(Thread thread);

    [LibraryImport(DllName, EntryPoint = "SDL_SetCurrentThreadPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetCurrentThreadPriority(ThreadPriority priority);

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
    private static unsafe partial void* _GetTls(ref TlsID id);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTLS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetTls(
        ref TlsID id,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void> destructor
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CleanupTLS")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CleanupTls();

    public static unsafe Thread CreateThread(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        string name,
        void* data
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _CreateThread(fn, name, data);
    }

    public static Thread CreateThreadWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThreadWithProperties(props);
    }

    public static unsafe Thread CreateThreadRuntime(
        delegate* unmanaged[Cdecl]<void*, int> fn,
        string name,
        void* data,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _CreateThreadRuntime(fn, name, data, pfnBeginThread, pfnEndThread);
    }

    public static unsafe Thread CreateThreadWithPropertiesRuntime(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void> pfnBeginThread,
        delegate* unmanaged[Cdecl]<void> pfnEndThread
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateThreadWithPropertiesRuntime(props, pfnBeginThread, pfnEndThread);
    }

    public static string? GetThreadName(Thread thread)
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

    public static bool SetCurrentThreadPriority(ThreadPriority priority)
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

    public static unsafe void* GetTls(ref TlsID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTls(ref id);
    }

    public static unsafe bool SetTls(
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
        public bool IsNull => Value == 0;

        public static implicit operator Thread(nint value) => new(value);

        public static implicit operator nint(Thread thread) => thread.Value;
    }

    public readonly record struct ThreadID(ulong Value)
    {
        public bool IsInvalid => Value == 0;

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
