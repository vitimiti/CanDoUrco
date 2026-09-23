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
