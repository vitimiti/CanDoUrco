// CanDoUrco.Types.ByteSize32 - Represents a 32-bit size in bytes for the CanDoUrco framework.
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

using CanDoUrco.Core.Numerics;

namespace CanDoUrco.Core.Types;

public readonly record struct ByteSize32(uint Value)
    : IByteSize<ByteSize32, uint>,
        IAssociativeArithmeticOperators<ByteSize32, uint, ByteSize32>,
        IAssociativeArithmeticOperators<ByteSize32, int, ByteSize32>
{
    public static implicit operator ByteSize(ByteSize32 value) => new(value.Value);

    public static implicit operator ByteSize32(ByteSize value) => new(checked((uint)value.Value));

    public static implicit operator int(ByteSize32 value) => (int)value.Value;

    public static explicit operator ByteSize32(int value) => new(checked((uint)value));

    public static explicit operator ByteSize32(uint value) => new(value);

    public static unsafe byte* operator +(byte* left, ByteSize32 right) => left + right.Value;

    public static ByteSize32 operator +(ByteSize32 left, ByteSize32 right) => left.Value + right;

    public static ByteSize32 operator -(ByteSize32 left, ByteSize32 right) => left.Value - right;

    public static ByteSize32 operator *(ByteSize32 left, ByteSize32 right) => left.Value * right;

    public static ByteSize32 operator /(ByteSize32 left, ByteSize32 right) => left.Value / right;

    public static ByteSize32 operator +(ByteSize32 left, uint right) =>
        (ByteSize32)(left.Value + right);

    public static ByteSize32 operator -(ByteSize32 left, uint right) =>
        (ByteSize32)(left.Value - right);

    public static ByteSize32 operator *(ByteSize32 left, uint right) =>
        (ByteSize32)(left.Value * right);

    public static ByteSize32 operator /(ByteSize32 left, uint right) =>
        (ByteSize32)(left.Value / right);

    public static ByteSize32 operator +(uint left, ByteSize32 right) =>
        (ByteSize32)(left + right.Value);

    public static ByteSize32 operator -(uint left, ByteSize32 right) =>
        (ByteSize32)(left - right.Value);

    public static ByteSize32 operator *(uint left, ByteSize32 right) =>
        (ByteSize32)(left * right.Value);

    public static ByteSize32 operator /(uint left, ByteSize32 right) =>
        (ByteSize32)(left / right.Value);

    public static ByteSize32 operator *(ByteSize32 left, ElementCount right) => left * (uint)right;

    public static ByteSize32 operator *(ElementCount left, ByteSize32 right) => (uint)left * right;

    public static ByteSize32 operator +(ByteSize32 left, int right) => left + (uint)right;

    public static ByteSize32 operator -(ByteSize32 left, int right) => left - (uint)right;

    public static ByteSize32 operator *(ByteSize32 left, int right) => left * (uint)right;

    public static ByteSize32 operator /(ByteSize32 left, int right) => left / (uint)right;

    public static ByteSize32 operator +(int left, ByteSize32 right) => (uint)left + right;

    public static ByteSize32 operator -(int left, ByteSize32 right) => (uint)left - right;

    public static ByteSize32 operator *(int left, ByteSize32 right) => (uint)left * right;

    public static ByteSize32 operator /(int left, ByteSize32 right) => (uint)left / right;

    public override string ToString() => ((ByteSize)this).ToString();

    public nuint Bytes => Value;
    public nuint Kilobytes => Bytes / ByteSize.KilobytesToBytes;
    public nuint Megabytes => Bytes / ByteSize.MegabytesToBytes;
    public nuint Gigabytes => Bytes / ByteSize.GigabytesToBytes;
}
