// Shared utilities for CanDoUrco.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
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

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace CanDoUrco.Utilities;

/// <summary>
/// Reports unhandled exceptions to the user with a dialog, so applications without a terminal don't crash silently.
/// </summary>
/// <remarks>
/// Only managed exceptions can be reported. Native crashes (for example an access violation inside a driver) cannot be caught.
/// </remarks>
public static partial class CrashReporter
{
    private const uint MbOk = 0x0;
    private const uint MbIconError = 0x10;
    private const uint MbSetForeground = 0x10000;

    private static int _reporting;

    /// <summary>
    /// Runs <paramref name="main"/> and reports any exception it throws.
    /// Also hooks the process-wide unhandled exception handlers as a backstop for other threads.
    /// </summary>
    /// <param name="main">The application entry point.</param>
    /// <param name="title">The title of the crash dialog.</param>
    /// <returns>The result of <paramref name="main"/>, or <c>1</c> if it crashed.</returns>
    public static int Run(Func<int> main, string title = "Application crash")
    {
        ArgumentNullException.ThrowIfNull(main);
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
            Report(
                e.ExceptionObject as Exception
                    ?? new InvalidOperationException(e.ExceptionObject?.ToString()),
                title
            );

        TaskScheduler.UnobservedTaskException += (_, e) => Report(e.Exception, title);
        try
        {
            return main();
        }
        catch (Exception ex)
        {
            Report(ex, title);
            return 1;
        }
    }

    /// <summary>
    /// Runs <paramref name="main"/> and reports any exception it throws.
    /// </summary>
    /// <param name="main">The application entry point.</param>
    /// <param name="title">The title of the crash dialog.</param>
    /// <returns><c>0</c> on success, or <c>1</c> if it crashed.</returns>
    public static int Run(Action main, string title = "Application crash")
    {
        ArgumentNullException.ThrowIfNull(main);
        return Run(
            () =>
            {
                main();
                return 0;
            },
            title
        );
    }

    /// <summary>
    /// Shows the exception to the user. This never throws.
    /// </summary>
    /// <param name="exception">The exception to report.</param>
    /// <param name="title">The title of the crash dialog.</param>
    public static void Report(Exception exception, string title = "Application crash")
    {
        // Avoid showing several dialogs when many threads fail at once.
        if (Interlocked.Exchange(ref _reporting, 1) != 0)
        {
            return;
        }

        var text = $"The application has crashed.\n\n{exception.Message}\n\nDetails:\n{exception}";
        try
        {
            if (!TryShowDialog(title, text))
            {
                Console.Error.WriteLine($"{title}: {text}");
            }
        }
        catch
        {
            // Nothing else can be done while crashing.
        }
    }

    private static bool TryShowDialog(string title, string text)
    {
        if (OperatingSystem.IsWindows())
        {
            MessageBox(0, Truncate(text, 3000), title, MbOk | MbIconError | MbSetForeground);
            return true;
        }

        if (OperatingSystem.IsMacOS())
        {
            var script =
                $"display alert \"{Escape(title)}\" message \"{Escape(Truncate(text, 1500))}\" as critical";

            return TryRun("osascript", ["-e", script]);
        }

        if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            var hasDisplay =
                !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("DISPLAY"))
                || !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WAYLAND_DISPLAY"));

            if (!hasDisplay)
            {
                return false;
            }

            var shown = Truncate(text, 3000);
            return PrefersKde()
                ? TryKdialog(title, shown) || TryZenity(title, shown) || TryXmessage(title, shown)
                : TryZenity(title, shown) || TryKdialog(title, shown) || TryXmessage(title, shown);
        }

        return false;
    }

    private static bool PrefersKde()
    {
        var desktop = Environment.GetEnvironmentVariable("XDG_CURRENT_DESKTOP") ?? "";
        return desktop.Contains("KDE", StringComparison.OrdinalIgnoreCase)
            || Environment.GetEnvironmentVariable("KDE_FULL_SESSION") == "true";
    }

    private static bool TryZenity(string title, string text) =>
        TryRun("zenity", ["--error", "--no-markup", "--title", title, "--text", text]);

    private static bool TryKdialog(string title, string text) =>
        TryRun("kdialog", ["--title", title, "--error", text]);

    private static bool TryXmessage(string title, string text) =>
        TryRun("xmessage", ["-center", title + "\n\n" + text]);

    private static bool TryRun(string fileName, string[] arguments)
    {
        try
        {
            var info = new ProcessStartInfo(fileName)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
            };

            foreach (var argument in arguments)
            {
                info.ArgumentList.Add(argument);
            }

            using var process = Process.Start(info);
            if (process is null)
            {
                return false;
            }

            process.WaitForExit();
            return true;
        }
        catch
        {
            // The tool isn't installed.
            return false;
        }
    }

    private static string Escape(string text) =>
        text.Replace("\\", "\\\\", StringComparison.Ordinal)
            .Replace("\"", "\\\"", StringComparison.Ordinal);

    private static string Truncate(string text, int length) =>
        text.Length <= length ? text : text[..length] + "…";

    [LibraryImport(
        "user32.dll",
        EntryPoint = "MessageBoxW",
        StringMarshalling = StringMarshalling.Utf16
    )]
    [SupportedOSPlatform("windows")]
    private static partial int MessageBox(nint hWnd, string text, string caption, uint type);
}
