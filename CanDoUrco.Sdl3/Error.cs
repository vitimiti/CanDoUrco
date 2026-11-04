// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetError",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetError(string fmt, string msg);

    [LibraryImport(DllName, EntryPoint = "SDL_OutOfMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _OutOfMemory();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetError",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetError();

    [LibraryImport(DllName, EntryPoint = "SDL_ClearError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ClearError();

    public static bool SetError(string message)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetError("%s", message ?? string.Empty);
    }

    public static bool OutOfMemory()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OutOfMemory();
    }

    public static string GetError()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetError() ?? string.Empty;
    }

    public static bool ClearError()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearError();
    }

    public static bool Unsupported()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return SetError("That operation is not supported");
    }

    public static bool InvalidParamError(string param)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return SetError($"Parameter '{param ?? string.Empty}' is invalid");
    }
}
