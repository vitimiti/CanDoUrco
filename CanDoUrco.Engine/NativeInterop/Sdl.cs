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

using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Engine.NativeInterop.CustomMarshallers;
using Microsoft.Extensions.Logging;

namespace CanDoUrco.Engine.NativeInterop;

internal static unsafe partial class Ffi
{
    #region SDL_error.h

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string SDL_GetError();

    #endregion // SDL_error.h

    #region SDL_init.h

    [LibraryImport(LibSdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_Quit();

    #endregion // SDL_init.h

    #region SDL_log.h

    public readonly record struct SDL_LogCategory(int Value)
    {
        public static implicit operator int(SDL_LogCategory category) => category.Value;

        public static implicit operator SDL_LogCategory(int value) => new(value);

        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Multiple if statements are more readable in this case."
        )]
        public override string ToString()
        {
            if (this == SDL_LOG_CATEGORY_APPLICATION)
            {
                return "Application";
            }
            if (this == SDL_LOG_CATEGORY_ERROR)
            {
                return "Error";
            }
            if (this == SDL_LOG_CATEGORY_ASSERT)
            {
                return "Assert";
            }
            if (this == SDL_LOG_CATEGORY_SYSTEM)
            {
                return "System";
            }
            if (this == SDL_LOG_CATEGORY_AUDIO)
            {
                return "Audio";
            }
            if (this == SDL_LOG_CATEGORY_VIDEO)
            {
                return "Video";
            }
            if (this == SDL_LOG_CATEGORY_RENDER)
            {
                return "Render";
            }
            if (this == SDL_LOG_CATEGORY_INPUT)
            {
                return "Input";
            }
            if (this == SDL_LOG_CATEGORY_TEST)
            {
                return "Test";
            }
            if (this == SDL_LOG_CATEGORY_GPU)
            {
                return "GPU";
            }
            if (this == SDL_LOG_CATEGORY_CUSTOM)
            {
                return "Custom";
            }
            return $"{nameof(SDL_LogCategory)}({Value})";
        }
    }

    public static SDL_LogCategory SDL_LOG_CATEGORY_APPLICATION => new(0);
    public static SDL_LogCategory SDL_LOG_CATEGORY_ERROR => new(1);
    public static SDL_LogCategory SDL_LOG_CATEGORY_ASSERT => new(2);
    public static SDL_LogCategory SDL_LOG_CATEGORY_SYSTEM => new(3);
    public static SDL_LogCategory SDL_LOG_CATEGORY_AUDIO => new(4);
    public static SDL_LogCategory SDL_LOG_CATEGORY_VIDEO => new(5);
    public static SDL_LogCategory SDL_LOG_CATEGORY_RENDER => new(6);
    public static SDL_LogCategory SDL_LOG_CATEGORY_INPUT => new(7);
    public static SDL_LogCategory SDL_LOG_CATEGORY_TEST => new(8);
    public static SDL_LogCategory SDL_LOG_CATEGORY_GPU => new(9);
    public static SDL_LogCategory SDL_LOG_CATEGORY_CUSTOM => new(19);

    public readonly record struct SDL_LogPriority(int Value)
    {
        public static implicit operator int(SDL_LogPriority priority) => priority.Value;

        public static implicit operator SDL_LogPriority(int value) => new(value);

        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Multiple if statements are more readable in this case."
        )]
        public static SDL_LogPriority FromLogger(ILogger logger)
        {
            if (logger.IsEnabled(LogLevel.Trace))
            {
                return SDL_LOG_PRIORITY_TRACE;
            }
            if (logger.IsEnabled(LogLevel.Debug))
            {
                return SDL_LOG_PRIORITY_DEBUG;
            }
            if (logger.IsEnabled(LogLevel.Information))
            {
                return SDL_LOG_PRIORITY_INFO;
            }
            if (logger.IsEnabled(LogLevel.Warning))
            {
                return SDL_LOG_PRIORITY_WARN;
            }
            if (logger.IsEnabled(LogLevel.Error))
            {
                return SDL_LOG_PRIORITY_ERROR;
            }
            if (logger.IsEnabled(LogLevel.Critical))
            {
                return SDL_LOG_PRIORITY_CRITICAL;
            }
            return SDL_LOG_PRIORITY_INVALID;
        }

        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Multiple if statements are more readable in this case."
        )]
        public LogLevel ToLogLevel()
        {
            if (this == SDL_LOG_PRIORITY_INVALID)
            {
                return LogLevel.None;
            }
            if (this == SDL_LOG_PRIORITY_TRACE)
            {
                return LogLevel.Trace;
            }
            if (this == SDL_LOG_PRIORITY_DEBUG)
            {
                return LogLevel.Debug;
            }
            if (this == SDL_LOG_PRIORITY_INFO)
            {
                return LogLevel.Information;
            }
            if (this == SDL_LOG_PRIORITY_WARN)
            {
                return LogLevel.Warning;
            }
            if (this == SDL_LOG_PRIORITY_ERROR)
            {
                return LogLevel.Error;
            }
            if (this == SDL_LOG_PRIORITY_CRITICAL)
            {
                return LogLevel.Critical;
            }
            return LogLevel.None;
        }

        [SuppressMessage(
            "Style",
            "IDE0046:Convert to conditional expression",
            Justification = "Multiple if statements are more readable in this case."
        )]
        public override string ToString()
        {
            if (this == SDL_LOG_PRIORITY_INVALID)
            {
                return "Invalid";
            }
            if (this == SDL_LOG_PRIORITY_TRACE)
            {
                return "Trace";
            }
            if (this == SDL_LOG_PRIORITY_DEBUG)
            {
                return "Debug";
            }
            if (this == SDL_LOG_PRIORITY_INFO)
            {
                return "Info";
            }
            if (this == SDL_LOG_PRIORITY_WARN)
            {
                return "Warn";
            }
            if (this == SDL_LOG_PRIORITY_ERROR)
            {
                return "Error";
            }
            if (this == SDL_LOG_PRIORITY_CRITICAL)
            {
                return "Critical";
            }
            return $"{nameof(SDL_LogPriority)}({Value})";
        }
    }

    public static SDL_LogPriority SDL_LOG_PRIORITY_INVALID => new(0);
    public static SDL_LogPriority SDL_LOG_PRIORITY_TRACE => new(1);
    public static SDL_LogPriority SDL_LOG_PRIORITY_DEBUG => new(2);
    public static SDL_LogPriority SDL_LOG_PRIORITY_INFO => new(3);
    public static SDL_LogPriority SDL_LOG_PRIORITY_WARN => new(4);
    public static SDL_LogPriority SDL_LOG_PRIORITY_ERROR => new(5);
    public static SDL_LogPriority SDL_LOG_PRIORITY_CRITICAL => new(6);

    [LibraryImport(LibSdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetLogPriorities(SDL_LogPriority priority);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogTrace(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogVerbose(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogDebug(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogInfo(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogWarn(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogError(SDL_LogCategory category, string message);

    [LibraryImport(
        LibSdl3,
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_LogCritical(SDL_LogCategory category, string message);

    [LibraryImport(LibSdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetLogOutputFunction(
        delegate* unmanaged[Cdecl]<void*, SDL_LogCategory, SDL_LogPriority, byte*, void> callback,
        void* userdata
    );

    #endregion // SDL_log.h

    #region SDL_main.h

    [LibraryImport(LibSdl3)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SDL_SetMainReady();

    #endregion // SDL_main.h

    #region SDL_messagebox.h

    public readonly record struct SDL_MessageboxFlags(uint Value)
    {
        public static implicit operator uint(SDL_MessageboxFlags flags) => flags.Value;

        public static implicit operator SDL_MessageboxFlags(uint value) => new(value);
    }

    public static SDL_MessageboxFlags SDL_MESSAGEBOX_ERROR => new(0x0000_0010U);

    [LibraryImport(LibSdl3, StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SDL_ShowSimpleMessageBox(
        SDL_MessageboxFlags flags,
        string title,
        string message,
        SDL_Window window
    );

    #endregion // SDL_messagebox.h

    #region SDL_video.h

    public readonly record struct SDL_Window(nint Handle)
    {
        public static SDL_Window Null => new(0);

        public bool IsNull => Handle == 0;

        public static implicit operator nint(SDL_Window window) => window.Handle;

        public static implicit operator SDL_Window(nint handle) => new(handle);
    }

    #endregion // SDL_video.h
}
