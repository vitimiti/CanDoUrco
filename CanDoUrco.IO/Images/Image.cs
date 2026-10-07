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

using CanDoUrco.IO.Images.Internals;
using CanDoUrco.Math;

namespace CanDoUrco.IO.Images;

/// <summary>
/// Provides methods for loading image data from various sources.
/// </summary>
public static class Image
{
    /// <summary>
    /// Loads an image from the specified file path.
    /// </summary>
    /// <param name="path">The path to the image file.</param>
    /// <param name="requiredChannelCount">The number of color channels required in the loaded image. If 0, the original channel count is used.</param>
    /// <returns>An <see cref="ImageData"/> object containing the loaded image data.</returns>
    /// <exception cref="ArgumentNullException">Thrown if the path is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the image cannot be loaded.</exception>
    public static ImageData Load(string path, int requiredChannelCount = 0)
    {
        ArgumentNullException.ThrowIfNull(path);
        using var stream = File.OpenRead(path);
        return Load(stream, requiredChannelCount);
    }

    /// <summary>
    /// Loads an image from the specified stream.
    /// </summary>
    /// <param name="stream">The stream containing the image data.</param>
    /// <param name="requiredChannelCount">The number of color channels required in the loaded image. If 0, the original channel count is used.</param>
    /// <returns>An <see cref="ImageData"/> object containing the loaded image data.</returns>
    /// <remarks>
    /// The stream must support seeking.
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if the stream is null.</exception>
    /// <exception cref="InvalidOperationException">Thrown if the image cannot be loaded.</exception>
    public static ImageData Load(Stream stream, int requiredChannelCount = 0)
    {
        ArgumentNullException.ThrowIfNull(stream);
        Methods.ClearFailure();
        var context = Methods.StartFile(stream);
        var result =
            context.LoadAndPostprocess8Bit(out var x, out var y, out var comp, requiredChannelCount)
            ?? throw new InvalidOperationException(
                Methods.FailureReason ?? "Unknown image load failure."
            );

        stream.Seek(
            -(context.ImgBufferEndPosition - context.ImgBufferPosition),
            SeekOrigin.Current
        );

        var channels = requiredChannelCount != 0 ? requiredChannelCount : comp;
        return new ImageData(
            result.AsSpan(0, x * y * channels).ToArray(),
            new Size(x, y),
            (ImageChannelCount)channels
        );
    }
}
