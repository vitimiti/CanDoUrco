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

using System.Diagnostics;

namespace CanDoUrco.IO.Images.Internals;

internal sealed class Context
{
    public uint ImgX { get; set; }
    public uint ImgY { get; set; }
    public int ImgN { get; set; }
    public int ImgOutN { get; set; }
    public IOCallbacks IO { get; set; } = new();
    public object? IOUserData { get; set; }
    public int ReadFromCallbacks { get; set; }
    public int BufLen { get; set; }
    public byte[] BufferStart { get; } = new byte[Constants.BufferStartLength];
    public int BufferStartPosition { get; set; }
    public int CallbackAlreadyRead { get; set; }
    public byte[]? ImgBuffer { get; set; }
    public int ImgBufferPosition { get; set; }
    public byte[]? ImgBufferEnd { get; set; }
    public int ImgBufferOriginalPosition { get; set; }
    public byte[]? ImgBufferOriginal { get; set; }
    public int ImgBufferOriginalEndPosition { get; set; }
    public byte[]? ImgBufferOriginalEnd { get; set; }
    public int ImgBufferEndPosition { get; set; }

    public bool JpegTest()
    {
        var jpeg = new Jpeg { Context = this };

        if (!jpeg.AllocTables())
        {
            return false;
        }

        jpeg.Setup();
        var r = jpeg.DecodeHeader(Scan.Type);
        Rewind();
        jpeg.FreeComponents(Constants.JpegFixedArrayLength, false);
        return r;
    }

    public byte[]? JpegLoad(
        out int x,
        out int y,
        out int channelCount,
        int requiredChannels,
        ResultInfo _
    )
    {
        var jpeg = new Jpeg { Context = this };
        if (!jpeg.AllocTables())
        {
            x = y = channelCount = 0;
            return null;
        }

        jpeg.Setup();
        var result = jpeg.LoadImage(out x, out y, out channelCount, requiredChannels);
        return result;
    }

    public byte[]? LoadMain(out int x, out int y, out int comp, int reqComp, ResultInfo ri, int _)
    {
        ri.BitsPerChannel = 8;
        ri.ChannelOrder = default;
        ri.NumChannels = 0;
        if (JpegTest())
        {
            return JpegLoad(out x, out y, out comp, reqComp, ri);
        }

        x = y = comp = 0;
        return Methods.ErrorPtr("Unsupported image format. No decoder recognized the data.");
    }

    public byte[]? LoadAndPostprocess8Bit(out int x, out int y, out int comp, int reqComp)
    {
        var ri = new ResultInfo();
        var result = LoadMain(out x, out y, out comp, reqComp, ri, 8);
        if (result is null)
        {
            return null;
        }

        Debug.Assert(ri.BitsPerChannel == 8 || ri.BitsPerChannel == 16);
        if (ri.BitsPerChannel != 8)
        {
            result = Methods.Convert16To8(result, x, y, reqComp == 0 ? comp : reqComp);
            ri.BitsPerChannel = 8;
        }

        if (Constants.VerticallyFlipOnLoad)
        {
            var channels = reqComp != 0 ? reqComp : comp;
            Methods.VerticalFlip(result!, x, y, channels);
        }

        return result;
    }

    public void Rewind()
    {
        ImgBuffer = ImgBufferOriginal;
        ImgBufferPosition = ImgBufferOriginalPosition;
        ImgBufferEnd = ImgBufferOriginalEnd;
        ImgBufferEndPosition = ImgBufferOriginalEndPosition;
    }

    public void RefillBuffer()
    {
        var n = IO.Read!.Invoke(IOUserData, BufferStart, BufLen);
        CallbackAlreadyRead += ImgBufferPosition - ImgBufferOriginalPosition;
        ImgBuffer = BufferStart;
        ImgBufferEnd = BufferStart;
        ImgBufferPosition = 0;
        if (n == 0)
        {
            ReadFromCallbacks = 0;
            ImgBufferEndPosition = 1;
            BufferStart[0] = 0;
        }
        else
        {
            ImgBufferEndPosition = n;
        }
    }

    public byte Get8()
    {
        if (ImgBufferPosition < ImgBufferEndPosition)
        {
            return ImgBuffer![ImgBufferPosition++];
        }

        if (ReadFromCallbacks != 0)
        {
            RefillBuffer();
            return ImgBuffer![ImgBufferPosition++];
        }

        return 0;
    }

    public ushort Get16BE()
    {
        var z = (int)Get8();
        return unchecked((ushort)((z << 8) + Get8()));
    }

    public void Skip(int count)
    {
        if (count == 0)
        {
            return;
        }

        if (count < 0)
        {
            ImgBuffer = ImgBufferEnd;
            ImgBufferPosition = ImgBufferEndPosition;
            return;
        }

        if (IO.Read is not null)
        {
            var blen = ImgBufferEndPosition - ImgBufferPosition;
            if (blen < count)
            {
                ImgBuffer = ImgBufferEnd;
                ImgBufferPosition = ImgBufferEndPosition;
                IO.Skip!.Invoke(IOUserData, count - blen);
                return;
            }
        }

        ImgBufferPosition += count;
    }

    public void StartMem(byte[] buffer, int len)
    {
        IO.Read = null;
        ReadFromCallbacks = 0;
        CallbackAlreadyRead = 0;
        ImgBufferOriginal = buffer;
        ImgBufferOriginalPosition = 0;
        ImgBuffer = buffer;
        ImgBufferPosition = 0;
        ImgBufferOriginalEnd = buffer;
        ImgBufferOriginalEndPosition = len;
        ImgBufferEnd = buffer;
        ImgBufferEndPosition = len;
    }

    public void StartCallbacks(IOCallbacks callbacks, object? userData)
    {
        IO = callbacks;
        IOUserData = userData;
        BufLen = Constants.BufferStartLength;
        ReadFromCallbacks = 1;
        CallbackAlreadyRead = 0;
        ImgBufferOriginal = BufferStart;
        ImgBufferOriginalPosition = 0;
        ImgBuffer = BufferStart;
        ImgBufferPosition = 0;
        RefillBuffer();
        ImgBufferOriginalEnd = ImgBufferEnd;
        ImgBufferOriginalEndPosition = ImgBufferEndPosition;
    }

    public bool AtEof()
    {
        if (IO.Read is not null)
        {
            if (!IO.Eof!(IOUserData))
            {
                return false;
            }

            if (ReadFromCallbacks == 0)
            {
                return true;
            }
        }

        return ImgBufferPosition >= ImgBufferEndPosition;
    }
}
