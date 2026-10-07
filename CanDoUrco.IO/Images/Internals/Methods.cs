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

    public static bool Eof(object? user) =>
        user is not Stream stream || (stream.Position >= stream.Length);

    public static bool Eoi(int x) => x == 0xD9;

    public static bool Sos(int x) => x == 0xDA;

    public static Context StartFile(Stream stream)
    {
        var context = new Context();
        context.IO.Read = Read;
        context.IO.Skip = Skip;
        context.IO.Eof = Eof;
        context.StartCallbacks(context.IO, stream);
        return context;
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

    public static bool Soi(int value) => value == 0xD8;

    public static bool Sof(int value) => value == 0xC0 || value == 0xC1 || value == 0xC2;

    public static bool SofProgressive(int value) => value == 0xC2;

    public static bool Dnl(int value) => value == 0xDC;

    public static bool Restart(int x) => x >= 0xD0 && x <= 0xD7;

    public static byte Blinn8x8(byte x, byte y)
    {
        var t = (uint)(x * y + 128);
        return unchecked((byte)((t + (t >> 8)) >> 8));
    }

    public static byte ComputeY(int r, int g, int b) =>
        unchecked((byte)(((r * 77) + (g * 150) + (29 * b)) >> 8));

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
}
