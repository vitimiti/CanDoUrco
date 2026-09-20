// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The list of supported gamepad buttons.
/// </summary>
public enum GamepadButton
{
    A = Native.Glfw.GamepadButtonADefine,
    B = Native.Glfw.GamepadButtonBDefine,
    X = Native.Glfw.GamepadButtonXDefine,
    Y = Native.Glfw.GamepadButtonYDefine,
    LeftBumper = Native.Glfw.GamepadButtonLeftBumperDefine,
    RightBumper = Native.Glfw.GamepadButtonRightBumperDefine,
    Back = Native.Glfw.GamepadButtonBackDefine,
    Start = Native.Glfw.GamepadButtonStartDefine,
    Guide = Native.Glfw.GamepadButtonGuideDefine,
    LeftThumb = Native.Glfw.GamepadButtonLeftThumbDefine,
    RightThumb = Native.Glfw.GamepadButtonRightThumbDefine,
    DPadUp = Native.Glfw.GamepadButtonDpadUpDefine,
    DPadRight = Native.Glfw.GamepadButtonDpadRightDefine,
    DPadDown = Native.Glfw.GamepadButtonDpadDownDefine,
    DPadLeft = Native.Glfw.GamepadButtonDpadLeftDefine,
    Cross = A,
    Circle = B,
    Square = X,
    Triangle = Y,
}
