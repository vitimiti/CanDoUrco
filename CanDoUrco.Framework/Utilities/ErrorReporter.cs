// A common framework for the projects and engines of Can Do Urco
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
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CanDoUrco.Framework.Native.SDL3;

namespace CanDoUrco.Framework.Utilities;

public static class ErrorReporter
{
    public static int RunWithErrorReporting(Action action, string? contactDetails = null)
    {
        try
        {
            action();
            return 0;
        }
        catch (Exception ex)
        {
            string? errorLogFilePath = null;
            Exception? logException = null;
            List<string> logDirectories;
            try
            {
                logDirectories = GetErrorLogDirectories();
            }
            catch (Exception reportingException)
            {
                logDirectories = [];
                logException = reportingException;
            }

            var usedFallback = false;
            foreach (var logDirectory in logDirectories)
            {
                var candidatePath = Path.Combine(logDirectory, "CanDoUrco.Error.log");
                try
                {
                    Directory.CreateDirectory(logDirectory);
                    using (var errorLogFile = new StreamWriter(candidatePath, append: true))
                    {
                        var errorLogMessage = new StringBuilder();
                        errorLogMessage.AppendLine($"[{DateTimeOffset.UtcNow:O} UTC]");
                        errorLogMessage.AppendLine("----------------------------------------");
                        errorLogMessage.AppendLine($"A fatal error occurred: {ex.Message}.");
                        errorLogMessage.AppendLine();
                        errorLogMessage.AppendLine("Details:");
                        errorLogMessage.AppendLine(ex.ToString());
                        errorLogMessage.AppendLine("----------------------------------------");
                        errorLogMessage.AppendLine();
                        errorLogFile.WriteLine(errorLogMessage.ToString());
                    }

                    TrimErrorLog(candidatePath);
                    errorLogFilePath = candidatePath;
                    break;
                }
                catch (Exception reportingException)
                {
                    logException = reportingException;
                    usedFallback = true;
                }
            }

            var popupErrorMessage = new StringBuilder();
            popupErrorMessage.AppendLine($"A fatal error occurred: {ex.Message}.");
            popupErrorMessage.AppendLine();
            if (errorLogFilePath is not null)
            {
                if (usedFallback)
                {
                    popupErrorMessage.AppendLine(
                        "The preferred log location was unavailable; the report was saved to a fallback location."
                    );
                }

                popupErrorMessage.AppendLine(
                    $"A detailed error log has been saved to: {errorLogFilePath}"
                );
            }
            else
            {
                popupErrorMessage.AppendLine(
                    $"The error log could not be saved to: {errorLogFilePath}"
                );

                popupErrorMessage.AppendLine(
                    $"Reporting error: {logException?.Message ?? "No absolute log directory is available."}"
                );
            }

            if (errorLogFilePath is not null)
            {
                popupErrorMessage.AppendLine(
                    "The error details may contain sensitive information; review the log before submitting it."
                );

                popupErrorMessage.AppendLine(
                    string.IsNullOrWhiteSpace(contactDetails)
                        ? "Please send the error log to the developers."
                        : $"Please send the error log to the developers. Contact details: {contactDetails}"
                );
            }
            else
            {
                popupErrorMessage.AppendLine("Please report this error to the developers.");
                if (!string.IsNullOrWhiteSpace(contactDetails))
                {
                    popupErrorMessage.AppendLine($"Contact details: {contactDetails}");
                }
            }

            ShowErrorDialog(popupErrorMessage.ToString());
            return 1;
        }
    }

    private static unsafe void ShowErrorDialog(string message)
    {
        var titleMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        var messageMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            titleMarshaller.FromManaged(
                "CDU - Error",
                new byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
            );

            messageMarshaller.FromManaged(
                message,
                new byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
            );

            if (
                !Sdl.ShowSimpleMessageBox(
                    Sdl.MessageBoxError,
                    titleMarshaller.ToUnmanaged(),
                    messageMarshaller.ToUnmanaged(),
                    null
                )
            )
            {
                Console.Error.WriteLine($"Failed to display the fatal error dialog: {message}");
            }
        }
        catch (Exception reportingException)
        {
            Console.Error.WriteLine(
                $"Failed to display the fatal error dialog: {reportingException}"
            );
        }
        finally
        {
            titleMarshaller.Free();
            messageMarshaller.Free();
        }
    }

    private static List<string> GetErrorLogDirectories()
    {
        string? preferredDirectory = null;
        if (OperatingSystem.IsWindows())
        {
            preferredDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CanDoUrco",
                "Logs"
            );
        }
        else if (OperatingSystem.IsMacOS())
        {
            preferredDirectory = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                "Library",
                "Logs",
                "CanDoUrco"
            );
        }
        else if (OperatingSystem.IsLinux())
        {
            var xdgStateHome = Environment.GetEnvironmentVariable("XDG_STATE_HOME");
            if (string.IsNullOrWhiteSpace(xdgStateHome) || !Path.IsPathRooted(xdgStateHome))
            {
                xdgStateHome = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                    ".local",
                    "state"
                );
            }

            preferredDirectory = Path.Combine(xdgStateHome, "CanDoUrco", "Logs");
        }

        var directories = new List<string>();
        if (!string.IsNullOrWhiteSpace(preferredDirectory) && Path.IsPathRooted(preferredDirectory))
        {
            directories.Add(preferredDirectory);
        }

        var fallbackDirectory = Path.Combine(Path.GetTempPath(), "CanDoUrco", "Logs");
        if (
            Path.IsPathRooted(fallbackDirectory)
            && !directories.Contains(fallbackDirectory, StringComparer.Ordinal)
        )
        {
            directories.Add(fallbackDirectory);
        }

        return directories;
    }

    private static void TrimErrorLog(string errorLogFilePath)
    {
        var lines = new Queue<string>(10000);
        var exceededLineLimit = false;
        foreach (var line in File.ReadLines(errorLogFilePath))
        {
            lines.Enqueue(line);
            if (lines.Count > 10000)
            {
                lines.Dequeue();
                exceededLineLimit = true;
            }
        }

        if (exceededLineLimit)
        {
            File.WriteAllLines(errorLogFilePath, lines);
        }
    }
}
