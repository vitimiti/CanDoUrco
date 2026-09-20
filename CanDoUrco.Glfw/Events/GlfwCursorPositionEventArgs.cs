// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a cursor is repositioned.
/// </summary>
/// <param name="position">A <see cref="PointF"/> with the new position.</param>
public sealed class GlfwCursorPositionEventArgs(PointF position) : EventArgs
{
    /// <summary>
    /// Gets the cursor position.
    /// </summary>
    public PointF Position { get; init; } = position;
}
