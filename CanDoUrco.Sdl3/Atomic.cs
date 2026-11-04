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
    [LibraryImport(DllName, EntryPoint = "SDL_TryLockSpinlock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _TryLockSpinlock(SpinLock spinlock);

    [LibraryImport(DllName, EntryPoint = "SDL_LockSpinlock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _LockSpinlock(SpinLock spinlock);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockSpinlock")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockSpinlock(SpinLock spinlock);

    [LibraryImport(DllName, EntryPoint = "SDL_MemoryBarrierReleaseFunction")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _MemoryBarrierReleaseFunction();

    [LibraryImport(DllName, EntryPoint = "SDL_MemoryBarrierAcquireFunction")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _MemoryBarrierAcquireFunction();

    [LibraryImport(DllName, EntryPoint = "SDL_CompareAndSwapAtomicInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CompareAndSwapAtomicInt(ref AtomicInt a, int oldVal, int newVal);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAtomicInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _SetAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAtomicInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetAtomicInt(ref AtomicInt a);

    [LibraryImport(DllName, EntryPoint = "SDL_AddAtomicInt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _AddAtomicInt(ref AtomicInt a, int v);

    [LibraryImport(DllName, EntryPoint = "SDL_CompareAndSwapAtomicU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldVal, uint newVal);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAtomicU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _SetAtomicU32(ref AtomicU32 a, uint v);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAtomicU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _GetAtomicU32(ref AtomicU32 a);

    [LibraryImport(DllName, EntryPoint = "SDL_AddAtomicU32")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _AddAtomicU32(ref AtomicU32 a, uint v);

    [LibraryImport(DllName, EntryPoint = "SDL_CompareAndSwapAtomicPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _CompareAndSwapAtomicPointer(
        void** a,
        void* oldVal,
        void* newVal
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAtomicPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _SetAtomicPointer(void** a, void* v);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAtomicPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _GetAtomicPointer(void** a);

    public static bool TryLockSpinlock(SpinLock spinlock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TryLockSpinlock(spinlock);
    }

    public static void LockSpinlock(SpinLock spinlock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _LockSpinlock(spinlock);
    }

    public static void UnlockSpinlock(SpinLock spinlock)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockSpinlock(spinlock);
    }

    public static void MemoryBarrierReleaseFunction()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _MemoryBarrierReleaseFunction();
    }

    public static void MemoryBarrierAcquireFunction()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _MemoryBarrierAcquireFunction();
    }

    public static void MemoryBarrierRelease()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        MemoryBarrierReleaseFunction();
    }

    public static void MemoryBarrierAcquire()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        MemoryBarrierAcquireFunction();
    }

    public static bool CompareAndSwapAtomicInt(ref AtomicInt a, int oldVal, int newVal)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CompareAndSwapAtomicInt(ref a, oldVal, newVal);
    }

    public static int SetAtomicInt(ref AtomicInt a, int v)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAtomicInt(ref a, v);
    }

    public static int GetAtomicInt(ref AtomicInt a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAtomicInt(ref a);
    }

    public static int AddAtomicInt(ref AtomicInt a, int v)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddAtomicInt(ref a, v);
    }

    public static int AtomicIncRef(ref AtomicInt a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return AddAtomicInt(ref a, 1);
    }

    public static bool AtomicDecRef(ref AtomicInt a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return AddAtomicInt(ref a, -1) == 1;
    }

    public static bool CompareAndSwapAtomicU32(ref AtomicU32 a, uint oldVal, uint newVal)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CompareAndSwapAtomicU32(ref a, oldVal, newVal);
    }

    public static uint SetAtomicU32(ref AtomicU32 a, uint v)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAtomicU32(ref a, v);
    }

    public static uint GetAtomicU32(ref AtomicU32 a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAtomicU32(ref a);
    }

    public static uint AddAtomicU32(ref AtomicU32 a, uint v)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddAtomicU32(ref a, v);
    }

    public static unsafe bool CompareAndSwapAtomicPointer(void** a, void* oldVal, void* newVal)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CompareAndSwapAtomicPointer(a, oldVal, newVal);
    }

    public static unsafe void* SetAtomicPointer(void** a, void* v)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAtomicPointer(a, v);
    }

    public static unsafe void* GetAtomicPointer(void** a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAtomicPointer(a);
    }

    public readonly record struct SpinLock(int Value)
    {
        public static implicit operator SpinLock(int value) => new SpinLock(value);

        public static implicit operator int(SpinLock spinLock) => spinLock.Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AtomicInt
    {
        public int Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AtomicU32
    {
        public uint Value;
    }
}
