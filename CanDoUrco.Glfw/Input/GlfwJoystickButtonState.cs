// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The current state of a joystick button.
/// </summary>
/// <seealso cref="GlfwJoystick.GetButtons()"/>
public enum GlfwJoystickButtonState
{
    /// <summary>
    /// The joystick button is pressed.
    /// </summary>
    Pressed = Native.Glfw.PressDefine,

    /// <summary>
    /// The joystick button is released.
    /// </summary>
    Released = Native.Glfw.ReleaseDefine,
}
