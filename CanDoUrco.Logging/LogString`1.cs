// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

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

    internal ReadOnlySpan<char> ReadOnlySpan { get; }

    private LogString(string message) => _message = message;

    private LogString(ReadOnlySpan<char> message) => ReadOnlySpan = message;

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

    public void AppendLiteral(string value) => _sb?.Append(value);

    public void AppendFormatted<T>(T value) => _sb?.Append(value);

    public void AppendFormatted(IFormatProvider? provider, string format, params object?[] args) =>
        _sb?.AppendFormat(provider, format, args);

    public static implicit operator LogString<TLevel>(string message) => new(message);

    public static implicit operator LogString<TLevel>(ReadOnlySpan<char> message) => new(message);
}
