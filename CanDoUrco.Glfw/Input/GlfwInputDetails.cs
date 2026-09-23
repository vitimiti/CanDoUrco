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
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Input;

public static class GlfwInputDetails
{
    /// <summary>
    /// Gets whether the raw mouse motion input mode is supported.
    /// </summary>
    /// <returns><see langword="true"/> if the raw mouse motion input mode is supported, <see langword="false"/> otherwise.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static bool IsRawMouseMotionSupported()
    {
        var result = Native.Glfw.RawMouseMotionSupported();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gets the name for a given key and scancode.
    /// </summary>
    /// <param name="key">The <see cref="GlfwKey"/> to get the name from.</param>
    /// <param name="scancode">The scancode corresponding to the <paramref name="key"/>.aram>
    /// <returns>The name of the given <paramref name="key"/> and <paramref name="scancode"/>, or <see langword="null"/> if no name could be found.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <seealso cref="GetKeyScancode"/>
    public static unsafe string? GetKeyName(GlfwKey key, int scancode)
    {
        var ptr = Native.Glfw.GetKeyName((int)key, scancode);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return Utf8StringMarshaller.ConvertToManaged(ptr);
    }

    /// <summary>
    /// Gets the scancode for a given key.
    /// </summary>
    /// <param name="key">The <see cref="GlfwKey"/> to get the scancode from.</param>
    /// <returns>An <see cref="int"/> with the <paramref name="key"/> scancode</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <seealso cref="GetKeyName"/>
    public static int GetKeyScancode(GlfwKey key)
    {
        var result = Native.Glfw.GetKeyScancode((int)key);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }
}
