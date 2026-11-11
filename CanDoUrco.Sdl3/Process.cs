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
    public static ReadOnlySpan<byte> PropProcessCreateArgsPointer => "SDL.process.create.args\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateEnvironmentPointer =>
        "SDL.process.create.environment\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateWorkingDirectoryString =>
        "SDL.process.create.working_directory\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdInNumber =>
        "SDL.process.create.stdin_option\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdInPointer =>
        "SDL.process.create.stdin_source\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdOutNumber =>
        "SDL.process.create.stdout_option\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdOutPointer =>
        "SDL.process.create.stdout_source\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdErrNumber =>
        "SDL.process.create.stderr_option\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdErrPointer =>
        "SDL.process.create.stderr_source\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateStdErrToStdOutBoolean =>
        "SDL.process.create.stderr_to_stdout\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateBackgroundBoolean =>
        "SDL.process.create.background\0"u8;
    public static ReadOnlySpan<byte> PropProcessCreateCmdLineString =>
        "SDL.process.create.cmdline\0"u8;
    public static ReadOnlySpan<byte> PropProcessPidNumber => "SDL.process.pid\0"u8;
    public static ReadOnlySpan<byte> PropProcessStdInPointer => "SDL.process.stdin\0"u8;
    public static ReadOnlySpan<byte> PropProcessStdOutPointer => "SDL.process.stdout\0"u8;
    public static ReadOnlySpan<byte> PropProcessStdErrPointer => "SDL.process.stderr\0"u8;
    public static ReadOnlySpan<byte> PropProcessBackgroundBoolean => "SDL.process.background\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_CreateProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Process _CreateProcess(byte** args, Bool pipeStdIO);

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
    private static partial Bool _KillProcess(Process process, Bool force);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitProcess(Process process, Bool block, out int exitCode);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyProcess")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyProcess(Process process);

    public static unsafe Process CreateProcess(byte** args, Bool pipeStdIO)
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

    public static Bool KillProcess(Process process, Bool force)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _KillProcess(process, force);
    }

    public static Bool WaitProcess(Process process, Bool block, out int exitCode)
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
        public static implicit operator nint(Process process) => process.Value;

        public static implicit operator Process(nint value) => new(value);

        public static implicit operator bool(Process process) => process.Value != 0;
    }
}
