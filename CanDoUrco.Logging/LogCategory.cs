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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using CanDoUrco.Logging.Exceptions;
using CanDoUrco.Logging.LogLevels;
using Microsoft.Extensions.Logging;

namespace CanDoUrco.Logging;

public sealed class LogCategory
{
    internal static readonly Dictionary<string, LogCategory> Categories = [];

    private bool LoggingEnabled => CoreLog.IsEnabled && IsCategoryEnabled;

    public string Name { get; }

    public bool IsCategoryEnabled = true;

    public bool AppendCallerInfo = false;

    public ILogger? Logger { get; internal set; }

    private LogCategory(string name) => Name = name;

    private void Log<TLevel>(
        LogLevel logLevel,
        [InterpolatedStringHandlerArgument("")] LogString<TLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0,
        bool? appendCallerInfo = null
    )
        where TLevel : unmanaged, ILogLevel
    {
        var messageString = message.ToStringAndClear();
        if (!LoggingEnabled)
        {
            return;
        }

        if (Logger is null)
        {
            throw new LogNotInitializedException();
        }

        if (!Logger.IsEnabled(logLevel))
        {
            return;
        }

        messageString ??= message.ReadOnlySpan.ToString();
        if (appendCallerInfo ?? AppendCallerInfo)
        {
            Logger.Log(
                logLevel,
                "{Message} [{SourceMemberName} (in {SourceFilePath}:{SourceLineNumber})]",
                messageString,
                sourceMemberName,
                sourceFilePath,
                sourceLineNumber
            );
        }
        else
        {
            Logger.Log(logLevel, "{Message}", messageString);
        }
    }

    public static LogCategory Get(string categoryName) => Categories[categoryName];

    public static bool TryGet(
        string categoryName,
        [MaybeNullWhen(false)] out LogCategory? category
    ) => Categories.TryGetValue(categoryName, out category);

    public static LogCategory MakeOrGet(string categoryName)
    {
        if (Categories.TryGetValue(categoryName, out var category))
        {
            return category;
        }

        category = new LogCategory(categoryName)
        {
            Logger = CoreLog.Factory?.CreateLogger(categoryName),
        };

        Categories[categoryName] = category;
        return category;
    }

    public void Log(
        LogLevel logLevel,
        [InterpolatedStringHandlerArgument("", nameof(logLevel))]
            LogString<DynamicLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        Log<DynamicLogLevel>(logLevel, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Trace(
        [InterpolatedStringHandlerArgument("")] LogString<TraceLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Trace, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Debug(
        [InterpolatedStringHandlerArgument("")] LogString<DebugLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Debug, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Info(
        [InterpolatedStringHandlerArgument("")] LogString<InformationLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Information, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Warn(
        [InterpolatedStringHandlerArgument("")] LogString<WarningLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Warning, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Error(
        [InterpolatedStringHandlerArgument("")] LogString<ErrorLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Error, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Critical(
        [InterpolatedStringHandlerArgument("")] LogString<CriticalLogLevel> message = default,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) => Log(LogLevel.Critical, message, sourceMemberName, sourceFilePath, sourceLineNumber);

    public void Error(
        Exception exception,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        Log(
            LogLevel.Error,
            exception.ToString(),
            sourceMemberName,
            sourceFilePath,
            sourceLineNumber
        );

    public void Critical(
        Exception exception,
        [CallerMemberName] string sourceMemberName = "",
        [CallerFilePath] string sourceFilePath = "",
        [CallerLineNumber] int sourceLineNumber = 0
    ) =>
        Log(
            LogLevel.Critical,
            exception.ToString(),
            sourceMemberName,
            sourceFilePath,
            sourceLineNumber
        );

    public bool IsEnabled(LogLevel level) =>
        CoreLog.IsEnabled && IsCategoryEnabled && Logger?.IsEnabled(level) == true;
}
