// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropSurfaceSdrWhitePointFloat =>
        "SDL.surface.SDR_white_point"u8;
    public static ReadOnlySpan<byte> PropSurfaceHdrHeadroomFloat => "SDL.surface.HDR_headroom"u8;
    public static ReadOnlySpan<byte> PropSurfaceTonemapOperatorString => "SDL.surface.tonemap"u8;
    public static ReadOnlySpan<byte> PropSurfaceHotspotXNumber => "SDL.surface.hotspot.x"u8;
    public static ReadOnlySpan<byte> PropSurfaceHotspotYNumber => "SDL.surface.hotspot.y"u8;
    public static ReadOnlySpan<byte> PropSurfaceRotationFloat => "SDL.surface.rotation"u8;

    public static SurfaceFlags SurfacePreAllocated => 0x0000_0001U;
    public static SurfaceFlags SurfaceLockNeeded => 0x0000_0002U;
    public static SurfaceFlags SurfaceLocked => 0x0000_0004U;
    public static SurfaceFlags SurfaceSimdAligned => 0x0000_0008U;

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _CreateSurface(int width, int height, PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurfaceFrom")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _CreateSurfaceFrom(
        int width,
        int height,
        PixelFormat format,
        void* pixels,
        int pitch
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroySurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroySurface(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetSurfaceProperties(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceColorspace(Surface* surface, Colorspace colorspace);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Colorspace _GetSurfaceColorspace(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* _CreateSurfacePalette(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfacePalette(Surface* surface, Palette* palette);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfacePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Palette* _GetSurfacePalette(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_AddSurfaceAlternateImage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _AddSurfaceAlternateImage(Surface* surface, Surface* image);

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SurfaceHasAlternateImages(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface** _GetSurfaceImages(Surface* surface, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveSurfaceAlternateImages")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _RemoveSurfaceAlternateImages(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_LockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LockSurface(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockSurface(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadSurface_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadSurfaceIO(IOStream src, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadSurface(byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadBmpIO(IOStream src, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadBMP")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadBmp(byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveBMP_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SaveBmpIO(Surface* surface, IOStream dst, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_SaveBMP")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SaveBmp(Surface* surface, byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadPNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadPngIO(IOStream src, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_LoadPNG")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _LoadPng(byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_SavePNG_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SavePngIO(Surface* surface, IOStream dst, Bool closeIO);

    [LibraryImport(DllName, EntryPoint = "SDL_SavePNG")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SavePng(Surface* surface, byte* file);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceRle(Surface* surface, Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasRLE")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SurfaceHasRle(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceColorKey(Surface* surface, Bool enabled, uint key);

    [LibraryImport(DllName, EntryPoint = "SDL_SurfaceHasColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SurfaceHasColorKey(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSurfaceColorKey(Surface* surface, out uint key);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceColorMod(Surface* surface, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceColorMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSurfaceColorMod(
        Surface* surface,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceAlphaMod(Surface* surface, byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceAlphaMod")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSurfaceAlphaMod(Surface* surface, out byte alpha);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceBlendMode(Surface* surface, BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSurfaceBlendMode(Surface* surface, out BlendMode blendMode);

    [LibraryImport(DllName, EntryPoint = "SDL_SetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetSurfaceClipRect(Surface* surface, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSurfaceClipRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetSurfaceClipRect(Surface* surface, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_FlipSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FlipSurface(Surface* surface, FlipMode flip);

    [LibraryImport(DllName, EntryPoint = "SDL_RotateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _RotateSurface(Surface* surface, float rotate);

    [LibraryImport(DllName, EntryPoint = "SDL_DuplicateSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _DuplicateSurface(Surface* surface);

    [LibraryImport(DllName, EntryPoint = "SDL_ScaleSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _ScaleSurface(
        Surface* surface,
        int width,
        int height,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _ConvertSurface(Surface* surface, PixelFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertSurfaceAndColorspace")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _ConvertSurfaceAndColorspace(
        Surface* surface,
        PixelFormat format,
        Palette* palette,
        Colorspace colorspace,
        PropertiesID props
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ConvertPixels(
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
    private static partial Bool _ConvertPixelsAndColorspace(
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
    private static partial Bool _PremultiplyAlpha(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch,
        Bool linear
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PremultiplySurfaceAlpha")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PremultiplySurfaceAlpha(Surface* surface, Bool linear);

    [LibraryImport(DllName, EntryPoint = "SDL_ClearSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClearSurface(Surface* surface, float r, float g, float b, float a);

    [LibraryImport(DllName, EntryPoint = "SDL_FillSurfaceRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FillSurfaceRect(Surface* dst, Rect* rect, uint color);

    [LibraryImport(DllName, EntryPoint = "SDL_FillSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FillSurfaceRects(Surface* dst, Rect* rects, int count, uint color);

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurface(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceUnchecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurfaceUnchecked(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurfaceScaled(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceUncheckedScaled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurfaceUncheckedScaled(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_StretchSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StretchSurface(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceTiled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurfaceTiled(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurfaceTiledWithScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurfaceTiledWithScale(
        Surface* src,
        Rect* srcRect,
        float scale,
        ScaleMode scaleMode,
        Surface* dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BlitSurface9Grid")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BlitSurface9Grid(
        Surface* src,
        Rect* srcRect,
        int leftWidth,
        int rightWidth,
        int topHeight,
        int bottomHeight,
        float scale,
        ScaleMode scaleMode,
        Surface* dst,
        Rect* dstRect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_MapSurfaceRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapSurfaceRgb(Surface* surface, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_MapSurfaceRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _MapSurfaceRgba(Surface* surface, byte r, byte g, byte b, byte a);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadSurfacePixel(
        Surface* surface,
        int x,
        int y,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ReadSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ReadSurfacePixelFloat(
        Surface* surface,
        int x,
        int y,
        out float r,
        out float g,
        out float b,
        out float a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WriteSurfacePixel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteSurfacePixel(
        Surface* surface,
        int x,
        int y,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WriteSurfacePixelFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WriteSurfacePixelFloat(
        Surface* surface,
        int x,
        int y,
        float r,
        float g,
        float b,
        float a
    );

    public static Bool MustLock(Surface* s) => (s->Flags & SurfaceLockNeeded) == SurfaceLockNeeded;

    public static Surface* CreateSurface(int width, int height, PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSurface(width, height, format);
    }

    public static Surface* CreateSurfaceFrom(
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

    public static void DestroySurface(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroySurface(surface);
    }

    public static PropertiesID GetSurfaceProperties(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceProperties(surface);
    }

    public static Bool SetSurfaceColorspace(Surface* surface, Colorspace colorspace)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorspace(surface, colorspace);
    }

    public static Colorspace GetSurfaceColorspace(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorspace(surface);
    }

    public static Palette* CreateSurfacePalette(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSurfacePalette(surface);
    }

    public static Bool SetSurfacePalette(Surface* surface, Palette* palette)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfacePalette(surface, palette);
    }

    public static Palette* GetSurfacePalette(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfacePalette(surface);
    }

    public static Bool AddSurfaceAlternateImage(Surface* surface, Surface* image)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddSurfaceAlternateImage(surface, image);
    }

    public static Bool SurfaceHasAlternateImages(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasAlternateImages(surface);
    }

    public static Surface** GetSurfaceImages(Surface* surface, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceImages(surface, out count);
    }

    public static void RemoveSurfaceAlternateImages(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RemoveSurfaceAlternateImages(surface);
    }

    public static Bool LockSurface(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockSurface(surface);
    }

    public static void UnlockSurface(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockSurface(surface);
    }

    public static Surface* LoadSurfaceIO(IOStream src, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadSurfaceIO(src, closeIO);
    }

    public static Surface* LoadSurface(byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadSurface(file);
    }

    public static Surface* LoadBmpIO(IOStream src, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadBmpIO(src, closeIO);
    }

    public static Surface* LoadBmp(byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadBmp(file);
    }

    public static Bool SaveBmpIO(Surface* surface, IOStream dst, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SaveBmpIO(surface, dst, closeIO);
    }

    public static Bool SaveBmp(Surface* surface, byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SaveBmp(surface, file);
    }

    public static Surface* LoadPngIO(IOStream src, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadPngIO(src, closeIO);
    }

    public static Surface* LoadPng(byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadPng(file);
    }

    public static Bool SavePngIO(Surface* surface, IOStream dst, Bool closeIO)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SavePngIO(surface, dst, closeIO);
    }

    public static Bool SavePng(Surface* surface, byte* file)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SavePng(surface, file);
    }

    public static Bool SetSurfaceRle(Surface* surface, Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceRle(surface, enabled);
    }

    public static Bool SurfaceHasRle(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasRle(surface);
    }

    public static Bool SetSurfaceColorKey(Surface* surface, Bool enabled, uint key)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorKey(surface, enabled, key);
    }

    public static Bool SurfaceHasColorKey(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SurfaceHasColorKey(surface);
    }

    public static Bool GetSurfaceColorKey(Surface* surface, out uint key)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorKey(surface, out key);
    }

    public static Bool SetSurfaceColorMod(Surface* surface, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceColorMod(surface, r, g, b);
    }

    public static Bool GetSurfaceColorMod(Surface* surface, out byte r, out byte g, out byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceColorMod(surface, out r, out g, out b);
    }

    public static Bool SetSurfaceAlphaMod(Surface* surface, byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceAlphaMod(surface, alpha);
    }

    public static Bool GetSurfaceAlphaMod(Surface* surface, out byte alpha)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceAlphaMod(surface, out alpha);
    }

    public static Bool SetSurfaceBlendMode(Surface* surface, BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceBlendMode(surface, blendMode);
    }

    public static Bool GetSurfaceBlendMode(Surface* surface, out BlendMode blendMode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceBlendMode(surface, out blendMode);
    }

    public static Bool SetSurfaceClipRect(Surface* surface, Rect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetSurfaceClipRect(surface, rect);
    }

    public static Bool GetSurfaceClipRect(Surface* surface, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSurfaceClipRect(surface, out rect);
    }

    public static Bool FlipSurface(Surface* surface, FlipMode flip)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlipSurface(surface, flip);
    }

    public static Surface* RotateSurface(Surface* surface, float rotate)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RotateSurface(surface, rotate);
    }

    public static Surface* DuplicateSurface(Surface* surface)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DuplicateSurface(surface);
    }

    public static Surface* ScaleSurface(
        Surface* surface,
        int width,
        int height,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScaleSurface(surface, width, height, scaleMode);
    }

    public static Surface* ConvertSurface(Surface* surface, PixelFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertSurface(surface, format);
    }

    public static Surface* ConvertSurfaceAndColorspace(
        Surface* surface,
        PixelFormat format,
        Palette* palette,
        Colorspace colorspace,
        PropertiesID props
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertSurfaceAndColorspace(surface, format, palette, colorspace, props);
    }

    public static Bool ConvertPixels(
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

    public static Bool ConvertPixelsAndColorspace(
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

    public static Bool PremultiplyAlpha(
        int width,
        int height,
        PixelFormat srcFormat,
        void* src,
        int srcPitch,
        PixelFormat dstFormat,
        void* dst,
        int dstPitch,
        Bool linear
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

    public static Bool PremultiplySurfaceAlpha(Surface* surface, Bool linear)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PremultiplySurfaceAlpha(surface, linear);
    }

    public static Bool ClearSurface(Surface* surface, float r, float g, float b, float a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearSurface(surface, r, g, b, a);
    }

    public static Bool FillSurfaceRect(Surface* dst, Rect* rect, uint color)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FillSurfaceRect(dst, rect, color);
    }

    public static Bool FillSurfaceRects(Surface* dst, Rect* rects, int count, uint color)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FillSurfaceRects(dst, rects, count, color);
    }

    public static Bool BlitSurface(Surface* src, Rect* srcRect, Surface* dst, Rect* dstRect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurface(src, srcRect, dst, dstRect);
    }

    public static Bool BlitSurfaceUnchecked(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurfaceUnchecked(src, srcRect, dst, dstRect);
    }

    public static Bool BlitSurfaceScaled(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurfaceScaled(src, srcRect, dst, dstRect, scaleMode);
    }

    public static Bool BlitSurfaceUncheckedScaled(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurfaceUncheckedScaled(src, srcRect, dst, dstRect, scaleMode);
    }

    public static Bool StretchSurface(
        Surface* src,
        Rect* srcRect,
        Surface* dst,
        Rect* dstRect,
        ScaleMode scaleMode
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StretchSurface(src, srcRect, dst, dstRect, scaleMode);
    }

    public static Bool BlitSurfaceTiled(Surface* src, Rect* srcRect, Surface* dst, Rect* dstRect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurfaceTiled(src, srcRect, dst, dstRect);
    }

    public static Bool BlitSurfaceTiledWithScale(
        Surface* src,
        Rect* srcRect,
        float scale,
        ScaleMode scaleMode,
        Surface* dst,
        Rect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurfaceTiledWithScale(src, srcRect, scale, scaleMode, dst, dstRect);
    }

    public static Bool BlitSurface9Grid(
        Surface* src,
        Rect* srcRect,
        int leftWidth,
        int rightWidth,
        int topHeight,
        int bottomHeight,
        float scale,
        ScaleMode scaleMode,
        Surface* dst,
        Rect* dstRect
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BlitSurface9Grid(
            src,
            srcRect,
            leftWidth,
            rightWidth,
            topHeight,
            bottomHeight,
            scale,
            scaleMode,
            dst,
            dstRect
        );
    }

    public static uint MapSurfaceRgb(Surface* surface, byte r, byte g, byte b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapSurfaceRgb(surface, r, g, b);
    }

    public static uint MapSurfaceRgba(Surface* surface, byte r, byte g, byte b, byte a)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MapSurfaceRgba(surface, r, g, b, a);
    }

    public static Bool ReadSurfacePixel(
        Surface* surface,
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

    public static Bool ReadSurfacePixelFloat(
        Surface* surface,
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

    public static Bool WriteSurfacePixel(
        Surface* surface,
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

    public static Bool WriteSurfacePixelFloat(
        Surface* surface,
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

    [StructLayout(LayoutKind.Sequential)]
    public struct Surface
    {
        public SurfaceFlags Flags;
        public PixelFormat Format;
        public int W;
        public int H;
        public int Pitch;
        public void* Pixels;
        public int RefCount;
        private readonly void* _reserved;
    }
}
