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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl3.Internals;

internal static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial byte* GetPixelFormatName(uint format);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMasksForPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetMasksForPixelFormat(
        uint format,
        out int bpp,
        out uint rMask,
        out uint gMask,
        out uint bMask,
        out uint aMask
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatForMasks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial uint GetPixelFormatForMasks(
        int bpp,
        uint rMask,
        uint gMask,
        uint bMask,
        uint aMask
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetPixelFormatDetails")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial PixelFormatDetails* GetPixelFormatDetails(uint format);

    [LibraryImport(DllName, EntryPoint = "SDL_CreatePalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreatePalette(int nColors);

    [LibraryImport(DllName, EntryPoint = "SDL_SetPaletteColors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static unsafe partial bool SetPaletteColors(
        nint palette,
        Color* colors,
        int firstColor,
        int nColors
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyPalette")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyPalette(nint palette);

    [LibraryImport(DllName, EntryPoint = "SDL_MapRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint MapRgb(nint format, nint palette, byte r, byte g, byte b);

    [LibraryImport(DllName, EntryPoint = "SDL_MapRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial uint MapRgba(
        nint format,
        nint palette,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRGB")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetRgb(
        uint pixelValue,
        nint format,
        nint palette,
        out byte r,
        out byte g,
        out byte b
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRGBA")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static unsafe partial void GetRgba(
        uint pixelValue,
        nint format,
        nint palette,
        out byte r,
        out byte g,
        out byte b,
        out byte a
    );

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

    [StructLayout(LayoutKind.Sequential)]
    public struct Palette
    {
        public int NColors;
        public unsafe Color* Colors;
        public uint Version;
        public int RefCount;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PixelFormatDetails
    {
        public uint Format;
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
}
