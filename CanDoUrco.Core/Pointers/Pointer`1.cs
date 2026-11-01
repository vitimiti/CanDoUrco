// Pointer`1.cs - Defines generic pointer-related functionality for CanDoUrco.Pointers
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

using CanDoUrco.Core.Extensions;
using CanDoUrco.Core.Types;

namespace CanDoUrco.Core.Pointers;

public readonly unsafe struct Pointer<TValueType>(TValueType* pointer) : INullable
    where TValueType : unmanaged
{
    public TValueType* Value => pointer;
    public ref TValueType Ref => ref *Value;
    public bool IsNull => Value is null;

    public Pointer<TValueType> Offset(ElementCount count) => new(Value + count);

    public Span<TValueType> AsSpan(ElementCount length) => new(Value, length);

    public Pointer AsPtr() => new(Value);

    public Pointer<TCastType> Cast<TCastType>()
        where TCastType : unmanaged => new((TCastType*)Value);

    public ref TValueType this[int index] => ref Value[index];

    public static implicit operator Pointer(Pointer<TValueType> ptr) => ptr.AsPtr();

    public static implicit operator Pointer<TValueType>(TValueType* pointer) => new(pointer);

    public static implicit operator TValueType*(Pointer<TValueType> ptr) => ptr.Value;

    public static implicit operator void*(Pointer<TValueType> ptr) => ptr.Value;

    public static implicit operator Pointer<TValueType>(ReadOnlySpan<TValueType> span) =>
        span.AsPtr();

    public static implicit operator Pointer<TValueType>(Span<TValueType> span) => span.AsPtr();

    public static explicit operator Pointer<TValueType>(nint ptr) => new((TValueType*)ptr);

    public static explicit operator nint(Pointer<TValueType> ptr) => (nint)ptr.Value;
}
