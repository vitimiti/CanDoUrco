// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3.CustomMarshallers;

[CustomMarshaller(
    typeof(CustomMarshallerAttribute.GenericPlaceholder[]),
    MarshalMode.Default,
    typeof(SdlAllocatedArrayMarshaller<,>)
)]
[CustomMarshaller(
    typeof(CustomMarshallerAttribute.GenericPlaceholder[]),
    MarshalMode.ManagedToUnmanagedIn,
    typeof(SdlAllocatedArrayMarshaller<,>.ManagedToUnmanagedIn)
)]
[ContiguousCollectionMarshaller]
public static class SdlAllocatedArrayMarshaller<T, TUnmanagedElement>
    where TUnmanagedElement : unmanaged
{
    public static unsafe TUnmanagedElement* AllocateContainerForUnmanagedElements(
        T[]? managed,
        out int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        if (managed is null)
        {
            numElements = 0;
            return null;
        }

        numElements = managed.Length;
        var spaceToAllocate = int.Max(checked(sizeof(TUnmanagedElement) * numElements), 1);
        return (TUnmanagedElement*)Sdl.MAlloc((nuint)spaceToAllocate);
    }

    public static ReadOnlySpan<T> GetManagedValuesSource(T[]? managed)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return managed;
    }

    public static unsafe Span<TUnmanagedElement> GetUnmanagedValuesDestination(
        TUnmanagedElement* unmanaged,
        int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return unmanaged is null ? [] : new Span<TUnmanagedElement>(unmanaged, numElements);
    }

    public static unsafe T[]? AllocateContainerForManagedElements(
        TUnmanagedElement* unmanaged,
        int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return unmanaged is null ? null : (new T[numElements]);
    }

    public static Span<T> GetManagedValuesDestination(T[]? managed)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return managed;
    }

    public static unsafe ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(
        TUnmanagedElement* unmanagedValue,
        int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return unmanagedValue is null
            ? []
            : new ReadOnlySpan<TUnmanagedElement>(unmanagedValue, numElements);
    }

    public static unsafe void Free(TUnmanagedElement* unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        if (unmanaged is not null)
        {
            Sdl.Free(unmanaged);
        }
    }

    public ref struct ManagedToUnmanagedIn
    {
        private T[]? _managedArray;
        private unsafe TUnmanagedElement* _allocatedMemory;
        private Span<TUnmanagedElement> _span;

        public static unsafe int BufferSize => 0x200 / sizeof(TUnmanagedElement);

        public unsafe void FromManaged(T[]? array, Span<TUnmanagedElement> buffer)
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            _allocatedMemory = null;
            if (array is null)
            {
                _managedArray = null;
                _span = default;
                return;
            }

            _managedArray = array;
            if (array.Length <= buffer.Length)
            {
                _span = buffer[0..array.Length];
            }
            else
            {
                int bufferSize = checked(array.Length * sizeof(TUnmanagedElement));
                int spaceToAllocate = Math.Max(bufferSize, 1);
                _allocatedMemory = (TUnmanagedElement*)Sdl.MAlloc((nuint)spaceToAllocate);
                _span = new Span<TUnmanagedElement>(_allocatedMemory, array.Length);
            }
        }

        public ReadOnlySpan<T> GetManagedValuesSource()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _managedArray;
        }

        public Span<TUnmanagedElement> GetUnmanagedValuesDestination()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _span;
        }

        public ref TUnmanagedElement GetPinnableReference()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return ref MemoryMarshal.GetReference(_span);
        }

        public unsafe TUnmanagedElement* ToUnmanaged()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return (TUnmanagedElement*)Unsafe.AsPointer(ref GetPinnableReference());
        }

        public readonly unsafe void Free()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            if (_allocatedMemory is not null)
            {
                Sdl.Free(_allocatedMemory);
            }
        }

        public static ref T GetPinnableReference(T[]? array)
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return ref array is null
                ? ref Unsafe.NullRef<T>()
                : ref MemoryMarshal.GetArrayDataReference(array);
        }
    }
}
