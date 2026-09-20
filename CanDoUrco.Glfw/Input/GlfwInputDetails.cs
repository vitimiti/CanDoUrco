// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
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
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
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
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }
}
