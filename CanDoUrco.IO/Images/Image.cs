// A set of utilities to handle IO for Can Do Urco.
// Copyright (C) <year>  <name of author>
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
using System.Runtime.InteropServices;
using CanDoUrco.IO.Images.Internals;

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
    public static unsafe ImageData Load(Stream stream, int requiredChannelCount = 0)
    {
        ArgumentNullException.ThrowIfNull(stream);
        Methods.ClearFailure();
        Methods.StartFile(stream, out var context);
        var x = 0;
        var y = 0;
        var comp = 0;
        var result = Methods.LoadAndPostprocess8Bit(&context, &x, &y, &comp, requiredChannelCount);
        if (result is null)
        {
            throw new InvalidOperationException(
                Methods.FailureReason ?? "Unknown image load failure."
            );
        }

        try
        {
            stream.Seek(-(context.ImgBufferEnd - context.ImgBuffer), SeekOrigin.Current);
            var channels = requiredChannelCount != 0 ? requiredChannelCount : comp;
            return new ImageData(
                new Span<byte>(result, x * y * channels).ToArray(),
                new Size(x, y),
                (ImageChannelCount)channels
            );
        }
        finally
        {
            NativeMemory.Free(result);
            if (Methods.FileStreamHandle.IsAllocated)
            {
                Methods.FileStreamHandle.Free();
            }
        }
    }
}
