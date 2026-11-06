// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const byte AlphaOpaque = 255;
    public const float AlphaOpaqueFloat = 1F;
    public const byte AlphaTransparent = 0;
    public const float AlphaTransparentFloat = 0F;

    public static PixelFormat PixelFormatUnknown => 0x0000_0000U;
    public static PixelFormat PixelFormatIndex1Lsb => 0x1110_0100U;
    public static PixelFormat PixelFormatIndex1Msb => 0x1120_0100U;
    public static PixelFormat PixelFormatIndex2Lsb => 0x1C10_0200U;
    public static PixelFormat PixelFormatIndex2Msb => 0x1C20_0200U;
    public static PixelFormat PixelFormatIndex4Lsb => 0x1210_0400U;
    public static PixelFormat PixelFormatIndex4Msb => 0x1220_0400U;
    public static PixelFormat PixelFormatIndex8 => 0x1300_0801U;
    public static PixelFormat PixelFormatRgb332 => 0x1411_0801U;
    public static PixelFormat PixelFormatXrgb4444 => 0x1512_0C02U;
    public static PixelFormat PixelFormatXbgr4444 => 0x1552_0C02U;
    public static PixelFormat PixelFormatXrgb1555 => 0x1513_0F02U;
    public static PixelFormat PixelFormatXbgr1555 => 0x1553_0F02U;
    public static PixelFormat PixelFormatArgb4444 => 0x1532_1002U;
    public static PixelFormat PixelFormatRgba4444 => 0x1542_1002U;
    public static PixelFormat PixelFormatAbgr4444 => 0x1572_1002U;
    public static PixelFormat PixelFormatBgra4444 => 0x1582_1002U;
    public static PixelFormat PixelFormatArgb1555 => 0x1533_1002U;
    public static PixelFormat PixelFormatRgba5551 => 0x1544_1002U;
    public static PixelFormat PixelFormatAbgr1555 => 0x1573_1002U;
    public static PixelFormat PixelFormatBgra5551 => 0x1584_1002U;
    public static PixelFormat PixelFormatRgb565 => 0x1515_1002U;
    public static PixelFormat PixelFormatBgr565 => 0x1555_1002U;
    public static PixelFormat PixelFormatRgb24 => 0x1710_1803U;
    public static PixelFormat PixelFormatBgr24 => 0x1740_1803U;
    public static PixelFormat PixelFormatXrgb8888 => 0x1616_1804U;
    public static PixelFormat PixelFormatRgbx8888 => 0x1626_1804U;
    public static PixelFormat PixelFormatXbgr8888 => 0x1656_1804U;
    public static PixelFormat PixelFormatBgrx8888 => 0x1666_1804U;
    public static PixelFormat PixelFormatArgb8888 => 0x1636_2004U;
    public static PixelFormat PixelFormatRgba8888 => 0x1646_2004U;
    public static PixelFormat PixelFormatAbgr8888 => 0x1676_2004U;
    public static PixelFormat PixelFormatBgra8888 => 0x1686_2004U;
    public static PixelFormat PixelFormatXrgb2101010 => 0x1617_2004U;
    public static PixelFormat PixelFormatXbgr2101010 => 0x1657_2004U;
    public static PixelFormat PixelFormatArgb2101010 => 0x1637_2004U;
    public static PixelFormat PixelFormatAbgr2101010 => 0x1677_2004U;
    public static PixelFormat PixelFormatRgb48 => 0x1810_3006U;
    public static PixelFormat PixelFormatBgr48 => 0x1840_3006U;
    public static PixelFormat PixelFormatRgba64 => 0x1820_4008U;
    public static PixelFormat PixelFormatArgb64 => 0x1830_4008U;
    public static PixelFormat PixelFormatBgra64 => 0x1850_4008U;
    public static PixelFormat PixelFormatAbgr64 => 0x1860_4008U;
    public static PixelFormat PixelFormatRgb48Float => 0x1A10_3006U;
    public static PixelFormat PixelFormatBgr48Float => 0x1A40_3006U;
    public static PixelFormat PixelFormatRgba64Float => 0x1A20_4008U;
    public static PixelFormat PixelFormatArgb64Float => 0x1A30_4008U;
    public static PixelFormat PixelFormatBgra64Float => 0x1A50_4008U;
    public static PixelFormat PixelFormatAbgr64Float => 0x1A60_4008U;
    public static PixelFormat PixelFormatRgb96Float => 0x1B10_600CU;
    public static PixelFormat PixelFormatBgr96Float => 0x1B40_600CU;
    public static PixelFormat PixelFormatRgba128Float => 0x1B20_8010U;
    public static PixelFormat PixelFormatArgb128Float => 0x1B30_8010U;
    public static PixelFormat PixelFormatBgra128Float => 0x1B50_8010U;
    public static PixelFormat PixelFormatAbgr128Float => 0x1B60_8010U;
    public static PixelFormat PixelFormatYV12 => 0x3231_5659U;
    public static PixelFormat PixelFormatIyuv => 0x5655_5949U;
    public static PixelFormat PixelFormatYuy2 => 0x3259_5559U;
    public static PixelFormat PixelFormatUyvy => 0x5956_5955U;
    public static PixelFormat PixelFormatYvyu => 0x5559_5659U;
    public static PixelFormat PixelFormatNV12 => 0x3231_564EU;
    public static PixelFormat PixelFormatNV21 => 0x3132_564EU;
    public static PixelFormat PixelFormatP010 => 0x3031_3050U;
    public static PixelFormat PixelFormatExternalOes => 0x2053_454FU;
    public static PixelFormat PixelFormatMjpg => 0x4750_4A4DU;
    public static PixelFormat PixelFormatRgba32 =>
        BitConverter.IsLittleEndian ? PixelFormatAbgr8888 : PixelFormatRgba8888;
    public static PixelFormat PixelFormatArgb32 =>
        BitConverter.IsLittleEndian ? PixelFormatBgra8888 : PixelFormatArgb8888;
    public static PixelFormat PixelFormatBgra32 =>
        BitConverter.IsLittleEndian ? PixelFormatArgb8888 : PixelFormatBgra8888;
    public static PixelFormat PixelFormatAbgr32 =>
        BitConverter.IsLittleEndian ? PixelFormatRgba8888 : PixelFormatAbgr8888;
    public static PixelFormat PixelFormatRgbx32 =>
        BitConverter.IsLittleEndian ? PixelFormatXbgr8888 : PixelFormatRgbx8888;
    public static PixelFormat PixelFormatXrgb32 =>
        BitConverter.IsLittleEndian ? PixelFormatBgrx8888 : PixelFormatXrgb8888;
    public static PixelFormat PixelFormatBgrx32 =>
        BitConverter.IsLittleEndian ? PixelFormatXrgb8888 : PixelFormatBgrx8888;
    public static PixelFormat PixelFormatXbgr32 =>
        BitConverter.IsLittleEndian ? PixelFormatRgbx8888 : PixelFormatXbgr8888;

    public static ColorPrimaries ColorPrimariesUnknown => 0;
    public static ColorPrimaries ColorPrimariesBT709 => 1;
    public static ColorPrimaries ColorPrimariesUnspecified => 2;
    public static ColorPrimaries ColorPrimariesBT470M => 4;
    public static ColorPrimaries ColorPrimariesBT470BG => 5;
    public static ColorPrimaries ColorPrimariesBT601 => 6;
    public static ColorPrimaries ColorPrimariesSmpte240 => 7;
    public static ColorPrimaries ColorPrimariesGenericFilm => 8;
    public static ColorPrimaries ColorPrimariesBT2020 => 9;
    public static ColorPrimaries ColorPrimariesXyz => 10;
    public static ColorPrimaries ColorPrimariesSmpte431 => 11;
    public static ColorPrimaries ColorPrimariesSmpte432 => 12;
    public static ColorPrimaries ColorPrimariesEbu3213 => 22;
    public static ColorPrimaries ColorPrimariesCustom => 31;

    public static TransferCharacteristics TransferCharacteristicsUnknown => 0;
    public static TransferCharacteristics TransferCharacteristicsBT709 => 1;
    public static TransferCharacteristics TransferCharacteristicsUnspecified => 2;
    public static TransferCharacteristics TransferCharacteristicsGamma22 => 4;
    public static TransferCharacteristics TransferCharacteristicsGamma28 => 5;
    public static TransferCharacteristics TransferCharacteristicsBT601 => 6;
    public static TransferCharacteristics TransferCharacteristicsSmpte240 => 7;
    public static TransferCharacteristics TransferCharacteristicsLinear => 8;
    public static TransferCharacteristics TransferCharacteristicsLog100 => 9;
    public static TransferCharacteristics TransferCharacteristicsLog100Sqrt10 => 10;
    public static TransferCharacteristics TransferCharacteristicsIec61966 => 11;
    public static TransferCharacteristics TransferCharacteristicsBT1361 => 12;
    public static TransferCharacteristics TransferCharacteristicsSrgb => 13;
    public static TransferCharacteristics TransferCharacteristicsBT202010Bit => 14;
    public static TransferCharacteristics TransferCharacteristicsBT202012Bit => 15;
    public static TransferCharacteristics TransferCharacteristicsPQ => 16;
    public static TransferCharacteristics TransferCharacteristicsSmpte438 => 17;
    public static TransferCharacteristics TransferCharacteristicsHlg => 18;
    public static TransferCharacteristics TransferCharacteristicsCustom => 31;

    public static MatrixCoefficients MatrixCoefficientsIdentity => 0;
    public static MatrixCoefficients MatrixCoefficientsBT709 => 1;
    public static MatrixCoefficients MatrixCoefficientsUnspecified => 2;
    public static MatrixCoefficients MatrixCoefficientsFcc => 4;
    public static MatrixCoefficients MatrixCoefficientsBT470BG => 5;
    public static MatrixCoefficients MatrixCoefficientsBT601 => 6;
    public static MatrixCoefficients MatrixCoefficientsSmpte240 => 7;
    public static MatrixCoefficients MatrixCoefficientsYCgCo => 8;
    public static MatrixCoefficients MatrixCoefficientsBT2020Ncl => 9;
    public static MatrixCoefficients MatrixCoefficientsBT2020CL => 10;
    public static MatrixCoefficients MatrixCoefficientsSmpte2085 => 11;
    public static MatrixCoefficients MatrixCoefficientsCHromaDerivedNcl => 12;
    public static MatrixCoefficients MatrixCoefficientsChromaDerivedCL => 13;
    public static MatrixCoefficients MatrixCoefficientsIcTcp => 14;
    public static MatrixCoefficients MatrixCoefficientsCustom => 31;

    public static Colorspace ColorspaceUnknown => 0x0000_0000U;
    public static Colorspace ColorspaceSrgb => 0x1200_05A0U;
    public static Colorspace ColorspaceSrgbLinear => 0x1200_0500U;
    public static Colorspace ColorspaceHdr10 => 0x1200_2600U;
    public static Colorspace ColorspaceJpeg => 0x2200_04C6U;
    public static Colorspace ColorspaceBT601Limited => 0x2110_18C6U;
    public static Colorspace ColorspaceBT601Full => 0x2210_18C6U;
    public static Colorspace ColorspaceBT709Limited => 0x2110_0421U;
    public static Colorspace ColorspaceBT709Full => 0x2210_0421U;
    public static Colorspace ColorspaceBT2020Limited => 0x2110_2609U;
    public static Colorspace ColorspaceBT2020Full => 0x2210_2609U;
    public static Colorspace ColorspaceRgbDefault => ColorspaceSrgb;
    public static Colorspace ColorspaceYuvDefault => ColorspaceBT601Limited;

    private static PixelFormat DefinePixelFormat(
        PixelType type,
        uint order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return 1 << 28
            | (uint)type << 24
            | order << 20
            | (uint)layout << 16
            | (uint)bits << 8
            | (uint)bytes;
    }

    private static uint PixelOrder(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (format.Value >> 20) & 0x0F;
    }

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPixelFormatName",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string _GetPixelFormatName(PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMasksForPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetMasksForPixelFormat(
        PixelFormat format,
        out int bpp,
        out uint rMask,
        out uint gMask,
        out uint bMask,
        out uint aMask
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatForMasks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat _GetPixelFormatForMasks(
        int bpp,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatDetails")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial PixelFormatDetails _GetPixelFormatDetails(PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_CreatePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette _CreatePalette(int nColors);

    [LibraryImport(DllName, EntryPoint = "SDL_SetPaletteColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetPaletteColors(
        Palette palette,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Color, Color>), CountElementName = nameof(nColors))]
            Color[] colors,
        int firstColor,
        int nColors
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyPalette(Palette palette);

    [LibraryImport(DllName, EntryPoint = "SDL_MapRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapRgb(
        PixelFormatDetails format,
        Palette palette,
        byte r,
        byte g,
        byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_MapRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapRgba(
        PixelFormatDetails format,
        Palette palette,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetRgb(
        uint pixelValue,
        PixelFormatDetails format,
        Palette palette,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetRgba(
        uint pixelValue,
        PixelFormatDetails format,
        Palette palette,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

    public static PixelFormat DefinePixelFourCC(char a, char b, char c, char d)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return FourCC(a, b, c, d);
    }

    public static PixelFormat DefinePixelFormat(
        PixelType type,
        BitmapOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return DefinePixelFormat(type, (uint)order, layout, bits, bytes);
    }

    public static PixelFormat DefinePixelFormat(
        PixelType type,
        ArrayOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return DefinePixelFormat(type, (uint)order, layout, bits, bytes);
    }

    public static PixelFormat DefinePixelFormat(
        PixelType type,
        PackedOrder order,
        PackedLayout layout,
        byte bits,
        byte bytes
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return DefinePixelFormat(type, (uint)order, layout, bits, bytes);
    }

    public static uint PixelFlag(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (format.Value >> 28) & 0x0F;
    }

    public static PixelType PixelTypeGet(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (PixelType)((format.Value >> 24) & 0x0F);
    }

    public static BitmapOrder BitmapOrderGet(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (BitmapOrder)PixelOrder(format);
    }

    public static ArrayOrder ArrayOrderGet(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ArrayOrder)PixelOrder(format);
    }

    public static PackedOrder PackedOrderGet(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (PackedOrder)PixelOrder(format);
    }

    public static PackedLayout PixelLayout(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (PackedLayout)((format.Value >> 16) & 0x0F);
    }

    public static uint BitsPerPixel(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return IsPixelFormatFourCC(format) ? 0 : (format.Value >> 8) & 0xFF;
    }

    public static uint BytesPerPixel(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return IsPixelFormatFourCC(format)
            ? (
                format == PixelFormatYuy2
                || format == PixelFormatUyvy
                || format == PixelFormatYvyu
                || format == PixelFormatP010
                    ? 2U
                    : 1U
            )
            : format & 0xFF;
    }

    public static bool IsPixelFormatIndexed(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var pixelType = PixelTypeGet(format);
        return !IsPixelFormatFourCC(format)
            && pixelType
                is PixelType.Index1
                    or PixelType.Index2
                    or PixelType.Index4
                    or PixelType.Index8;
    }

    public static bool IsPixelFormatPacked(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var pixelType = PixelTypeGet(format);
        return !IsPixelFormatFourCC(format)
            && pixelType is PixelType.Packed8 or PixelType.Packed16 or PixelType.Packed32;
    }

    public static bool IsPixelFormatArray(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var pixelType = PixelTypeGet(format);
        return !IsPixelFormatFourCC(format)
            && pixelType
                is PixelType.ArrayU8
                    or PixelType.ArrayU16
                    or PixelType.ArrayU32
                    or PixelType.ArrayF16
                    or PixelType.ArrayF32;
    }

    public static bool IsPixelFormat10Bit(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return !IsPixelFormatFourCC(format)
            && PixelTypeGet(format) is PixelType.Packed32
            && PixelLayout(format) is PackedLayout.Layout2101010;
    }

    public static bool IsPixelFormatFloat(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var pixelType = PixelTypeGet(format);
        return !IsPixelFormatFourCC(format)
            && (pixelType is PixelType.ArrayF16 or PixelType.ArrayF32);
    }

    public static bool IsPixelFormatAlpha(PixelFormat format)
    {
        var result = false;
        if (IsPixelFormatPacked(format))
        {
            result =
                PackedOrderGet(format)
                    is PackedOrder.Argb
                        or PackedOrder.Rgba
                        or PackedOrder.Abgr
                        or PackedOrder.Bgra;
        }

        if (IsPixelFormatArray(format))
        {
            result =
                result
                || (
                    ArrayOrderGet(format)
                    is ArrayOrder.Argb
                        or ArrayOrder.Rgba
                        or ArrayOrder.Abgr
                        or ArrayOrder.Bgra
                );
        }

        return result;
    }

    public static bool IsPixelFormatFourCC(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return format.Value != 0 && PixelFlag(format) != 1;
    }

    public static Colorspace DefineColorspace(
        ColorType type,
        ColorRange range,
        ColorPrimaries primaries,
        TransferCharacteristics transfer,
        MatrixCoefficients matrix,
        ChromaLocation chroma
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (uint)type << 28
            | (uint)range << 24
            | (uint)chroma << 20
            | (uint)primaries << 10
            | (uint)transfer << 5
            | (uint)matrix;
    }

    public static ColorType ColorspaceType(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ColorType)((cspace.Value >> 28) & 0x0F);
    }

    public static ColorRange ColorspaceRange(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ColorRange)((cspace.Value >> 24) & 0x0F);
    }

    public static ChromaLocation ColorspaceChroma(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ChromaLocation)((cspace.Value >> 20) & 0x0F);
    }

    public static ColorPrimaries ColorspacePrimaries(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ColorPrimaries)((cspace.Value >> 10) & 0x1F);
    }

    public static TransferCharacteristics ColorspaceTransfer(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (TransferCharacteristics)((cspace.Value >> 5) & 0x1F);
    }

    public static MatrixCoefficients ColorspaceMatrix(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (MatrixCoefficients)(cspace.Value & 0x1F);
    }

    public static bool IsColorspaceMatrixBt601(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var matrix = ColorspaceMatrix(cspace);
        return matrix == MatrixCoefficientsBT601 || matrix == MatrixCoefficientsBT470BG;
    }

    public static bool IsColorspaceMatrixBT709(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return ColorspaceMatrix(cspace) == MatrixCoefficientsBT709;
    }

    public static bool IsColorspaceMatrixBT2020Ncl(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return ColorspaceMatrix(cspace) == MatrixCoefficientsBT2020Ncl;
    }

    public static bool IsColorspaceLimitedRange(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return ColorspaceRange(cspace) is not ColorRange.Full;
    }

    public static bool IsColorspaceFullRange(Colorspace cspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return ColorspaceRange(cspace) is ColorRange.Full;
    }

    public static string GetPixelFormatName(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPixelFormatName(format);
    }

    public static bool GetMasksForPixelFormat(
        PixelFormat format,
        out int bpp,
        out uint rMask,
        out uint gMask,
        out uint bMask,
        out uint aMask
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMasksForPixelFormat(format, out bpp, out rMask, out gMask, out bMask, out aMask);
    }

    public static PixelFormat GetPixelFormatForMasks(
        int bpp,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPixelFormatForMasks(bpp, rMask, gMask, bMask, aMask);
    }

    public static PixelFormatDetails GetPixelFormatDetails(PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPixelFormatDetails(format);
    }

    public static Palette CreatePalette(int nColors)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreatePalette(nColors);
    }

    public static bool SetPaletteColors(
        Palette palette,
        Color[] colors,
        int firstColor,
        int nColors
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(colors);
        ArgumentOutOfRangeException.ThrowIfNegative(firstColor);
        ArgumentOutOfRangeException.ThrowIfNegative(nColors);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(firstColor, colors.Length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(nColors, colors.Length - firstColor);
        return _SetPaletteColors(palette, colors, firstColor, nColors);
    }

    public static void DestroyPalette(Palette palette)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyPalette(palette);
    }

    public static uint MapRgb(PixelFormatDetails format, Palette palette, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapRgb(format, palette, r, g, b);
    }

    public static uint MapRgba(
        PixelFormatDetails format,
        Palette palette,
        byte r,
        byte g,
        byte b,
        byte a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapRgba(format, palette, r, g, b, a);
    }

    public static void GetRgb(
        uint pixelValue,
        PixelFormatDetails format,
        Palette palette,
        out byte r,
        out byte g,
        out byte b
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetRgb(pixelValue, format, palette, out r, out g, out b);
    }

    public static void GetRgba(
        uint pixelValue,
        PixelFormatDetails format,
        Palette palette,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetRgba(pixelValue, format, palette, out r, out g, out b, out a);
    }

    public enum PixelType : uint
    {
        Unknown,
        Index1,
        Index4,
        Index8,
        Packed8,
        Packed16,
        Packed32,
        ArrayU8,
        ArrayU16,
        ArrayU32,
        ArrayF16,
        ArrayF32,
        Index2,
    }

    public enum BitmapOrder : uint
    {
        None,
        Order4321,
        Order1234,
    }

    public enum PackedOrder : uint
    {
        None,
        Xrgb,
        Rgbx,
        Argb,
        Rgba,
        Xbgr,
        Bgrx,
        Abgr,
        Bgra,
    }

    public enum ArrayOrder : uint
    {
        None,
        Rgb,
        Rgba,
        Argb,
        Bgr,
        Bgra,
        Abgr,
    }

    public enum PackedLayout : uint
    {
        None,
        Layout332,
        Layout4444,
        Layout1555,
        Layout5551,
        Layout565,
        Layout8888,
        Layout2101010,
        Layout1010102,
    }

    public enum ColorType : uint
    {
        Unknown,
        Rgb,
        YCbCr,
    }

    public enum ColorRange : uint
    {
        Unknown,
        Limited,
        Full,
    }

    public enum ChromaLocation : uint
    {
        None,
        Left,
        Center,
        TopLeft,
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct UnsafePixelFormatDetails
    {
        public PixelFormat Format;
        public byte BitsPerPixel;
        public byte BytesPerPixel;
        private unsafe fixed byte _padding[2];
        public uint RMask;
        public uint GMask;
        public uint BMask;
        public uint AMask;
        public byte RBits;
        public byte GBits;
        public byte BBits;
        public byte ABits;
        public byte RShift;
        public byte GShift;
        public byte BShift;
        public byte AShift;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct UnsafePalette
    {
        public int NColors;
        public unsafe Color* Colors;
        public uint Version;
        public int RefCount;
    }

    public readonly record struct PixelFormat(uint Value)
    {
        public static implicit operator uint(PixelFormat format) => format.Value;

        public static implicit operator PixelFormat(uint value) => new(value);
    }

    public readonly record struct ColorPrimaries(uint Value)
    {
        public static implicit operator uint(ColorPrimaries format) => format.Value;

        public static implicit operator ColorPrimaries(uint value) => new(value);
    }

    public readonly record struct TransferCharacteristics(uint Value)
    {
        public static implicit operator uint(TransferCharacteristics format) => format.Value;

        public static implicit operator TransferCharacteristics(uint value) => new(value);
    }

    public readonly record struct MatrixCoefficients(uint Value)
    {
        public static implicit operator uint(MatrixCoefficients format) => format.Value;

        public static implicit operator MatrixCoefficients(uint value) => new(value);
    }

    public readonly record struct Colorspace(uint Value)
    {
        public static implicit operator uint(Colorspace format) => format.Value;

        public static implicit operator Colorspace(uint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Color
    {
        public byte R;
        public byte G;
        public byte B;
        public byte A;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FColor
    {
        public float R;
        public float G;
        public float B;
        public float A;
    }

    public readonly record struct Palette(nint Value)
    {
        private readonly unsafe UnsafePalette* UnsafeValue => (UnsafePalette*)Value;

        public bool IsNull => Value == 0;
        public unsafe Color[]? Colors
        {
            get
            {
                if (UnsafeValue is null || UnsafeValue->Colors is null)
                {
                    return null;
                }

                var count = UnsafeValue->NColors;
                var colors = new Color[count];
                for (int i = 0; i < count; i++)
                {
                    colors[i] = UnsafeValue->Colors[i];
                }

                return colors;
            }
        }
        public unsafe uint Version => UnsafeValue->Version;
        public unsafe int RefCount => UnsafeValue->RefCount;

        public static implicit operator nint(Palette palette) => palette.Value;

        public static implicit operator Palette(nint value) => new(value);
    }

    public readonly record struct PixelFormatDetails(nint Value)
    {
        private readonly unsafe UnsafePixelFormatDetails* UnsafeValue =>
            (UnsafePixelFormatDetails*)Value;

        public bool IsNull => Value == 0;

        public unsafe PixelFormat Format => UnsafeValue->Format;
        public unsafe byte BitsPerPixel => UnsafeValue->BitsPerPixel;
        public unsafe byte BytesPerPixel => UnsafeValue->BytesPerPixel;
        public unsafe uint RMask => UnsafeValue->RMask;
        public unsafe uint GMask => UnsafeValue->GMask;
        public unsafe uint BMask => UnsafeValue->BMask;
        public unsafe uint AMask => UnsafeValue->AMask;
        public unsafe byte RBits => UnsafeValue->RBits;
        public unsafe byte GBits => UnsafeValue->GBits;
        public unsafe byte BBits => UnsafeValue->BBits;
        public unsafe byte ABits => UnsafeValue->ABits;
        public unsafe byte RShift => UnsafeValue->RShift;
        public unsafe byte GShift => UnsafeValue->GShift;
        public unsafe byte BShift => UnsafeValue->BShift;
        public unsafe byte AShift => UnsafeValue->AShift;

        public static implicit operator nint(PixelFormatDetails details) => details.Value;

        public static implicit operator PixelFormatDetails(nint value) => new(value);
    }
}
