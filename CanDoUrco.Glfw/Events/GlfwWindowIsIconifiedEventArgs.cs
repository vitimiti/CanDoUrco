// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a window is iconified or not.
/// </summary>
/// <param name="iconified">Whether the window was iconified.</param>
public sealed class GlfwWindowIsIconifiedEventArgs(bool iconified) : EventArgs
{
    /// <summary>
    /// Gets whether the window is iconified.
    /// </summary>
    public bool Iconified { get; init; } = iconified;
}
