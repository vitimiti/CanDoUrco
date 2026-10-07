// A set of utilities to handle IO for Can Do Urco.
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

namespace CanDoUrco.IO.Images;

/// <summary>
/// Represents the number of color channels in an image.
/// </summary>
public enum ImageChannelCount
{
    /// <summary>
    /// No color channels.
    /// </summary>
    None,

    /// <summary>
    /// Single-channel grayscale image.
    /// </summary>
    Grey,

    /// <summary>
    /// Two-channel grayscale image with alpha.
    /// </summary>
    GreyAlpha,

    /// <summary>
    /// Three-channel RGB image.
    /// </summary>
    Rgb,

    /// <summary>
    /// Four-channel RGB image with alpha.
    /// </summary>
    RgbAlpha,
}
