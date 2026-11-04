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
    [LibraryImport(DllName, EntryPoint = "SDL_CreateMutex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Mutex _CreateMutex();

    [LibraryImport(DllName, EntryPoint = "SDL_LockMutex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _LockMutex(Mutex mutex);

    [LibraryImport(DllName, EntryPoint = "SDL_TryLockMutex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TryLockMutex(Mutex mutex);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockMutex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockMutex(Mutex mutex);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyMutex")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyMutex(Mutex mutex);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateRWLock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial RWLock _CreateRWLock();

    [LibraryImport(DllName, EntryPoint = "SDL_LockRWLockForReading")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _LockRWLockForReading(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_LockRWLockForWriting")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _LockRWLockForWriting(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_TryLockRWLockForReading")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TryLockRWLockForReading(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_TryLockRWLockForWriting")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TryLockRWLockForWriting(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockRWLock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockRWLock(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyRWLock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyRWLock(RWLock rwLock);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Semaphore _CreateSemaphore(uint initialValue);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroySemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroySemaphore(Semaphore sem);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _WaitSemaphore(Semaphore sem);

    [LibraryImport(DllName, EntryPoint = "SDL_TryWaitSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TryWaitSemaphore(Semaphore sem);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitSemaphoreTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WaitSemaphoreTimeout(Semaphore sem, uint timeoutMS);

    [LibraryImport(DllName, EntryPoint = "SDL_SignalSemaphore")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SignalSemaphore(Semaphore sem);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSemaphoreValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _GetSemaphoreValue(Semaphore sem);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateCondition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Condition _CreateCondition();

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyCondition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyCondition(Condition cond);

    [LibraryImport(DllName, EntryPoint = "SDL_SignalCondition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SignalCondition(Condition cond);

    [LibraryImport(DllName, EntryPoint = "SDL_BroadcastCondition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _BroadcastCondition(Condition cond);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitCondition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _WaitCondition(Condition cond, Mutex mutex);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitConditionTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WaitConditionTimeout(Condition cond, Mutex mutex, uint timeoutMS);

    [LibraryImport(DllName, EntryPoint = "SDL_ShouldInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ShouldInit(ref InitState state);

    [LibraryImport(DllName, EntryPoint = "SDL_ShouldQuit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ShouldQuit(ref InitState state);

    [LibraryImport(DllName, EntryPoint = "SDL_SetInitialized")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetInitialized(
        ref InitState state,
        [MarshalAs(UnmanagedType.I1)] bool initialized
    );

    public static Mutex CreateMutex()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateMutex();
    }

    public static void LockMutex(Mutex mutex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _LockMutex(mutex);
    }

    public static bool TryLockMutex(Mutex mutex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TryLockMutex(mutex);
    }

    public static void UnlockMutex(Mutex mutex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockMutex(mutex);
    }

    public static void DestroyMutex(Mutex mutex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyMutex(mutex);
    }

    public static RWLock CreateRWLock()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateRWLock();
    }

    public static void LockRWLockForReading(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _LockRWLockForReading(rwLock);
    }

    public static void LockRWLockForWriting(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _LockRWLockForWriting(rwLock);
    }

    public static bool TryLockRWLockForReading(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TryLockRWLockForReading(rwLock);
    }

    public static bool TryLockRWLockForWriting(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TryLockRWLockForWriting(rwLock);
    }

    public static void UnlockRWLock(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockRWLock(rwLock);
    }

    public static void DestroyRWLock(RWLock rwLock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyRWLock(rwLock);
    }

    public static Semaphore CreateSemaphore(uint initialValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSemaphore(initialValue);
    }

    public static void DestroySemaphore(Semaphore sem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroySemaphore(sem);
    }

    public static void WaitSemaphore(Semaphore sem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _WaitSemaphore(sem);
    }

    public static bool TryWaitSemaphore(Semaphore sem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TryWaitSemaphore(sem);
    }

    public static bool WaitSemaphoreTimeout(Semaphore sem, uint timeoutMS)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitSemaphoreTimeout(sem, timeoutMS);
    }

    public static void SignalSemaphore(Semaphore sem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SignalSemaphore(sem);
    }

    public static uint GetSemaphoreValue(Semaphore sem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSemaphoreValue(sem);
    }

    public static Condition CreateCondition()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateCondition();
    }

    public static void DestroyCondition(Condition cond)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyCondition(cond);
    }

    public static void SignalCondition(Condition cond)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SignalCondition(cond);
    }

    public static void BroadcastCondition(Condition cond)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _BroadcastCondition(cond);
    }

    public static void WaitCondition(Condition cond, Mutex mutex)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _WaitCondition(cond, mutex);
    }

    public static bool WaitConditionTimeout(Condition cond, Mutex mutex, uint timeoutMS)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitConditionTimeout(cond, mutex, timeoutMS);
    }

    public static bool ShouldInit(ref InitState state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShouldInit(ref state);
    }

    public static bool ShouldQuit(ref InitState state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShouldQuit(ref state);
    }

    public static void SetInitialized(ref InitState state, bool initialized)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetInitialized(ref state, initialized);
    }

    public enum InitStatus
    {
        Uninitialized,
        Initializing,
        Initialized,
        Uninitializing,
    }

    public readonly record struct Mutex(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(Mutex mutex) => mutex.Value;

        public static implicit operator Mutex(nint value) => new(value);
    }

    public readonly record struct RWLock(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(RWLock rwLock) => rwLock.Value;

        public static implicit operator RWLock(nint value) => new(value);
    }

    public readonly record struct Semaphore(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(Semaphore sem) => sem.Value;

        public static implicit operator Semaphore(nint value) => new(value);
    }

    public readonly record struct Condition(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(Condition cond) => cond.Value;

        public static implicit operator Condition(nint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct InitState
    {
        public AtomicInt Status;
        public ThreadID Thread;
        private readonly unsafe void* _reseved;
    }
}
