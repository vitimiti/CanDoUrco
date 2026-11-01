// ScratchSpan`1.cs - Defines a ref struct for managing temporary spans efficiently.
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
using System.Runtime.CompilerServices;

namespace CanDoUrco.Collections.Spans;

public readonly ref struct ScratchSpan<T> : IDisposable
{
    private readonly T[]? _rented;

    public Span<T> Span { get; }

    public ScratchSpan(int requiredLength, Span<T> stackBuffer)
    {
        Span =
            requiredLength <= stackBuffer.Length
                ? stackBuffer
                : _rented = ArrayPool<T>.Shared.Rent(requiredLength);
        Span = Span[..requiredLength];
    }

    public ScratchSpan(int requiredLength, Span<T> stackBuffer, out Span<T> span)
        : this(requiredLength, stackBuffer)
    {
        span = Span;
    }

    public void Dispose()
    {
        if (_rented is not null)
        {
            ArrayPool<T>.Shared.Return(
                _rented,
                clearArray: RuntimeHelpers.IsReferenceOrContainsReferences<T>()
            );
        }
    }

    public static implicit operator Span<T>(ScratchSpan<T> scratch) => scratch.Span;
}
