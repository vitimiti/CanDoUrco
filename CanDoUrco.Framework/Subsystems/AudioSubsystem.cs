// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Framework.Subsystems.Audio;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems;

public sealed class AudioSubsystem : SubsystemBase
{
    public static int AudioDriversCount => Sdl.GetNumAudioDrivers();

    public AudioSubsystem(AppContext app)
        : base(app, Sdl.InitAudio) => LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();

    public static unsafe ReadOnlyCollection<string> GetAudioDriverNames()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        var count = AudioDriversCount;
        if (count == 0)
        {
            return [];
        }

        var drivers = new string[count];
        for (int i = 0; i < count; i++)
        {
            drivers[i] =
                Utf8StringMarshaller.ConvertToManaged(Sdl.GetAudioDriver(i))
                ?? throw new InvalidOperationException(
                    $"Failed to get audio driver at index {i}: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
        }

        return new ReadOnlyCollection<string>(drivers);
    }

    public unsafe ReadOnlyCollection<AudioDevice> GetPlaybackDevices()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ObjectDisposedException.ThrowIf(Disposed, this);

        var ptr = Sdl.GetAudioPlaybackDevices(out var count);
        if (ptr is null)
        {
            throw new InvalidOperationException(
                $"Failed to get audio playback devices: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        try
        {
            if (count < 0)
            {
                throw new InvalidOperationException(
                    "SDL returned a negative playback-device count."
                );
            }
            if (count == 0)
            {
                return [];
            }

            var devices = new AudioDevice[count];
            for (int i = 0; i < count; i++)
            {
                devices[i] = new AudioDevice(ptr[i]);
            }

            return new ReadOnlyCollection<AudioDevice>(devices);
        }
        finally
        {
            Sdl.Free(ptr);
        }
    }

    public unsafe ReadOnlyCollection<AudioDevice> GetRecordingDevices()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ObjectDisposedException.ThrowIf(Disposed, this);

        var ptr = Sdl.GetAudioRecordingDevices(out var count);
        if (ptr is null)
        {
            throw new InvalidOperationException(
                $"Failed to get audio recording devices: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        try
        {
            if (count < 0)
            {
                throw new InvalidOperationException(
                    "SDL returned a negative recording-device count."
                );
            }
            if (count == 0)
            {
                return [];
            }

            var devices = new AudioDevice[count];
            for (int i = 0; i < count; i++)
            {
                devices[i] = new AudioDevice(ptr[i]);
            }

            return new ReadOnlyCollection<AudioDevice>(devices);
        }
        finally
        {
            Sdl.Free(ptr);
        }
    }

    public unsafe AudioDevice OpenDefaultPlaybackAudioDevice(AudioSpec? spec)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ObjectDisposedException.ThrowIf(Disposed, this);

        Sdl.AudioDeviceID result;
        if (spec is null)
        {
            result = Sdl.OpenAudioDevice(Sdl.AudioDeviceDefaultPlayback, null);
            return result == 0
                ? throw new InvalidOperationException(
                    $"Failed to open default playback audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                )
                : new AudioDevice(result, opened: true);
        }

        var nativeSpec = new Sdl.AudioSpec
        {
            Format = spec.Format.Value,
            Channels = spec.Channels,
            Freq = spec.Frequency,
        };

        result = Sdl.OpenAudioDevice(Sdl.AudioDeviceDefaultPlayback, &nativeSpec);
        return result == 0
            ? throw new InvalidOperationException(
                $"Failed to open default playback audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioDevice(result, opened: true);
    }

    public unsafe AudioDevice OpenDefaultRecordingAudioDevice(AudioSpec? spec)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ObjectDisposedException.ThrowIf(Disposed, this);

        Sdl.AudioDeviceID result;
        if (spec is null)
        {
            result = Sdl.OpenAudioDevice(Sdl.AudioDeviceDefaultRecording, null);
            return result == 0
                ? throw new InvalidOperationException(
                    $"Failed to open default recording audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                )
                : new AudioDevice(result, opened: true);
        }

        var nativeSpec = new Sdl.AudioSpec
        {
            Format = spec.Format.Value,
            Channels = spec.Channels,
            Freq = spec.Frequency,
        };

        result = Sdl.OpenAudioDevice(Sdl.AudioDeviceDefaultRecording, &nativeSpec);
        return result == 0
            ? throw new InvalidOperationException(
                $"Failed to open default recording audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioDevice(result, opened: true);
    }

    public unsafe AudioStream CreateStream(AudioSpec? src, AudioSpec? dstSpec)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ObjectDisposedException.ThrowIf(Disposed, this);

        Sdl.AudioSpec nativeSrcSpec;
        Sdl.AudioSpec nativeDstSpec;
        if (src is not null)
        {
            nativeSrcSpec = new Sdl.AudioSpec
            {
                Format = src.Format.Value,
                Channels = src.Channels,
                Freq = src.Frequency,
            };
        }
        if (dstSpec is not null)
        {
            nativeDstSpec = new Sdl.AudioSpec
            {
                Format = dstSpec.Format.Value,
                Channels = dstSpec.Channels,
                Freq = dstSpec.Frequency,
            };
        }

        var result = Sdl.CreateAudioStream(
            src is not null ? &nativeSrcSpec : null,
            dstSpec is not null ? &nativeDstSpec : null
        );
        return result == 0
            ? throw new InvalidOperationException(
                $"Failed to create audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioStream(result);
    }
}
