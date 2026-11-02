// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl;

public static partial class Sdl
{
    public static BlendMode BlendModeNone => new(0x0000_0000U);

    public static BlendMode BlendModeBlend => new(0x0000_0001U);

    public static BlendMode BlendModeBlendPremultiplied => new(0x0000_0010U);

    public static BlendMode BlendModeAdd => new(0x0000_0002U);

    public static BlendMode BlendModeAddPremultiplied => new(0x0000_0020U);

    public static BlendMode BlendModeMod => new(0x0000_0004U);

    public static BlendMode BlendModeMul => new(0x0000_0008U);

    public static BlendMode BlendModeInvalid => new(0x7FFF_FFFFU);

    [LibraryImport(DllName, EntryPoint = "SDL_ComposeCustomBlendMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial BlendMode _ComposeCustomBlendMode(
        BlendFactor srcColorFactor,
        BlendFactor dstColorFactor,
        BlendOperation colorOperation,
        BlendFactor srcAlphaFactor,
        BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation
    );

    public static BlendMode ComposeCustomBlendMode(
        BlendFactor srcColorFactor,
        BlendFactor dstColorFactor,
        BlendOperation colorOperation,
        BlendFactor srcAlphaFactor,
        BlendFactor dstAlphaFactor,
        BlendOperation alphaOperation
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ComposeCustomBlendMode(
            srcColorFactor,
            dstColorFactor,
            colorOperation,
            srcAlphaFactor,
            dstAlphaFactor,
            alphaOperation
        );
    }

    public readonly record struct BlendMode(uint Value)
    {
        public static BlendMode operator |(BlendMode left, BlendMode right) =>
            new(left.Value | right.Value);

        public static BlendMode operator &(BlendMode left, BlendMode right) =>
            new(left.Value & right.Value);

        public static BlendMode operator ^(BlendMode left, BlendMode right) =>
            new(left.Value ^ right.Value);

        public static BlendMode operator ~(BlendMode value) => new(~value.Value);
    }

    public enum BlendOperation
    {
        Add = 1,
        Subtract,
        RevSubtract,
        Minimum,
        Maximum,
    }

    public enum BlendFactor
    {
        Zero = 1,
        One,
        SrcColor,
        OneMinusSrcColor,
        SrcAlpha,
        OneMinusSrcAlpha,
        DstColor,
        OneMinusDstColor,
        DstAlpha,
        OneMinusDstAlpha,
    }
}
