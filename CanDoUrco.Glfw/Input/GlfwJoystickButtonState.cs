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
