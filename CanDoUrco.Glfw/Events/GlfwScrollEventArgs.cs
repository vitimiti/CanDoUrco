// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when scrolling happens.
/// </summary>
/// <param name="offset">A <see cref="PointF"/> with the scroll offset.</param>
public sealed class GlfwScrollEventArgs(PointF offset) : EventArgs
{
    /// <summary>
    /// Gets the scroll offset.
    /// </summary>
    public PointF Offset { get; init; } = offset;
}
