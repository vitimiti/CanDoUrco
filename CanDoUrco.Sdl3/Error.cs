// CanDoUrco.Sdl3 - SDL3 bindings for Can Do Urco
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetError",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(StdFmtCompatibleUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetError(string message);

    [LibraryImport(DllName, EntryPoint = "SDL_OutOfMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool OutOfMemory();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetError",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string GetError();

    [LibraryImport(DllName, EntryPoint = "SDL_ClearError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ClearError();

    public static bool Unsupported() => SetError("That operation is not supported");

    public static bool InvalidParamError(string param) =>
        SetError($"Parameter '{param}' is invalid");
}
