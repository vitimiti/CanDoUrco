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

public static unsafe partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_SetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetError(byte* fmt, byte* msg);

    [LibraryImport(DllName, EntryPoint = "SDL_OutOfMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _OutOfMemory();

    [LibraryImport(DllName, EntryPoint = "SDL_GetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetError();

    [LibraryImport(DllName, EntryPoint = "SDL_ClearError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClearError();

    public static Bool SetError(byte* fmt, byte* message)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetError(fmt, message);
    }

    public static Bool OutOfMemory()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OutOfMemory();
    }

    public static byte* GetError()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetError();
    }

    public static Bool ClearError()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearError();
    }

    public static Bool Unsupported()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* fmt = "%s"u8)
        fixed (byte* msg = "That operation is not supported"u8)
        {
            return SetError(fmt, msg);
        }
    }

    public static Bool InvalidParamError(byte* param)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* fmt = "Parameter '%s' is invalid"u8)
        {
            return SetError(fmt, param);
        }
    }
}
