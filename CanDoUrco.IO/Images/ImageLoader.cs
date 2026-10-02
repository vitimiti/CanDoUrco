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

using CanDoUrco.IO.Images.Decoders;

namespace CanDoUrco.IO.Images;

/// <summary>
/// Loads images of any supported format (currently JPEG only).
/// </summary>
public static class ImageLoader
{
    private const int HeaderLength = 16;

    private static readonly IImageDecoder[] Decoders = [new JpegImageDecoder()];

    /// <summary>
    /// Loads an image from a file.
    /// </summary>
    /// <param name="path">The path of the image.</param>
    /// <returns>The decoded image.</returns>
    public static ImageData Load(string path)
    {
        using FileStream stream = File.OpenRead(path);
        return Load(stream);
    }

    /// <summary>
    /// Loads an image from a stream, detecting its format from its header.
    /// </summary>
    /// <param name="stream">The stream positioned at the start of the image.</param>
    /// <returns>The decoded image.</returns>
    /// <exception cref="NotSupportedException">The image format is not supported.</exception>
    public static ImageData Load(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        if (!stream.CanSeek)
        {
            var copy = new MemoryStream();
            stream.CopyTo(copy);
            copy.Position = 0;
            using (copy)
            {
                return Load(copy);
            }
        }

        long start = stream.Position;
        Span<byte> header = stackalloc byte[HeaderLength];
        int read = stream.ReadAtLeast(header, HeaderLength, throwOnEndOfStream: false);
        stream.Position = start;

        foreach (IImageDecoder decoder in Decoders)
        {
            if (decoder.CanDecode(header[..read]))
            {
                return decoder.Decode(stream);
            }
        }

        throw new NotSupportedException("The image format is not supported.");
    }
}
