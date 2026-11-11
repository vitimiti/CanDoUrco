// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using Microsoft.Extensions.Logging;

namespace CanDoUrco.Logging;

public static class CoreLog
{
    internal static ILoggerFactory? Factory;

    public static bool IsEnabled { get; set; }

    public static void Initialize(Action<ILoggingBuilder> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        IsEnabled = true;
        Factory = LoggerFactory.Create(builder);
        foreach (var (categoryName, category) in LogCategory.Categories)
        {
            category.Logger = Factory.CreateLogger(categoryName);
            if (
                (category.Logger?.IsEnabled(LogLevel.Trace) ?? false)
                || (category.Logger?.IsEnabled(LogLevel.Debug) ?? false)
            )
            {
                category.AppendCallerInfo = true;
            }
        }
    }
}
