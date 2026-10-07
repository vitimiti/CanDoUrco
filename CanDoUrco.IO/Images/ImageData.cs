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

using CanDoUrco.Math;

namespace CanDoUrco.IO.Images;

/// <summary>
/// Represents image data including pixel values, size, and channel count.
/// </summary>
/// <param name="Pixels">The raw pixel data of the image.</param>
/// <param name="Size">The dimensions of the image.</param>
/// <param name="ChannelCount">The number of color channels in the image.</param>
public readonly record struct ImageData(byte[] Pixels, Size Size, ImageChannelCount ChannelCount);
