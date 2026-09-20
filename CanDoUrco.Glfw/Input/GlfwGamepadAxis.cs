// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// A list of supported gamepad axis.
/// </summary>
public enum GlfwGamepadAxis
{
    LeftX = Native.Glfw.GamepadAxisLeftXDefine,
    LeftY = Native.Glfw.GamepadAxisLeftYDefine,
    RightX = Native.Glfw.GamepadAxisRightXDefine,
    RightY = Native.Glfw.GamepadAxisRightYDefine,
    LeftTrigger = Native.Glfw.GamepadAxisLeftTriggerDefine,
    RightTrigger = Native.Glfw.GamepadAxisRightTriggerDefine,
}
