// CanDoUrco.Types.ByteSize64 - Represents a size in bytes for the CanDoUrco framework.
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

public readonly record struct ByteSize64(ulong Value)
    : IByteSize<ByteSize64, ulong>,
        IAssociativeArithmeticOperators<ByteSize64, ulong, ByteSize64>,
        IAssociativeArithmeticOperators<ByteSize64, int, ByteSize64>
{
    public static implicit operator ByteSize(ByteSize64 value) => new(value.Bytes);

    public static implicit operator ByteSize64(ByteSize value) => new(value.Value);

    public static implicit operator int(ByteSize64 value) => (int)value.Value;

    public static explicit operator ByteSize64(int value) => new((ulong)value);

    public static explicit operator ByteSize64(ulong value) => new(value);

    public static unsafe byte* operator +(byte* left, ByteSize64 right) => left + right.Value;

    public static ByteSize64 operator +(ByteSize64 left, ByteSize64 right) => left.Value + right;

    public static ByteSize64 operator -(ByteSize64 left, ByteSize64 right) => left.Value - right;

    public static ByteSize64 operator *(ByteSize64 left, ByteSize64 right) => left.Value * right;

    public static ByteSize64 operator /(ByteSize64 left, ByteSize64 right) => left.Value / right;

    public static ByteSize64 operator +(ByteSize64 left, ulong right) =>
        (ByteSize64)(left.Value + right);

    public static ByteSize64 operator -(ByteSize64 left, ulong right) =>
        (ByteSize64)(left.Value - right);

    public static ByteSize64 operator *(ByteSize64 left, ulong right) =>
        (ByteSize64)(left.Value * right);

    public static ByteSize64 operator /(ByteSize64 left, ulong right) =>
        (ByteSize64)(left.Value / right);

    public static ByteSize64 operator +(ulong left, ByteSize64 right) =>
        (ByteSize64)(left + right.Value);

    public static ByteSize64 operator -(ulong left, ByteSize64 right) =>
        (ByteSize64)(left - right.Value);

    public static ByteSize64 operator *(ulong left, ByteSize64 right) =>
        (ByteSize64)(left * right.Value);

    public static ByteSize64 operator /(ulong left, ByteSize64 right) =>
        (ByteSize64)(left / right.Value);

    public static ByteSize64 operator *(ByteSize64 left, ElementCount right) => left * (ulong)right;

    public static ByteSize64 operator *(ElementCount left, ByteSize64 right) => (ulong)left * right;

    public static ByteSize64 operator +(ByteSize64 left, int right) => left + (ulong)right;

    public static ByteSize64 operator -(ByteSize64 left, int right) => left - (ulong)right;

    public static ByteSize64 operator *(ByteSize64 left, int right) => left * (ulong)right;

    public static ByteSize64 operator /(ByteSize64 left, int right) => left / (ulong)right;

    public static ByteSize64 operator +(int left, ByteSize64 right) => (ulong)left + right;

    public static ByteSize64 operator -(int left, ByteSize64 right) => (ulong)left - right;

    public static ByteSize64 operator *(int left, ByteSize64 right) => (ulong)left * right;

    public static ByteSize64 operator /(int left, ByteSize64 right) => (ulong)left / right;

    public override string ToString() => ((ByteSize)this).ToString();

    public nuint Bytes => (nuint)Value;
    public nuint Kilobytes => Bytes / ByteSize.KilobytesToBytes;
    public nuint Megabytes => Bytes / ByteSize.MegabytesToBytes;
    public nuint Gigabytes => Bytes / ByteSize.GigabytesToBytes;
}
