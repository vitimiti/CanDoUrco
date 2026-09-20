// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The possible key actions.
/// </summary>
public enum GlfwKeyAction
{
    /// <summary>
    /// The key was pressed.
    /// </summary>
    Press = Native.Glfw.PressDefine,

    /// <summary>
    /// The key was released.
    /// </summary>
    Release = Native.Glfw.ReleaseDefine,

    /// <summary>
    /// The key went onto repeat mode.
    /// </summary>
    Repeat = Native.Glfw.RepeatDefine,
}
