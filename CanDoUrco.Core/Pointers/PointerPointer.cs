// PointerPointer.cs - Defines pointer-to-pointer related functionality for CanDoUrco.Pointers
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

public readonly unsafe struct PointerPointer(void** pointerPointer) : INullable
{
    public void** Value => pointerPointer;
    public ref void* Ref => ref *Value;
    public bool IsNull => Value is null;

    public Span<Pointer> AsSpan(ElementCount length) => new((Pointer*)Value, length);

    public Span<Pointer<T>> AsSpan<T>(ElementCount length)
        where T : unmanaged => new((Pointer<T>*)Value, length);

    public Pointer this[int index]
    {
        get => Value[index];
        set => Value[index] = value.Value;
    }

    public static implicit operator PointerPointer(void** address) => new(address);

    public static implicit operator void**(PointerPointer pointerPointer) => pointerPointer.Value;

    public static explicit operator PointerPointer(nint address) => new((void**)address);

    public static explicit operator nint(PointerPointer pointerPointer) =>
        (nint)pointerPointer.Value;
}
