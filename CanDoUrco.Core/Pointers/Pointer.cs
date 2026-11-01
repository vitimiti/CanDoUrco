// Pointer.cs - Defines pointer-related functionality for CanDoUrco.Pointers
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

public readonly unsafe struct Pointer(void* ptr) : INullable, IEquatable<Pointer>
{
    public void* Value => ptr;
    public bool IsNull => Value is null;

    public Pointer Offset(ByteSize byteOffset) => new((byte*)Value + byteOffset.Bytes);

    public Span<TValueType> AsSpan<TValueType>(ElementCount length) => new(Value, length);

    public Pointer<TCastType> Cast<TCastType>()
        where TCastType : unmanaged => new((TCastType*)Value);

    public bool Equals(Pointer other) => Value == other.Value;

    public override bool Equals(object? obj) => obj is Pointer other && Equals(other);

    public override int GetHashCode() => ((nint)Value).GetHashCode();

    public static bool operator ==(Pointer left, Pointer right) => left.Equals(right);

    public static bool operator !=(Pointer left, Pointer right) => !left.Equals(right);

    public static implicit operator Pointer(void* pointer) => new(pointer);

    public static implicit operator void*(Pointer ptr) => ptr.Value;

    public static explicit operator Pointer(nint ptr) => new((void*)ptr);

    public static explicit operator nint(Pointer ptr) => (nint)ptr.Value;
}
