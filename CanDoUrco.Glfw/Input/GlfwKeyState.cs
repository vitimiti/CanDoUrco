// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The current state of a key.
/// </summary>
/// <seealso cref="GlfwWindow.GetKeyState(GlfwKey)"/>
public enum GlfwKeyState
{
    /// <summary>
    /// The key is pressed.
    /// </summary>
    Pressed = Native.Glfw.PressDefine,

    /// <summary>
    /// The key is released.
    /// </summary>
    Released = Native.Glfw.ReleaseDefine,
}
