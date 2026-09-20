// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents an image.
/// </summary>
public sealed record GlfwImage
{
    /// <summary>
    /// Gets the size of the image.
    /// </summary>
    public Size Size { get; init; }

    /// <summary>
    /// Gets the pixel data of the image.
    /// </summary>
    public ICollection<byte> PixelData { get; init; } = [];

    internal unsafe Native.Glfw.Image ConvertToUnmanaged()
    {
        var nativeImage = new Native.Glfw.Image { Width = Size.Width, Height = Size.Height };

        var pixelsSpan = PixelData.ToArray().AsSpan();
        fixed (byte* pixelsPtr = pixelsSpan)
        {
            nativeImage.Pixels = pixelsPtr;
        }

        return nativeImage;
    }
}
