// ElementCount.cs - Defines the ElementCount type for CanDoUrco.Types
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

using System.Diagnostics;

namespace CanDoUrco.Core.Types;

[DebuggerDisplay("{Value}")]
public readonly record struct ElementCount(int Value)
{
    public static ElementCount Zero => 0;

    public static ElementCount One => 1;

    public void Deconstruct(out int value) => value = Value;

    public static implicit operator ElementCount(int elements) => new(elements);

    public static explicit operator ElementCount(uint elements) => new((int)elements);

    public static explicit operator ElementCount(ulong elements) => new((int)elements);

    public static explicit operator ElementCount(long elements) => new((int)elements);

    public static explicit operator ElementCount(nuint elements) => new((int)elements);

    public static explicit operator ElementCount(nint elements) => new((int)elements);

    public static explicit operator ElementCount(double elements) => new((int)elements);

    public static explicit operator ElementCount(float elements) => new((int)elements);

    public static implicit operator int(ElementCount elementCount) => elementCount.Value;

    public static explicit operator uint(ElementCount elementCount) => (uint)elementCount.Value;

    public static implicit operator long(ElementCount elementCount) => elementCount.Value;

    public static explicit operator ulong(ElementCount elementCount) => (nuint)elementCount.Value;

    public static implicit operator nint(ElementCount elementCount) => elementCount.Value;

    public static explicit operator nuint(ElementCount elementCount) => (nuint)elementCount.Value;

    public static implicit operator float(ElementCount elementCount) => elementCount.Value;

    public static implicit operator double(ElementCount elementCount) => elementCount.Value;

    public static ElementCount operator +(ElementCount left, ElementCount right) =>
        left.Value + right.Value;

    public static ElementCount operator -(ElementCount left, ElementCount right) =>
        left.Value - right.Value;

    public static ElementCount operator *(ElementCount left, ElementCount right) =>
        left.Value * right.Value;

    public static ElementCount operator /(ElementCount left, ElementCount right) =>
        left.Value / right.Value;

    public static ElementCount Of(ElementCount count) => count;

    public static ElementCount Of(byte numElements) => numElements;

    public static ElementCount Of(int numElements) => numElements;

    public static ElementCount Of(uint numElements) => (int)numElements;

    public static ElementCount Of(long numElements) => (ElementCount)numElements;

    public static ElementCount Of(ulong numElements) => (int)numElements;
}
