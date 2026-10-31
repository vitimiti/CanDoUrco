// CanDoUrco.Native.Sdl3 - Provides native SDL3 bindings for CanDoUrco
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
using System.Text;

namespace CanDoUrco.Native.Sdl3;

public static unsafe partial class Sdl
{
    #region SetError

    public static bool TrySetError(string message)
    {
        ArgumentNullException.ThrowIfNull(message);
        ReadOnlySpan<byte> messageBytes = Encoding.UTF8.GetBytes(message);
        return TrySetError(messageBytes);
    }

    public static bool TrySetError(ReadOnlySpan<byte> message)
    {
        fixed (byte* messagePtr = message)
        fixed (byte* fmtPtr = "%s"u8)
        {
            return SetErrorUnsafe(fmtPtr, messagePtr);
        }
    }

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_SetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool SetErrorUnsafe(byte* fmt, byte* arg1);

    #endregion // SetError

    #region OutOfMemory

    public static bool TrySetErrorOutOfMemory() => OutOfMemoryUnsafe();

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_OutOfMemory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool OutOfMemoryUnsafe();

    #endregion // OutOfMemory

    #region GetError

    public static string GetError()
    {
        byte* errorPtr = GetErrorUnsafe();
        return Marshal.PtrToStringUTF8((nint)errorPtr) ?? string.Empty;
    }

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_GetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* GetErrorUnsafe();

    #endregion // GetError

    #region ClearError

    public static bool TryClearError() => ClearErrorUnsafe();

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_ClearError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool ClearErrorUnsafe();

    #endregion // ClearError

    public static bool TrySetErrorUnsupported() => TrySetError("That operation is unsupported");

    public static bool TrySetInvalidParamError(string param)
    {
        ArgumentNullException.ThrowIfNull(param);
        return TrySetError($"Parameter '{param}' is invalid");
    }
}
