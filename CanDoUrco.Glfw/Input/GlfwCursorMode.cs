// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
