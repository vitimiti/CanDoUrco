// An SDL3 interop library with dotnet safety and idioms.
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

using System.Runtime.InteropServices;
using CanDoUrco.Sdl3.Exceptions;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3.Pixels;

public sealed record PixelFormat(uint Value)
{
    public static PixelFormat Unknown => new(0U);
    public static PixelFormat Index1Lsb => new(0x1110_0100U);
    public static PixelFormat Index1Msb => new(0x1120_0100);
    public static PixelFormat Index2Lsb => new(0x1C10_0200);
    public static PixelFormat Index2Msb => new(0x1C20_0200);
    public static PixelFormat Index4Lsb => new(0x1210_0400);
    public static PixelFormat Index4Msb => new(0x1220_0400);
    public static PixelFormat Index8 => new(0x1300_0801);
    public static PixelFormat Rgb332 => new(0x1411_0801);
    public static PixelFormat Xrgb4444 => new(0x1512_0C02);
    public static PixelFormat Xbgr4444 => new(0x1552_0C02);
    public static PixelFormat Xrgb1555 => new(0x1513_0F02);
    public static PixelFormat Xbgr1555 => new(0x1553_0F02);
    public static PixelFormat Argb4444 => new(0x1532_1002);
    public static PixelFormat Rgba4444 => new(0x1542_1002);
    public static PixelFormat Abgr4444 => new(0x1572_1002);
    public static PixelFormat Bgra4444 => new(0x1582_1002);
    public static PixelFormat Argb1555 => new(0x1533_1002);
    public static PixelFormat Rgba5551 => new(0x1544_1002);
    public static PixelFormat Abgr1555 => new(0x1573_1002);
    public static PixelFormat Bgra5551 => new(0x1584_1002);
    public static PixelFormat Rgb565 => new(0x1515_1002);
    public static PixelFormat Bgr565 => new(0x1555_1002);
    public static PixelFormat Rgb24 => new(0x1710_1803);
    public static PixelFormat Bgr24 => new(0x1740_1803);
    public static PixelFormat Xrgb8888 => new(0x1616_1804);
    public static PixelFormat Rgbx8888 => new(0x1626_1804);
    public static PixelFormat Xbgr8888 => new(0x1656_1804);
    public static PixelFormat Bgrx8888 => new(0x1666_1804);
    public static PixelFormat Argb8888 => new(0x1636_2004);
    public static PixelFormat Rgba8888 => new(0x1646_2004);
    public static PixelFormat Abgr8888 => new(0x1676_2004);
    public static PixelFormat Bgra8888 => new(0x1686_2004);
    public static PixelFormat Xrgb2101010 => new(0x1617_2004);
    public static PixelFormat Xbgr2101010 => new(0x1657_2004);
    public static PixelFormat Argb2101010 => new(0x1637_2004);
    public static PixelFormat Abgr2101010 => new(0x1677_2004);
    public static PixelFormat Rgb48 => new(0x1810_3006);
    public static PixelFormat Bgr48 => new(0x1840_3006);
    public static PixelFormat Rgba64 => new(0x1820_4008);
    public static PixelFormat Argb64 => new(0x1830_4008);
    public static PixelFormat Bgra64 => new(0x1850_4008);
    public static PixelFormat Abgr64 => new(0x1860_4008);
    public static PixelFormat Rgb48Float => new(0x1A10_3006);
    public static PixelFormat Bgr48Float => new(0x1A40_3006);
    public static PixelFormat Rgba64Float => new(0x1A20_4008);
    public static PixelFormat Argb64Float => new(0x1A30_4008);
    public static PixelFormat Bgra64Float => new(0x1A50_4008);
    public static PixelFormat Abgr64Float => new(0x1A60_4008);
    public static PixelFormat Rgb96Float => new(0x1B10_600C);
    public static PixelFormat Bgr96Float => new(0x1B40_600C);
    public static PixelFormat Rgba128Float => new(0x1B20_8010);
    public static PixelFormat Argb128Float => new(0x1B30_8010);
    public static PixelFormat Bgra128Float => new(0x1B50_8010);
    public static PixelFormat Abgr128Float => new(0x1B60_8010);
    public static PixelFormat Yv12 => new(0x3231_5659);
    public static PixelFormat Iyuv => new(0x5655_5949);
    public static PixelFormat Yuy2 => new(0x3259_5559);
    public static PixelFormat Uyvy => new(0x5956_5955);
    public static PixelFormat Yvyu => new(0x5559_5659);
    public static PixelFormat Nv12 => new(0x3231_564E);
    public static PixelFormat Nv21 => new(0x3132_564E);
    public static PixelFormat P010 => new(0x3031_3050);
    public static PixelFormat ExternalOes => new(0x2053_454F);
    public static PixelFormat Mjpg => new(0x4750_4A4D);
    public static PixelFormat Rgba32 => BitConverter.IsLittleEndian ? Abgr8888 : Rgba8888;
    public static PixelFormat Argb32 => BitConverter.IsLittleEndian ? Bgra8888 : Argb8888;
    public static PixelFormat Bgra32 => BitConverter.IsLittleEndian ? Argb8888 : Bgra8888;
    public static PixelFormat Abgr32 => BitConverter.IsLittleEndian ? Rgba8888 : Abgr8888;
    public static PixelFormat Rgbx32 => BitConverter.IsLittleEndian ? Xbgr8888 : Rgbx8888;
    public static PixelFormat Xrgb32 => BitConverter.IsLittleEndian ? Bgrx8888 : Xrgb8888;
    public static PixelFormat Bgrx32 => BitConverter.IsLittleEndian ? Xrgb8888 : Bgrx8888;
    public static PixelFormat Xbgr32 => BitConverter.IsLittleEndian ? Rgbx8888 : Xbgr8888;

    public uint PixelFlag => (Value >> 28) & 0x0F;
    public PixelType PixelType => (PixelType)((Value >> 24) & 0x0F);
    public uint PixelOrder => (Value >> 20) & 0x0F;
    public PackedOrder PackedOrder => IsPacked ? (PackedOrder)PixelOrder : PackedOrder.None;
    public ArrayOrder ArrayOrder => IsArray ? (ArrayOrder)PixelOrder : ArrayOrder.None;
    public PackedLayout PixelLayout => (PackedLayout)((Value >> 16) & 0x0F);
    public byte BitsPerPixel => IsFourCC ? (byte)0 : (byte)((Value >> 8) & 0xFF);
    public byte BytesPerPixel =>
        IsFourCC
            ? this == Yuy2 || this == Uyvy || this == Yvyu || this == P010
                ? (byte)2
                : (byte)1
            : (byte)((Value >> 0) & 0xFF);

    public bool IsIndexed =>
        !IsFourCC
        && (
            PixelType
            is PixelType.Index1
                or PixelType.Index2
                or PixelType.Index4
                or PixelType.Index8
        );

    public bool IsPacked =>
        !IsFourCC && (PixelType is PixelType.Packed8 or PixelType.Packed16 or PixelType.Packed32);

    public bool IsArray =>
        !IsFourCC
        && (
            PixelType
            is PixelType.ArrayU8
                or PixelType.ArrayU16
                or PixelType.ArrayU32
                or PixelType.ArrayF16
                or PixelType.ArrayF32
        );

    public bool Is10Bit =>
        !IsFourCC && (PixelType is PixelType.Packed32 && PixelLayout is PackedLayout.Bits2101010);

    public bool IsFloat => !IsFourCC && (PixelType is PixelType.ArrayF16 or PixelType.ArrayF32);

    public bool IsAlpha =>
        (
            IsPacked
            && PackedOrder
                is PackedOrder.Argb
                    or PackedOrder.Rgba
                    or PackedOrder.Abgr
                    or PackedOrder.Bgra
        )
        || (
            IsArray
            && ArrayOrder
                is ArrayOrder.Argb
                    or ArrayOrder.Rgba
                    or ArrayOrder.Abgr
                    or ArrayOrder.Bgra
        );

    public bool IsFourCC => Value != 0 && PixelFlag != 1;

    public static PixelFormat DefineFourCC(char a, char b, char c, char d) =>
        new((uint)(a | (b << 8) | (c << 16) | (d << 24)));

    public static PixelFormat Define(
        PixelType type,
        ArrayOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    ) => Define(type, (uint)order, layout, bits, bytes);

    public static PixelFormat Define(
        PixelType type,
        BitmapOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    ) => Define(type, (uint)order, layout, bits, bytes);

    public static PixelFormat Define(
        PixelType type,
        PackedOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    ) => Define(type, (uint)order, layout, bits, bytes);

    public static PixelFormat FromMasks(
        int bitsPerPixel,
        (uint Red, uint Green, uint Blue, uint Alpha) masks
    ) =>
        new(
            Sdl.GetPixelFormatForMasks(
                bitsPerPixel,
                masks.Red,
                masks.Green,
                masks.Blue,
                masks.Alpha
            )
        );

    public (int BitsPerPixel, uint Red, uint Green, uint Blue, uint Alpha) GetMasks() =>
        !Sdl.GetMasksForPixelFormat(
            Value,
            out var bpp,
            out var rMask,
            out var gMask,
            out var bMask,
            out var aMask
        )
            ? throw new Sdl3Exception($"Unable to get masks for pixel format {this}")
            : ((int BitsPerPixel, uint Red, uint Green, uint Blue, uint Alpha))
                (bpp, rMask, gMask, bMask, aMask);

    public unsafe PixelFormatDetails GetDetails()
    {
        var details = Sdl.GetPixelFormatDetails(Value);
        return details is null
            ? throw new Sdl3Exception($"Unable to get details for pixel format {this}")
            : new PixelFormatDetails(details);
    }

    public override unsafe string ToString()
    {
        var ptr = Sdl.GetPixelFormatName(Value);
        return ptr is null ? string.Empty : Marshal.PtrToStringUTF8((nint)ptr) ?? string.Empty;
    }

    private static PixelFormat Define(
        PixelType type,
        uint order,
        PackedLayout layout,
        byte bits,
        byte bytes
    ) =>
        new(
            (1 << 28)
                | ((uint)type << 24)
                | (order << 20)
                | ((uint)layout << 16)
                | ((uint)bits << 8)
                | bytes
        );
}
