// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The possible states of a joystick hat.
/// </summary>
[Flags]
public enum GlfwJoystickHatStates
{
    None = Native.Glfw.HatCenteredDefine,
    Centered = None,
    Up = Native.Glfw.HatUpDefine,
    Right = Native.Glfw.HatRightDefine,
    Down = Native.Glfw.HatDownDefine,
    Left = Native.Glfw.HatLeftDefine,
    RightUp = Native.Glfw.HatRightUpDefine,
    RightDown = Native.Glfw.HatRightDownDefine,
    LeftUp = Native.Glfw.HatLeftUpDefine,
    LeftDown = Native.Glfw.HatLeftDownDefine,
}
