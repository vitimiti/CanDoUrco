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
    public const string PropSurfaceSdrWhitePointFloat = "SDL.surface.SDR_white_point";
    public const string PropSurfaceHdrHeadroomFloat = "SDL.surface.HDR_headroom";
    public const string PropSurfaceTonemapOperatorString = "SDL.surface.tonemap";
    public const string PropSurfaceHotspotXNumber = "SDL.surface.hotspot.x";
    public const string PropSurfaceHotspotYNumber = "SDL.surface.hotspot.y";
    public const string PropSurfaceRotationFloat = "SDL.surface.rotation";

    public static SurfaceFlags SurfacePreAllocated => 0x0000_0001U;
    public static SurfaceFlags SurfaceLockNeeded => 0x0000_0002U;
    public static SurfaceFlags SurfaceLocked => 0x0000_0004U;
    public static SurfaceFlags SurfaceSimdAligned => 0x0000_0008U;

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Surface _CreateSurfaceFrom(
        int width,
        int height,
        PixelFormat format,
        void* pixels,
        int pitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroySurface(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetSurfaceProperties(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceColorspace(Surface surface, Colorspace colorspace);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Colorspace _GetSurfaceColorspace(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette _CreateSurfacePalette(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfacePalette(Surface surface, Palette palette);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette _GetSurfacePalette(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_AddSurfaceAlternateImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _AddSurfaceAlternateImage(Surface surface, Surface image);

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SurfaceHasAlternateImages(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial nint* _GetSurfaceImages(Surface surface, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveSurfaceAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _RemoveSurfaceAlternateImages(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _LockSurface(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockSurface(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadSurface_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadSurfaceIO(
        IOStream src,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_LoadSurface",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadSurface(string file);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadBmpIO(
        IOStream src,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LoadBMP", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadBmp(string file);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SaveBmpIO(
        Surface surface,
        IOStream dst,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SaveBMP", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SaveBmp(Surface surface, string file);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadPNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadPngIO(
        IOStream src,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LoadPNG", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _LoadPng(string file);

    [LibraryImport(DllName, EntryPoint = "SDL_SavePNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SavePngIO(
        Surface surface,
        IOStream dst,
        [MarshalAs(UnmanagedType.I1)] bool closeIO
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SavePNG", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SavePng(Surface surface, string file);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceRle(
        Surface surface,
        [MarshalAs(UnmanagedType.I1)] bool enabled
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SurfaceHasRle(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceColorKey(
        Surface surface,
        [MarshalAs(UnmanagedType.I1)] bool enabled,
        uint key
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SurfaceHasColorKey(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetSurfaceColorKey(Surface surface, out uint key);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceColorMod(Surface surface, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetSurfaceColorMod(
        Surface surface,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceAlphaMod(Surface surface, byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetSurfaceAlphaMod(Surface surface, out byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetSurfaceBlendMode(Surface surface, BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetSurfaceBlendMode(Surface surface, out BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetSurfaceClipRect(Surface surface, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetSurfaceClipRect(Surface surface, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_FlipSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _FlipSurface(Surface surface, FlipMode flip);

    [LibraryImport(DllName, EntryPoint = "SDL_RotateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _RotateSurface(Surface surface, float rotate);

    [LibraryImport(DllName, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _DuplicateSurface(Surface surface);

    [LibraryImport(DllName, EntryPoint = "SDL_ScaleSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _ScaleSurface(
        Surface surface,
        int width,
        int height,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _ConvertSurface(Surface surface, PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertSurfaceAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface _ConvertSurfaceAndColorspace(
        Surface surface,
        PixelFormat format,
        Palette palette,
        Colorspace colorspace,
        PropertiesID props
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _ConvertPixels(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertPixelsAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _ConvertPixelsAndColorspace(
        int width,
        int height,
        PixelFormat srcFormat,
        Colorspace srcColorspace,
        PropertiesID srcProperties,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        Colorspace dstColorspace,
        PropertiesID dstProperties,
        void* dst,
        int dstPitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PremultiplyAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _PremultiplyAlpha(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch,
        [MarshalAs(UnmanagedType.I1)] bool linear
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PremultiplySurfaceAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _PremultiplySurfaceAlpha(
        Surface surface,
        [MarshalAs(UnmanagedType.I1)] bool linear
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ClearSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ClearSurface(Surface surface, float r, float g, float b, float a);

    [LibraryImport(DllName, EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _FillSurfaceRect(Surface dst, Rect* rect, uint color);

    [LibraryImport(DllName, EntryPoint = "SDL_FillSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _FillSurfaceRects(
        Surface dst,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Rect, Rect>), CountElementName = nameof(count))]
            Rect[] rects,
        int count,
        uint color
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurface(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceUnchecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurfaceUnchecked(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurfaceScaled(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceUncheckedScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurfaceUncheckedScaled(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_StretchSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _StretchSurface(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurfaceTiled(
        Surface src,
        Rect* srcRect,
        Surface dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceTiledWithScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurfaceTiledWithScale(
        Surface src,
        Rect* srcRect,
        float scale,
        ScaleMode scaleMode,
        Surface dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurface9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _BlitSurface9Grid(
        Surface src,
        Rect* srcRect,
        int leftWidth,
        int rightWidth,
        int topHeight,
        int bottomHeight,
        float scale,
        ScaleMode scaleMode,
        Surface dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_MapSurfaceRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapSurfaceRgb(Surface surface, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_MapSurfaceRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapSurfaceRgba(Surface surface, byte r, byte g, byte b, byte a);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadSurfacePixel(
        Surface surface,
        int x,
        int y,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ReadSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadSurfacePixelFloat(
        Surface surface,
        int x,
        int y,
        out float r,
        out float g,
        out float b,
        out float a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WriteSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteSurfacePixel(
        Surface surface,
        int x,
        int y,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WriteSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteSurfacePixelFloat(
        Surface surface,
        int x,
        int y,
        float r,
        float g,
        float b,
        float a
    );

    public static bool MustLock(Surface s) => (s.Flags & SurfaceLockNeeded) == SurfaceLockNeeded;

    public static Surface CreateSurface(int width, int height, PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSurface(width, height, format);
    }

    public static unsafe Surface CreateSurfaceFrom(
        int width,
        int height,
        PixelFormat format,
        void* pixels,
        int pitch
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSurfaceFrom(width, height, format, pixels, pitch);
    }

    public static void DestroySurface(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroySurface(surface);
    }

    public static PropertiesID GetSurfaceProperties(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceProperties(surface);
    }

    public static bool SetSurfaceColorspace(Surface surface, Colorspace colorspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorspace(surface, colorspace);
    }

    public static Colorspace GetSurfaceColorspace(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorspace(surface);
    }

    public static Palette CreateSurfacePalette(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSurfacePalette(surface);
    }

    public static bool SetSurfacePalette(Surface surface, Palette palette)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfacePalette(surface, palette);
    }

    public static Palette GetSurfacePalette(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfacePalette(surface);
    }

    public static bool AddSurfaceAlternateImage(Surface surface, Surface image)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddSurfaceAlternateImage(surface, image);
    }

    public static bool SurfaceHasAlternateImages(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasAlternateImages(surface);
    }

    public static unsafe Surface[]? GetSurfaceImages(Surface surface)
    {
        // Making a custom marshaller for this appears more complex than necessary, so handling manually.
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var images = _GetSurfaceImages(surface, out int count);
        if (images is null)
        {
            return null;
        }

        try
        {
            Surface[] result = new Surface[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = new Surface(images[i]);
            }

            return result;
        }
        finally
        {
            Free(images);
        }
    }

    public static void RemoveSurfaceAlternateImages(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RemoveSurfaceAlternateImages(surface);
    }

    public static bool LockSurface(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockSurface(surface);
    }

    public static void UnlockSurface(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockSurface(surface);
    }

    public static Surface LoadSurfaceIO(IOStream src, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadSurfaceIO(src, closeIO);
    }

    public static Surface LoadSurface(string file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _LoadSurface(file);
    }

    public static Surface LoadBmpIO(IOStream src, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadBmpIO(src, closeIO);
    }

    public static Surface LoadBmp(string file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _LoadBmp(file);
    }

    public static bool SaveBmpIO(Surface surface, IOStream dst, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SaveBmpIO(surface, dst, closeIO);
    }

    public static bool SaveBmp(Surface surface, string file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _SaveBmp(surface, file);
    }

    public static Surface LoadPngIO(IOStream src, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadPngIO(src, closeIO);
    }

    public static Surface LoadPng(string file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _LoadPng(file);
    }

    public static bool SavePngIO(Surface surface, IOStream dst, bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SavePngIO(surface, dst, closeIO);
    }

    public static bool SavePng(Surface surface, string file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(file);
        return _SavePng(surface, file);
    }

    public static bool SetSurfaceRle(Surface surface, bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceRle(surface, enabled);
    }

    public static bool SurfaceHasRle(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasRle(surface);
    }

    public static bool SetSurfaceColorKey(Surface surface, bool enabled, uint key)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorKey(surface, enabled, key);
    }

    public static bool SurfaceHasColorKey(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasColorKey(surface);
    }

    public static bool GetSurfaceColorKey(Surface surface, out uint key)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorKey(surface, out key);
    }

    public static bool SetSurfaceColorMod(Surface surface, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorMod(surface, r, g, b);
    }

    public static bool GetSurfaceColorMod(Surface surface, out byte r, out byte g, out byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorMod(surface, out r, out g, out b);
    }

    public static bool SetSurfaceAlphaMod(Surface surface, byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceAlphaMod(surface, alpha);
    }

    public static bool GetSurfaceAlphaMod(Surface surface, out byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceAlphaMod(surface, out alpha);
    }

    public static bool SetSurfaceBlendMode(Surface surface, BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceBlendMode(surface, blendMode);
    }

    public static bool GetSurfaceBlendMode(Surface surface, out BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceBlendMode(surface, out blendMode);
    }

    public static unsafe bool SetSurfaceClipRect(Surface surface, Rect? rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        if (rect.HasValue)
        {
            var value = rect.Value;
            return _SetSurfaceClipRect(surface, &value);
        }

        return _SetSurfaceClipRect(surface, null);
    }

    public static bool GetSurfaceClipRect(Surface surface, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceClipRect(surface, out rect);
    }

    public static bool FlipSurface(Surface surface, FlipMode flip)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlipSurface(surface, flip);
    }

    public static Surface RotateSurface(Surface surface, float rotate)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RotateSurface(surface, rotate);
    }

    public static Surface DuplicateSurface(Surface surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DuplicateSurface(surface);
    }

    public static Surface ScaleSurface(Surface surface, int width, int height, ScaleMode scaleMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScaleSurface(surface, width, height, scaleMode);
    }

    public static Surface ConvertSurface(Surface surface, PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertSurface(surface, format);
    }

    public static Surface ConvertSurfaceAndColorspace(
        Surface surface,
        PixelFormat format,
        Palette palette,
        Colorspace colorspace,
        PropertiesID props
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
    }

    public static unsafe bool ConvertPixels(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertPixels(width, height, srcFormat, src, srcPitch, dstFormat, dst, dstPitch);
    }

    public static unsafe bool ConvertPixelsAndColorspace(
        int width,
        int height,
        PixelFormat srcFormat,
        Colorspace srcColorspace,
        PropertiesID srcProperties,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        Colorspace dstColorspace,
        PropertiesID dstProperties,
        void* dst,
        int dstPitch
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertPixelsAndColorspace(
            width,
            height,
            srcFormat,
            srcColorspace,
            srcProperties,
            src,
            srcPitch,
            dstFormat,
            dstColorspace,
            dstProperties,
            dst,
            dstPitch
        );
    }

    public static unsafe bool PremultiplyAlpha(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch,
        bool linear
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PremultiplyAlpha(
            width,
            height,
            srcFormat,
            src,
            srcPitch,
            dstFormat,
            dst,
            dstPitch,
            linear
        );
    }

    public static bool PremultiplySurfaceAlpha(Surface surface, bool linear)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PremultiplySurfaceAlpha(surface, linear);
    }

    public static bool ClearSurface(Surface surface, float r, float g, float b, float a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearSurface(surface, r, g, b, a);
    }

    public static unsafe bool FillSurfaceRect(Surface dst, Rect? rect, uint color)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        if (rect.HasValue)
        {
            var value = rect.Value;
            return _FillSurfaceRect(dst, &value, color);
        }

        return _FillSurfaceRect(dst, null, color);
    }

    public static bool FillSurfaceRects(Surface dst, ReadOnlySpan<Rect> rects, uint color)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FillSurfaceRects(dst, rects.ToArray(), rects.Length, color);
    }

    public static unsafe bool BlitSurface(Surface src, Rect? srcRect, Surface dst, Rect? dstRect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurface(src, srcRectPtr, dst, dstRectPtr);
    }

    public static unsafe bool BlitSurfaceUnchecked(
        Surface src,
        Rect? srcRect,
        Surface dst,
        Rect? dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurfaceUnchecked(src, srcRectPtr, dst, dstRectPtr);
    }

    public static unsafe bool BlitSurfaceScaled(
        Surface src,
        Rect? srcRect,
        Surface dst,
        Rect? dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurfaceScaled(src, srcRectPtr, dst, dstRectPtr, scaleMode);
    }

    public static unsafe bool BlitSurfaceUncheckedScaled(
        Surface src,
        Rect? srcRect,
        Surface dst,
        Rect? dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurfaceUncheckedScaled(src, srcRectPtr, dst, dstRectPtr, scaleMode);
    }

    public static unsafe bool StretchSurface(
        Surface src,
        Rect? srcRect,
        Surface dst,
        Rect? dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _StretchSurface(src, srcRectPtr, dst, dstRectPtr, scaleMode);
    }

    public static unsafe bool BlitSurfaceTiled(
        Surface src,
        Rect? srcRect,
        Surface dst,
        Rect? dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurfaceTiled(src, srcRectPtr, dst, dstRectPtr);
    }

    public static unsafe bool BlitSurfaceTiledWithScale(
        Surface src,
        Rect? srcRect,
        float scale,
        ScaleMode scaleMode,
        Surface dst,
        Rect? dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurfaceTiledWithScale(src, srcRectPtr, scale, scaleMode, dst, dstRectPtr);
    }

    public static unsafe bool BlitSurface9Grid(
        Surface src,
        Rect? srcRect,
        int leftWidth,
        int rightWidth,
        int topHeight,
        int bottomHeight,
        float scale,
        ScaleMode scaleMode,
        Surface dst,
        Rect? dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        GetRectPointer(srcRect, out var srcRectPtr);
        GetRectPointer(dstRect, out var dstRectPtr);
        return _BlitSurface9Grid(
            src,
            srcRectPtr,
            leftWidth,
            rightWidth,
            topHeight,
            bottomHeight,
            scale,
            scaleMode,
            dst,
            dstRectPtr
        );
    }

    public static uint MapSurfaceRgb(Surface surface, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapSurfaceRgb(surface, r, g, b);
    }

    public static uint MapSurfaceRgba(Surface surface, byte r, byte g, byte b, byte a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapSurfaceRgba(surface, r, g, b, a);
    }

    public static bool ReadSurfacePixel(
        Surface surface,
        int x,
        int y,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadSurfacePixel(surface, x, y, out r, out g, out b, out a);
    }

    public static bool ReadSurfacePixelFloat(
        Surface surface,
        int x,
        int y,
        out float r,
        out float g,
        out float b,
        out float a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadSurfacePixelFloat(surface, x, y, out r, out g, out b, out a);
    }

    public static bool WriteSurfacePixel(
        Surface surface,
        int x,
        int y,
        byte r,
        byte g,
        byte b,
        byte a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteSurfacePixel(surface, x, y, r, g, b, a);
    }

    public static bool WriteSurfacePixelFloat(
        Surface surface,
        int x,
        int y,
        float r,
        float g,
        float b,
        float a
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteSurfacePixelFloat(surface, x, y, r, g, b, a);
    }

    public enum ScaleMode
    {
        Invalid = -1,
        Nearest,
        Linear,
        PixelArt,
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct UnsafeSurface
    {
        public SurfaceFlags Flags;
        public PixelFormat Format;
        public int W;
        public int H;
        public int Pitch;
        public unsafe void* Pixels;
        public int RefCount;
        private readonly unsafe void* _reserved;
    }

    public enum FlipMode
    {
        None,
        Horizontal,
        Vertical,
        HorizontalVertical = Horizontal | Vertical,
    }

    public readonly record struct SurfaceFlags(uint Value)
    {
        public static SurfaceFlags operator |(SurfaceFlags left, SurfaceFlags right) =>
            new(left.Value | right.Value);

        public static SurfaceFlags operator &(SurfaceFlags left, SurfaceFlags right) =>
            new(left.Value & right.Value);

        public static SurfaceFlags operator ^(SurfaceFlags left, SurfaceFlags right) =>
            new(left.Value ^ right.Value);

        public static SurfaceFlags operator ~(SurfaceFlags value) => new(~value.Value);

        public static implicit operator SurfaceFlags(uint value) => new(value);

        public static implicit operator uint(SurfaceFlags flags) => flags.Value;
    }

    public readonly record struct Surface(nint Value)
    {
        private unsafe UnsafeSurface* UnsafeValue => (UnsafeSurface*)Value;

        public unsafe SurfaceFlags Flags => UnsafeValue->Flags;

        public unsafe PixelFormat Format => UnsafeValue->Format;

        public unsafe int W => UnsafeValue->W;

        public unsafe int H => UnsafeValue->H;

        public unsafe int Pitch => UnsafeValue->Pitch;

        public unsafe void* Pixels
        {
            get { return UnsafeValue->Pixels; }
            set
            {
                if (UnsafeValue->Pixels is null)
                {
                    return;
                }

                UnsafeValue->Pixels = value;
            }
        }

        public unsafe int RefCount => UnsafeValue->RefCount;

        public static implicit operator Surface(nint value) => new(value);

        public static implicit operator nint(Surface surface) => surface.Value;
    }
}
