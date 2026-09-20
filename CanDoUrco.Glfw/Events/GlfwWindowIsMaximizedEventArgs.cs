// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a window is maiximized or not.
/// </summary>
/// <param name="maximized">Whether the window was maximized.</param>
public sealed class GlfwWindowIsMaximizedEventArgs(bool maximized) : EventArgs
{
    /// <summary>
    /// Gets whether the window is maximized.
    /// </summary>
    public bool Maximized { get; init; } = maximized;
}
