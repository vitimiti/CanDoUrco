// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a cursor enter or leaves a window.
/// </summary>
/// <param name="entered">Whether the cursor entered the window.</param>
public sealed class GlfwCursorEnteredEventArgs(bool entered) : EventArgs
{
    /// <summary>
    /// Gets whether the cursor entered the window.
    /// </summary>
    public bool Entered { get; init; } = entered;
}
