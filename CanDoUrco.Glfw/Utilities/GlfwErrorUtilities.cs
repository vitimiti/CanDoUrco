// This is a small testing ground for CanDoUrco.
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

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;

namespace CanDoUrco.Glfw.Utilities;

internal static unsafe class GlfwErrorUtilities
{
    public static void CheckErrorCodeAndMaybeThrowError()
    {
        var errorCode = Native.Glfw.GetError(out var errorPtr);
        if (errorCode != Native.Glfw.NoErrorDefine)
        {
            var error = Utf8StringMarshaller.ConvertToManaged(errorPtr);
            throw new GlfwException(error, errorCode);
        }
    }

    public static void ThrowError()
    {
        var errorCode = Native.Glfw.GetError(out var errorPtr);
        var error = Utf8StringMarshaller.ConvertToManaged(errorPtr);
        throw new GlfwException(error, errorCode);
    }
}
