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

public static class Image
{
    public static ImageData Load(string path, int requiredChannelCount = 0)
    {
        using var stream = File.OpenRead(path);
        return Load(stream, requiredChannelCount);
    }

    public static unsafe ImageData Load(Stream stream, int requiredChannelCount = 0)
    {
        Methods.ClearFailure();
        Context s = default;
        Methods.StartFile(&s, stream);
        var x = 0;
        var y = 0;
        var comp = 0;
        var result = Methods.LoadAndPostprocess8Bit(&s, &x, &y, &comp, requiredChannelCount);
        if (result is null)
        {
            throw new InvalidOperationException(
                Methods.FailureReason ?? "Unknown image load failure."
            );
        }

        try
        {
            stream.Seek(-(s.ImgBufferEnd - s.ImgBuffer), SeekOrigin.Current);
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
