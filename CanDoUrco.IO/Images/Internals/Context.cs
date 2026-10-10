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

    public bool BmpTestRaw()
    {
        if (Get8() != 'B' || Get8() != 'M')
        {
            return false;
        }

        _ = Get32LE(); // file size
        _ = Get16LE(); // reserved1
        _ = Get16LE(); // reserved2
        _ = Get32LE(); // data offset

        var sz = Get32LE();
        return sz == 12 || sz == 40 || sz == 56 || sz == 108 || sz == 124;
    }

    public bool BmpTest()
    {
        var r = BmpTestRaw();
        Rewind();
        return r;
    }

    public byte[]? ParseBmpHeader(Bmp bmp)
    {
        if (Get8() != 'B' || Get8() != 'M')
        {
            return Methods.ErrorPtr("Not BMP. Corrupt BMP");
        }

        _ = Get32LE(); // file size
        _ = Get16LE(); // reserved1
        _ = Get16LE(); // reserved2
        bmp.Offset = unchecked((int)Get32LE());

        int hsz;
        bmp.Hsz = hsz = unchecked((int)Get32LE());
        bmp.MR = bmp.MG = bmp.MB = bmp.MA = 0;
        bmp.ExtraRead = 14;

        if (bmp.Offset < 0)
        {
            return Methods.ErrorPtr("Bad BMP. Bad BMP");
        }

        if (hsz != 12 && hsz != 40 && hsz != 56 && hsz != 108 && hsz != 124)
        {
            return Methods.ErrorPtr("Unknown BMP. BMP type not supported: unknown");
        }

        if (hsz == 12)
        {
            ImgX = Get16LE();
            ImgY = Get16LE();
        }
        else
        {
            ImgX = Get32LE();
            ImgY = Get32LE();
        }

        if (Get16LE() != 1)
        {
            return Methods.ErrorPtr("Bad BMP. Bad BMP.");
        }

        bmp.Bpp = Get16LE();
        if (hsz != 12)
        {
            var compressLevel = unchecked((int)Get32LE());
            if (compressLevel == 1 || compressLevel == 2)
            {
                return Methods.ErrorPtr("BMP RLE. BMP type not supported: RLE.");
            }

            if (compressLevel >= 4)
            {
                return Methods.ErrorPtr(
                    "BMP JPEG/PNG. BMP type not supported: unsupported compression."
                );
            }

            if (compressLevel == 3 && bmp.Bpp != 16 && bmp.Bpp != 32)
            {
                return Methods.ErrorPtr("Bad BMP. Bad BMP.");
            }

            _ = Get32LE(); // sizeof
            _ = Get32LE(); // hres
            _ = Get32LE(); // vres
            _ = Get32LE(); // colorsused
            _ = Get32LE(); // max important
            if (hsz == 40 || hsz == 56)
            {
                if (hsz == 56)
                {
                    _ = Get32LE(); // red mask
                    _ = Get32LE(); // green mask
                    _ = Get32LE(); // blue mask
                    _ = Get32LE(); // alpha mask
                }

                if (bmp.Bpp == 16 || bmp.Bpp == 32)
                {
                    if (compressLevel == 0)
                    {
                        bmp.SetMaskDefaults(unchecked((int)compressLevel));
                    }
                    else if (compressLevel == 3)
                    {
                        bmp.MR = Get32LE();
                        bmp.MG = Get32LE();
                        bmp.MB = Get32LE();
                        bmp.ExtraRead += 12;
                        if (bmp.MR == bmp.MG && bmp.MG == bmp.MB)
                        {
                            return Methods.ErrorPtr("Bad BMP. Bad BMP.");
                        }
                    }
                    else
                    {
                        return Methods.ErrorPtr("Bad BMP. Bad BMP.");
                    }
                }
            }
            else
            {
                if (hsz != 108 && hsz != 124)
                {
                    return Methods.ErrorPtr("Bad BMP. Bad BMP.");
                }

                bmp.MR = Get32LE();
                bmp.MG = Get32LE();
                bmp.MB = Get32LE();
                bmp.MA = Get32LE();
                if (compressLevel != 3)
                {
                    bmp.SetMaskDefaults(unchecked((int)compressLevel));
                }
                _ = Get32LE(); // color space
                for (var i = 0; i < 12; i++)
                {
                    _ = Get32LE(); // color space parameters
                }

                if (hsz == 124)
                {
                    _ = Get32LE(); // rendering intent
                    _ = Get32LE(); // offset of profile data
                    _ = Get32LE(); // size of profile data
                    _ = Get32LE(); // reserved
                }
            }
        }

        return [1];
    }

    public byte[]? BmpLoad(out int x, out int y, out int comp, int reqComp, ResultInfo _)
    {
        x = 0;
        y = 0;
        comp = 0;

        var bmp = new Bmp { AllA = 255 };
        if (ParseBmpHeader(bmp) is null)
        {
            return null;
        }

        var signedHeight = unchecked((int)ImgY);
        var flipVertically = signedHeight > 0;
        ImgY = (uint)long.Abs(signedHeight);
        if (ImgY > Constants.MaxDimensions || ImgX > Constants.MaxDimensions)
        {
            return Methods.ErrorPtr("Too large. Very large image (corrupt?).");
        }

        var mr = bmp.MR;
        var mg = bmp.MG;
        var mb = bmp.MB;
        var ma = bmp.MA;
        var allA = bmp.AllA;
        var pSize = 0;
        if (bmp.Hsz == 12)
        {
            if (bmp.Bpp < 24)
            {
                pSize = unchecked((bmp.Offset - bmp.ExtraRead - 24) / 3);
            }
        }
        else
        {
            if (bmp.Bpp < 16)
            {
                pSize = unchecked((bmp.Offset - bmp.ExtraRead - bmp.Hsz) >> 2);
            }
        }

        if (pSize == 0)
        {
            var bytesReadSoFar =
                CallbackAlreadyRead + (ImgBufferPosition - (long)ImgBufferOriginalPosition);

            const int headerLimit = 1024;
            const int extraDataLimit = 256 * 4;
            if (bytesReadSoFar <= 0 || bytesReadSoFar > headerLimit)
            {
                return Methods.ErrorPtr("Bad header. Corrupt BMP.");
            }

            if (bmp.Offset < bytesReadSoFar || bmp.Offset - bytesReadSoFar > extraDataLimit)
            {
                return Methods.ErrorPtr("Bad offset. Corrupt BMP.");
            }

            Skip((int)(bmp.Offset - bytesReadSoFar));
        }

        ImgN =
            bmp.Bpp == 24 && ma == 0xFF00_0000 ? 3
            : ma != 0 ? 4
            : 3;

        var target = reqComp != 0 && reqComp >= 3 ? reqComp : ImgN;
        if (!Methods.Mad3SizesValid(target, ImgX, ImgY))
        {
            return Methods.ErrorPtr("Too large. Corrupt BMP.");
        }

        var result = Methods.MallocMad3(target, ImgX, ImgY);
        if (result is null)
        {
            return null;
        }

        var pal = new byte[256, 4];
        var z = 0;
        int width;
        int pad;
        if (bmp.Bpp < 16)
        {
            if (pSize == 0 || pSize > 256)
            {
                return Methods.ErrorPtr("Invalid. Corrupt BMP.");
            }

            for (var i = 0; i < pSize; i++)
            {
                pal[i, 2] = Get8();
                pal[i, 1] = Get8();
                pal[i, 0] = Get8();
                if (bmp.Hsz != 12)
                {
                    Get8();
                }

                pal[i, 3] = 255;
            }

            Skip(bmp.Offset - bmp.ExtraRead - bmp.Hsz - pSize * (bmp.Hsz == 12 ? 3 : 4));
            if (bmp.Bpp == 1)
            {
                width = unchecked((int)((ImgX + 7) >> 3));
            }
            else if (bmp.Bpp == 4)
            {
                width = unchecked((int)((ImgX + 1) >> 1));
            }
            else if (bmp.Bpp == 8)
            {
                width = unchecked((int)(ImgX));
            }
            else
            {
                return Methods.ErrorPtr("Bad BPP. Corrupt BMP.");
            }

            pad = (-width) & 3;
            if (bmp.Bpp == 1)
            {
                for (var j = 0; j < unchecked((int)ImgY); j++)
                {
                    var bitOffset = 7;
                    var v = (int)Get8();
                    for (var i = 0; i < unchecked((int)ImgX); i++)
                    {
                        var color = (v >> bitOffset) & 0x1;
                        result[z++] = pal[color, 0];
                        result[z++] = pal[color, 1];
                        result[z++] = pal[color, 2];
                        if (target == 4)
                        {
                            result[z++] = 255;
                        }

                        if (i + 1 == unchecked((int)ImgX))
                        {
                            break;
                        }

                        if ((--bitOffset) < 0)
                        {
                            bitOffset = 7;
                            v = Get8();
                        }
                    }

                    Skip(pad);
                }
            }
            else
            {
                for (var j = 0; j < unchecked((int)ImgY); j++)
                {
                    for (var i = 0; i < unchecked((int)ImgX); i += 2)
                    {
                        var v = Get8();
                        var v2 = 0;
                        if (bmp.Bpp == 4)
                        {
                            v2 = v & 15;
                            v >>= 4;
                        }

                        result[z++] = pal[v, 0];
                        result[z++] = pal[v, 1];
                        result[z++] = pal[v, 2];
                        if (target == 4)
                        {
                            result[z++] = 255;
                        }

                        if (i + 1 == unchecked((int)ImgX))
                        {
                            break;
                        }

                        v = unchecked((byte)((bmp.Bpp == 8) ? Get8() : v2));
                        result[z++] = pal[v, 0];
                        result[z++] = pal[v, 1];
                        result[z++] = pal[v, 2];
                        if (target == 4)
                        {
                            result[z++] = 255;
                        }
                    }

                    Skip(pad);
                }
            }
        }
        else
        {
            Skip(bmp.Offset - bmp.ExtraRead - bmp.Hsz);
            width =
                bmp.Bpp == 24 ? unchecked((int)(3 * ImgX))
                : bmp.Bpp == 16 ? unchecked((int)(2 * ImgX))
                : 0;

            pad = (-width) & 3;
            var easy = 0;
            if (bmp.Bpp == 24)
            {
                easy = 1;
            }
            else if (bmp.Bpp == 32)
            {
                if (mb == 0xFF && mg == 0xFF00 && mr == 0x00FF_0000 && ma == 0xFF00_0000)
                {
                    easy = 2;
                }
            }

            var rShift = 0;
            var rCount = 0;
            var gShift = 0;
            var gCount = 0;
            var bShift = 0;
            var bCount = 0;
            var aShift = 0;
            var aCount = 0;
            if (easy == 0)
            {
                if (mr == 0 || mg == 0 || mb == 0)
                {
                    return Methods.ErrorPtr("Bad masks. Corrupt BMP.");
                }

                rShift = Methods.HighBit(mr) - 7;
                rCount = Methods.BitCount(mr);
                gShift = Methods.HighBit(mg) - 7;
                gCount = Methods.BitCount(mg);
                bShift = Methods.HighBit(mb) - 7;
                bCount = Methods.BitCount(mb);
                aShift = Methods.HighBit(ma) - 7;
                aCount = Methods.BitCount(ma);
                if (rCount > 8 || gCount > 8 || bCount > 8 || aCount > 8)
                {
                    return Methods.ErrorPtr("Bad masks. Corrupt BMP.");
                }
            }

            for (var j = 0; j < unchecked((int)ImgY); j++)
            {
                if (easy != 0)
                {
                    for (var i = 0; i < unchecked((int)ImgX); i++)
                    {
                        result[z + 2] = Get8();
                        result[z + 1] = Get8();
                        result[z + 0] = Get8();
                        z += 3;
                        var a = easy == 2 ? Get8() : (byte)255;
                        allA |= a;
                        if (target == 4)
                        {
                            result[z++] = a;
                        }
                    }
                }
                else
                {
                    var bpp = bmp.Bpp;
                    for (var i = 0; i < unchecked((int)ImgX); i++)
                    {
                        var v = bpp == 16 ? (uint)Get16LE() : Get32LE();
                        result[z++] = Methods.ByteCast(Methods.ShiftSigned(v & mr, rShift, rCount));
                        result[z++] = Methods.ByteCast(Methods.ShiftSigned(v & mg, gShift, gCount));
                        result[z++] = Methods.ByteCast(Methods.ShiftSigned(v & mb, bShift, bCount));
                        var a = ma != 0 ? Methods.ShiftSigned(v & ma, aShift, aCount) : 255;
                        allA |= unchecked((uint)a);
                        if (target == 4)
                        {
                            result[z++] = Methods.ByteCast(a);
                        }
                    }
                }

                Skip(pad);
            }
        }

        if (target == 4 && allA == 0)
        {
            for (var i = unchecked((int)(4 * ImgX * ImgY - 1)); i >= 0; i -= 4)
            {
                result[i] = 255;
            }
        }

        if (flipVertically)
        {
            Methods.VerticalFlip(result, (int)ImgX, (int)ImgY, target);
        }

        if (reqComp != 0 && reqComp != target)
        {
            result = Methods.ConvertFormat(result, target, reqComp, ImgX, ImgY);
            if (result == null)
            {
                return result;
            }
        }

        x = unchecked((int)ImgX);
        y = unchecked((int)ImgY);
        comp = ImgN;
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

        if (BmpTest())
        {
            return BmpLoad(out x, out y, out comp, reqComp, ri);
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

    public ushort Get16LE()
    {
        var z = (int)Get8();
        return unchecked((ushort)(z + (Get8() << 8)));
    }

    public uint Get32LE()
    {
        var low = Get16LE();
        var high = Get16LE();
        return unchecked(low | ((uint)high << 16));
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
