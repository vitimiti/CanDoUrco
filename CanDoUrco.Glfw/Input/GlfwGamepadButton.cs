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
