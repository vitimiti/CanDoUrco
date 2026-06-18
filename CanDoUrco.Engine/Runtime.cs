// The MIT License (MIT)
//
// Copyright (C) 2026 Victor Matia (vitimiti)
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software
// and associated documentation files (the “Software”), to deal in the Software without
// restriction, including without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom
// the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED “AS IS”, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING
// BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Engine.Internals;
using Microsoft.Extensions.Logging;
using static CanDoUrco.Engine.NativeInterop.Ffi;

namespace CanDoUrco.Engine;

public sealed partial class Runtime(ILogger<Runtime> logger) : IDisposable
{
    public void Initialize()
    {
        LinkUnhandledExceptionsHandler();
        InitializeSdlLogging();
    }

    private unsafe void InitializeSdlLogging()
    {
        SDL_SetMainReady();
        var logLevel = SDL_LogPriority.FromLogger(logger);
        SDL_SetLogPriorities(logLevel);

        _logFunctionHandle = GCHandle.Alloc((LogFunction)Log);
        SDL_SetLogOutputFunction(&LogFunctionImpl, (void*)GCHandle.ToIntPtr(_logFunctionHandle));
    }

    #region IDisposable Support

    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            // Nothing to dispose in managed code for now.
        }

        SDL_Quit();
        if (_logFunctionHandle.IsAllocated)
        {
            _logFunctionHandle.Free();
        }

        _disposedValue = true;
    }

    ~Runtime()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    #endregion // IDisposable Support

    #region Unhandled Exceptions Bridging

    private static void LinkUnhandledExceptionsHandler()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            if (e.ExceptionObject is Exception ex)
            {
                UnhandledException(ex);
            }
            else
            {
                UnhandledException(e.ExceptionObject);
            }

            // Exit the application with a non-zero exit code to indicate an error occurred.
            Environment.Exit(1);
        };
    }

    private static void UnhandledException(Exception ex)
    {
        try
        {
            SDL_LogCritical(LogCategories.Runtime, $"Unhandled exception: {ex}");
            if (
                !SDL_ShowSimpleMessageBox(
                    SDL_MESSAGEBOX_ERROR,
                    "Unhandled Exception",
                    $"An unhandled exception occurred:\n\n{ex}",
                    SDL_Window.Null
                )
            )
            {
                SDL_LogError(LogCategories.Runtime, $"Failed to show simple messagebox: {ex}");
            }
        }
        catch
        {
            // Nothing we can do here, just swallow the exception to avoid crashing the application.
        }
    }

    private static void UnhandledException(Object? obj)
    {
        try
        {
            SDL_LogCritical(LogCategories.Runtime, $"Unknown unhandled exception: {obj}");
            if (
                !SDL_ShowSimpleMessageBox(
                    SDL_MESSAGEBOX_ERROR,
                    "Unhandled Exception",
                    $"An unknown unhandled exception occurred: {obj}",
                    SDL_Window.Null
                )
            )
            {
                SDL_LogError(LogCategories.Runtime, $"Failed to show simple messagebox: {obj}");
            }
        }
        catch
        {
            // Nothing we can do here, just swallow the exception to avoid crashing the application.
        }
    }

    #endregion // Unhandled Exceptions Bridging

    #region Logging Bridging

    private delegate void LogFunction(
        SDL_LogCategory category,
        SDL_LogPriority priority,
        string message
    );

    private GCHandle _logFunctionHandle;

    private void Log(SDL_LogCategory category, SDL_LogPriority priority, string message)
    {
        var level = priority.ToLogLevel();
        LogMessage(logger, level, category, message);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void LogFunctionImpl(
        void* userdata,
        SDL_LogCategory category,
        SDL_LogPriority priority,
        byte* message
    )
    {
        if (userdata is null)
        {
            return;
        }

        var handle = GCHandle.FromIntPtr((nint)userdata);
        if (!handle.IsAllocated || handle.Target is not LogFunction callback)
        {
            return;
        }

        var messageString = Utf8StringMarshaller.ConvertToManaged(message) ?? string.Empty;
        callback(category, priority, messageString);
    }

    [LoggerMessage(EventId = 9000, Message = "[{Category}] {Message}")]
    private static partial void LogMessage(
        ILogger logger,
        LogLevel level,
        SDL_LogCategory category,
        string message
    );

    #endregion // Logging Bridging
}
