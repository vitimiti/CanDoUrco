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

using System.Drawing;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3.Pixels;

public sealed class PixelFormatDetails
{
    private readonly nint _handle;

    public unsafe PixelFormat Format => new(UnsafeHandle->Format);
    public unsafe byte BitsPerPixel => UnsafeHandle->BitsPerPixel;
    public unsafe byte BytesPerPixel => UnsafeHandle->BytesPerPixel;

    public unsafe (uint R, uint G, uint B, uint A) Masks =>
        (UnsafeHandle->RMask, UnsafeHandle->GMask, UnsafeHandle->BMask, UnsafeHandle->AMask);

    public unsafe (byte R, byte G, byte B, byte A) Bits =>
        (UnsafeHandle->RBits, UnsafeHandle->GBits, UnsafeHandle->BBits, UnsafeHandle->ABits);

    public unsafe (byte R, byte G, byte B, byte A) Shifts =>
        (UnsafeHandle->RShift, UnsafeHandle->GShift, UnsafeHandle->BShift, UnsafeHandle->AShift);

    private unsafe Sdl.PixelFormatDetails* UnsafeHandle => (Sdl.PixelFormatDetails*)_handle;

    internal unsafe PixelFormatDetails(Sdl.PixelFormatDetails* details) => _handle = (nint)details;

    public uint MapRgb(Palette? palette, Color color) =>
        Sdl.MapRgb(_handle, palette is null ? 0 : palette.Handle, color.R, color.G, color.B);

    public uint MapRgba(Palette? palette, Color color) =>
        Sdl.MapRgba(
            _handle,
            palette is null ? 0 : palette.Handle,
            color.R,
            color.G,
            color.B,
            color.A
        );

    public Color GetRgb(uint pixelValue, Palette? palette)
    {
        Sdl.GetRgb(
            pixelValue,
            _handle,
            palette is null ? 0 : palette.Handle,
            out var r,
            out var g,
            out var b
        );

        return Color.FromArgb(r, g, b);
    }

    public Color GetRgba(uint pixelValue, Palette? palette)
    {
        Sdl.GetRgba(
            pixelValue,
            _handle,
            palette is null ? 0 : palette.Handle,
            out var r,
            out var g,
            out var b,
            out var a
        );

        return Color.FromArgb(a, r, g, b);
    }
}
