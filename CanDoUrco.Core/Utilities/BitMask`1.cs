// BitMask`1.cs - Defines a generic bitmask utility for the CanDoUrco framework.
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

using System.Collections.Concurrent;
using System.Numerics;
using System.Runtime.CompilerServices;
using CanDoUrco.Core.Buffers;
using CanDoUrco.Core.Extensions;

namespace CanDoUrco.Core.Utilities;

public struct BitMask<T>
    : IEquatable<BitMask<T>>,
        IBitwiseOperators<BitMask<T>, BitMask<T>, BitMask<T>>
    where T : unmanaged, IBinaryInteger<T>, IUnsignedNumber<T>
{
    private static readonly ConcurrentDictionary<T, string> ToStringCache = [];

    public static int BitCount => Unsafe.SizeOf<T>() * 8;

    public static BitMask<T> Empty => default;

    public static BitMask<T> Full => new(T.AllBitsSet);

    public static string[] BitNames => ComputeBitNames();

    public T Value { get; private set; }

    public readonly bool IsEmpty => Value == default;

    public readonly bool IsFull => Value == T.AllBitsSet;

    public readonly bool IsPow2 => T.IsPow2(Value);

    public readonly BitMask<T> Inversed => new(~Value);

    private BitMask(T mask) => Value = mask;

    public BitMask(byte bitIndex) => Value = T.One << bitIndex;

    public BitMask(Range bitRange) => this[bitRange] = true;

    public BitMask(params ReadOnlySpan<byte> bitIndices)
    {
        foreach (var bitIndex in bitIndices)
        {
            Value |= T.One << bitIndex;
        }
    }

    public static string[] ComputeBitNames()
    {
        var names = new string[BitCount];
        for (var i = 0; i < BitCount; i++)
        {
            names[i] = i.ToString();
        }
        return names;
    }

    public static BitMask<T> FromMask(T mask) => new(mask);

    public static BitMask<T> FromBit(byte bitIndex) => new(bitIndex);

    public readonly bool Contains(byte bitIndex) => ContainsAny(FromBit(bitIndex));

    public readonly bool ContainsAll(BitMask<T> other) => (Value & other.Value) == other.Value;

    public readonly bool ContainsAny(BitMask<T> other) => (Value & other.Value) != T.Zero;

    public BitMask<T> Add(byte bitIndex) => Add(FromBit(bitIndex));

    public BitMask<T> Add(BitMask<T> other)
    {
        Value |= other.Value;
        return this;
    }

    public BitMask<T> Remove(byte bitIndex) => Remove(FromBit(bitIndex));

    public BitMask<T> Remove(BitMask<T> other)
    {
        Value &= ~other.Value;
        return this;
    }

    public BitMask<T> Set(byte bitIndex, bool enabled) => Set(FromBit(bitIndex), enabled);

    public BitMask<T> Set(BitMask<T> other, bool enabled) => enabled ? Add(other) : Remove(other);

    public readonly bool TryGetBitIndex(out byte index) =>
        BitOperations.TryGetBitIndex(Value, out index);

    public override readonly string ToString()
    {
        if (ToStringCache.TryGetValue(Value, out var str))
        {
            return str;
        }

        if (IsEmpty)
        {
            return "(empty)";
        }
        if (IsFull)
        {
            return "(full)";
        }

        using var tmpBitNames = SpanOwner<string>.Allocate(BitCount);
        var tmpBitNameCount = 0;
        for (byte i = 0; i < BitCount; i++)
        {
            if (Contains(i))
            {
                tmpBitNames.Span[tmpBitNameCount++] = BitNames[i];
            }
        }

        ToStringCache[Value] = str = $"({string.Join(", ", tmpBitNames.Span[..tmpBitNameCount])})";
        return str;
    }

    public readonly bool Equals(BitMask<T> other) => Value.Equals(other.Value);

    public override readonly bool Equals(object? obj) => obj is BitMask<T> other && Equals(other);

    public override readonly int GetHashCode() => Value.GetHashCode();

    public bool this[byte bitIndex]
    {
        readonly get => Contains(bitIndex);
        set => Set(bitIndex, value);
    }

    public bool this[Index bitIndex]
    {
        readonly get => Contains((byte)bitIndex.GetOffset(BitCount));
        set => Set((byte)bitIndex.GetOffset(BitCount), value);
    }

    public bool this[Range bitRange]
    {
        readonly get => ContainsAll(new BitMask<T>(bitRange));
        set
        {
            var (offset, length) = bitRange.GetOffsetAndLength(BitCount);
            var start = (byte)offset;
            var end = offset + (byte)length;
            for (var i = start; i < end; i++)
            {
                this[i] = value;
            }
        }
    }

    public static BitMask<T> operator |(BitMask<T> a, BitMask<T> b) => FromMask(a.Value | b.Value);

    public static BitMask<T> operator &(BitMask<T> a, BitMask<T> b) => FromMask(a.Value & b.Value);

    public static BitMask<T> operator ^(BitMask<T> a, BitMask<T> b) => FromMask(a.Value ^ b.Value);

    public static BitMask<T> operator ~(BitMask<T> a) => FromMask(~a.Value);

    public static bool operator ==(BitMask<T> left, BitMask<T> right) => left.Equals(right);

    public static bool operator !=(BitMask<T> left, BitMask<T> right) => !left.Equals(right);
}
