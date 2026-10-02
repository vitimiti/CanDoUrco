// These are I/O utilities for the CanDoUrco project.
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
/// Decodes a specific image format into <see cref="ImageData"/>.
/// </summary>
public interface IImageDecoder
{
    /// <summary>
    /// Determines whether the header bytes belong to the format handled by this decoder.
    /// </summary>
    /// <param name="header">The first bytes of the image.</param>
    /// <returns><see langword="true"/> if the format is supported.</returns>
    bool CanDecode(ReadOnlySpan<byte> header);

    /// <summary>
    /// Decodes the image in the stream.
    /// </summary>
    /// <param name="stream">The stream positioned at the start of the image.</param>
    /// <returns>The decoded image.</returns>
    ImageData Decode(Stream stream);
}
