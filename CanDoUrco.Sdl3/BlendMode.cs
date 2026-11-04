// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

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

    public readonly record struct BlendMode(uint Value)
    {
        public static BlendMode operator |(BlendMode left, BlendMode right) =>
            new(left.Value | right.Value);

        public static BlendMode operator &(BlendMode left, BlendMode right) =>
            new(left.Value & right.Value);

        public static BlendMode operator ^(BlendMode left, BlendMode right) =>
            new(left.Value ^ right.Value);

        public static BlendMode operator ~(BlendMode value) => new(~value.Value);

        public static implicit operator uint(BlendMode blendMode) => blendMode.Value;

        public static implicit operator BlendMode(uint value) => new(value);
    }
}
