// CanDoUrco.Types.ByteSize - Represents a size in bytes for the CanDoUrco framework.
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
using CanDoUrco.Core.Numerics;

namespace CanDoUrco.Core.Types;

public readonly record struct ByteSize(nuint Value)
    : IByteSize,
        IAssociativeArithmeticOperators<ByteSize, nuint, ByteSize>,
        IAssociativeArithmeticOperators<ByteSize, int, ByteSize>
{
    public const nuint KilobytesToBytes = 1024;
    public const nuint MegabytesToBytes = 1024 * KilobytesToBytes;
    public const nuint GigabytesToBytes = 1024 * MegabytesToBytes;

    private static readonly string[] Suffixes = ["B", "KB", "MB", "GB", "TB", "PB", "EB"];

    public static ByteSize Zero => new(0);
    public static ByteSize One => new(1);

    public nuint Bytes => Value;
    public nuint Kilobytes => Bytes / KilobytesToBytes;
    public nuint Megabytes => Bytes / MegabytesToBytes;
    public nuint Gigabytes => Bytes / GigabytesToBytes;

    public static ByteSize Of(byte value) => (ByteSize)(int)value;

    public static ByteSize Of(int value) => (ByteSize)value;

    public static ByteSize Of(uint value) => (ByteSize)value;

    public static ByteSize Of(long value) => (ByteSize)value;

    public static ByteSize Of(ulong value) => (ByteSize)value;

    public static ByteSize Of<T>()
        where T : unmanaged => Of<T>(ElementCount.One);

    public static ByteSize Of<T>(ElementCount count)
        where T : unmanaged => Of(Unsafe.SizeOf<T>() * count.Value);

    public static ByteSize Of<T>(T[] array)
        where T : unmanaged => Of<T>(array.Length);

    public static ByteSize Of<T>(Span<T> span)
        where T : unmanaged => Of<T>(span.Length);

    public static ByteSize Of<T>(ReadOnlySpan<T> span)
        where T : unmanaged => Of<T>(span.Length);

    public static ByteSize Of<T>(List<T> list)
        where T : unmanaged => Of<T>(list.Count);

    public static ByteSize Of<T>(T span)
        where T : ISpanLike, allows ref struct => span.Length * span.Stride;

    public static ByteSize Of<T1, T2>(T1 span)
        where T1 : ISpanLike<T2>
        where T2 : unmanaged => Of<T2>(span.Length);

    public static ByteSize OffsetOf<T>(string fieldName)
        where T : struct => (ByteSize)Marshal.OffsetOf<T>(fieldName);

    public static ByteSize AlignmentOf<T>()
        where T : unmanaged
    {
        var probe = default(AlignmentProbe<T>);
        return (ByteSize)
            Unsafe.ByteOffset(ref probe.Padding, ref Unsafe.As<T, byte>(ref probe.Value));
    }

    public static ByteSize Max(ByteSize a, ByteSize b) => (ByteSize)nuint.Max(a.Bytes, b.Bytes);

    public static ByteSize Min(ByteSize a, ByteSize b) => (ByteSize)nuint.Min(a.Bytes, b.Bytes);

    public static ByteSize FromKilobytes(byte value) => (ByteSize)(value * KilobytesToBytes);

    public static ByteSize FromKilobytes(int value) => (ByteSize)((nuint)value * KilobytesToBytes);

    public static ByteSize FromKilobytes(uint value) => (ByteSize)(value * KilobytesToBytes);

    public static ByteSize FromKilobytes(long value) => (ByteSize)((nuint)value * KilobytesToBytes);

    public static ByteSize FromKilobytes(ulong value) => (ByteSize)(value * KilobytesToBytes);

    public static ByteSize FromMegabytes(byte value) => (ByteSize)(value * MegabytesToBytes);

    public static ByteSize FromMegabytes(int value) => (ByteSize)((nuint)value * MegabytesToBytes);

    public static ByteSize FromMegabytes(uint value) => (ByteSize)(value * MegabytesToBytes);

    public static ByteSize FromMegabytes(long value) => (ByteSize)((nuint)value * MegabytesToBytes);

    public static ByteSize FromMegabytes(ulong value) => (ByteSize)(value * MegabytesToBytes);

    public static ByteSize FromGigabytes(byte value) => (ByteSize)(value * GigabytesToBytes);

    public static ByteSize FromGigabytes(int value) => (ByteSize)((nuint)value * GigabytesToBytes);

    public static ByteSize FromGigabytes(uint value) => (ByteSize)(value * GigabytesToBytes);

    public static ByteSize FromGigabytes(long value) => (ByteSize)((nuint)value * GigabytesToBytes);

    public static ByteSize FromGigabytes(ulong value) => (ByteSize)(value * GigabytesToBytes);

    public ByteSize AlignTo<T>(T alignment)
        where T : IByteSize
    {
        var a = alignment.Bytes;
        return (ByteSize)((Bytes + a - 1) & ~(a - 1));
    }

    public ElementCount ToElementCount(ByteSize stride) => new((int)(Bytes / stride.Bytes));

    public ElementCount ToElementCount<T>()
        where T : unmanaged => ToElementCount(Of(Unsafe.SizeOf<T>()));

    public override string ToString()
    {
        if (Bytes == 0)
        {
            return "0B";
        }

        var order = (int)float.Floor(float.Log(Bytes, 1024));
        order = int.Min(order, Suffixes.Length - 1);
        var value = Bytes / float.Pow(1024, order);
        return $"{value:0.##}{Suffixes[order]}";
    }

    public static explicit operator ByteSize(int bytes) => new((nuint)bytes);

    public static explicit operator ByteSize(uint bytes) => new(bytes);

    public static explicit operator ByteSize(ulong bytes) => new((nuint)bytes);

    public static explicit operator ByteSize(long bytes) => new((nuint)bytes);

    public static explicit operator ByteSize(nuint bytes) => new(bytes);

    public static explicit operator ByteSize(nint bytes) => new((nuint)bytes);

    public static explicit operator ByteSize(double bytes) => new((nuint)bytes);

    public static explicit operator ByteSize(float bytes) => new((nuint)bytes);

    public static explicit operator int(ByteSize byteSize) => (int)byteSize.Bytes;

    public static explicit operator uint(ByteSize byteSize) => (uint)byteSize.Bytes;

    public static explicit operator long(ByteSize byteSize) => (long)byteSize.Bytes;

    public static implicit operator ulong(ByteSize byteSize) => byteSize.Bytes;

    public static explicit operator nint(ByteSize byteSize) => (nint)byteSize.Bytes;

    public static explicit operator nuint(ByteSize byteSize) => byteSize.Bytes;

    public static explicit operator float(ByteSize byteSize) => byteSize.Bytes;

    public static explicit operator double(ByteSize byteSize) => byteSize.Bytes;

    public static unsafe byte* operator +(byte* left, ByteSize right) => left + right.Bytes;

    public static ByteSize operator +(ByteSize left, ByteSize right) => left.Bytes + right;

    public static ByteSize operator -(ByteSize left, ByteSize right) => left.Bytes - right;

    public static ByteSize operator *(ByteSize left, ByteSize right) => left.Bytes * right;

    public static ByteSize operator /(ByteSize left, ByteSize right) => left.Bytes / right;

    public static ByteSize operator +(ByteSize left, nuint right) => (ByteSize)(left.Bytes + right);

    public static ByteSize operator -(ByteSize left, nuint right) => (ByteSize)(left.Bytes - right);

    public static ByteSize operator *(ByteSize left, nuint right) => (ByteSize)(left.Bytes * right);

    public static ByteSize operator /(ByteSize left, nuint right) => (ByteSize)(left.Bytes / right);

    public static ByteSize operator +(nuint left, ByteSize right) => (ByteSize)(left + right.Bytes);

    public static ByteSize operator -(nuint left, ByteSize right) => (ByteSize)(left - right.Bytes);

    public static ByteSize operator *(nuint left, ByteSize right) => (ByteSize)(left * right.Bytes);

    public static ByteSize operator /(nuint left, ByteSize right) => (ByteSize)(left / right.Bytes);

    public static ByteSize operator *(ByteSize left, ElementCount right) => left * (nuint)right;

    public static ByteSize operator *(ElementCount left, ByteSize right) => (nuint)left * right;

    public static ByteSize operator +(ByteSize left, int right) => left + (nuint)right;

    public static ByteSize operator -(ByteSize left, int right) => left - (nuint)right;

    public static ByteSize operator *(ByteSize left, int right) => left * (nuint)right;

    public static ByteSize operator /(ByteSize left, int right) => left / (nuint)right;

    public static ByteSize operator +(int left, ByteSize right) => (nuint)left + right;

    public static ByteSize operator -(int left, ByteSize right) => (nuint)left - right;

    public static ByteSize operator *(int left, ByteSize right) => (nuint)left * right;

    public static ByteSize operator /(int left, ByteSize right) => (nuint)left / right;

    private struct AlignmentProbe<T>
        where T : unmanaged
    {
        public byte Padding;
        public T Value;
    }
}
