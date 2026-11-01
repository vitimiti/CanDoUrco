// SpanOwner`1.cs - Defines a generic class for managing spans of memory efficiently.
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

using System.Buffers;
using System.Diagnostics;
using System.Runtime.InteropServices;
using CanDoUrco.Core.Extensions;

namespace CanDoUrco.Core.Buffers;

[DebuggerDisplay("{ToString(),raw}")]
public readonly ref struct SpanOwner<T> : IDisposable
{
    private readonly ArrayPool<T> _pool;
    private readonly T[] _array;

    public static SpanOwner<T> Empty => new(0, ArrayPool<T>.Shared, AllocationMode.Default);

    public int Length { get; }

    public Span<T> Span
    {
        get
        {
            ref T r0 = ref _array!.DangerousGetReference();
            return MemoryMarshal.CreateSpan(ref r0, Length);
        }
    }

    private SpanOwner(int length, ArrayPool<T> pool, AllocationMode mode)
    {
        Length = length;
        _pool = pool;
        _array = pool.Rent(length);

        if (mode == AllocationMode.Clear)
        {
            _array.AsSpan(0, length).Clear();
        }
    }

    public static SpanOwner<T> Allocate(int size) =>
        new(size, ArrayPool<T>.Shared, AllocationMode.Default);

    public static SpanOwner<T> Allocate(int size, ArrayPool<T> pool) =>
        new(size, pool, AllocationMode.Default);

    public static SpanOwner<T> Allocate(int size, AllocationMode mode) =>
        new(size, ArrayPool<T>.Shared, mode);

    public static SpanOwner<T> Allocate(int size, ArrayPool<T> pool, AllocationMode mode) =>
        new(size, pool, mode);

    public ref T DangerousGetReference() => ref _array.DangerousGetReference();

    public ArraySegment<T> DangerousGetArray() => new(_array!, 0, Length);

    public void Dispose() => _pool.Return(_array);

    public override string ToString() =>
        typeof(T) == typeof(char) && _array is char[] chars
            ? new(chars, 0, Length)
            : $"CanDoUrco.Core.Buffers.SpanOwner<{typeof(T)}>[{Length}]";
}
