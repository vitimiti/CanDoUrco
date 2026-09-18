// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents information about a video mode.
/// </summary>
public readonly record struct GlfwVideoMode
{
    /// <summary>
    /// Gets the size of the video mode.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// Gets the bit depth of the color channels of the video mode.
    /// </summary>
    public (int Red, int Green, int Blue) BitDepth { get; init; }

    /// <summary>
    /// Gets the refresh rate, in hertz, of the video mode.
    /// </summary>
    public int RefreshRateInHertz { get; init; }
}
