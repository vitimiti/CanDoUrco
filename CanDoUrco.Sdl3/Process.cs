// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const string PropProcessCreateArgsPointer = "SDL.process.create.args";
    public const string PropProcessCreateEnvironmentPointer = "SDL.process.create.environment";
    public const string PropProcessCreateWorkingDirectoryString =
        "SDL.process.create.working_directory";
    public const string PropProcessCreateStdInNumber = "SDL.process.create.stdin_option";
    public const string PropProcessCreateStdInPointer = "SDL.process.create.stdin_source";
    public const string PropProcessCreateStdOutNumber = "SDL.process.create.stdout_option";
    public const string PropProcessCreateStdOutPointer = "SDL.process.create.stdout_source";
    public const string PropProcessCreateStdErrNumber = "SDL.process.create.stderr_option";
    public const string PropProcessCreateStdErrPointer = "SDL.process.create.stderr_source";
    public const string PropProcessCreateStdErrToStdOutBoolean =
        "SDL.process.create.stderr_to_stdout";
    public const string PropProcessCreateBackgroundBoolean = "SDL.process.create.background";
    public const string PropProcessCreateCmdLineString = "SDL.process.create.cmdline";
    public const string PropProcessPidNumber = "SDL.process.pid";
    public const string PropProcessStdInPointer = "SDL.process.stdin";
    public const string PropProcessStdOutPointer = "SDL.process.stdout";
    public const string PropProcessStdErrPointer = "SDL.process.stderr";
    public const string PropProcessBackgroundBoolean = "SDL.process.background";

    [LibraryImport(DllName, EntryPoint = "SDL_CreateProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Process _CreateProcess(
        byte** args,
        [MarshalAs(UnmanagedType.I1)] bool pipeStdIO
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateProcessWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Process _CreateProcessWithProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_GetProcessProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetProcessProperties(Process process);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _ReadProcess(
        Process process,
        out nuint dataSize,
        out int exitCode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetProcessInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _GetProcessInput(Process process);

    [LibraryImport(DllName, EntryPoint = "SDL_GetProcessOutput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial IOStream _GetProcessOutput(Process process);

    [LibraryImport(DllName, EntryPoint = "SDL_KillProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _KillProcess(
        Process process,
        [MarshalAs(UnmanagedType.I1)] bool force
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WaitProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WaitProcess(
        Process process,
        [MarshalAs(UnmanagedType.I1)] bool block,
        out int exitCode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyProcess(Process process);

    public static unsafe Process CreateProcess(byte** args, bool pipeStdIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateProcess(args, pipeStdIO);
    }

    public static Process CreateProcessWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateProcessWithProperties(props);
    }

    public static PropertiesID GetProcessProperties(Process process)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetProcessProperties(process);
    }

    public static unsafe void* ReadProcess(Process process, out nuint dataSize, out int exitCode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadProcess(process, out dataSize, out exitCode);
    }

    public static IOStream GetProcessInput(Process process)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetProcessInput(process);
    }

    public static IOStream GetProcessOutput(Process process)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetProcessOutput(process);
    }

    public static bool KillProcess(Process process, bool force)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _KillProcess(process, force);
    }

    public static bool WaitProcess(Process process, bool block, out int exitCode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitProcess(process, block, out exitCode);
    }

    public static void DestroyProcess(Process process)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyProcess(process);
    }

    public enum ProcessIO
    {
        Inherited,
        Null,
        App,
        Redirect,
    }

    public readonly record struct Process(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator nint(Process process) => process.Value;

        public static implicit operator Process(nint value) => new(value);
    }
}
