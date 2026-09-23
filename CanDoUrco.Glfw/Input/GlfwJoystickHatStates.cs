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
