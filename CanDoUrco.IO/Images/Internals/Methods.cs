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
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using V16 = System.Runtime.Intrinsics.Vector128<short>;
using V32 = System.Runtime.Intrinsics.Vector128<int>;
using V8 = System.Runtime.Intrinsics.Vector128<byte>;

namespace CanDoUrco.IO.Images.Internals;

internal static class Methods
{
    [field: ThreadStatic]
    public static string? FailureReason { get; private set; }

    public static void ClearFailure() => FailureReason = null;

    public static bool Error(string reason)
    {
        FailureReason = reason;
        return false;
    }

    public static byte[]? ErrorPtr(string reason)
    {
        FailureReason = reason;
        return null;
    }

    public static int Read(object? user, Span<byte> data, int size) =>
        user is not Stream stream ? 0 : stream.Read(data[..size]);

    public static void Skip(object? user, int size)
    {
        if (user is not Stream stream)
        {
            return;
        }

        stream.Seek(size, SeekOrigin.Current);
    }

    public static void Skip(Context context, int count)
    {
        if (count == 0)
        {
            return;
        }

        if (count < 0)
        {
            context.ImgBuffer = context.ImgBufferEnd;
            context.ImgBufferPosition = context.ImgBufferEndPosition;
            return;
        }

        if (context.IO.Read is not null)
        {
            var blen = context.ImgBufferEndPosition - context.ImgBufferPosition;
            if (blen < count)
            {
                context.ImgBuffer = context.ImgBufferEnd;
                context.ImgBufferPosition = context.ImgBufferEndPosition;
                context.IO.Skip!.Invoke(context.IOUserData, count - blen);
                return;
            }
        }

        context.ImgBufferPosition += count;
    }

    public static bool Eof(object? user) =>
        user is not Stream stream || (stream.Position >= stream.Length);

    public static bool Eoi(int x) => x == 0xD9;

    public static bool Sos(int x) => x == 0xDA;

    public static void StartMem(Context context, byte[] buffer, int len)
    {
        context.IO.Read = null;
        context.ReadFromCallbacks = 0;
        context.CallbackAlreadyRead = 0;
        context.ImgBufferOriginal = buffer;
        context.ImgBufferOriginalPosition = 0;
        context.ImgBuffer = buffer;
        context.ImgBufferPosition = 0;
        context.ImgBufferOriginalEnd = buffer;
        context.ImgBufferOriginalEndPosition = len;
        context.ImgBufferEnd = buffer;
        context.ImgBufferEndPosition = len;
    }

    public static void StartCallbacks(Context context, IOCallbacks callbacks, object? userData)
    {
        context.IO = callbacks;
        context.IOUserData = userData;
        context.BufLen = Constants.BufferStartLength;
        context.ReadFromCallbacks = 1;
        context.CallbackAlreadyRead = 0;
        context.ImgBufferOriginal = context.BufferStart;
        context.ImgBufferOriginalPosition = 0;
        context.ImgBuffer = context.BufferStart;
        context.ImgBufferPosition = 0;
        RefillBuffer(context);
        context.ImgBufferOriginalEnd = context.ImgBufferEnd;
        context.ImgBufferOriginalEndPosition = context.ImgBufferEndPosition;
    }

    public static Context StartFile(Stream stream)
    {
        var context = new Context();
        context.IO.Read = Read;
        context.IO.Skip = Skip;
        context.IO.Eof = Eof;
        StartCallbacks(context, context.IO, stream);
        return context;
    }

    public static void Rewind(Context context)
    {
        context.ImgBuffer = context.ImgBufferOriginal;
        context.ImgBufferPosition = context.ImgBufferOriginalPosition;
        context.ImgBufferEnd = context.ImgBufferOriginalEnd;
        context.ImgBufferEndPosition = context.ImgBufferOriginalEndPosition;
    }

    public static void RefillBuffer(Context context)
    {
        var n = context.IO.Read!.Invoke(context.IOUserData, context.BufferStart, context.BufLen);
        context.CallbackAlreadyRead +=
            context.ImgBufferPosition - context.ImgBufferOriginalPosition;
        context.ImgBuffer = context.BufferStart;
        context.ImgBufferEnd = context.BufferStart;
        context.ImgBufferPosition = 0;
        if (n == 0)
        {
            context.ReadFromCallbacks = 0;
            context.ImgBufferEndPosition = 1;
            context.BufferStart[0] = 0;
        }
        else
        {
            context.ImgBufferEndPosition = n;
        }
    }

    public static byte Get8(Context context)
    {
        if (context.ImgBufferPosition < context.ImgBufferEndPosition)
        {
            return context.ImgBuffer![context.ImgBufferPosition++];
        }

        if (context.ReadFromCallbacks != 0)
        {
            RefillBuffer(context);
            return context.ImgBuffer![context.ImgBufferPosition++];
        }

        return 0;
    }

    public static ushort Get16BE(Context context)
    {
        var z = (int)Get8(context);
        return unchecked((ushort)((z << 8) + Get8(context)));
    }

    public static void BuildFastAC(Huffman huffman, short[] fastAC)
    {
        for (var i = 0; i < (1 << Constants.FastBits); i++)
        {
            var fast = huffman.Fast[i];
            fastAC[i] = 0;
            if (fast < 255)
            {
                var rs = huffman.Values[fast];
                var run = (rs >> 4) & 15;
                var magbits = rs & 15;
                var len = huffman.Size[fast];
                if (magbits != 0 && len + magbits <= Constants.FastBits)
                {
                    var k =
                        ((i << len) & ((1 << Constants.FastBits) - 1))
                        >> (Constants.FastBits - magbits);

                    var m = 1 << (magbits - 1);
                    if (k < m)
                    {
                        k += unchecked((int)((~0U << magbits) + 1));
                    }

                    if (k >= -128 && k <= 127)
                    {
                        fastAC[i] = unchecked((short)((k * 256) + (run * 16) + len + magbits));
                    }
                }
            }
        }
    }

    public static bool AddIntsValid(int lhs, int rhs)
    {
        if ((lhs >= 0) != (rhs >= 0))
        {
            return true;
        }

        if (lhs < 0 && rhs < 0)
        {
            return lhs >= int.MinValue - rhs;
        }

        return lhs <= int.MaxValue - rhs;
    }

    public static bool Mul2ShortsValid(int lhs, int rhs)
    {
        if (rhs == 0 || rhs == -1)
        {
            return true;
        }

        if ((lhs >= 0) == (rhs >= 0))
        {
            return lhs <= short.MaxValue / rhs;
        }

        if (rhs < 0)
        {
            return lhs <= short.MinValue / rhs;
        }

        return lhs >= short.MinValue / rhs;
    }

    public static void GrowBuffer(Jpeg j)
    {
        do
        {
            var b = (uint)(j.NoMore != 0 ? 0 : Methods.Get8(j.S));
            if (b == 0xFF)
            {
                var c = (int)Methods.Get8(j.S);
                while (c == 0xFF)
                {
                    c = Methods.Get8(j.S);
                }

                if (c != 0)
                {
                    j.Marker = (byte)c;
                    j.NoMore = 1;
                    return;
                }
            }

            j.CodeBuffer |= b << (24 - j.CodeBits);
            j.CodeBits += 8;
        } while (j.CodeBits <= 24);
    }

    public static int JpegHuffDecode(Jpeg jpeg, Huffman huffman)
    {
        if (jpeg.CodeBits < 16)
        {
            GrowBuffer(jpeg);
        }

        var c = unchecked(
            (int)((jpeg.CodeBuffer >> (32 - Constants.FastBits)) & ((1 << Constants.FastBits) - 1))
        );

        var k = (int)huffman.Fast[c];
        if (k < 255)
        {
            var s = (int)huffman.Size[k];
            if (s > jpeg.CodeBits)
            {
                return -1;
            }

            jpeg.CodeBuffer <<= s;
            jpeg.CodeBits -= s;
            return huffman.Values[k];
        }

        var temp = unchecked(jpeg.CodeBuffer >> 16);
        for (k = Constants.FastBits + 1; ; k++)
        {
            if (temp < huffman.MaxCode[k])
            {
                break;
            }
        }

        if (k == 17)
        {
            jpeg.CodeBits -= 16;
            return -1;
        }

        if (k > jpeg.CodeBits)
        {
            return -1;
        }

        c = unchecked(
            (int)(((jpeg.CodeBuffer >> (32 - k)) & Constants.BitsMask[k]) + huffman.Delta[k])
        );
        if (c < 0 || c >= 256)
        {
            return -1;
        }

        Debug.Assert(
            ((jpeg.CodeBuffer >> (32 - huffman.Size[c])) & Constants.BitsMask[huffman.Size[c]])
                == huffman.Code[c]
        );

        jpeg.CodeBits -= k;
        jpeg.CodeBuffer <<= k;
        return huffman.Values[c];
    }

    public static int ExtendReceive(Jpeg jpeg, int count)
    {
        if (jpeg.CodeBits < count)
        {
            GrowBuffer(jpeg);
        }

        if (jpeg.CodeBits < count)
        {
            return 0;
        }

        var sgn = unchecked((int)(jpeg.CodeBuffer >> 31));
        var k = BitOperations.RotateLeft(jpeg.CodeBuffer, count);
        jpeg.CodeBuffer = k & ~Constants.BitsMask[count];
        k &= Constants.BitsMask[count];
        jpeg.CodeBits -= count;
        return unchecked((int)(k + (Constants.JpegBias[count] & (sgn - 1))));
    }

    public static int JpegGetBits(Jpeg jpeg, int count)
    {
        if (jpeg.CodeBits < count)
        {
            GrowBuffer(jpeg);
        }

        if (jpeg.CodeBits < count)
        {
            return 0;
        }

        var k = BitOperations.RotateLeft(jpeg.CodeBuffer, count);
        jpeg.CodeBuffer = k & ~Constants.BitsMask[count];
        k &= Constants.BitsMask[count];
        jpeg.CodeBits -= count;
        return unchecked((int)k);
    }

    public static int JpegGetBit(Jpeg jpeg)
    {
        if (jpeg.CodeBits < 1)
        {
            GrowBuffer(jpeg);
        }

        if (jpeg.CodeBits < 1)
        {
            return 0;
        }

        var k = jpeg.CodeBuffer;
        jpeg.CodeBuffer <<= 1;
        --jpeg.CodeBits;
        return unchecked((int)(k & 0x8000_0000));
    }

    public static bool JpegDecodeBlock(
        Jpeg jpeg,
        Span<short> data,
        Huffman hdc,
        Huffman hac,
        short[] fac,
        int block,
        ushort[] dequant
    )
    {
        if (jpeg.CodeBits < 16)
        {
            GrowBuffer(jpeg);
        }

        var t = JpegHuffDecode(jpeg, hdc);
        if (t < 0 || t > 15)
        {
            return Error("Bad huffman code. Corrupt JPEG.");
        }

        data[..64].Clear();
        var diff = t != 0 ? ExtendReceive(jpeg, t) : 0;
        if (!AddIntsValid(jpeg.ImgComp[block].DCPred, diff))
        {
            return Error("Bad delta. Corrupt JPEG.");
        }

        var dc = jpeg.ImgComp[block].DCPred + diff;
        jpeg.ImgComp[block].DCPred = dc;
        if (!Mul2ShortsValid(dc, dequant[0]))
        {
            return Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        data[0] = unchecked((short)((nint)dc * dequant[0]));
        var k = 1;
        do
        {
            if (jpeg.CodeBits < 16)
            {
                GrowBuffer(jpeg);
            }

            var c = unchecked(
                (int)(
                    (jpeg.CodeBuffer >> (32 - Constants.FastBits)) & ((1 << Constants.FastBits) - 1)
                )
            );

            var r = (int)fac[c];
            if (r != 0)
            {
                k += (r >> 4) & 15;
                var s = r & 15;
                if (s > jpeg.CodeBits)
                {
                    return Error(
                        "Bad Huffman code. Combined length longer than code bits available."
                    );
                }

                jpeg.CodeBuffer <<= s;
                jpeg.CodeBits -= s;
                var zig = Constants.JpegDezigzag[k++];
                data[zig] = unchecked((short)((r >> 8) * dequant[zig]));
            }
            else
            {
                var rs = JpegHuffDecode(jpeg, hac);
                if (rs < 0)
                {
                    return Error("Bad Huffman code. Corrupt JPEG.");
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
                    data[zig] = unchecked((short)(ExtendReceive(jpeg, s) * dequant[zig]));
                }
            }
        } while (k < 64);

        return true;
    }

    public static bool JpegDecodeBlockProgDC(Jpeg j, Span<short> data, Huffman hdc, int b)
    {
        if (j.SpecEnd != 0)
        {
            return Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        if (j.CodeBits < 16)
        {
            GrowBuffer(j);
        }

        if (j.SuccHigh == 0)
        {
            data[..64].Clear();
            var t = JpegHuffDecode(j, hdc);
            if (t < 0 || t > 15)
            {
                return Error("Can't merge DC and AC. Corrupt JPEG.");
            }

            var diff = t != 0 ? ExtendReceive(j, t) : 0;
            if (!AddIntsValid(j.ImgComp[b].DCPred, diff))
            {
                return Error("Bad delta. Corrupt JPEG.");
            }

            var dc = j.ImgComp[b].DCPred + diff;
            j.ImgComp[b].DCPred = dc;
            if (!Mul2ShortsValid(dc, 1 << j.SuccLow))
            {
                return Error("Can't merge DC and AC. Corrupt JPEG.");
            }

            data[0] = unchecked((short)(dc * (1 << j.SuccLow)));
        }
        else
        {
            if (JpegGetBit(j) != 0)
            {
                data[0] += unchecked((short)(1 << j.SuccLow));
            }
        }

        return true;
    }

    public static bool JpegDecodeBlockProgAC(Jpeg j, Span<short> data, Huffman hac, short[] fac)
    {
        if (j.SpecStart == 0)
        {
            return Error("Can't merge DC and AC. Corrupt JPEG.");
        }

        if (j.SuccHigh == 0)
        {
            var shift = j.SuccLow;
            if (j.EobRun != 0)
            {
                --j.EobRun;
                return true;
            }

            var k = j.SpecStart;
            do
            {
                if (j.CodeBits < 16)
                {
                    GrowBuffer(j);
                }

                var c = unchecked(
                    (int)(
                        (j.CodeBuffer >> (32 - Constants.FastBits))
                        & ((1 << Constants.FastBits) - 1)
                    )
                );

                var r = (int)fac[c];
                if (r != 0)
                {
                    k += (r >> 4) & 15;
                    var s = r & 15;
                    if (s > j.CodeBits)
                    {
                        return Error(
                            "Bad Huffman code. Combined length longer than code bits available."
                        );
                    }

                    j.CodeBuffer <<= s;
                    j.CodeBits -= s;
                    var zig = (int)Constants.JpegDezigzag[k++];
                    data[zig] = unchecked((short)((r >> 8) * (1 << shift)));
                }
                else
                {
                    var rs = JpegHuffDecode(j, hac);
                    if (rs < 0)
                    {
                        return Error("Bad Huffman code. Corrupt JPEG.");
                    }

                    var s = rs & 15;
                    r = rs >> 4;
                    if (s == 0)
                    {
                        if (r < 15)
                        {
                            j.EobRun = 1 << r;
                            if (r != 0)
                            {
                                j.EobRun += JpegGetBits(j, r);
                            }

                            --j.EobRun;
                            break;
                        }

                        k += 16;
                    }
                    else
                    {
                        k += r;
                        var zig = (int)Constants.JpegDezigzag[k++];
                        data[zig] = unchecked((short)(ExtendReceive(j, s) * (1 << shift)));
                    }
                }
            } while (k <= j.SpecEnd);
        }
        else
        {
            var bit = unchecked((short)(1 << j.SuccLow));
            if (j.EobRun != 0)
            {
                --j.EobRun;
                for (var k = j.SpecStart; k <= j.SpecEnd; k++)
                {
                    ref var p = ref data[Constants.JpegDezigzag[k]];
                    if (p != 0)
                    {
                        if (JpegGetBit(j) != 0)
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
                var k = j.SpecStart;
                do
                {
                    var rs = JpegHuffDecode(j, hac);
                    if (rs < 0)
                    {
                        return Error("Bad Huffman code. Corrupt JPEG.");
                    }

                    var s = rs & 15;
                    var r = rs >> 4;
                    if (s == 0)
                    {
                        if (r < 15)
                        {
                            j.EobRun = (1 << r) - 1;
                            if (r != 0)
                            {
                                j.EobRun += JpegGetBits(j, r);
                            }

                            r = 64;
                        }
                    }
                    else
                    {
                        if (s != 1)
                        {
                            return Error("Bad Huffman code. Corrupt JPEG.");
                        }

                        s = JpegGetBit(j) != 0 ? bit : -bit;
                    }

                    while (k <= j.SpecEnd)
                    {
                        ref var p = ref data[Constants.JpegDezigzag[k++]];
                        if (p != 0)
                        {
                            if (JpegGetBit(j) != 0)
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
                } while (k <= j.SpecEnd);
            }
        }

        return true;
    }

    public static int F2F(double x) => unchecked((int)(x * 4096 + .5));

    public static int Fsh(int x) => unchecked(x * 4096);

    public static (int t0, int t1, int t2, int t3, int x0, int x1, int x2, int x3) Idct1D(
        int s0,
        int s1,
        int s2,
        int s3,
        int s4,
        int s5,
        int s6,
        int s7
    )
    {
        var p2 = s2;
        var p3 = s6;
        var p1 = (p2 + p3) * F2F(.5411961);
        var t2 = p1 + p3 * F2F(-1.847759065);
        var t3 = p1 + p2 * F2F(.765366865);
        p2 = s0;
        p3 = s4;
        var t0 = Fsh(p2 + p3);
        var t1 = Fsh(p2 - p3);
        var x0 = t0 + t3;
        var x3 = t0 - t3;
        var x1 = t1 + t2;
        var x2 = t1 - t2;
        t0 = s7;
        t1 = s5;
        t2 = s3;
        t3 = s1;
        p3 = t0 + t2;
        var p4 = t1 + t3;
        p1 = t0 + t3;
        p2 = t1 + t2;
        var p5 = (p3 + p4) * F2F(1.175875602);
        t0 *= F2F(.298631336);
        t1 *= F2F(2.053119869);
        t2 *= F2F(3.072711026);
        t3 *= F2F(1.501321110);
        p1 = p5 + p1 * F2F(-.899976223);
        p2 = p5 + p2 * F2F(-2.562915447);
        p3 *= F2F(-1.961570560);
        p4 *= F2F(-.390180644);
        t3 += p1 + p4;
        t2 += p2 + p3;
        t1 += p2 + p4;
        t0 += p1 + p3;
        return (t0, t1, t2, t3, x0, x1, x2, x3);
    }

    public static void IdctBlock(Span<byte> @out, int outStride, ReadOnlySpan<short> data)
    {
        Span<int> val = stackalloc int[64];
        for (var i = 0; i < 8; i++)
        {
            if (
                data[i + 8] == 0
                && data[i + 16] == 0
                && data[i + 24] == 0
                && data[i + 32] == 0
                && data[i + 40] == 0
                && data[i + 48] == 0
                && data[i + 56] == 0
            )
            {
                var dcterm = data[i + 0] * 4;
                val[i + 0] =
                    val[i + 8] =
                    val[i + 16] =
                    val[i + 24] =
                    val[i + 32] =
                    val[i + 40] =
                    val[i + 48] =
                    val[i + 56] =
                        dcterm;
            }
            else
            {
                var (t0, t1, t2, t3, x0, x1, x2, x3) = Idct1D(
                    data[i + 0],
                    data[i + 8],
                    data[i + 16],
                    data[i + 24],
                    data[i + 32],
                    data[i + 40],
                    data[i + 48],
                    data[i + 56]
                );

                const int offset = 512;
                const int shift = 10;
                x0 += offset;
                x1 += offset;
                x2 += offset;
                x3 += offset;
                val[i + 0] = (x0 + t3) >> shift;
                val[i + 56] = (x0 - t3) >> shift;
                val[i + 8] = (x1 + t2) >> shift;
                val[i + 48] = (x1 - t2) >> shift;
                val[i + 16] = (x2 + t1) >> shift;
                val[i + 40] = (x2 - t1) >> shift;
                val[i + 24] = (x3 + t0) >> shift;
                val[i + 32] = (x3 - t0) >> shift;
            }
        }

        for (var i = 0; i < 8; i++)
        {
            var (t0, t1, t2, t3, x0, x1, x2, x3) = Idct1D(
                val[i * 8 + 0],
                val[i * 8 + 1],
                val[i * 8 + 2],
                val[i * 8 + 3],
                val[i * 8 + 4],
                val[i * 8 + 5],
                val[i * 8 + 6],
                val[i * 8 + 7]
            );

            const int shift = 17;
            const int offset = 65536 + (128 << shift);
            x0 += offset;
            x1 += offset;
            x2 += offset;
            x3 += offset;
            @out[i * outStride + 0] = (byte)int.Clamp((x0 + t3) >> shift, 0, 255);
            @out[i * outStride + 7] = (byte)int.Clamp((x0 - t3) >> shift, 0, 255);
            @out[i * outStride + 1] = (byte)int.Clamp((x1 + t2) >> shift, 0, 255);
            @out[i * outStride + 6] = (byte)int.Clamp((x1 - t2) >> shift, 0, 255);
            @out[i * outStride + 2] = (byte)int.Clamp((x2 + t1) >> shift, 0, 255);
            @out[i * outStride + 5] = (byte)int.Clamp((x2 - t1) >> shift, 0, 255);
            @out[i * outStride + 3] = (byte)int.Clamp((x3 + t0) >> shift, 0, 255);
            @out[i * outStride + 4] = (byte)int.Clamp((x3 - t0) >> shift, 0, 255);
        }
    }

    public static void IdctSimd(Span<byte> @out, int outStride, ReadOnlySpan<short> data)
    {
        if (!Sse2.IsSupported && !AdvSimd.Arm64.IsSupported)
        {
            throw new PlatformNotSupportedException();
        }

        var row0 = Vector128.Create<short>(data.Slice(0 * 8, 8));
        var row1 = Vector128.Create<short>(data.Slice(1 * 8, 8));
        var row2 = Vector128.Create<short>(data.Slice(2 * 8, 8));
        var row3 = Vector128.Create<short>(data.Slice(3 * 8, 8));
        var row4 = Vector128.Create<short>(data.Slice(4 * 8, 8));
        var row5 = Vector128.Create<short>(data.Slice(5 * 8, 8));
        var row6 = Vector128.Create<short>(data.Slice(6 * 8, 8));
        var row7 = Vector128.Create<short>(data.Slice(7 * 8, 8));

        DctPass(
            ref row0,
            ref row1,
            ref row2,
            ref row3,
            ref row4,
            ref row5,
            ref row6,
            ref row7,
            Vector128.Create(512),
            10
        );

        Interleave16(ref row0, ref row4);
        Interleave16(ref row1, ref row5);
        Interleave16(ref row2, ref row6);
        Interleave16(ref row3, ref row7);
        Interleave16(ref row0, ref row2);
        Interleave16(ref row1, ref row3);
        Interleave16(ref row4, ref row6);
        Interleave16(ref row5, ref row7);
        Interleave16(ref row0, ref row1);
        Interleave16(ref row2, ref row3);
        Interleave16(ref row4, ref row5);
        Interleave16(ref row6, ref row7);

        DctPass(
            ref row0,
            ref row1,
            ref row2,
            ref row3,
            ref row4,
            ref row5,
            ref row6,
            ref row7,
            Vector128.Create(65536 + (128 << 17)),
            17
        );

        var p0 = PackUnsigned16(row0, row1);
        var p1 = PackUnsigned16(row2, row3);
        var p2 = PackUnsigned16(row4, row5);
        var p3 = PackUnsigned16(row6, row7);

        Interleave8(ref p0, ref p2);
        Interleave8(ref p1, ref p3);
        Interleave8(ref p0, ref p1);
        Interleave8(ref p2, ref p3);
        Interleave8(ref p0, ref p2);
        Interleave8(ref p1, ref p3);

        var outOffset = 0;
        StoreRow(@out, ref outOffset, outStride, p0);
        StoreRow(@out, ref outOffset, outStride, p2);
        StoreRow(@out, ref outOffset, outStride, p1);
        StoreRow(@out, ref outOffset, outStride, p3);
    }

    public static void YCbCrToRgbRow(
        Span<byte> @out,
        ReadOnlySpan<byte> y,
        ReadOnlySpan<byte> pcb,
        ReadOnlySpan<byte> pcr,
        int count,
        int step
    )
    {
        var o = 0;
        for (var i = 0; i < count; i++)
        {
            var yFixed = (y[i] << 20) + (1 << 19);
            var cr = pcr[i] - 128;
            var cb = pcb[i] - 128;
            var r = yFixed + cr * Float2Fixed(1.40200F);
            var g = unchecked(
                (int)(
                    yFixed
                    + (cr * -Float2Fixed(.71414F))
                    + ((cb * -Float2Fixed(.34414F)) & 0xFFFF_0000)
                )
            );

            var b = yFixed + cb * Float2Fixed(1.77200F);
            r >>= 20;
            g >>= 20;
            b >>= 20;
            r = int.Clamp(r, 0, 255);
            g = int.Clamp(g, 0, 255);
            b = int.Clamp(b, 0, 255);
            @out[o + 0] = (byte)r;
            @out[o + 1] = (byte)g;
            @out[o + 2] = (byte)b;
            @out[o + 3] = 255;
            o += step;
        }
    }

    public static unsafe void YCbCrToRgbSimd(
        Span<byte> @out,
        ReadOnlySpan<byte> y,
        ReadOnlySpan<byte> pcb,
        ReadOnlySpan<byte> pcr,
        int count,
        int step
    )
    {
        var i = 0;
        var o = 0;
        if (step == 4 && Sse2.IsSupported)
        {
            var signFlip = Vector128.Create((byte)0x80);
            var crConst0 = Vector128.Create((short)(1.40200F * 4096F + .5F));
            var crConst1 = Vector128.Create((short)-(short)(.71414F * 4096F + .5F));
            var cbConst0 = Vector128.Create((short)-(short)(.34414F * 4096F + .5F));
            var cbConst1 = Vector128.Create((short)(1.77200F * 4096F + .5F));
            var yBias = Vector128.Create((byte)128);
            var xw = Vector128.Create((short)255);
            for (; i + 7 < count; i += 8)
            {
                var yBytes = Vector128.CreateScalar(MemoryMarshal.Read<long>(y[i..])).AsByte();
                var crBytes = Vector128.CreateScalar(MemoryMarshal.Read<long>(pcr[i..])).AsByte();
                var cbBytes = Vector128.CreateScalar(MemoryMarshal.Read<long>(pcb[i..])).AsByte();
                var crBiased = Sse2.Xor(crBytes, signFlip);
                var cbBiased = Sse2.Xor(cbBytes, signFlip);

                var yw = Sse2.UnpackLow(yBias, yBytes).AsInt16();
                var crw = Sse2.UnpackLow(V8.Zero, crBiased).AsInt16();
                var cbw = Sse2.UnpackLow(V8.Zero, cbBiased).AsInt16();

                var yws = Sse2.ShiftRightLogical(yw.AsUInt16(), 4).AsInt16();
                var cr0 = Sse2.MultiplyHigh(crConst0, crw);
                var cb0 = Sse2.MultiplyHigh(cbConst0, cbw);
                var cb1 = Sse2.MultiplyHigh(cbw, cbConst1);
                var cr1 = Sse2.MultiplyHigh(crw, crConst1);
                var rws = Sse2.Add(cr0, yws);
                var gwt = Sse2.Add(cb0, yws);
                var bws = Sse2.Add(yws, cb1);
                var gws = Sse2.Add(gwt, cr1);

                var rw = Sse2.ShiftRightArithmetic(rws, 4);
                var bw = Sse2.ShiftRightArithmetic(bws, 4);
                var gw = Sse2.ShiftRightArithmetic(gws, 4);

                var brb = Sse2.PackUnsignedSaturate(rw, bw);
                var gxb = Sse2.PackUnsignedSaturate(gw, xw);

                var t0 = Sse2.UnpackLow(brb, gxb).AsInt16();
                var t1 = Sse2.UnpackHigh(brb, gxb).AsInt16();
                var o0 = Sse2.UnpackLow(t0, t1);
                var o1 = Sse2.UnpackHigh(t0, t1);

                o0.AsByte().CopyTo(@out.Slice(o, 16));
                o1.AsByte().CopyTo(@out.Slice(o + 16, 16));
                o += 32;
            }
        }
        else if (step == 4 && AdvSimd.Arm64.IsSupported)
        {
            var signFlip = Vector64.Create((byte)0x80);
            var crConst0 = Vector128.Create((short)(1.40200F * 4096F + .5F));
            var crConst1 = Vector128.Create((short)-(short)(.71414F * 4096F + .5F));
            var cbConst0 = Vector128.Create((short)-(short)(.34414F * 4096F + .5F));
            var cbConst1 = Vector128.Create((short)(1.77200F * 4096F + .5F));

            for (; i + 7 < count; i += 8)
            {
                var yBytes = Vector64.Create<byte>(y.Slice(i, 8));
                var crBytes = Vector64.Create<byte>(pcr.Slice(i, 8));
                var cbBytes = Vector64.Create<byte>(pcb.Slice(i, 8));
                var crBiased = AdvSimd.Subtract(crBytes, signFlip).AsSByte();
                var cbBiased = AdvSimd.Subtract(cbBytes, signFlip).AsSByte();

                var yws = AdvSimd.ShiftLeftLogicalWideningLower(yBytes, 4).AsInt16();
                var crw = AdvSimd.ShiftLeftLogicalWideningLower(crBiased, 7);
                var cbw = AdvSimd.ShiftLeftLogicalWideningLower(cbBiased, 7);

                var cr0 = AdvSimd.MultiplyDoublingSaturateHigh(crw, crConst0);
                var cb0 = AdvSimd.MultiplyDoublingSaturateHigh(cbw, cbConst0);
                var cr1 = AdvSimd.MultiplyDoublingSaturateHigh(crw, crConst1);
                var cb1 = AdvSimd.MultiplyDoublingSaturateHigh(cbw, cbConst1);
                var rws = AdvSimd.Add(yws, cr0);
                var gws = AdvSimd.Add(AdvSimd.Add(yws, cb0), cr1);
                var bws = AdvSimd.Add(yws, cb1);

                var r = AdvSimd.ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(rws, 4);
                var g = AdvSimd.ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(gws, 4);
                var b = AdvSimd.ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(bws, 4);
                var a = Vector64.Create((byte)255);

                fixed (byte* p = &@out[o])
                {
                    AdvSimd.StoreVectorAndZip(p, (r, g, b, a));
                }

                o += 8 * 4;
            }
        }

        if (i < count)
        {
            YCbCrToRgbRow(@out[o..], y[i..], pcb[i..], pcr[i..], count - i, step);
        }
    }

    public static byte Div4(int value) => unchecked((byte)(value >> 2));

    public static byte Div16(int value) => unchecked((byte)(value >> 4));

    public static (byte[] Data, int Offset) ResampleRow1(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int hs
    ) => (inNear, inNearPos);

    public static (byte[] Data, int Offset) ResampleRowV2(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int hs
    )
    {
        for (var i = 0; i < w; i++)
        {
            @out[i] = Div4(3 * inNear[inNearPos + i] + inFar[inFarPos + i] + 2);
        }

        return (@out, 0);
    }

    public static (byte[] Data, int Offset) ResampleRowH2(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int hs
    )
    {
        var input = inNear.AsSpan(inNearPos);
        if (w == 1)
        {
            @out[0] = @out[1] = input[0];
            return (@out, 0);
        }

        @out[0] = input[0];
        @out[1] = Div4(input[0] * 3 + input[1] + 2);
        int i;
        for (i = 1; i < w - 1; i++)
        {
            var n = 3 * input[i] + 2;
            @out[i * 2 + 0] = Div4(n + input[i - 1]);
            @out[i * 2 + 1] = Div4(n + input[i + 1]);
        }

        @out[i * 2 + 0] = Div4(input[w - 2] * 3 + input[w - 1] + 2);
        @out[i * 2 + 1] = input[w - 1];

        return (@out, 0);
    }

    public static (byte[] Data, int Offset) ResampleRowGeneric(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int hs
    )
    {
        for (var i = 0; i < w; i++)
        {
            for (var j = 0; j < hs; j++)
            {
                @out[i * hs + j] = inNear[inNearPos + i];
            }
        }

        return (@out, 0);
    }

    public static (byte[] Data, int Offset) ResampleRowHV2(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int _
    )
    {
        var near = inNear.AsSpan(inNearPos);
        var far = inFar.AsSpan(inFarPos);
        if (w == 1)
        {
            @out[0] = @out[1] = Div4(3 * near[0] + far[0] + 2);
            return (@out, 0);
        }

        var t1 = 3 * near[0] + far[0];
        @out[0] = Div4(t1 + 2);
        for (var i = 1; i < w; i++)
        {
            var t0 = t1;
            t1 = 3 * near[i] + far[i];
            @out[i * 2 - 1] = Div16(3 * t0 + t1 + 8);
            @out[i * 2] = Div16(3 * t1 + t0 + 8);
        }

        @out[w * 2 - 1] = Div4(t1 + 2);
        return (@out, 0);
    }

    public static unsafe (byte[] Data, int Offset) ResampleRowHV2Simd(
        byte[] @out,
        byte[] inNear,
        int inNearPos,
        byte[] inFar,
        int inFarPos,
        int w,
        int hs
    )
    {
        if (!Sse2.IsSupported && !AdvSimd.Arm64.IsSupported)
        {
            return ResampleRowHV2(@out, inNear, inNearPos, inFar, inFarPos, w, hs);
        }

        var near = inNear.AsSpan(inNearPos);
        var far = inFar.AsSpan(inFarPos);
        if (w == 1)
        {
            @out[0] = @out[1] = Div4(3 * near[0] + far[0] + 2);
            return (@out, 0);
        }

        var i = 0;
        var t1 = 3 * near[0] + far[0];
        for (; i < ((w - 1) & ~7); i += 8)
        {
            if (Sse2.IsSupported)
            {
                var zero = V8.Zero;
                var farB = Vector128.CreateScalar(MemoryMarshal.Read<long>(far[i..])).AsByte();
                var nearB = Vector128.CreateScalar(MemoryMarshal.Read<long>(near[i..])).AsByte();
                var farW = Sse2.UnpackLow(farB, zero).AsInt16();
                var nearW = Sse2.UnpackLow(nearB, zero).AsInt16();
                var diff = Sse2.Subtract(farW, nearW);
                var nears = Sse2.ShiftLeftLogical(nearW, 2);
                var curr = Sse2.Add(nears, diff);

                var prv0 = Sse2.ShiftLeftLogical128BitLane(curr, 2);
                var nxt0 = Sse2.ShiftRightLogical128BitLane(curr, 2);
                var prev = Sse2.Insert(prv0, (short)t1, 0);
                var next = Sse2.Insert(nxt0, (short)(3 * near[i + 8] + far[i + 8]), 7);

                var bias = Vector128.Create((short)8);
                var curs = Sse2.ShiftLeftLogical(curr, 2);
                var prvd = Sse2.Subtract(prev, curr);
                var nxtd = Sse2.Subtract(next, curr);
                var curb = Sse2.Add(curs, bias);
                var even = Sse2.Add(prvd, curb);
                var odd = Sse2.Add(nxtd, curb);

                var int0 = Sse2.UnpackLow(even, odd);
                var int1 = Sse2.UnpackHigh(even, odd);
                var de0 = Sse2.ShiftRightLogical(int0.AsUInt16(), 4).AsInt16();
                var de1 = Sse2.ShiftRightLogical(int1.AsUInt16(), 4).AsInt16();

                Sse2.PackUnsignedSaturate(de0, de1).CopyTo(@out, i * 2);
            }
            else
            {
                var farB = Vector64.Create<byte>(far.Slice(i, 8));
                var nearB = Vector64.Create<byte>(near.Slice(i, 8));
                var diff = AdvSimd.SubtractWideningLower(farB, nearB).AsInt16();
                var nears = AdvSimd.ShiftLeftLogicalWideningLower(nearB, 2).AsInt16();
                var curr = AdvSimd.Add(nears, diff);

                var prv0 = AdvSimd.ExtractVector128(curr, curr, 7);
                var nxt0 = AdvSimd.ExtractVector128(curr, curr, 1);
                var prev = AdvSimd.Insert(prv0, 0, (short)t1);
                var next = AdvSimd.Insert(nxt0, 7, (short)(3 * near[i + 8] + far[i + 8]));

                var curs = AdvSimd.ShiftLeftLogical(curr, 2);
                var prvd = AdvSimd.Subtract(prev, curr);
                var nxtd = AdvSimd.Subtract(next, curr);
                var even = AdvSimd.Add(curs, prvd);
                var odd = AdvSimd.Add(curs, nxtd);

                fixed (byte* p = &@out[i * 2])
                {
                    AdvSimd.StoreVectorAndZip(
                        p,
                        (
                            AdvSimd.ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(
                                even,
                                4
                            ),
                            AdvSimd.ShiftRightArithmeticRoundedNarrowingSaturateUnsignedLower(
                                odd,
                                4
                            )
                        )
                    );
                }
            }

            t1 = 3 * near[i + 7] + far[i + 7];
        }

        var t0 = t1;
        t1 = 3 * near[i] + far[i];
        @out[i * 2] = Div16(3 * t1 + t0 + 8);
        for (++i; i < w; ++i)
        {
            t0 = t1;
            t1 = 3 * near[i] + far[i];
            @out[i * 2 - 1] = Div16(3 * t0 + t1 + 8);
            @out[i * 2] = Div16(3 * t1 + t0 + 8);
        }

        @out[w * 2 - 1] = Div4(t1 + 2);
        return (@out, 0);
    }

    public static bool AllocJpegTables(Jpeg j)
    {
        j.HuffDC = new Huffman[Constants.JpegFixedArrayLength];
        j.HuffAC = new Huffman[Constants.JpegFixedArrayLength];
        j.Dequant = new ushort[Constants.JpegFixedArrayLength][];
        j.FastAC = new short[Constants.JpegFixedArrayLength][];
        j.ImgComp = new Jpeg.CompStruct[Constants.JpegFixedArrayLength];
        for (var i = 0; i < Constants.JpegFixedArrayLength; i++)
        {
            j.HuffDC[i] = new Huffman();
            j.HuffAC[i] = new Huffman();
            j.ImgComp[i] = new Jpeg.CompStruct();
            j.Dequant[i] = new ushort[Constants.JpegDequantLength];
            j.FastAC[i] = new short[Constants.JpegACLength];
        }

        return true;
    }

    public static void FreeJpegTables(Jpeg j) { }

    public static void SetupJpeg(Jpeg j)
    {
        if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
        {
            j.IdctBlockKernel = IdctSimd;
            j.YCbCrToRgbKernel = YCbCrToRgbSimd;
            j.ResampleRowHV2Kernel = ResampleRowHV2Simd;
        }
        else
        {
            j.IdctBlockKernel = IdctBlock;
            j.YCbCrToRgbKernel = YCbCrToRgbRow;
            j.ResampleRowHV2Kernel = ResampleRowHV2;
        }
    }

    public static byte GetMarker(Jpeg j)
    {
        byte x;
        if (j.Marker != Constants.MarkerNone)
        {
            x = j.Marker;
            j.Marker = Constants.MarkerNone;
            return x;
        }

        x = Get8(j.S);
        if (x != 0xFF)
        {
            return Constants.MarkerNone;
        }

        while (x == 0xFF)
        {
            x = Get8(j.S);
        }

        return x;
    }

    public static bool Soi(int value) => value == 0xD8;

    public static bool Sof(int value) => value == 0xC0 || value == 0xC1 || value == 0xC2;

    public static bool SofProgressive(int value) => value == 0xC2;

    public static bool Dnl(int value) => value == 0xDC;

    public static bool BuildHuffman(Huffman h, ReadOnlySpan<int> count)
    {
        int j;
        var k = 0;
        for (var i = 0; i < 16; i++)
        {
            for (j = 0; j < count[i]; j++)
            {
                h.Size[k++] = unchecked((byte)(i + 1));
                if (k >= 257)
                {
                    return Error("Bad size list. Corrupt JPEG.");
                }
            }
        }

        h.Size[k] = 0;
        var code = 0U;
        k = 0;
        for (j = 1; j <= 16; j++)
        {
            h.Delta[j] = unchecked((int)(k - code));
            if (h.Size[k] == j)
            {
                while (h.Size[k] == j)
                {
                    h.Code[k++] = unchecked((ushort)code++);
                }

                if (code - 1 >= (1U << j))
                {
                    return Error("Bad code lengths. Corrupt JPEG.");
                }
            }

            h.MaxCode[j] = code << (16 - j);
            code <<= 1;
        }

        h.MaxCode[j] = 0xFFFF_FFFF;
        Array.Fill(h.Fast, (byte)255, 0, 1 << Constants.FastBits);
        for (var i = 0; i < k; i++)
        {
            var s = (int)h.Size[i];
            if (s <= Constants.FastBits)
            {
                var c = h.Code[i] << (Constants.FastBits - s);
                var m = 1 << (Constants.FastBits - s);
                for (j = 0; j < m; j++)
                {
                    h.Fast[c + j] = unchecked((byte)i);
                }
            }
        }

        return true;
    }

    public static bool ProcessMarker(Jpeg z, int m)
    {
        int l;
        switch (m)
        {
            case Constants.MarkerNone:
                return Error("Expected marker. Corrupt JPEG.");
            case 0xDD:
                if (Get16BE(z.S) != 4)
                {
                    return Error("Invalid DRI length. Corrupt JPEG.");
                }

                z.RestartInterval = Get16BE(z.S);
                return true;
            case 0xDB:
                l = Get16BE(z.S) - 2;
                while (l > 0)
                {
                    var q = (int)Get8(z.S);
                    var p = q >> 4;
                    var sixteen = p != 0;
                    var t = q & 15;
                    if (p != 0 && p != 1)
                    {
                        return Error("Bad DQT type. Corrupt JPEG.");
                    }

                    if (t > 3)
                    {
                        return Error("Bad DQT table. Corrupt JPEG.");
                    }

                    for (var i = 0; i < 64; i++)
                    {
                        z.Dequant[t][Constants.JpegDezigzag[i]] = unchecked(
                            (ushort)(sixteen ? Get16BE(z.S) : Get8(z.S))
                        );
                    }

                    l -= sixteen ? 129 : 65;
                }

                return l == 0;
            case 0xC4:
                l = Get16BE(z.S) - 2;
                while (l > 0)
                {
                    byte[] v;
                    Span<int> sizes = stackalloc int[16];
                    var n = 0;
                    var q = (int)Get8(z.S);
                    var tc = q >> 4;
                    var th = q & 15;
                    if (tc > 1 || th > 3)
                    {
                        return Error("Bad DHT header. Corrupt JPEG.");
                    }

                    for (var i = 0; i < 16; i++)
                    {
                        sizes[i] = Get8(z.S);
                        n += sizes[i];
                    }

                    if (n > 256)
                    {
                        return Error("Bad DHT header. Corrupt JPEG.");
                    }

                    l -= 17;
                    if (tc == 0)
                    {
                        if (!BuildHuffman(z.HuffDC[th], sizes))
                        {
                            return false;
                        }

                        v = z.HuffDC[th].Values;
                    }
                    else
                    {
                        if (!BuildHuffman(z.HuffAC[th], sizes))
                        {
                            return false;
                        }

                        v = z.HuffAC[th].Values;
                    }

                    for (var i = 0; i < n; i++)
                    {
                        v[i] = Get8(z.S);
                    }

                    if (tc != 0)
                    {
                        BuildFastAC(z.HuffAC[th], z.FastAC[th]);
                    }

                    l -= n;
                }
                return l == 0;
        }

        if ((m >= 0xE0 && m <= 0xEF) || m == 0xFE)
        {
            l = Get16BE(z.S);
            if (l < 2)
            {
                return m == 0xFE
                    ? Error("Bad COM length. Corrupt JPEG.")
                    : Error("Bad APP length. Corrupt JPEG.");
            }

            l -= 2;
            if (m == 0xE0 && l >= 5)
            {
                var tag = "JFIF\0"u8;
                var ok = true;
                for (var i = 0; i < 5; i++)
                {
                    if (Get8(z.S) != tag[i])
                    {
                        ok = false;
                    }
                }

                l -= 5;
                if (ok)
                {
                    z.JFif = 1;
                }
            }
            else if (m == 0xEE && l >= 12)
            {
                var tag = "Adobe\0"u8;
                var ok = true;
                for (var i = 0; i < 6; i++)
                {
                    if (Get8(z.S) != tag[i])
                    {
                        ok = false;
                    }
                }

                l -= 6;
                if (ok)
                {
                    _ = Get8(z.S); // version
                    _ = Get16BE(z.S); // flags0
                    _ = Get16BE(z.S); // flags1
                    z.App14ColorTransform = Get8(z.S);
                    l -= 6;
                }
            }

            Skip(z.S, l);
            return true;
        }

        return Error("Unknown marker. Corrupt JPEG.");
    }

    public static bool AtEof(Context context)
    {
        if (context.IO.Read is not null)
        {
            if (!context.IO.Eof!(context.IOUserData))
            {
                return false;
            }

            if (context.ReadFromCallbacks == 0)
            {
                return true;
            }
        }

        return context.ImgBufferPosition >= context.ImgBufferEndPosition;
    }

    public static bool ProcessFrameHeader(Jpeg z, Scan scan)
    {
        var s = z.S;
        var hMax = 1;
        var vMax = 1;
        var lf = Get16BE(s);
        if (lf < 11)
        {
            return Error("Bad SOF length. Corrupt JPEG.");
        }

        var p = (int)Get8(s);
        if (p != 8)
        {
            return Error("Only 8-bit. JPEG format not supported: 8-bit only.");
        }

        s.ImgY = Get16BE(s);
        if (s.ImgY == 0)
        {
            return Error("No Header height. JPEG format not supported: delayed height.");
        }

        s.ImgX = Get16BE(s);
        if (s.ImgX == 0)
        {
            return Error("0 width. Corrupt JPEG.");
        }

        if (s.ImgY > Constants.MaxDimensions || s.ImgX > Constants.MaxDimensions)
        {
            return Error("Too large. Very large image (corrupt?).");
        }

        var c = (int)Get8(s);
        if (c != 3 && c != 1 && c != 4)
        {
            return Error("Bad component count. Corrupt JPEG.");
        }

        s.ImgN = c;
        for (var i = 0; i < c; i++)
        {
            z.ImgComp[i].Data = null;
            z.ImgComp[i].LineBuf = null;
        }

        if (lf != 8 + 3 * s.ImgN)
        {
            return Error("Bad SOF length. Corrupt JPEG.");
        }

        z.Rgb = 0;
        for (var i = 0; i < s.ImgN; i++)
        {
            var rgb = "RGB"u8;
            z.ImgComp[i].ID = Get8(s);
            if (s.ImgN == 3 && z.ImgComp[i].ID == rgb[i])
            {
                ++z.Rgb;
            }

            var q = Get8(s);
            z.ImgComp[i].H = q >> 4;
            if (z.ImgComp[i].H == 0 || z.ImgComp[i].H > 4)
            {
                return Error("Bad H. Corrupt JPEG.");
            }

            z.ImgComp[i].V = q & 15;
            if (z.ImgComp[i].V == 0 || z.ImgComp[i].V > 4)
            {
                return Error("Bad V. Corrupt JPEG.");
            }

            z.ImgComp[i].TQ = Get8(s);
            if (z.ImgComp[i].TQ > 3)
            {
                return Error("Bad TQ. Corrupt JPEG.");
            }
        }

        if (scan is not Scan.Load)
        {
            return true;
        }

        if (s.ImgX * s.ImgY * s.ImgN > int.MaxValue)
        {
            return Error("Too large. Image too large to decode.");
        }

        for (var i = 0; i < s.ImgN; i++)
        {
            if (z.ImgComp[i].H > hMax)
            {
                hMax = z.ImgComp[i].H;
            }

            if (z.ImgComp[i].V > vMax)
            {
                vMax = z.ImgComp[i].V;
            }
        }

        for (var i = 0; i < s.ImgN; i++)
        {
            if (hMax % z.ImgComp[i].H != 0)
            {
                return Error("Bad H. Corrupt JPEG.");
            }

            if (vMax % z.ImgComp[i].V != 0)
            {
                return Error("Bad V. Corrupt JPEG.");
            }
        }

        z.ImgHMax = hMax;
        z.ImgVMax = vMax;
        z.ImgMcuW = hMax * 8;
        z.ImgMcuH = vMax * 8;
        z.ImgMcuX = unchecked((int)((s.ImgX + z.ImgMcuW - 1) / z.ImgMcuW));
        z.ImgMcuY = unchecked((int)((s.ImgY + z.ImgMcuH - 1) / z.ImgMcuH));
        for (var i = 0; i < s.ImgN; i++)
        {
            z.ImgComp[i].X = unchecked((int)((s.ImgX * z.ImgComp[i].H + hMax - 1) / hMax));
            z.ImgComp[i].Y = unchecked((int)((s.ImgY * z.ImgComp[i].V + vMax - 1) / vMax));
            z.ImgComp[i].W2 = z.ImgMcuX * z.ImgComp[i].H * 8;
            z.ImgComp[i].H2 = z.ImgMcuY * z.ImgComp[i].V * 8;
            z.ImgComp[i].Coeff = null;
            z.ImgComp[i].RawCoeff = null;
            z.ImgComp[i].LineBuf = null;
            z.ImgComp[i].RawData = new byte[z.ImgComp[i].W2 * z.ImgComp[i].H2 + 15];
            z.ImgComp[i].Data = z.ImgComp[i].RawData;
            if (z.Progressive)
            {
                z.ImgComp[i].CoeffW = z.ImgComp[i].W2 / 8;
                z.ImgComp[i].CoeffH = z.ImgComp[i].H2 / 8;
                z.ImgComp[i].Coeff = new short[z.ImgComp[i].W2 * z.ImgComp[i].H2 + 15];
            }
        }

        return true;
    }

    public static bool DecodeJpegHeader(Jpeg z, Scan scan)
    {
        z.JFif = 0;
        z.App14ColorTransform = -1;
        z.Marker = Constants.MarkerNone;
        var m = (int)GetMarker(z);
        if (!Soi(m))
        {
            return Error("No SOI. Corrupt JPEG.");
        }

        if (scan is Scan.Type)
        {
            return true;
        }

        m = GetMarker(z);
        while (!Sof(m))
        {
            if (!ProcessMarker(z, m))
            {
                return false;
            }

            m = GetMarker(z);
            while (m == Constants.MarkerNone)
            {
                if (AtEof(z.S))
                {
                    return Error("No SOF. Corrupt JPEG.");
                }

                m = GetMarker(z);
            }
        }

        z.Progressive = SofProgressive(m);
        return ProcessFrameHeader(z, scan);
    }

    public static bool JpegTest(Context s)
    {
        var j = new Jpeg();
        j.S = s;
        if (!AllocJpegTables(j))
        {
            return false;
        }

        SetupJpeg(j);
        var r = DecodeJpegHeader(j, Scan.Type);
        Rewind(s);
        FreeJpegComponents(j, Constants.JpegFixedArrayLength, false);
        FreeJpegTables(j);
        return r;
    }

    public static bool ProcessScanHeader(Jpeg z)
    {
        var ls = Get16BE(z.S);
        z.ScanN = Get8(z.S);
        if (z.ScanN < 1 || z.ScanN > 4 || z.ScanN > (int)z.S.ImgN)
        {
            return Error("Bad SOS component count. Corrupt JPEG.");
        }

        if (ls != 6 + 2 * z.ScanN)
        {
            return Error("Bad SOS length. Corrupt JPG.");
        }

        for (var i = 0; i < z.ScanN; i++)
        {
            var id = (int)Get8(z.S);
            var q = (int)Get8(z.S);
            int which;
            for (which = 0; which < z.S.ImgN; which++)
            {
                if (z.ImgComp[which].ID == id)
                {
                    break;
                }
            }

            if (which == z.S.ImgN)
            {
                return false;
            }

            z.ImgComp[which].HD = q >> 4;
            if (z.ImgComp[which].HD > 3)
            {
                return Error("Bad DC Huffman. Corrupt JPEG.");
            }

            z.ImgComp[which].HA = q & 15;
            if (z.ImgComp[which].HA > 3)
            {
                return Error("Bad AC Huffman. Corrupt JPEG.");
            }

            z.Order[i] = which;
        }

        {
            z.SpecStart = Get8(z.S);
            z.SpecEnd = Get8(z.S);
            var aa = (int)Get8(z.S);
            z.SuccHigh = aa >> 4;
            z.SuccLow = aa & 15;
            if (z.Progressive)
            {
                if (
                    z.SpecStart > 63
                    || z.SpecEnd > 63
                    || z.SpecStart > z.SpecEnd
                    || z.SuccHigh > 13
                    || z.SuccLow > 13
                )
                {
                    return Error("Bad SOS. Corrupt JPEG.");
                }
            }
            else
            {
                if (z.SpecStart != 0)
                {
                    return Error("Bad SOS. Corrupt JPEG.");
                }

                if (z.SuccHigh != 0 || z.SuccLow != 0)
                {
                    return Error("Bad SOS. Corrupt JPEG.");
                }

                z.SpecEnd = 63;
            }
        }

        return true;
    }

    public static void JpegReset(Jpeg j)
    {
        j.CodeBits = 0;
        j.CodeBuffer = 0;
        j.NoMore = 0;
        j.ImgComp[0].DCPred = j.ImgComp[1].DCPred = j.ImgComp[2].DCPred = j.ImgComp[3].DCPred = 0;

        j.Marker = Constants.MarkerNone;
        j.Todo = j.RestartInterval != 0 ? j.RestartInterval : 0x7FFF_FFFF;
        j.EobRun = 0;
    }

    public static bool Restart(int x) => x >= 0xD0 && x <= 0xD7;

    public static bool ParseEntropyCodedData(Jpeg z)
    {
        JpegReset(z);
        if (!z.Progressive)
        {
            if (z.ScanN == 1)
            {
                Span<short> data = stackalloc short[64];
                var n = z.Order[0];
                var w = (z.ImgComp[n].X + 7) >> 3;
                var h = (z.ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var ha = z.ImgComp[n].HA;
                        if (
                            !JpegDecodeBlock(
                                z,
                                data,
                                z.HuffDC[z.ImgComp[n].HD],
                                z.HuffAC[ha],
                                z.FastAC[ha],
                                n,
                                z.Dequant[z.ImgComp[n].TQ]
                            )
                        )
                        {
                            return false;
                        }

                        z.IdctBlockKernel(
                            z.ImgComp[n].Data.AsSpan(z.ImgComp[n].W2 * j * 8 + i * 8),
                            z.ImgComp[n].W2,
                            data
                        );

                        if (--z.Todo <= 0)
                        {
                            if (z.CodeBits < 24)
                            {
                                GrowBuffer(z);
                            }

                            if (!Restart(z.Marker))
                            {
                                return true;
                            }

                            JpegReset(z);
                        }
                    }
                }

                return true;
            }
            else
            {
                Span<short> data = stackalloc short[64];
                for (var j = 0; j < z.ImgMcuY; j++)
                {
                    for (var i = 0; i < z.ImgMcuX; i++)
                    {
                        for (var k = 0; k < z.ScanN; k++)
                        {
                            var n = z.Order[k];
                            for (var y = 0; y < z.ImgComp[n].V; y++)
                            {
                                for (var x = 0; x < z.ImgComp[n].H; x++)
                                {
                                    var x2 = (i * z.ImgComp[n].H + x) * 8;
                                    var y2 = (j * z.ImgComp[n].V + y) * 8;
                                    var ha = z.ImgComp[n].HA;
                                    if (
                                        !JpegDecodeBlock(
                                            z,
                                            data,
                                            z.HuffDC[z.ImgComp[n].HD],
                                            z.HuffAC[ha],
                                            z.FastAC[ha],
                                            n,
                                            z.Dequant[z.ImgComp[n].TQ]
                                        )
                                    )
                                    {
                                        return false;
                                    }

                                    z.IdctBlockKernel(
                                        z.ImgComp[n].Data.AsSpan(z.ImgComp[n].W2 * y2 + x2),
                                        z.ImgComp[n].W2,
                                        data
                                    );
                                }
                            }
                        }

                        if (--z.Todo <= 0)
                        {
                            if (z.CodeBits < 24)
                            {
                                GrowBuffer(z);
                            }

                            if (!Restart(z.Marker))
                            {
                                return true;
                            }

                            JpegReset(z);
                        }
                    }
                }

                return true;
            }
        }
        else
        {
            if (z.ScanN == 1)
            {
                var n = z.Order[0];
                var w = (z.ImgComp[n].X + 7) >> 3;
                var h = (z.ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var data = z.ImgComp[n].Coeff.AsSpan(64 * (i + j * z.ImgComp[n].CoeffW));
                        if (z.SpecStart == 0)
                        {
                            if (!JpegDecodeBlockProgDC(z, data, z.HuffDC[z.ImgComp[n].HD], n))
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var ha = z.ImgComp[n].HA;
                            if (!JpegDecodeBlockProgAC(z, data, z.HuffAC[ha], z.FastAC[ha]))
                            {
                                return false;
                            }
                        }

                        if (--z.Todo <= 0)
                        {
                            if (z.CodeBits < 24)
                            {
                                GrowBuffer(z);
                            }

                            if (!Restart(z.Marker))
                            {
                                return true;
                            }

                            JpegReset(z);
                        }
                    }
                }

                return true;
            }
            else
            {
                for (var j = 0; j < z.ImgMcuY; j++)
                {
                    for (var i = 0; i < z.ImgMcuX; i++)
                    {
                        for (var k = 0; k < z.ScanN; k++)
                        {
                            var n = z.Order[k];
                            for (var y = 0; y < z.ImgComp[n].V; y++)
                            {
                                for (var x = 0; x < z.ImgComp[n].H; x++)
                                {
                                    var x2 = (i * z.ImgComp[n].H + x);
                                    var y2 = (j * z.ImgComp[n].V + y);
                                    var data = z.ImgComp[n]
                                        .Coeff.AsSpan(64 * (x2 + y2 * z.ImgComp[n].CoeffW));
                                    if (
                                        !JpegDecodeBlockProgDC(
                                            z,
                                            data,
                                            z.HuffDC[z.ImgComp[n].HD],
                                            n
                                        )
                                    )
                                    {
                                        return false;
                                    }
                                }
                            }
                        }

                        if (--z.Todo <= 0)
                        {
                            if (z.CodeBits < 24)
                            {
                                GrowBuffer(z);
                            }

                            if (!Restart(z.Marker))
                            {
                                return true;
                            }

                            JpegReset(z);
                        }
                    }
                }

                return true;
            }
        }
    }

    public static byte SkipJpegJunkAtEnd(Jpeg j)
    {
        while (!AtEof(j.S))
        {
            var x = Get8(j.S);
            while (x == 0xFF)
            {
                if (AtEof(j.S))
                {
                    return Constants.MarkerNone;
                }

                x = Get8(j.S);
                if (x != 0x00 && x != 0xFF)
                {
                    return x;
                }
            }
        }

        return Constants.MarkerNone;
    }

    public static void JpegDequantize(Span<short> data, ushort[] dequant)
    {
        for (var i = 0; i < 64; i++)
        {
            data[i] *= unchecked((short)dequant[i]);
        }
    }

    public static void JpegFinish(Jpeg z)
    {
        if (z.Progressive)
        {
            for (var n = 0; n < z.S.ImgN; n++)
            {
                var w = (z.ImgComp[n].X + 7) >> 3;
                var h = (z.ImgComp[n].Y + 7) >> 3;
                for (var j = 0; j < h; j++)
                {
                    for (var i = 0; i < w; i++)
                    {
                        var data = z.ImgComp[n].Coeff.AsSpan(64 * (i + j * z.ImgComp[n].CoeffW));
                        JpegDequantize(data, z.Dequant[z.ImgComp[n].TQ]);
                        z.IdctBlockKernel(
                            z.ImgComp[n].Data.AsSpan(z.ImgComp[n].W2 * j * 8 + i * 8),
                            z.ImgComp[n].W2,
                            data
                        );
                    }
                }
            }
        }
    }

    public static bool DecodeJpegImage(Jpeg j)
    {
        int m;
        for (m = 0; m < 4; m++)
        {
            j.ImgComp[m].RawData = null;
            j.ImgComp[m].RawCoeff = null;
        }

        j.RestartInterval = 0;
        if (!DecodeJpegHeader(j, Scan.Load))
        {
            return false;
        }

        m = GetMarker(j);
        while (!Eoi(m))
        {
            if (Sos(m))
            {
                if (!ProcessScanHeader(j))
                {
                    return false;
                }

                if (!ParseEntropyCodedData(j))
                {
                    return false;
                }

                if (j.Marker == Constants.MarkerNone)
                {
                    j.Marker = SkipJpegJunkAtEnd(j);
                }

                m = GetMarker(j);
                if (Restart(m))
                {
                    m = GetMarker(j);
                }
            }
            else if (Dnl(m))
            {
                var ld = Get16BE(j.S);
                var nl = unchecked((uint)Get16BE(j.S));
                if (ld != 4)
                {
                    return Error("Bad DNL length. Corrupt JPEG.");
                }

                if (nl != j.S.ImgY)
                {
                    return Error("Bad DNL height. Corrupt JPEG.");
                }

                m = GetMarker(j);
            }
            else
            {
                if (!ProcessMarker(j, m))
                {
                    return true;
                }

                m = GetMarker(j);
            }
        }

        if (j.Progressive)
        {
            JpegFinish(j);
        }

        return true;
    }

    public static bool FreeJpegComponents(Jpeg z, int ncomp, bool why)
    {
        for (var i = 0; i < ncomp; i++)
        {
            z.ImgComp[i].RawData = null;
            z.ImgComp[i].Data = null;
            z.ImgComp[i].RawCoeff = null;
            z.ImgComp[i].Coeff = null;
            z.ImgComp[i].LineBuf = null;
        }

        return why;
    }

    public static void CleanupJpeg(Jpeg j) => FreeJpegComponents(j, j.S.ImgN, false);

    public static byte Blinn8x8(byte x, byte y)
    {
        var t = (uint)(x * y + 128);
        return unchecked((byte)((t + (t >> 8)) >> 8));
    }

    public static byte ComputeY(int r, int g, int b) =>
        unchecked((byte)(((r * 77) + (g * 150) + (29 * b)) >> 8));

    public static byte[]? LoadJpegImage(
        Jpeg z,
        out int outX,
        out int outY,
        out int comp,
        int reqComp
    )
    {
        outX = 0;
        outY = 0;
        comp = 0;
        return LoadJpegImageCore(z, ref outX, ref outY, ref comp, reqComp);
    }

    private static byte[]? LoadJpegImageCore(
        Jpeg z,
        ref int outX,
        ref int outY,
        ref int comp,
        int reqComp
    )
    {
        z.S.ImgN = 0;
        if (reqComp is < 0 or > 4)
        {
            return ErrorPtr("Bad required component count. Internal error.");
        }

        if (!DecodeJpegImage(z))
        {
            CleanupJpeg(z);
            return null;
        }

        var n =
            reqComp != 0 ? reqComp
            : z.S.ImgN >= 3 ? 3
            : 1;

        var isRgb = z.S.ImgN == 3 && (z.Rgb == 3 || (z.App14ColorTransform == 0 && z.JFif == 0));

        int decodeN = z.S.ImgN == 3 && n < 3 && !isRgb ? 1 : z.S.ImgN;
        if (decodeN <= 0)
        {
            CleanupJpeg(z);
            return null;
        }

        {
            byte[] output;
            var coutput = new (byte[] Data, int Offset)[4];
            var resComp = new Resample[4];
            for (var k = 0; k < decodeN; k++)
            {
                var r = resComp[k] = new Resample();
                z.ImgComp[k].LineBuf = new byte[unchecked((int)z.S.ImgX) + 3];

                r.HS = z.ImgHMax / z.ImgComp[k].H;
                r.VS = z.ImgVMax / z.ImgComp[k].V;
                r.YStep = r.VS >> 1;
                r.WLores = unchecked((int)((z.S.ImgX + r.HS - 1) / r.HS));
                r.YPos = 0;
                r.Line0 = r.Line1 = z.ImgComp[k].Data;
                r.Line0Position = r.Line1Position = 0;
                r.ResampleAction = r.HS switch
                {
                    1 when r.VS == 1 => ResampleRow1,
                    1 when r.VS == 2 => ResampleRowV2,
                    2 when r.VS == 1 => ResampleRowH2,
                    2 when r.VS == 2 => z.ResampleRowHV2Kernel,
                    _ => ResampleRowGeneric,
                };
            }

            output = new byte[unchecked((int)(n * z.S.ImgX * z.S.ImgY)) + 1];

            for (var j = 0; j < z.S.ImgY; j++)
            {
                Span<byte> @out = output.AsSpan(unchecked((int)(n * z.S.ImgX)) * j);
                for (var k = 0; k < decodeN; k++)
                {
                    var r = resComp[k];
                    var yBot = r.YStep >= (r.VS >> 1);
                    coutput[k] = r.ResampleAction(
                        z.ImgComp[k].LineBuf!,
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
                        if (++r.YPos < z.ImgComp[k].Y)
                        {
                            r.Line1Position += z.ImgComp[k].W2;
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
                    if (z.S.ImgN == 3)
                    {
                        if (isRgb)
                        {
                            for (var i = 0; i < z.S.ImgX; i++)
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
                            z.YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)z.S.ImgX), n);
                        }
                    }
                    else if (z.S.ImgN == 4)
                    {
                        if (z.App14ColorTransform == 0)
                        {
                            for (var i = 0; i < z.S.ImgX; i++)
                            {
                                var m = c3[i];
                                @out[0] = Blinn8x8(c0[i], m);
                                @out[1] = Blinn8x8(c1[i], m);
                                @out[2] = Blinn8x8(c2[i], m);
                                @out[3] = 255;
                                @out = @out[n..];
                            }
                        }
                        else if (z.App14ColorTransform == 2)
                        {
                            z.YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)z.S.ImgX), n);
                            for (var i = 0; i < z.S.ImgX; i++)
                            {
                                var m = c3[i];
                                @out[0] = Blinn8x8(unchecked((byte)(255 - @out[0])), m);
                                @out[1] = Blinn8x8(unchecked((byte)(255 - @out[1])), m);
                                @out[2] = Blinn8x8(unchecked((byte)(255 - @out[2])), m);
                                @out = @out[n..];
                            }
                        }
                        else
                        {
                            z.YCbCrToRgbKernel(@out, y, c1, c2, unchecked((int)z.S.ImgX), n);
                        }
                    }
                    else
                    {
                        for (var i = 0; i < z.S.ImgX; i++)
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
                            for (var i = 0; i < z.S.ImgX; i++)
                            {
                                @out[0] = ComputeY(c0[i], c1[i], c2[i]);
                                @out = @out[1..];
                            }
                        }
                        else
                        {
                            for (var i = 0; i < z.S.ImgX; i++, @out = @out[2..])
                            {
                                @out[0] = ComputeY(c0[i], c1[i], c2[i]);
                                @out[1] = 255;
                            }
                        }
                    }
                    else if (z.S.ImgN == 4 && z.App14ColorTransform == 0)
                    {
                        for (var i = 0; i < z.S.ImgX; i++)
                        {
                            var m = c3[i];
                            var r = Blinn8x8(c0[i], m);
                            var g = Blinn8x8(c1[i], m);
                            var b = Blinn8x8(c2[i], m);
                            @out[0] = ComputeY(r, g, b);
                            @out[1] = 255;
                            @out = @out[n..];
                        }
                    }
                    else if (z.S.ImgN == 4 && z.App14ColorTransform == 2)
                    {
                        for (var i = 0; i < z.S.ImgX; i++)
                        {
                            @out[0] = Blinn8x8(unchecked((byte)(255 - c0[i])), c3[i]);

                            @out[1] = 255;
                            @out = @out[n..];
                        }
                    }
                    else
                    {
                        var y = c0;
                        if (n == 1)
                        {
                            for (var i = 0; i < z.S.ImgX; i++)
                            {
                                @out[i] = y[i];
                            }
                        }
                        else
                        {
                            for (var i = 0; i < z.S.ImgX; i++)
                            {
                                @out[0] = y[i];
                                @out[1] = 255;
                                @out = @out[2..];
                            }
                        }
                    }
                }
            }

            CleanupJpeg(z);
            outX = unchecked((int)z.S.ImgX);
            outY = unchecked((int)z.S.ImgY);
            comp = z.S.ImgN >= 3 ? 3 : 1;

            return output;
        }
    }

    public static byte[]? JpegLoad(
        Context s,
        out int x,
        out int y,
        out int channelCount,
        int requiredChannels,
        ResultInfo _
    )
    {
        var j = new Jpeg { S = s };
        if (!AllocJpegTables(j))
        {
            x = y = channelCount = 0;
            return null;
        }

        SetupJpeg(j);
        var result = LoadJpegImage(j, out x, out y, out channelCount, requiredChannels);
        FreeJpegTables(j);
        return result;
    }

    public static byte[]? LoadMain(
        Context s,
        out int x,
        out int y,
        out int comp,
        int reqComp,
        ResultInfo ri,
        int _
    )
    {
        ri.BitsPerChannel = 8;
        ri.ChannelOrder = default;
        ri.NumChannels = 0;
        if (JpegTest(s))
        {
            return JpegLoad(s, out x, out y, out comp, reqComp, ri);
        }

        x = y = comp = 0;
        return ErrorPtr("Unsupported image format. No decoder recognized the data.");
    }

    public static void StoreRow(Span<byte> output, ref int offset, int stride, V8 pair)
    {
        var u = pair.AsUInt64();
        MemoryMarshal.Write(output.Slice(offset, 8), u.GetElement(0));
        offset += stride;
        MemoryMarshal.Write(output.Slice(offset, 8), u.GetElement(1));
        offset += stride;
    }

    public static V16 DctConst(int x, int y) =>
        Vector128.Create((y << 16) | (x & 0xFFFF)).AsInt16();

    public static V16 UnpackLow16(V16 a, V16 b) =>
        Sse2.IsSupported ? Sse2.UnpackLow(a, b) : AdvSimd.Arm64.ZipLow(a, b);

    public static V16 UnpackHigh16(V16 a, V16 b) =>
        Sse2.IsSupported ? Sse2.UnpackHigh(a, b) : AdvSimd.Arm64.ZipHigh(a, b);

    public static V8 UnpackLow8(V8 a, V8 b) =>
        Sse2.IsSupported ? Sse2.UnpackLow(a, b) : AdvSimd.Arm64.ZipLow(a, b);

    public static V8 UnpackHigh8(V8 a, V8 b) =>
        Sse2.IsSupported ? Sse2.UnpackHigh(a, b) : AdvSimd.Arm64.ZipHigh(a, b);

    public static V32 MultiplyAddAdjacent(V16 a, V16 b) =>
        Sse2.IsSupported
            ? Sse2.MultiplyAddAdjacent(a, b)
            : AdvSimd.Arm64.AddPairwise(
                AdvSimd.MultiplyWideningLower(a.GetLower(), b.GetLower()),
                AdvSimd.MultiplyWideningUpper(a, b)
            );

    public static V16 PackSigned32(V32 lo, V32 hi) =>
        Sse2.IsSupported
            ? Sse2.PackSignedSaturate(lo, hi)
            : AdvSimd.ExtractNarrowingSaturateUpper(AdvSimd.ExtractNarrowingSaturateLower(lo), hi);

    public static V8 PackUnsigned16(V16 lo, V16 hi) =>
        Sse2.IsSupported
            ? Sse2.PackUnsignedSaturate(lo, hi)
            : AdvSimd.ExtractNarrowingSaturateUnsignedUpper(
                AdvSimd.ExtractNarrowingSaturateUnsignedLower(lo),
                hi
            );

    public static void DctRot(V16 x, V16 y, V16 c0, V16 c1, out Wide out0, out Wide out1)
    {
        var lo = UnpackLow16(x, y);
        var hi = UnpackHigh16(x, y);
        out0 = new Wide(MultiplyAddAdjacent(lo, c0), MultiplyAddAdjacent(hi, c0));
        out1 = new Wide(MultiplyAddAdjacent(lo, c1), MultiplyAddAdjacent(hi, c1));
    }

    public static Wide DctWiden(V16 v) =>
        new(
            Vector128.ShiftRightArithmetic(UnpackLow16(V16.Zero, v).AsInt32(), 4),
            Vector128.ShiftRightArithmetic(UnpackHigh16(V16.Zero, v).AsInt32(), 4)
        );

    public static Wide WAdd(Wide a, Wide b) => new(a.Lo + b.Lo, a.Hi + b.Hi);

    public static Wide WSub(Wide a, Wide b) => new(a.Lo - b.Lo, a.Hi - b.Hi);

    public static void DctButterfly(Wide a, Wide b, V32 bias, int shift, out V16 out0, out V16 out1)
    {
        var biased = new Wide(a.Lo + bias, a.Hi + bias);
        var sum = WAdd(biased, b);
        var dif = WSub(biased, b);
        out0 = PackSigned32(
            Vector128.ShiftRightArithmetic(sum.Lo, shift),
            Vector128.ShiftRightArithmetic(sum.Hi, shift)
        );
        out1 = PackSigned32(
            Vector128.ShiftRightArithmetic(dif.Lo, shift),
            Vector128.ShiftRightArithmetic(dif.Hi, shift)
        );
    }

    public static void Interleave16(ref V16 a, ref V16 b)
    {
        var tmp = a;
        a = UnpackLow16(a, b);
        b = UnpackHigh16(tmp, b);
    }

    public static void Interleave8(ref V8 a, ref V8 b)
    {
        var tmp = a;
        a = UnpackLow8(a, b);
        b = UnpackHigh8(tmp, b);
    }

    public static void DctPass(
        ref V16 row0,
        ref V16 row1,
        ref V16 row2,
        ref V16 row3,
        ref V16 row4,
        ref V16 row5,
        ref V16 row6,
        ref V16 row7,
        V32 bias,
        int shift
    )
    {
        DctRot(row2, row6, Constants.Rot00, Constants.Rot01, out var t2E, out var t3E);
        var t0E = DctWiden(row0 + row4);
        var t1E = DctWiden(row0 - row4);
        var x0 = WAdd(t0E, t3E);
        var x3 = WSub(t0E, t3E);
        var x1 = WAdd(t1E, t2E);
        var x2 = WSub(t1E, t2E);

        DctRot(row7, row3, Constants.Rot20, Constants.Rot21, out var y0O, out var y2O);
        DctRot(row5, row1, Constants.Rot30, Constants.Rot31, out var y1O, out var y3O);
        DctRot(
            row1 + row7,
            row3 + row5,
            Constants.Rot10,
            Constants.Rot11,
            out var y4O,
            out var y5O
        );
        var x4 = WAdd(y0O, y4O);
        var x5 = WAdd(y1O, y5O);
        var x6 = WAdd(y2O, y5O);
        var x7 = WAdd(y3O, y4O);

        DctButterfly(x0, x7, bias, shift, out row0, out row7);
        DctButterfly(x1, x6, bias, shift, out row1, out row6);
        DctButterfly(x2, x5, bias, shift, out row2, out row5);
        DctButterfly(x3, x4, bias, shift, out row3, out row4);
    }

    public static V32 MultiplyAddAdjacent128(V16 a, V16 b)
    {
        var r0 = a.GetElement(0) * b.GetElement(0) + a.GetElement(1) * b.GetElement(1);
        var r1 = a.GetElement(2) * b.GetElement(2) + a.GetElement(3) * b.GetElement(3);
        var r2 = a.GetElement(4) * b.GetElement(4) + a.GetElement(5) * b.GetElement(5);
        var r3 = a.GetElement(6) * b.GetElement(6) + a.GetElement(7) * b.GetElement(7);

        return Vector128.Create(r0, r1, r2, r3);
    }

    public static int Float2Fixed(float value) => unchecked((int)(value * 4096F + .5F) << 8);

    public static byte[]? Convert16To8(byte[] orig, int w, int h, int channels)
    {
        var imgLen = w * h * channels;
        var reduced = new byte[imgLen];
        var source = MemoryMarshal.Cast<byte, ushort>(orig.AsSpan());
        for (var i = 0; i < imgLen; i++)
        {
            reduced[i] = unchecked((byte)((source[i] >> 8) & 0xFF));
        }

        return reduced;
    }

    public static void SetFlipVerticallyOnLoad(bool flagTrueIfShouldFlip)
    {
        Constants.VerticallyFlipOnLoadGlobal = flagTrueIfShouldFlip;
        Constants.VerticallyFlipOnLoadSet = true;
    }

    public static void VerticalFlip(byte[] image, int w, int h, int bytesPerPixel)
    {
        var bytesPerRow = w * bytesPerPixel;
        Span<byte> temp = stackalloc byte[2048];
        for (var row = 0; row < (h >> 1); row++)
        {
            var row0 = row * bytesPerRow;
            var row1 = (h - row - 1) * bytesPerRow;
            var bytesLeft = bytesPerRow;
            while (bytesLeft > 0)
            {
                var bytesCopy = bytesLeft < 2048 ? bytesLeft : 2048;
                image.AsSpan(row0, bytesCopy).CopyTo(temp);
                image.AsSpan(row1, bytesCopy).CopyTo(image.AsSpan(row0, bytesCopy));
                temp[..bytesCopy].CopyTo(image.AsSpan(row1, bytesCopy));
                row0 += bytesCopy;
                row1 += bytesCopy;
                bytesLeft -= bytesCopy;
            }
        }
    }

    public static byte[]? LoadAndPostprocess8Bit(
        Context s,
        out int x,
        out int y,
        out int comp,
        int reqComp
    )
    {
        var ri = new ResultInfo();
        var result = LoadMain(s, out x, out y, out comp, reqComp, ri, 8);
        if (result is null)
        {
            return null;
        }

        Debug.Assert(ri.BitsPerChannel == 8 || ri.BitsPerChannel == 16);
        if (ri.BitsPerChannel != 8)
        {
            result = Convert16To8(result, x, y, reqComp == 0 ? comp : reqComp);
            ri.BitsPerChannel = 8;
        }

        if (Constants.VerticallyFlipOnLoad)
        {
            var channels = reqComp != 0 ? reqComp : comp;
            VerticalFlip(result!, x, y, channels);
        }

        return result;
    }

    public enum Scan
    {
        Load,
        Type,
        Header,
    }

    public readonly struct Wide(V32 lo, V32 hi)
    {
        public readonly V32 Lo = lo;
        public readonly V32 Hi = hi;
    }
}
