// PointerPointer`1.cs - Defines generic pointer-to-pointer related functionality for CanDoUrco.Pointers
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

using CanDoUrco.Core.Types;

namespace CanDoUrco.Core.Pointers;

public readonly unsafe struct PointerPointer<TValueType>(TValueType** address) : INullable
    where TValueType : unmanaged
{
    public TValueType** Value => address;
    public ref TValueType* Ref => ref *Value;
    public bool IsNull => Value is null;

    public Span<Pointer<TValueType>> AsSpan(ElementCount length) =>
        new((Pointer<TValueType>*)Value, length);

    public Pointer<TValueType> this[int index]
    {
        get => Value[index];
        set => Value[index] = value.Value;
    }

    public static implicit operator PointerPointer<TValueType>(TValueType** address) =>
        new(address);

    public static implicit operator TValueType**(PointerPointer<TValueType> pointerPointer) =>
        pointerPointer.Value;

    public static explicit operator PointerPointer<TValueType>(nint address) =>
        new((TValueType**)address);

    public static explicit operator nint(PointerPointer<TValueType> pointerPointer) =>
        (nint)pointerPointer.Value;
}
