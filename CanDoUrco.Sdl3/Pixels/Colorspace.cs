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

namespace CanDoUrco.Sdl3.Pixels;

public sealed record Colorspace(uint Value)
{
    public static Colorspace Unknown => new(0U);
    public static Colorspace Srgb => new(0x120005A0U);
    public static Colorspace SrgbLinear => new(0x12000500U);
    public static Colorspace Hdr10 => new(0x12002600U);
    public static Colorspace Jpeg => new(0x220004C6U);
    public static Colorspace BT601Limited => new(0x211018C6U);
    public static Colorspace BT601Full => new(0x221018C6U);
    public static Colorspace BT709Limited => new(0x21100421U);
    public static Colorspace BT709Full => new(0x22100421U);
    public static Colorspace BT2020Limited => new(0x21102609U);
    public static Colorspace BT2020Full => new(0x22102609U);
    public static Colorspace RgbDefault => Srgb;
    public static Colorspace YuvDefault => BT601Limited;

    public ColorType SpaceType => (ColorType)((Value >> 28) & 0x0F);
    public ColorRange Range => (ColorRange)((Value >> 24) & 0x0F);
    public ChromaLocation Chroma => (ChromaLocation)((Value >> 20) & 0x0F);
    public ColorPrimaries Primaries => (ColorPrimaries)((Value >> 10) & 0x1F);
    public TransferCharacteristics Transfer => (TransferCharacteristics)((Value >> 5) & 0x1F);
    public MatrixCoefficients Matrix => (MatrixCoefficients)(Value & 0x1F);
    public bool IsMatrixBT601 => Matrix is MatrixCoefficients.BT601 or MatrixCoefficients.BT470BG;
    public bool IsMatrixBT709 => Matrix is MatrixCoefficients.BT709;
    public bool IsMatrixBT2020Ncl => Matrix is MatrixCoefficients.BT2020Ncl;
    public bool IsLimitedRange => Range is not ColorRange.Full;
    public bool IsFullRange => Range is ColorRange.Full;

    public static Colorspace Define(
        ColorType type,
        ColorRange range,
        ColorPrimaries primaries,
        TransferCharacteristics transfer,
        MatrixCoefficients matrix,
        ChromaLocation chroma
    ) =>
        new(
            ((uint)type << 28)
                | ((uint)range << 24)
                | ((uint)chroma << 20)
                | ((uint)primaries << 10)
                | ((uint)transfer << 5)
                | ((uint)matrix)
        );
}
