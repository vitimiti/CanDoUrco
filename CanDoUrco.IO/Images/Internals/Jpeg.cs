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
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace CanDoUrco.IO.Images.Internals;

internal sealed class Jpeg
{
    public Context Context { get; set; } = null!;
    public Huffman[] HuffDC { get; set; } = null!;
    public Huffman[] HuffAC { get; set; } = null!;
    public ushort[][] Dequant { get; set; } = null!;
    public int DequantPosition1 { get; set; }
    public int DequantPosition2 { get; set; }
    public short[][] FastAC { get; set; } = null!;
    public int FastACPosition1 { get; set; }
    public int FastACPosition2 { get; set; }
    public int ImgHMax { get; set; }
    public int ImgVMax { get; set; }
    public int ImgMcuX { get; set; }
    public int ImgMcuY { get; set; }
    public int ImgMcuW { get; set; }
    public int ImgMcuH { get; set; }
    public CompData[] ImgComp { get; set; } = null!;
    public uint CodeBuffer { get; set; }
    public int CodeBits { get; set; }
    public byte Marker { get; set; }
    public int NoMore { get; set; }
    public bool Progressive { get; set; }
    public int SpecStart { get; set; }
    public int SpecEnd { get; set; }
    public int SuccHigh { get; set; }
    public int SuccLow { get; set; }
    public int EobRun { get; set; }
    public int JFif { get; set; }
    public int App14ColorTransform { get; set; }
    public int Rgb { get; set; }
    public int ScanN { get; set; }
    public int[] Order { get; } = new int[4];
    public int OrderPosition { get; set; }
    public int RestartInterval { get; set; }
    public int Todo { get; set; }
    public IdctKernel IdctBlockKernel { get; set; } = null!;
    public YCbCrToRgbKernelFunc YCbCrToRgbKernel { get; set; } = null!;
    public ResampleFunc ResampleRowHV2Kernel { get; set; } = null!;

    public byte[]? LoadImage(out int outX, out int outY, out int comp, int reqComp)
    {
        outX = 0;
        outY = 0;
        comp = 0;
        return LoadImageCore(ref outX, ref outY, ref comp, reqComp);
    }

    private byte[]? LoadImageCore(ref int outX, ref int outY, ref int comp, int reqComp)
    {
        Context.ImgN = 0;
        if (reqComp is < 0 or > 4)
        {
            return Methods.ErrorPtr("Bad required component count. Internal error.");
        }

        if (!DecodeImage())
        {
            Cleanup();
            return null;
        }

        var n =
            reqComp != 0 ? reqComp
            : Context.ImgN >= 3 ? 3
            : 1;

        var isRgb = Context.ImgN == 3 && (Rgb == 3 || (App14ColorTransform == 0 && JFif == 0));

        int decodeN = Context.ImgN == 3 && n < 3 && !isRgb ? 1 : Context.ImgN;
        if (decodeN <= 0)
        {
            Cleanup();
            return null;
        }

        {
            byte[] output;
            var coutput = new (byte[] Data, int Offset)[4];
            var resComp = new Resample[4];
            for (var k = 0; k < decodeN; k++)
            {
                var r = resComp[k] = new Resample();
                ImgComp[k].LineBuf = new byte[unchecked((int)Context.ImgX) + 3];

                r.HS = ImgHMax / ImgComp[k].H;
                r.VS = ImgVMax / ImgComp[k].V;
                r.YStep = r.VS >> 1;
                r.WLores = unchecked((int)((Context.ImgX + r.HS - 1) / r.HS));
                r.YPos = 0;
                r.Line0 = r.Line1 = ImgComp[k].Data;
                r.Line0Position = r.Line1Position = 0;
                r.ResampleAction = r.HS switch
                {
                    1 when r.VS == 1 => Methods.ResampleRow1,
                    1 when r.VS == 2 => Methods.ResampleRowV2,
                    2 when r.VS == 1 => Methods.ResampleRowH2,
                    2 when r.VS == 2 => ResampleRowHV2Kernel,
                    _ => Methods.ResampleRowGeneric,
                };
            }

            output = new byte[unchecked((int)(n * Context.ImgX * Context.ImgY)) + 1];
            for (var j = 0; j < Context.ImgY; j++)
            {
                Span<byte> @out = output.AsSpan(unchecked((int)(n * Context.ImgX)) * j);
                for (var k = 0; k < decodeN; k++)
                {
                    var r = resComp[k];
                    var yBot = r.YStep >= (r.VS >> 1);
                    coutput[k] = r.ResampleAction(
                        ImgComp[k].LineBuf!,
                        yBot ? r.Line1! : r.Line0!,
                        yBot ? r.Line1Position : r.Line0Position,
                        yBot ? r.Line0! : r.Line1!,
                        yBot ? r.Line0Position : r.Line1Position,
                        r.WLores,
                        r.HS
                    );

                    if (++r.YStep >= r.VS)
                    {
                        r.YStep = 0;
                        r.Line0 = r.Line1;
                        r.Line0Position = r.Line1Position;
                        if (++r.YPos < ImgComp[k].Y)
                        {
                            r.Line1Position += ImgComp[k].W2;
                        }
                    }
                }

                ReadOnlySpan<byte> c0 = coutput[0].Data.AsSpan(coutput[0].Offset);
                ReadOnlySpan<byte> c1 =
                    decodeN > 1 ? coutput[1].Data.AsSpan(coutput[1].Offset) : default;

                ReadOnlySpan<byte> c2 =
                    decodeN > 2 ? coutput[2].Data.AsSpan(coutput[2].Offset) : default;

                ReadOnlySpan<byte> c3 =
                    decodeN > 3 ? coutput[3].Data.AsSpan(coutput[3].Offset) : default;

                if (n >= 3)
                {
                    var y = c0;
                    if (Context.ImgN == 3)
                    {
                        if (isRgb)
                        {
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                @out[0] = y[i];
                                @out[1] = c1[i];
                                @out[2] = c2[i];
                                @out[3] = 255;
                                @out = @out[n..];
                            }
                        }
                        else
                        {
                            YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)Context.ImgX), n);
                        }
                    }
                    else if (Context.ImgN == 4)
                    {
                        if (App14ColorTransform == 0)
                        {
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                var m = c3[i];
                                @out[0] = Methods.Blinn8x8(c0[i], m);
                                @out[1] = Methods.Blinn8x8(c1[i], m);
                                @out[2] = Methods.Blinn8x8(c2[i], m);
                                @out[3] = 255;
                                @out = @out[n..];
                            }
                        }
                        else if (App14ColorTransform == 2)
                        {
                            YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)Context.ImgX), n);
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                var m = c3[i];
                                @out[0] = Methods.Blinn8x8(unchecked((byte)(255 - @out[0])), m);
                                @out[1] = Methods.Blinn8x8(unchecked((byte)(255 - @out[1])), m);
                                @out[2] = Methods.Blinn8x8(unchecked((byte)(255 - @out[2])), m);
                                @out = @out[n..];
                            }
                        }
                        else
                        {
                            YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)Context.ImgX), n);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < Context.ImgX; i++)
                        {
                            @out[0] = @out[1] = @out[2] = y[i];
                            @out[3] = 255;
                            @out = @out[n..];
                        }
                    }
                }
                else
                {
                    if (isRgb)
                    {
                        if (n == 1)
                        {
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                @out[0] = Methods.ComputeY(c0[i], c1[i], c2[i]);
                                @out = @out[1..];
                            }
                        }
                        else
                        {
                            for (var i = 0; i < Context.ImgX; i++, @out = @out[2..])
                            {
                                @out[0] = Methods.ComputeY(c0[i], c1[i], c2[i]);
                                @out[1] = 255;
                            }
                        }
                    }
                    else if (Context.ImgN == 4 && App14ColorTransform == 0)
                    {
                        for (var i = 0; i < Context.ImgX; i++)
                        {
                            var m = c3[i];
                            var r = Methods.Blinn8x8(c0[i], m);
                            var g = Methods.Blinn8x8(c1[i], m);
                            var b = Methods.Blinn8x8(c2[i], m);
                            @out[0] = Methods.ComputeY(r, g, b);
                            @out[1] = 255;
                            @out = @out[n..];
                        }
                    }
                    else if (Context.ImgN == 4 && App14ColorTransform == 2)
                    {
                        for (var i = 0; i < Context.ImgX; i++)
                        {
                            @out[0] = Methods.Blinn8x8(unchecked((byte)(255 - c0[i])), c3[i]);

                            @out[1] = 255;
                            @out = @out[n..];
                        }
                    }
                    else
                    {
                        var y = c0;
                        if (n == 1)
                        {
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                @out[i] = y[i];
                            }
                        }
                        else
                        {
                            for (var i = 0; i < Context.ImgX; i++)
                            {
                                @out[0] = y[i];
                                @out[1] = 255;
                                @out = @out[2..];
                            }
                        }
                    }
                }
            }

            Cleanup();
            outX = unchecked((int)Context.ImgX);
            outY = unchecked((int)Context.ImgY);
            comp = Context.ImgN >= 3 ? 3 : 1;

            return output;
        }
    }

    public bool AllocTables()
    {
        HuffDC = new Huffman[Constants.JpegFixedArrayLength];
        HuffAC = new Huffman[Constants.JpegFixedArrayLength];
        Dequant = new ushort[Constants.JpegFixedArrayLength][];
        FastAC = new short[Constants.JpegFixedArrayLength][];
        ImgComp = new Jpeg.CompData[Constants.JpegFixedArrayLength];
        for (var i = 0; i < Constants.JpegFixedArrayLength; i++)
        {
            HuffDC[i] = new Huffman();
            HuffAC[i] = new Huffman();
            ImgComp[i] = new Jpeg.CompData();
            Dequant[i] = new ushort[Constants.JpegDequantLength];
            FastAC[i] = new short[Constants.JpegACLength];
        }

        return true;
    }

    public void GrowBuffer()
    {
        do
        {
            var b = (uint)(NoMore != 0 ? 0 : Context.Get8());
            if (b == 0xFF)
            {
                var c = (int)Context.Get8();
                while (c == 0xFF)
                {
                    c = Context.Get8();
                }

                if (c != 0)
                {
                    Marker = (byte)c;
                    NoMore = 1;
                    return;
                }
            }

            CodeBuffer |= b << (24 - CodeBits);
            CodeBits += 8;
        } while (CodeBits <= 24);
    }

    public byte SkipJunkAtEnd()
    {
        while (!Context.AtEof())
        {
            var x = Context.Get8();
            while (x == 0xFF)
            {
                if (Context.AtEof())
                {
                    return Constants.MarkerNone;
                }

                x = Context.Get8();
                if (x != 0x00 && x != 0xFF)
                {
                    return x;
                }
            }
        }

        return Constants.MarkerNone;
    }

    public int HuffmanDecode(Huffman huffman)
    {
        if (CodeBits < 16)
        {
            GrowBuffer();
        }

        var c = unchecked(
            (int)((CodeBuffer >> (32 - Constants.FastBits)) & ((1 << Constants.FastBits) - 1))
        );

        var k = (int)huffman.Fast[c];
        if (k < 255)
        {
            var s = (int)huffman.Size[k];
            if (s > CodeBits)
            {
                return -1;
            }

            CodeBuffer <<= s;
            CodeBits -= s;
            return huffman.Values[k];
        }

        var temp = unchecked(CodeBuffer >> 16);
        for (k = Constants.FastBits + 1; ; k++)
        {
            if (temp < huffman.MaxCode[k])
            {
                break;
            }
        }

        if (k == 17)
        {
            CodeBits -= 16;
            return -1;
        }

        if (k > CodeBits)
        {
            return -1;
        }

        c = unchecked((int)(((CodeBuffer >> (32 - k)) & Constants.BitsMask[k]) + huffman.Delta[k]));
        if (c < 0 || c >= 256)
        {
            return -1;
        }

        Debug.Assert(
            ((CodeBuffer >> (32 - huffman.Size[c])) & Constants.BitsMask[huffman.Size[c]])
                == huffman.Code[c]
        );

        CodeBits -= k;
        CodeBuffer <<= k;
        return huffman.Values[c];
    }

    public int ExtendReceive(int count)
    {
        if (CodeBits < count)
        {
            GrowBuffer();
        }

        if (CodeBits < count)
        {
            return 0;
        }

        var sgn = unchecked((int)(CodeBuffer >> 31));
        var k = BitOperations.RotateLeft(CodeBuffer, count);
        CodeBuffer = k & ~Constants.BitsMask[count];
        k &= Constants.BitsMask[count];
        CodeBits -= count;
        return unchecked((int)(k + (Constants.JpegBias[count] & (sgn - 1))));
    }

    public int GetBits(int count)
    {
        if (CodeBits < count)
        {
            GrowBuffer();
        }

        if (CodeBits < count)
        {
            return 0;
        }

        var k = BitOperations.RotateLeft(CodeBuffer, count);
        CodeBuffer = k & ~Constants.BitsMask[count];
        k &= Constants.BitsMask[count];
        CodeBits -= count;
        return unchecked((int)k);
    }

    public int GetBit()
    {
        if (CodeBits < 1)
        {
            GrowBuffer();
        }

        if (CodeBits < 1)
        {
            return 0;
        }

        var k = CodeBuffer;
        CodeBuffer <<= 1;
        --CodeBits;
        return unchecked((int)(k & 0x8000_0000));
    }

    public bool DecodeBlockProgDC(Span<short> data, Huffman hdc, int b)
    {
        if (SpecEnd != 0)
        {
            return Methods.Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        if (CodeBits < 16)
        {
            GrowBuffer();
        }

        if (SuccHigh == 0)
        {
            data[..64].Clear();
            var t = HuffmanDecode(hdc);
            if (t < 0 || t > 15)
            {
                return Methods.Error("Can't merge DC and AC. Corrupt JPEG.");
            }

            var diff = t != 0 ? ExtendReceive(t) : 0;
            if (!Methods.AddIntsValid(ImgComp[b].DCPred, diff))
            {
                return Methods.Error("Bad delta. Corrupt JPEG.");
            }

            var dc = ImgComp[b].DCPred + diff;
            ImgComp[b].DCPred = dc;
            if (!Methods.Mul2ShortsValid(dc, 1 << SuccLow))
            {
                return Methods.Error("Can't merge DC and AC. Corrupt JPEG.");
            }

            data[0] = unchecked((short)(dc * (1 << SuccLow)));
        }
        else
        {
            if (GetBit() != 0)
            {
                data[0] += unchecked((short)(1 << SuccLow));
            }
        }

        return true;
    }

    public bool DecodeBlockProgAC(Span<short> data, Huffman hac, short[] fac)
    {
        if (SpecStart == 0)
        {
            return Methods.Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        if (SuccHigh == 0)
        {
            var shift = SuccLow;
            if (EobRun != 0)
            {
                --EobRun;
                return true;
            }

            var k = SpecStart;
            do
            {
                if (CodeBits < 16)
                {
                    GrowBuffer();
                }

                var c = unchecked(
                    (int)(
                        (CodeBuffer >> (32 - Constants.FastBits)) & ((1 << Constants.FastBits) - 1)
                    )
                );

                var r = (int)fac[c];
                if (r != 0)
                {
                    k += (r >> 4) & 15;
                    var s = r & 15;
                    if (s > CodeBits)
                    {
                        return Methods.Error(
                            "Bad Huffman code. Combined length longer than code bits available."
                        );
                    }

                    CodeBuffer <<= s;
                    CodeBits -= s;
                    var zig = (int)Constants.JpegDezigzag[k++];
                    data[zig] = unchecked((short)((r >> 8) * (1 << shift)));
                }
                else
                {
                    var rs = HuffmanDecode(hac);
                    if (rs < 0)
                    {
                        return Methods.Error("Bad Huffman code. Corrupt JPEG.");
                    }

                    var s = rs & 15;
                    r = rs >> 4;
                    if (s == 0)
                    {
                        if (r < 15)
                        {
                            EobRun = 1 << r;
                            if (r != 0)
                            {
                                EobRun += GetBits(r);
                            }

                            --EobRun;
                            break;
                        }

                        k += 16;
                    }
                    else
                    {
                        k += r;
                        var zig = (int)Constants.JpegDezigzag[k++];
                        data[zig] = unchecked((short)(ExtendReceive(s) * (1 << shift)));
                    }
                }
            } while (k <= SpecEnd);
        }
        else
        {
            var bit = unchecked((short)(1 << SuccLow));
            if (EobRun != 0)
            {
                --EobRun;
                for (var k = SpecStart; k <= SpecEnd; k++)
                {
                    ref var p = ref data[Constants.JpegDezigzag[k]];
                    if (p != 0)
                    {
                        if (GetBit() != 0)
                        {
                            if ((p & bit) == 0)
                            {
                                if (p > 0)
                                {
                                    p += bit;
                                }
                                else
                                {
                                    p -= bit;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                var k = SpecStart;
                do
                {
                    var rs = HuffmanDecode(hac);
                    if (rs < 0)
                    {
                        return Methods.Error("Bad Huffman code. Corrupt JPEG.");
                    }

                    var s = rs & 15;
                    var r = rs >> 4;
                    if (s == 0)
                    {
                        if (r < 15)
                        {
                            EobRun = (1 << r) - 1;
                            if (r != 0)
                            {
                                EobRun += GetBits(r);
                            }

                            r = 64;
                        }
                    }
                    else
                    {
                        if (s != 1)
                        {
                            return Methods.Error("Bad Huffman code. Corrupt JPEG.");
                        }

                        s = GetBit() != 0 ? bit : -bit;
                    }

                    while (k <= SpecEnd)
                    {
                        ref var p = ref data[Constants.JpegDezigzag[k++]];
                        if (p != 0)
                        {
                            if (GetBit() != 0)
                            {
                                if ((p & bit) == 0)
                                {
                                    if (p > 0)
                                    {
                                        p += bit;
                                    }
                                    else
                                    {
                                        p -= bit;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (r == 0)
                            {
                                p = unchecked((short)s);
                                break;
                            }

                            --r;
                        }
                    }
                } while (k <= SpecEnd);
            }
        }

        return true;
    }

    public void Setup()
    {
        if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
        {
            IdctBlockKernel = Methods.IdctSimd;
            YCbCrToRgbKernel = Methods.YCbCrToRgbSimd;
            ResampleRowHV2Kernel = Methods.ResampleRowHV2Simd;
        }
        else
        {
            IdctBlockKernel = Methods.IdctBlock;
            YCbCrToRgbKernel = Methods.YCbCrToRgbRow;
            ResampleRowHV2Kernel = Methods.ResampleRowHV2;
        }
    }

    public byte GetMarker()
    {
        byte x;
        if (Marker != Constants.MarkerNone)
        {
            x = Marker;
            Marker = Constants.MarkerNone;
            return x;
        }

        x = Context.Get8();
        if (x != 0xFF)
        {
            return Constants.MarkerNone;
        }

        while (x == 0xFF)
        {
            x = Context.Get8();
        }

        return x;
    }

    public bool ProcessMarker(int m)
    {
        int l;
        switch (m)
        {
            case Constants.MarkerNone:
                return Methods.Error("Expected marker. Corrupt JPEG.");
            case 0xDD:
                if (Context.Get16BE() != 4)
                {
                    return Methods.Error("Invalid DRI length. Corrupt JPEG.");
                }

                RestartInterval = Context.Get16BE();
                return true;
            case 0xDB:
                l = Context.Get16BE() - 2;
                while (l > 0)
                {
                    var q = (int)Context.Get8();
                    var p = q >> 4;
                    var sixteen = p != 0;
                    var t = q & 15;
                    if (p != 0 && p != 1)
                    {
                        return Methods.Error("Bad DQT type. Corrupt JPEG.");
                    }

                    if (t > 3)
                    {
                        return Methods.Error("Bad DQT table. Corrupt JPEG.");
                    }

                    for (var i = 0; i < 64; i++)
                    {
                        Dequant[t][Constants.JpegDezigzag[i]] = sixteen
                            ? Context.Get16BE()
                            : Context.Get8();
                    }

                    l -= sixteen ? 129 : 65;
                }

                return l == 0;
            case 0xC4:
                l = Context.Get16BE() - 2;
                while (l > 0)
                {
                    byte[] v;
                    Span<int> sizes = stackalloc int[16];
                    var n = 0;
                    var q = (int)Context.Get8();
                    var tc = q >> 4;
                    var th = q & 15;
                    if (tc > 1 || th > 3)
                    {
                        return Methods.Error("Bad DHT header. Corrupt JPEG.");
                    }

                    for (var i = 0; i < 16; i++)
                    {
                        sizes[i] = Context.Get8();
                        n += sizes[i];
                    }

                    if (n > 256)
                    {
                        return Methods.Error("Bad DHT header. Corrupt JPEG.");
                    }

                    l -= 17;
                    if (tc == 0)
                    {
                        if (!HuffDC[th].Build(sizes))
                        {
                            return false;
                        }

                        v = HuffDC[th].Values;
                    }
                    else
                    {
                        if (!HuffAC[th].Build(sizes))
                        {
                            return false;
                        }

                        v = HuffAC[th].Values;
                    }

                    for (var i = 0; i < n; i++)
                    {
                        v[i] = Context.Get8();
                    }

                    if (tc != 0)
                    {
                        HuffAC[th].BuildFastAC(FastAC[th]);
                    }

                    l -= n;
                }
                return l == 0;
        }

        if ((m >= 0xE0 && m <= 0xEF) || m == 0xFE)
        {
            l = Context.Get16BE();
            if (l < 2)
            {
                return m == 0xFE
                    ? Methods.Error("Bad COM length. Corrupt JPEG.")
                    : Methods.Error("Bad APP length. Corrupt JPEG.");
            }

            l -= 2;
            if (m == 0xE0 && l >= 5)
            {
                var tag = "JFIF\0"u8;
                var ok = true;
                for (var i = 0; i < 5; i++)
                {
                    if (Context.Get8() != tag[i])
                    {
                        ok = false;
                    }
                }

                l -= 5;
                if (ok)
                {
                    JFif = 1;
                }
            }
            else if (m == 0xEE && l >= 12)
            {
                var tag = "Adobe\0"u8;
                var ok = true;
                for (var i = 0; i < 6; i++)
                {
                    if (Context.Get8() != tag[i])
                    {
                        ok = false;
                    }
                }

                l -= 6;
                if (ok)
                {
                    _ = Context.Get8(); // version
                    _ = Context.Get16BE(); // flags0
                    _ = Context.Get16BE(); // flags1
                    App14ColorTransform = Context.Get8();
                    l -= 6;
                }
            }

            Context.Skip(l);
            return true;
        }

        return Methods.Error("Unknown marker. Corrupt JPEG.");
    }

    public bool ProcessFrameHeader(Scan scan)
    {
        var context = Context;
        var hMax = 1;
        var vMax = 1;
        var lf = context.Get16BE();
        if (lf < 11)
        {
            return Methods.Error("Bad SOF length. Corrupt JPEG.");
        }

        var p = (int)context.Get8();
        if (p != 8)
        {
            return Methods.Error("Only 8-bit. JPEG format not supported: 8-bit only.");
        }

        context.ImgY = context.Get16BE();
        if (context.ImgY == 0)
        {
            return Methods.Error("No Header height. JPEG format not supported: delayed height.");
        }

        context.ImgX = context.Get16BE();
        if (context.ImgX == 0)
        {
            return Methods.Error("0 width. Corrupt JPEG.");
        }

        if (context.ImgY > Constants.MaxDimensions || context.ImgX > Constants.MaxDimensions)
        {
            return Methods.Error("Too large. Very large image (corrupt?).");
        }

        var c = (int)context.Get8();
        if (c != 3 && c != 1 && c != 4)
        {
            return Methods.Error("Bad component count. Corrupt JPEG.");
        }

        context.ImgN = c;
        for (var i = 0; i < c; i++)
        {
            ImgComp[i].Data = null;
            ImgComp[i].LineBuf = null;
        }

        if (lf != 8 + 3 * context.ImgN)
        {
            return Methods.Error("Bad SOF length. Corrupt JPEG.");
        }

        Rgb = 0;
        for (var i = 0; i < context.ImgN; i++)
        {
            var rgb = "RGB"u8;
            ImgComp[i].ID = context.Get8();
            if (context.ImgN == 3 && ImgComp[i].ID == rgb[i])
            {
                ++Rgb;
            }

            var q = context.Get8();
            ImgComp[i].H = q >> 4;
            if (ImgComp[i].H == 0 || ImgComp[i].H > 4)
            {
                return Methods.Error("Bad H. Corrupt JPEG.");
            }

            ImgComp[i].V = q & 15;
            if (ImgComp[i].V == 0 || ImgComp[i].V > 4)
            {
                return Methods.Error("Bad V. Corrupt JPEG.");
            }

            ImgComp[i].TQ = context.Get8();
            if (ImgComp[i].TQ > 3)
            {
                return Methods.Error("Bad TQ. Corrupt JPEG.");
            }
        }

        if (scan is not Scan.Load)
        {
            return true;
        }

        if (context.ImgX * context.ImgY * context.ImgN > int.MaxValue)
        {
            return Methods.Error("Too large. Image too large to decode.");
        }

        for (var i = 0; i < context.ImgN; i++)
        {
            if (ImgComp[i].H > hMax)
            {
                hMax = ImgComp[i].H;
            }

            if (ImgComp[i].V > vMax)
            {
                vMax = ImgComp[i].V;
            }
        }

        for (var i = 0; i < context.ImgN; i++)
        {
            if (hMax % ImgComp[i].H != 0)
            {
                return Methods.Error("Bad H. Corrupt JPEG.");
            }

            if (vMax % ImgComp[i].V != 0)
            {
                return Methods.Error("Bad V. Corrupt JPEG.");
            }
        }

        ImgHMax = hMax;
        ImgVMax = vMax;
        ImgMcuW = hMax * 8;
        ImgMcuH = vMax * 8;
        ImgMcuX = unchecked((int)((context.ImgX + ImgMcuW - 1) / ImgMcuW));
        ImgMcuY = unchecked((int)((context.ImgY + ImgMcuH - 1) / ImgMcuH));
        for (var i = 0; i < context.ImgN; i++)
        {
            ImgComp[i].X = unchecked((int)((context.ImgX * ImgComp[i].H + hMax - 1) / hMax));
            ImgComp[i].Y = unchecked((int)((context.ImgY * ImgComp[i].V + vMax - 1) / vMax));
            ImgComp[i].W2 = ImgMcuX * ImgComp[i].H * 8;
            ImgComp[i].H2 = ImgMcuY * ImgComp[i].V * 8;
            ImgComp[i].Coeff = null;
            ImgComp[i].RawCoeff = null;
            ImgComp[i].LineBuf = null;
            ImgComp[i].RawData = new byte[ImgComp[i].W2 * ImgComp[i].H2 + 15];
            ImgComp[i].Data = ImgComp[i].RawData;
            if (Progressive)
            {
                ImgComp[i].CoeffW = ImgComp[i].W2 / 8;
                ImgComp[i].CoeffH = ImgComp[i].H2 / 8;
                ImgComp[i].Coeff = new short[ImgComp[i].W2 * ImgComp[i].H2 + 15];
            }
        }

        return true;
    }

    public bool DecodeHeader(Scan scan)
    {
        JFif = 0;
        App14ColorTransform = -1;
        Marker = Constants.MarkerNone;
        var m = (int)GetMarker();
        if (!Methods.Soi(m))
        {
            return Methods.Error("No SOI. Corrupt JPEG.");
        }

        if (scan is Scan.Type)
        {
            return true;
        }

        m = GetMarker();
        while (!Methods.Sof(m))
        {
            if (!ProcessMarker(m))
            {
                return false;
            }

            m = GetMarker();
            while (m == Constants.MarkerNone)
            {
                if (Context.AtEof())
                {
                    return Methods.Error("No SOF. Corrupt JPEG.");
                }

                m = GetMarker();
            }
        }

        Progressive = Methods.SofProgressive(m);
        return ProcessFrameHeader(scan);
    }

    public bool ProcessScanHeader()
    {
        var ls = Context.Get16BE();
        ScanN = Context.Get8();
        if (ScanN < 1 || ScanN > 4 || ScanN > Context.ImgN)
        {
            return Methods.Error("Bad SOS component count. Corrupt JPEG.");
        }

        if (ls != 6 + 2 * ScanN)
        {
            return Methods.Error("Bad SOS length. Corrupt JPG.");
        }

        for (var i = 0; i < ScanN; i++)
        {
            var id = (int)Context.Get8();
            var q = (int)Context.Get8();
            int which;
            for (which = 0; which < Context.ImgN; which++)
            {
                if (ImgComp[which].ID == id)
                {
                    break;
                }
            }

            if (which == Context.ImgN)
            {
                return false;
            }

            ImgComp[which].HD = q >> 4;
            if (ImgComp[which].HD > 3)
            {
                return Methods.Error("Bad DC Huffman. Corrupt JPEG.");
            }

            ImgComp[which].HA = q & 15;
            if (ImgComp[which].HA > 3)
            {
                return Methods.Error("Bad AC Huffman. Corrupt JPEG.");
            }

            Order[i] = which;
        }

        {
            SpecStart = Context.Get8();
            SpecEnd = Context.Get8();
            var aa = (int)Context.Get8();
            SuccHigh = aa >> 4;
            SuccLow = aa & 15;
            if (Progressive)
            {
                if (
                    SpecStart > 63
                    || SpecEnd > 63
                    || SpecStart > SpecEnd
                    || SuccHigh > 13
                    || SuccLow > 13
                )
                {
                    return Methods.Error("Bad SOS. Corrupt JPEG.");
                }
            }
            else
            {
                if (SpecStart != 0)
                {
                    return Methods.Error("Bad SOS. Corrupt JPEG.");
                }

                if (SuccHigh != 0 || SuccLow != 0)
                {
                    return Methods.Error("Bad SOS. Corrupt JPEG.");
                }

                SpecEnd = 63;
            }
        }

        return true;
    }

    public void Reset()
    {
        CodeBits = 0;
        CodeBuffer = 0;
        NoMore = 0;
        ImgComp[0].DCPred = ImgComp[1].DCPred = ImgComp[2].DCPred = ImgComp[3].DCPred = 0;

        Marker = Constants.MarkerNone;
        Todo = RestartInterval != 0 ? RestartInterval : 0x7FFF_FFFF;
        EobRun = 0;
    }

    public bool DecodeBlock(
        Span<short> data,
        Huffman hdc,
        Huffman hac,
        short[] fac,
        int block,
        ushort[] dequant
    )
    {
        if (CodeBits < 16)
        {
            GrowBuffer();
        }

        var t = HuffmanDecode(hdc);
        if (t < 0 || t > 15)
        {
            return Methods.Error("Bad huffman code. Corrupt JPEG.");
        }

        data[..64].Clear();
        var diff = t != 0 ? ExtendReceive(t) : 0;
        if (!Methods.AddIntsValid(ImgComp[block].DCPred, diff))
        {
            return Methods.Error("Bad delta. Corrupt JPEG.");
        }

        var dc = ImgComp[block].DCPred + diff;
        ImgComp[block].DCPred = dc;
        if (!Methods.Mul2ShortsValid(dc, dequant[0]))
        {
            return Methods.Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        data[0] = unchecked((short)((nint)dc * dequant[0]));
        var k = 1;
        do
        {
            if (CodeBits < 16)
            {
                GrowBuffer();
            }

            var c = unchecked(
                (int)((CodeBuffer >> (32 - Constants.FastBits)) & ((1 << Constants.FastBits) - 1))
            );

            var r = (int)fac[c];
            if (r != 0)
            {
                k += (r >> 4) & 15;
                var s = r & 15;
                if (s > CodeBits)
                {
                    return Methods.Error(
                        "Bad Huffman code. Combined length longer than code bits available."
                    );
                }

                CodeBuffer <<= s;
                CodeBits -= s;
                var zig = Constants.JpegDezigzag[k++];
                data[zig] = unchecked((short)((r >> 8) * dequant[zig]));
            }
            else
            {
                var rs = HuffmanDecode(hac);
                if (rs < 0)
                {
                    return Methods.Error("Bad Huffman code. Corrupt JPEG.");
                }

                var s = rs & 15;
                r = rs >> 4;
                if (s == 0)
                {
                    if (rs != 0xF0)
                    {
                        break;
                    }

                    k += 16;
                }
                else
                {
                    k += r;
                    var zig = (int)Constants.JpegDezigzag[k++];
                    data[zig] = unchecked((short)(ExtendReceive(s) * dequant[zig]));
                }
            }
        } while (k < 64);

        return true;
    }

    public bool ParseEntropyCodedData()
    {
        Reset();
        if (!Progressive)
        {
            if (ScanN == 1)
            {
                Span<short> data = stackalloc short[64];
                var n = Order[0];
                var w = (ImgComp[n].X + 7) >> 3;
                var h = (ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var ha = ImgComp[n].HA;
                        if (
                            !DecodeBlock(
                                data,
                                HuffDC[ImgComp[n].HD],
                                HuffAC[ha],
                                FastAC[ha],
                                n,
                                Dequant[ImgComp[n].TQ]
                            )
                        )
                        {
                            return false;
                        }

                        IdctBlockKernel(
                            ImgComp[n].Data.AsSpan(ImgComp[n].W2 * j * 8 + i * 8),
                            ImgComp[n].W2,
                            data
                        );

                        if (--Todo <= 0)
                        {
                            if (CodeBits < 24)
                            {
                                GrowBuffer();
                            }

                            if (!Methods.Restart(Marker))
                            {
                                return true;
                            }

                            Reset();
                        }
                    }
                }

                return true;
            }
            else
            {
                Span<short> data = stackalloc short[64];
                for (var j = 0; j < ImgMcuY; j++)
                {
                    for (var i = 0; i < ImgMcuX; i++)
                    {
                        for (var k = 0; k < ScanN; k++)
                        {
                            var n = Order[k];
                            for (var y = 0; y < ImgComp[n].V; y++)
                            {
                                for (var x = 0; x < ImgComp[n].H; x++)
                                {
                                    var x2 = (i * ImgComp[n].H + x) * 8;
                                    var y2 = (j * ImgComp[n].V + y) * 8;
                                    var ha = ImgComp[n].HA;
                                    if (
                                        !DecodeBlock(
                                            data,
                                            HuffDC[ImgComp[n].HD],
                                            HuffAC[ha],
                                            FastAC[ha],
                                            n,
                                            Dequant[ImgComp[n].TQ]
                                        )
                                    )
                                    {
                                        return false;
                                    }

                                    IdctBlockKernel(
                                        ImgComp[n].Data.AsSpan(ImgComp[n].W2 * y2 + x2),
                                        ImgComp[n].W2,
                                        data
                                    );
                                }
                            }
                        }

                        if (--Todo <= 0)
                        {
                            if (CodeBits < 24)
                            {
                                GrowBuffer();
                            }

                            if (!Methods.Restart(Marker))
                            {
                                return true;
                            }

                            Reset();
                        }
                    }
                }

                return true;
            }
        }
        else
        {
            if (ScanN == 1)
            {
                var n = Order[0];
                var w = (ImgComp[n].X + 7) >> 3;
                var h = (ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var data = ImgComp[n].Coeff.AsSpan(64 * (i + j * ImgComp[n].CoeffW));
                        if (SpecStart == 0)
                        {
                            if (!DecodeBlockProgDC(data, HuffDC[ImgComp[n].HD], n))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var ha = ImgComp[n].HA;
                            if (!DecodeBlockProgAC(data, HuffAC[ha], FastAC[ha]))
                            {
                                return false;
                            }
                        }

                        if (--Todo <= 0)
                        {
                            if (CodeBits < 24)
                            {
                                GrowBuffer();
                            }

                            if (!Methods.Restart(Marker))
                            {
                                return true;
                            }

                            Reset();
                        }
                    }
                }

                return true;
            }
            else
            {
                for (var j = 0; j < ImgMcuY; j++)
                {
                    for (var i = 0; i < ImgMcuX; i++)
                    {
                        for (var k = 0; k < ScanN; k++)
                        {
                            var n = Order[k];
                            for (var y = 0; y < ImgComp[n].V; y++)
                            {
                                for (var x = 0; x < ImgComp[n].H; x++)
                                {
                                    var x2 = i * ImgComp[n].H + x;
                                    var y2 = j * ImgComp[n].V + y;
                                    var data = ImgComp[n]
                                        .Coeff.AsSpan(64 * (x2 + y2 * ImgComp[n].CoeffW));
                                    if (!DecodeBlockProgDC(data, HuffDC[ImgComp[n].HD], n))
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                        if (--Todo <= 0)
                        {
                            if (CodeBits < 24)
                            {
                                GrowBuffer();
                            }

                            if (!Methods.Restart(Marker))
                            {
                                return true;
                            }

                            Reset();
                        }
                    }
                }

                return true;
            }
        }
    }

    public void Finish()
    {
        if (Progressive)
        {
            for (var n = 0; n < Context.ImgN; n++)
            {
                var w = (ImgComp[n].X + 7) >> 3;
                var h = (ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var data = ImgComp[n].Coeff.AsSpan(64 * (i + j * ImgComp[n].CoeffW));
                        Dequantize(data, Dequant[ImgComp[n].TQ]);
                        IdctBlockKernel(
                            ImgComp[n].Data.AsSpan(ImgComp[n].W2 * j * 8 + i * 8),
                            ImgComp[n].W2,
                            data
                        );
                    }
                }
            }
        }
    }

    public bool DecodeImage()
    {
        int m;
        for (m = 0; m < 4; m++)
        {
            ImgComp[m].RawData = null;
            ImgComp[m].RawCoeff = null;
        }

        RestartInterval = 0;
        if (!DecodeHeader(Scan.Load))
        {
            return false;
        }

        m = GetMarker();
        while (!Methods.Eoi(m))
        {
            if (Methods.Sos(m))
            {
                if (!ProcessScanHeader())
                {
                    return false;
                }

                if (!ParseEntropyCodedData())
                {
                    return false;
                }

                if (Marker == Constants.MarkerNone)
                {
                    Marker = SkipJunkAtEnd();
                }

                m = GetMarker();
                if (Methods.Restart(m))
                {
                    m = GetMarker();
                }
            }
            else if (Methods.Dnl(m))
            {
                var ld = Context.Get16BE();
                var nl = unchecked((uint)Context.Get16BE());
                if (ld != 4)
                {
                    return Methods.Error("Bad DNL length. Corrupt JPEG.");
                }

                if (nl != Context.ImgY)
                {
                    return Methods.Error("Bad DNL height. Corrupt JPEG.");
                }

                m = GetMarker();
            }
            else
            {
                if (!ProcessMarker(m))
                {
                    return true;
                }

                m = GetMarker();
            }
        }

        if (Progressive)
        {
            Finish();
        }

        return true;
    }

    public bool FreeComponents(int ncomp, bool why)
    {
        for (var i = 0; i < ncomp; i++)
        {
            ImgComp[i].RawData = null;
            ImgComp[i].Data = null;
            ImgComp[i].RawCoeff = null;
            ImgComp[i].Coeff = null;
            ImgComp[i].LineBuf = null;
        }

        return why;
    }

    public void Cleanup() => FreeComponents(Context.ImgN, false);

    public static void Dequantize(Span<short> data, ushort[] dequant)
    {
        for (var i = 0; i < 64; i++)
        {
            data[i] *= unchecked((short)dequant[i]);
        }
    }

    public sealed class CompData
    {
        public int ID { get; set; }
        public int H { get; set; }
        public int V { get; set; }
        public int TQ { get; set; }
        public int HD { get; set; }
        public int HA { get; set; }
        public int DCPred { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W2 { get; set; }
        public int H2 { get; set; }
        public byte[]? Data { get; set; }
        public byte[]? RawData { get; set; }
        public object? RawCoeff { get; set; }
        public byte[]? LineBuf { get; set; }
        public short[]? Coeff { get; set; }
        public int CoeffW { get; set; }
        public int CoeffH { get; set; }
    }
}
