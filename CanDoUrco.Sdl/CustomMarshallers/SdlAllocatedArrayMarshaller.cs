// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) <year>  <name of author>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl.CustomMarshallers;

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
public static unsafe class SdlAllocatedArrayMarshaller<T, TUnmanagedElement>
    where TUnmanagedElement : unmanaged
{
    public static TUnmanagedElement* AllocateContainerForUnmanagedElements(
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
        int spaceToAllocate = Math.Max(checked(sizeof(TUnmanagedElement) * numElements), 1);
        return (TUnmanagedElement*)Sdl.Malloc((nuint)spaceToAllocate);
    }

    public static ReadOnlySpan<T> GetManagedValuesSource(T[]? managed)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return managed;
    }

    public static Span<TUnmanagedElement> GetUnmanagedValuesDestination(
        TUnmanagedElement* unmanaged,
        int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return unmanaged is null ? [] : new Span<TUnmanagedElement>(unmanaged, numElements);
    }

    public static T[]? AllocateContainerForManagedElements(
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

    public static ReadOnlySpan<TUnmanagedElement> GetUnmanagedValuesSource(
        TUnmanagedElement* unmanagedValue,
        int numElements
    )
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return unmanagedValue is null
            ? []
            : new ReadOnlySpan<TUnmanagedElement>(unmanagedValue, numElements);
    }

    public static void Free(TUnmanagedElement* unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        Sdl.Free(unmanaged);
    }

    public ref struct ManagedToUnmanagedIn
    {
        private T[]? _managedArray;
        private TUnmanagedElement* _allocatedMemory;
        private Span<TUnmanagedElement> _span;

        public static int BufferSize => 0x200 / sizeof(TUnmanagedElement);

        public void FromManaged(T[]? array, Span<TUnmanagedElement> buffer)
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
                _allocatedMemory = (TUnmanagedElement*)Sdl.Malloc((nuint)spaceToAllocate);
                _span = new Span<TUnmanagedElement>(_allocatedMemory, array.Length);
            }
        }

        public readonly ReadOnlySpan<T> GetManagedValuesSource()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _managedArray;
        }

        public readonly Span<TUnmanagedElement> GetUnmanagedValuesDestination()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _span;
        }

        public readonly ref TUnmanagedElement GetPinnableReference()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return ref MemoryMarshal.GetReference(_span);
        }

        public readonly TUnmanagedElement* ToUnmanaged()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return (TUnmanagedElement*)Unsafe.AsPointer(ref GetPinnableReference());
        }

        public readonly void Free()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            Sdl.Free(_allocatedMemory);
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
