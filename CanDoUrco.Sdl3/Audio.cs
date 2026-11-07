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
    public const uint AudioMaskBitsize = 0xFFU;
    public const uint AudioMaskFloat = 1U << 8;
    public const uint AudioMaskBigEndian = 1U << 12;
    public const uint AudioMaskSigned = 1U << 15;

    public static AudioFormat AudioUnknown => 0x0000;
    public static AudioFormat AudioU8 => 0x0008;
    public static AudioFormat AudioS8 => 0x8008;
    public static AudioFormat AudioS16LE => 0x8010;
    public static AudioFormat AudioS16BE => 0x9010;
    public static AudioFormat AudioS32LE => 0x8020;
    public static AudioFormat AudioS32BE => 0x9020;
    public static AudioFormat AudioF32LE => 0x8120;
    public static AudioFormat AudioF32BE => 0x9120;
    public static AudioFormat AudioS16 => BitConverter.IsLittleEndian ? AudioS16LE : AudioS16BE;
    public static AudioFormat AudioS32 => BitConverter.IsLittleEndian ? AudioS32LE : AudioS32BE;
    public static AudioFormat AudioF32 => BitConverter.IsLittleEndian ? AudioF32LE : AudioF32BE;

    public static AudioDeviceID AudioDeviceDefaultPlayback => 0xFFFF_FFFFU;
    public static AudioDeviceID AudioDeviceDefaultRecording => 0xFFFF_FFFEU;

    public static ReadOnlySpan<byte> PropAudioStreamAutoCleanupBoolean =>
        "SDL.audiostream.auto_cleanup"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumAudioDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumAudioDrivers();

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetAudioDriver(int index);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentAudioDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCurrentAudioDriver();

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioPlaybackDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioDeviceID* _GetAudioPlaybackDevices(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioRecordingDevices")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioDeviceID* _GetAudioRecordingDevices(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioDeviceName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetAudioDeviceName(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioDeviceFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetAudioDeviceFormat(
        AudioDeviceID devID,
        out AudioSpec spec,
        out int sampleFrames
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioDeviceChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int* _GetAudioDeviceChannelMap(AudioDeviceID devID, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioDeviceID _OpenAudioDevice(AudioDeviceID devID, AudioSpec* spec);

    [LibraryImport(DllName, EntryPoint = "SDL_IsAudioDevicePhysical")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsAudioDevicePhysical(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_IsAudioDevicePlayback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsAudioDevicePlayback(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_PauseAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PauseAudioDevice(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_ResumeAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ResumeAudioDevice(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_AudioDevicePaused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _AudioDevicePaused(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioDeviceGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetAudioDeviceGain(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioDeviceGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioDeviceGain(AudioDeviceID devID, float gain);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseAudioDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _CloseAudioDevice(AudioDeviceID devID);

    [LibraryImport(DllName, EntryPoint = "SDL_BindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BindAudioStreams(
        AudioDeviceID devID,
        AudioStream* streams,
        int numStreams
    );

    [LibraryImport(DllName, EntryPoint = "SDL_BindAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _BindAudioStream(AudioDeviceID devID, AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_UnbindAudioStreams")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnbindAudioStreams(AudioStream* streams, int numStreams);

    [LibraryImport(DllName, EntryPoint = "SDL_UnbindAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnbindAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioDeviceID _GetAudioStreamDevice(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioStream _CreateAudioStream(AudioSpec* srcSpec, AudioSpec* dstSpec);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetAudioStreamProperties(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetAudioStreamFormat(
        AudioStream stream,
        out AudioSpec srcSpec,
        out AudioSpec dstSpec
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamFormat(
        AudioStream stream,
        AudioSpec* srcSpec,
        AudioSpec* dstSpec
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamFrequencyRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetAudioStreamFrequencyRatio(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamFrequencyRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamFrequencyRatio(AudioStream stream, float ratio);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetAudioStreamGain(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamGain")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamGain(AudioStream stream, float gain);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int* _GetAudioStreamInputChannelMap(AudioStream stream, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int* _GetAudioStreamOutputChannelMap(AudioStream stream, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamInputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamInputChannelMap(
        AudioStream stream,
        int* chMap,
        int count
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamOutputChannelMap")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamOutputChannelMap(
        AudioStream stream,
        int* chMap,
        int count
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PutAudioStreamData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PutAudioStreamData(AudioStream stream, void* buf, int len);

    [LibraryImport(DllName, EntryPoint = "SDL_PutAudioStreamDataNoCopy")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PutAudioStreamDataNoCopy(
        AudioStream stream,
        void* buf,
        int len,
        delegate* unmanaged[Cdecl]<void*, void*, int, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_PutAudioStreamPlanarData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PutAudioStreamPlanarData(
        AudioStream stream,
        void** channelBuffers,
        int numChannels,
        int numSamples
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamData")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetAudioStreamData(AudioStream stream, void* buf, int len);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamAvailable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetAudioStreamAvailable(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioStreamQueued")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetAudioStreamQueued(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FlushAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_ClearAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClearAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_PauseAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PauseAudioStreamDevice(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_ResumeAudioStreamDevice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ResumeAudioStreamDevice(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_AudioStreamDevicePaused")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _AudioStreamDevicePaused(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_LockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LockAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UnlockAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamGetCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamGetCallback(
        AudioStream stream,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioStreamPutCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioStreamPutCallback(
        AudioStream stream,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyAudioStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyAudioStream(AudioStream stream);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenAudioDeviceStream")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial AudioStream _OpenAudioDeviceStream(
        AudioDeviceID devID,
        AudioSpec* spec,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAudioPostmixCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAudioPostmixCallback(
        AudioDeviceID devID,
        delegate* unmanaged[Cdecl]<void*, AudioSpec*, float*, int, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LoadWAV_IO")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LoadWavIO(
        IOStream src,
        Bool closeIO,
        out AudioSpec spec,
        out byte* audioBuf,
        out uint audioLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_LoadWAV")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _LoadWav(
        byte* path,
        out AudioSpec spec,
        out byte* audioBuf,
        out uint audioLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_MixAudio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _MixAudio(
        byte* dst,
        byte* src,
        AudioFormat format,
        uint len,
        float volume
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ConvertAudioSamples")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ConvertAudioSamples(
        in AudioSpec srcSpec,
        byte* srcData,
        int srcLen,
        in AudioSpec dstSpec,
        out byte* dstData,
        out int dstLen
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetAudioFormatName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetAudioFormatName(AudioFormat format);

    [LibraryImport(DllName, EntryPoint = "SDL_GetSilenceValueForFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetSilenceValueForFormat(AudioFormat format);

    public static AudioFormat DefineAudioFormat(bool signed, bool bigEndian, bool flt, ushort size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new AudioFormat(
            (ushort)(
                (ushort)(signed ? 1 : 0) << 15
                | (ushort)(bigEndian ? 1 : 0) << 12
                | (ushort)(flt ? 1 : 0) << 8
                | (ushort)(size & AudioMaskBitsize)
            )
        );
    }

    public static ushort AudioBitsize(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ushort)(x & AudioMaskBitsize);
    }

    public static ushort AudioByteSize(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (ushort)(AudioBitsize(x) / 8);
    }

    public static bool AudioIsFloat(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (x & AudioMaskFloat) != 0;
    }

    public static bool AudioIsBigEndian(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (x & AudioMaskBigEndian) != 0;
    }

    public static bool AudioIsLittleEndian(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return !AudioIsBigEndian(x);
    }

    public static bool AudioIsSigned(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (x & AudioMaskSigned) != 0;
    }

    public static bool AudioIsInt(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return !AudioIsFloat(x);
    }

    public static bool AudioIsUnsigned(AudioFormat x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return !AudioIsSigned(x);
    }

    public static uint AudioFrameSize(AudioSpec x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return (uint)(AudioByteSize(x.Format) * x.Channels);
    }

    public static int GetNumAudioDrivers()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumAudioDrivers();
    }

    public static byte* GetAudioDriver(int index)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioDriver(index);
    }

    public static byte* GetCurrentAudioDriver()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentAudioDriver();
    }

    public static AudioDeviceID* GetAudioPlaybackDevices(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioPlaybackDevices(out count);
    }

    public static AudioDeviceID* GetAudioRecordingDevices(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioRecordingDevices(out count);
    }

    public static byte* GetAudioDeviceName(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioDeviceName(devID);
    }

    public static Bool GetAudioDeviceFormat(
        AudioDeviceID devID,
        out AudioSpec spec,
        out int sampleFrames
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioDeviceFormat(devID, out spec, out sampleFrames);
    }

    public static int* GetAudioDeviceChannelMap(AudioDeviceID devID, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioDeviceChannelMap(devID, out count);
    }

    public static AudioDeviceID OpenAudioDevice(AudioDeviceID devID, AudioSpec* spec)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenAudioDevice(devID, spec);
    }

    public static Bool IsAudioDevicePhysical(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsAudioDevicePhysical(devID);
    }

    public static Bool IsAudioDevicePlayback(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsAudioDevicePlayback(devID);
    }

    public static Bool PauseAudioDevice(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PauseAudioDevice(devID);
    }

    public static Bool ResumeAudioDevice(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ResumeAudioDevice(devID);
    }

    public static Bool AudioDevicePaused(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AudioDevicePaused(devID);
    }

    public static float GetAudioDeviceGain(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioDeviceGain(devID);
    }

    public static Bool SetAudioDeviceGain(AudioDeviceID devID, float gain)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioDeviceGain(devID, gain);
    }

    public static void CloseAudioDevice(AudioDeviceID devID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _CloseAudioDevice(devID);
    }

    public static Bool BindAudioStreams(AudioDeviceID devID, AudioStream* streams, int numStreams)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BindAudioStreams(devID, streams, numStreams);
    }

    public static Bool BindAudioStream(AudioDeviceID devID, AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _BindAudioStream(devID, stream);
    }

    public static void UnbindAudioStreams(AudioStream* streams, int numStreams)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnbindAudioStreams(streams, numStreams);
    }

    public static void UnbindAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnbindAudioStream(stream);
    }

    public static AudioDeviceID GetAudioStreamDevice(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamDevice(stream);
    }

    public static AudioStream CreateAudioStream(AudioSpec* srcSpec, AudioSpec* dstSpec)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateAudioStream(srcSpec, dstSpec);
    }

    public static PropertiesID GetAudioStreamProperties(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamProperties(stream);
    }

    public static Bool GetAudioStreamFormat(
        AudioStream stream,
        out AudioSpec srcSpec,
        out AudioSpec dstSpec
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamFormat(stream, out srcSpec, out dstSpec);
    }

    public static Bool SetAudioStreamFormat(
        AudioStream stream,
        AudioSpec* srcSpec,
        AudioSpec* dstSpec
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamFormat(stream, srcSpec, dstSpec);
    }

    public static float GetAudioStreamFrequencyRatio(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamFrequencyRatio(stream);
    }

    public static Bool SetAudioStreamFrequencyRatio(AudioStream stream, float ratio)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamFrequencyRatio(stream, ratio);
    }

    public static float GetAudioStreamGain(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamGain(stream);
    }

    public static Bool SetAudioStreamGain(AudioStream stream, float gain)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamGain(stream, gain);
    }

    public static int* GetAudioStreamInputChannelMap(AudioStream stream, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamInputChannelMap(stream, out count);
    }

    public static int* GetAudioStreamOutputChannelMap(AudioStream stream, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamOutputChannelMap(stream, out count);
    }

    public static Bool SetAudioStreamInputChannelMap(AudioStream stream, int* chMap, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamInputChannelMap(stream, chMap, count);
    }

    public static Bool SetAudioStreamOutputChannelMap(AudioStream stream, int* chMap, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamOutputChannelMap(stream, chMap, count);
    }

    public static Bool PutAudioStreamData(AudioStream stream, void* buf, int len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PutAudioStreamData(stream, buf, len);
    }

    public static Bool PutAudioStreamDataNoCopy(
        AudioStream stream,
        void* buf,
        int len,
        delegate* unmanaged[Cdecl]<void*, void*, int, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PutAudioStreamDataNoCopy(stream, buf, len, callback, userdata);
    }

    public static Bool PutAudioStreamPlanarData(
        AudioStream stream,
        void** channelBuffers,
        int numChannels,
        int numSamples
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PutAudioStreamPlanarData(stream, channelBuffers, numChannels, numSamples);
    }

    public static int GetAudioStreamData(AudioStream stream, void* buf, int len)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamData(stream, buf, len);
    }

    public static int GetAudioStreamAvailable(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamAvailable(stream);
    }

    public static int GetAudioStreamQueued(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioStreamQueued(stream);
    }

    public static Bool FlushAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlushAudioStream(stream);
    }

    public static Bool ClearAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearAudioStream(stream);
    }

    public static Bool PauseAudioStreamDevice(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PauseAudioStreamDevice(stream);
    }

    public static Bool ResumeAudioStreamDevice(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ResumeAudioStreamDevice(stream);
    }

    public static Bool AudioStreamDevicePaused(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AudioStreamDevicePaused(stream);
    }

    public static Bool LockAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockAudioStream(stream);
    }

    public static Bool UnlockAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UnlockAudioStream(stream);
    }

    public static Bool SetAudioStreamGetCallback(
        AudioStream stream,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamGetCallback(stream, callback, userdata);
    }

    public static Bool SetAudioStreamPutCallback(
        AudioStream stream,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioStreamPutCallback(stream, callback, userdata);
    }

    public static void DestroyAudioStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyAudioStream(stream);
    }

    public static AudioStream OpenAudioDeviceStream(
        AudioDeviceID devID,
        AudioSpec* spec,
        delegate* unmanaged[Cdecl]<void*, AudioStream, int, int, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenAudioDeviceStream(devID, spec, callback, userdata);
    }

    public static Bool SetAudioPostmixCallback(
        AudioDeviceID devID,
        delegate* unmanaged[Cdecl]<void*, AudioSpec*, float*, int, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAudioPostmixCallback(devID, callback, userdata);
    }

    public static Bool LoadWavIO(
        IOStream src,
        Bool closeIO,
        out AudioSpec spec,
        out byte* audioBuf,
        out uint audioLen
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadWavIO(src, closeIO, out spec, out audioBuf, out audioLen);
    }

    public static Bool LoadWav(
        byte* path,
        out AudioSpec spec,
        out byte* audioBuf,
        out uint audioLen
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LoadWav(path, out spec, out audioBuf, out audioLen);
    }

    public static Bool MixAudio(byte* dst, byte* src, AudioFormat format, uint len, float volume)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MixAudio(dst, src, format, len, volume);
    }

    public static Bool ConvertAudioSamples(
        in AudioSpec srcSpec,
        byte* srcData,
        int srcLen,
        in AudioSpec dstSpec,
        out byte* dstData,
        out int dstLen
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ConvertAudioSamples(srcSpec, srcData, srcLen, dstSpec, out dstData, out dstLen);
    }

    public static byte* GetAudioFormatName(AudioFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAudioFormatName(format);
    }

    public static int GetSilenceValueForFormat(AudioFormat format)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSilenceValueForFormat(format);
    }

    public readonly record struct AudioFormat(ushort Value)
    {
        public static implicit operator ushort(AudioFormat format) => format.Value;

        public static implicit operator AudioFormat(ushort value) => new(value);
    }

    public readonly record struct AudioDeviceID(uint Value)
    {
        public static implicit operator uint(AudioDeviceID id) => id.Value;

        public static implicit operator AudioDeviceID(uint value) => new(value);
    }

    public readonly record struct AudioStream(nint Value)
    {
        public static implicit operator nint(AudioStream stream) => stream.Value;

        public static implicit operator AudioStream(nint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AudioSpec
    {
        public AudioFormat Format;
        public int Channels;
        public int Freq;
    }
}
