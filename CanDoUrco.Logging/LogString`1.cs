// CanDoUrco.Logging - Provides structured logging with interpolated string support.
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
using System.Text;
using CanDoUrco.Logging.LogLevels;
using Microsoft.Extensions.Logging;

namespace CanDoUrco.Logging;

[InterpolatedStringHandler]
public readonly ref struct LogString<TLevel>
    where TLevel : unmanaged, ILogLevel
{
    private readonly StringBuilder? _sb;
    private readonly string? _message;
    private readonly ReadOnlySpan<char> _span;

    internal ReadOnlySpan<char> ReadOnlySpan => _span;

    private LogString(string message) => _message = message;

    private LogString(ReadOnlySpan<char> message) => _span = message;

    public LogString(
        int literalLength,
        int formattedCount,
        LogCategory category,
        LogLevel logLevel,
        out bool shouldAppend
    )
    {
        shouldAppend = category.IsEnabled(logLevel);
        _sb = shouldAppend ? new StringBuilder(literalLength) : null;
    }

    public LogString(
        int literalLength,
        int formattedCount,
        LogCategory category,
        out bool shouldAppend
    )
        : this(literalLength, formattedCount, category, default(TLevel).Level, out shouldAppend) { }

    public void AppendLiteral(string value) => _sb?.Append(value);

    public void AppendFormatted(IFormatProvider? provider, string format, params object?[] args) =>
        _sb?.AppendFormat(provider, format, args);

    internal string? ToStringAndClear()
    {
        if (_message is not null)
        {
            return _message;
        }

        if (_sb is null)
        {
            return null;
        }

        var result = _sb.ToString();
        _sb.Clear();
        return result;
    }

    public static implicit operator LogString<TLevel>(string message) => new(message);

    public static implicit operator LogString<TLevel>(ReadOnlySpan<char> message) => new(message);
}
