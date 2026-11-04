// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CduLogCategory = CanDoUrco.Logging.LogCategory;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_SetLogPriorities")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetLogPriorities(PrivateLogPriority priority);

    [LibraryImport(DllName, EntryPoint = "SDL_SetLogOutputFunction")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _SetLogOutputFunction(
        delegate* unmanaged[Cdecl]<
            void*,
            PrivateLogCategory,
            PrivateLogPriority,
            byte*,
            void> callback,
        void* userdata
    );

    private enum PrivateLogCategory
    {
        Application,
        Error,
        Assert,
        System,
        Audio,
        Video,
        Render,
        Input,
        Test,
        Gpu,
        Reserved2,
        Reserved3,
        Reserved4,
        Reserved5,
        Reserved6,
        Reserved7,
        Reserved8,
        Reserved9,
        Reserved10,
        Custom,
    }

    private enum PrivateLogPriority
    {
        Invalid,
        Trace,
        Verbose,
        Debug,
        Info,
        Warn,
        Error,
        Critical,
    }
}
