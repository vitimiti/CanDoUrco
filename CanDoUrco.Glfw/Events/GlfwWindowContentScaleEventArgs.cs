// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when the window content scale is updated.
/// </summary>
/// <param name="scale">A <see cref="PointF"/> with the new window content scale.</param>
public sealed class GlfwWindowContentScaleEventArgs(PointF scale) : EventArgs
{
    /// <summary>
    /// Gets the window content scale.
    /// </summary>
    public PointF Scale { get; init; } = scale;
}
