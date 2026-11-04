// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using Microsoft.Extensions.Logging;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    private const string DllName = "SDL3";

    internal const string CustomLogCategory = "CanDoUrco.Sdl";

    static unsafe Sdl()
    {
        NativeLibrary.SetDllImportResolver(
            typeof(Sdl).Assembly,
            (name, assembly, path) =>
                NativeLibrary.Load(
                    name switch
                    {
                        DllName when OperatingSystem.IsWindows() => "SDL3.dll",
                        DllName when OperatingSystem.IsLinux() => "libSDL3.so.0",
                        DllName when OperatingSystem.IsMacOS() => "libSDL3.0.dylib",
                        _ => name,
                    },
                    assembly,
                    path
                )
        );

        // Use this category to get the logger since we know it will always exist and is specific to our SDL bindings.
        var logger = LogCategory.MakeOrGet(CustomLogCategory).Logger;
        LogLevel logLevel =
            logger?.IsEnabled(LogLevel.Trace) ?? false ? LogLevel.Trace
            : logger?.IsEnabled(LogLevel.Debug) ?? false ? LogLevel.Debug
            : logger?.IsEnabled(LogLevel.Information) ?? false ? LogLevel.Information
            : logger?.IsEnabled(LogLevel.Warning) ?? false ? LogLevel.Warning
            : logger?.IsEnabled(LogLevel.Error) ?? false ? LogLevel.Error
            : logger?.IsEnabled(LogLevel.Critical) ?? false ? LogLevel.Critical
            : LogLevel.None;

        _SetLogPriorities(
            logLevel switch
            {
                LogLevel.Trace => PrivateLogPriority.Trace,
                LogLevel.Debug => PrivateLogPriority.Debug,
                LogLevel.Information => PrivateLogPriority.Info,
                LogLevel.Warning => PrivateLogPriority.Warn,
                LogLevel.Error => PrivateLogPriority.Error,
                LogLevel.Critical => PrivateLogPriority.Critical,
                _ => PrivateLogPriority.Invalid,
            }
        );

        _SetLogOutputFunction(&LogOutputFunction, null);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void LogOutputFunction(
        void* userdata,
        PrivateLogCategory category,
        PrivateLogPriority priority,
        byte* message
    )
    {
        var categoryStr = $"{CustomLogCategory}.{category}";
        var actualLogPriority = priority switch
        {
            PrivateLogPriority.Trace => LogLevel.Trace,
            PrivateLogPriority.Verbose => LogLevel.Trace,
            PrivateLogPriority.Debug => LogLevel.Debug,
            PrivateLogPriority.Info => LogLevel.Information,
            PrivateLogPriority.Warn => LogLevel.Warning,
            PrivateLogPriority.Error => LogLevel.Error,
            PrivateLogPriority.Critical => LogLevel.Critical,
            _ => LogLevel.None,
        };

        var msg = Marshal.PtrToStringUTF8((nint)message) ?? string.Empty;
        switch (actualLogPriority)
        {
            case LogLevel.Trace:
                LogCategory.MakeOrGet(categoryStr).Trace(msg);
                break;
            case LogLevel.Debug:
                LogCategory.MakeOrGet(categoryStr).Debug(msg);
                break;
            case LogLevel.Information:
                LogCategory.MakeOrGet(categoryStr).Info(msg);
                break;
            case LogLevel.Warning:
                LogCategory.MakeOrGet(categoryStr).Warn(msg);
                break;
            case LogLevel.Error:
                LogCategory.MakeOrGet(categoryStr).Error(msg);
                break;
            case LogLevel.Critical:
                LogCategory.MakeOrGet(categoryStr).Critical(msg);
                break;
            case LogLevel.None:
                break;
        }
    }
}
