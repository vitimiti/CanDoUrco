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
/// The possible cursor modes.
/// </summary>
public enum GlfwCursorMode
{
    /// <summary>
    /// The cursor is visible and behaves normal.
    /// </summary>
    Normal = Native.Glfw.CursorNormalDefine,

    /// <summary>
    /// The cursor is invisible when it is over the content area of the window, but does not restrict the cursor from leaving.
    /// </summary>
    Hidden = Native.Glfw.CursorHiddenDefine,

    /// <summary>
    /// The cursor is hidden and grabbed, providing virtual and unlimited cursor movement.
    /// </summary>
    /// <remarks>This is useful for implementing, for example, 3D camera controls.</remarks>
    Disabled = Native.Glfw.CursorDisabledDefine,

    /// <summary>
    /// The cursor is visible and confined to the area of the window.
    /// </summary>
    Captured = Native.Glfw.CursorCapturedDefine,
}
