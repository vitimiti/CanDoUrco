// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) <year>  <name of author>
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl.CustomMarshallers;

namespace CanDoUrco.Sdl;

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
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return _SetError("%s", message);
    }

    public static bool OutOfMemory()
    {
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return _OutOfMemory();
    }

    public static string GetError()
    {
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return _GetError() ?? string.Empty;
    }

    public static bool ClearError()
    {
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return _ClearError();
    }

    public static bool Unsupported()
    {
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return SetError("That operation is not supported");
    }

    public static bool InvalidParamError(string param)
    {
        LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
        return SetError($"Parameter '{param ?? string.Empty}' is invalid");
    }
}
