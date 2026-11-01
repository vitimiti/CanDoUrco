// Bool8.cs - Defines an 8-bit boolean type for the CanDoUrco framework.
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

namespace CanDoUrco.Core.Types;

public readonly struct Bool8(bool value) : IUtf8SpanFormattable, ISpanFormattable
{
    private readonly byte _value = value ? (byte)1 : (byte)0;

    private bool AsBool => _value != 0;

    public override string ToString() => AsBool.ToString();

    public string ToString(string? format, IFormatProvider? formatProvider) =>
        AsBool.ToString(formatProvider);

    public bool TryFormat(
        Span<char> destination,
        out int charsWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null
    ) => AsBool.TryFormat(destination, out charsWritten, format);

    public bool TryFormat(
        Span<byte> destination,
        out int bytesWritten,
        ReadOnlySpan<char> format = default,
        IFormatProvider? provider = null
    ) => AsBool.TryFormat(destination, out bytesWritten, format);

    public static implicit operator bool(Bool8 b) => b._value != 0;

    public static implicit operator Bool8(bool value) => new(value);
}
