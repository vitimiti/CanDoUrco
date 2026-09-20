// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The different shapes of a standard cursor.
/// </summary>
public enum GlfwStandardCursorShape
{
    Arrow = Native.Glfw.ArrowCursorDefine,
    IBeam = Native.Glfw.IBeamCursorDefine,
    Crosshair = Native.Glfw.CrosshairCursorDefine,
    PointingHand = Native.Glfw.PointingHandCursorDefine,
    ResizeEastWest = Native.Glfw.ResizeEastWestCursorDefine,
    ResizeNorthSouth = Native.Glfw.ResizeNorthSouthCursorDefine,
    ResizeNorthWestSouthEast = Native.Glfw.ResizeNorthWestSouthEastCursorDefine,
    ResizeNorthEastSouthWest = Native.Glfw.ResizeNorthEastSouthWestCursorDefine,
    ResizeAll = Native.Glfw.ResizeAllCursorDefine,
    NotAllowed = Native.Glfw.NotAllowedCursorDefine,
    HorizontalResize = ResizeEastWest,
    VerticalResize = ResizeNorthSouth,
    Hand = PointingHand,
}
