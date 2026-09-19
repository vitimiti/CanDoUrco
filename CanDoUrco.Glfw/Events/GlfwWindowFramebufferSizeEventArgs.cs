// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when the window framebuffer size is updated.
/// </summary>
/// <param name="size">A <see cref="Size"/> with the new window framebuffer dimensions.</param>
public sealed class GlfwWindowFramebufferSizeEventArgs(Size size) : EventArgs
{
    /// <summary>
    /// Gets the window framebuffer dimensions.
    /// </summary>
    public Size Size { get; init; } = size;
}
