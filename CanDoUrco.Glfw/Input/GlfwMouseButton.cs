// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// A list of predefined mouse buttons.
/// </summary>
public enum GlfwMouseButton
{
    Button1 = Native.Glfw.MouseButton1Define,
    Button2 = Native.Glfw.MouseButton2Define,
    Button3 = Native.Glfw.MouseButton3Define,
    Button4 = Native.Glfw.MouseButton4Define,
    Button5 = Native.Glfw.MouseButton5Define,
    Button6 = Native.Glfw.MouseButton6Define,
    Button7 = Native.Glfw.MouseButton7Define,
    Button8 = Native.Glfw.MouseButton8Define,
    Left = Button1,
    Right = Button2,
    Middle = Button3,
}
