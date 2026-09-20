// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Input;

public static class GlfwInputDetails
{
    /// <summary>
    /// Gets whether the raw mouse motion input mode is supported.
    /// </summary>
    /// <returns><see langword="true"/> if the raw mouse motion input mode is supported, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static bool IsRawMouseMotionSupported()
    {
        var result = Native.Glfw.RawMouseMotionSupported();
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }
}
