// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems.Audio;

[DebuggerDisplay("{ToString()}")]
public readonly record struct AudioFormat(ushort Value)
{
    public static AudioFormat Unknown => new(Sdl.AudioUnknown);
    public static AudioFormat U8 => new(Sdl.AudioU8);
    public static AudioFormat S8 => new(Sdl.AudioS8);
    public static AudioFormat S16LE => new(Sdl.AudioS16LE);
    public static AudioFormat S16BE => new(Sdl.AudioS16BE);
    public static AudioFormat S32LE => new(Sdl.AudioS32LE);
    public static AudioFormat S32BE => new(Sdl.AudioS32BE);
    public static AudioFormat F32LE => new(Sdl.AudioF32LE);
    public static AudioFormat F32BE => new(Sdl.AudioF32BE);
    public static AudioFormat S16 => new(Sdl.AudioS16);
    public static AudioFormat S32 => new(Sdl.AudioS32);
    public static AudioFormat F32 => new(Sdl.AudioF32);

    public int Silence => Sdl.GetSilenceValueForFormat(Value);
    public ushort BitSize => Sdl.AudioBitsize(Value);
    public ushort ByteSize => Sdl.AudioByteSize(Value);
    public bool IsFloat => Sdl.AudioIsFloat(Value);
    public bool IsBigEndian => Sdl.AudioIsBigEndian(Value);
    public bool IsLittleEndian => Sdl.AudioIsLittleEndian(Value);
    public bool IsSigned => Sdl.AudioIsSigned(Value);
    public bool IsInteger => Sdl.AudioIsInt(Value);
    public bool IsUnsigned => Sdl.AudioIsUnsigned(Value);

    public static AudioFormat Define(bool signed, bool bigEdian, bool @float, ushort size)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return new AudioFormat(Sdl.DefineAudioFormat(signed, bigEdian, @float, size));
    }

    public unsafe void MixAudio(Span<byte> dst, ReadOnlySpan<byte> src, int length, float volume)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentOutOfRangeException.ThrowIfNegative(length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(length, dst.Length);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(length, src.Length);

        fixed (byte* pDst = dst)
        fixed (byte* pSrc = src)
        {
            if (!Sdl.MixAudio(pDst, pSrc, Value, (uint)length, volume))
            {
                throw new InvalidOperationException(
                    $"Failed to mix audio: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}"
                );
            }
        }
    }

    public override unsafe string ToString()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return $"{Utf8StringMarshaller.ConvertToManaged(Sdl.GetAudioFormatName(Value))} (0x{Value:X4})";
    }

    public static implicit operator AudioFormat(ushort value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return new(value);
    }

    public static implicit operator ushort(AudioFormat format)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return format.Value;
    }
}
