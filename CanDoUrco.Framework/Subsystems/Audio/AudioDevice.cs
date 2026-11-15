// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Framework.Properties;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems.Audio;

[DebuggerDisplay("{ToString()}")]
public sealed class AudioDevice : IDisposable
{
    private readonly Sdl.AudioDeviceID _id;
    private readonly bool _opened;
    private readonly List<AudioStream> _boundStreams = [];
    private bool _disposed;

    public bool IsPhysical
    {
        get
        {
            ThrowIfDisposed();
            return Sdl.IsAudioDevicePhysical(_id);
        }
    }

    public bool IsPlayback
    {
        get
        {
            ThrowIfDisposed();
            return Sdl.IsAudioDevicePlayback(_id);
        }
    }

    public bool IsRecording => !IsPlayback;
    public bool IsPaused
    {
        get
        {
            ThrowIfDisposed();
            return Sdl.AudioDevicePaused(_id);
        }
    }

    internal AudioDevice(Sdl.AudioDeviceID id, bool opened = false)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        _id = id;
        _opened = opened;
    }

    public unsafe void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (_disposed)
        {
            return;
        }

        if (_opened)
        {
            Sdl.CloseAudioDevice(_id);
        }
        else
        {
            var nativeStreams = new Sdl.AudioStream[_boundStreams.Count];
            for (var i = 0; i < _boundStreams.Count; i++)
            {
                nativeStreams[i] = _boundStreams[i].Handle;
            }

            if (nativeStreams.Length > 0)
            {
                fixed (Sdl.AudioStream* nativeStreamsPtr = nativeStreams)
                {
                    Sdl.UnbindAudioStreams(nativeStreamsPtr, nativeStreams.Length);
                }
            }
        }

        foreach (var stream in _boundStreams)
        {
            stream.MarkUnbound(this);
        }
        _boundStreams.Clear();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public unsafe (AudioSpec Spec, int SampleFrames) GetFormat()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        return !Sdl.GetAudioDeviceFormat(_id, out var spec, out var sampleFrames)
            ? throw new InvalidOperationException(
                $"Failed to get audio device format: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : (new AudioSpec(new AudioFormat(spec.Format), spec.Channels, spec.Freq), sampleFrames);
    }

    public unsafe ReadOnlyCollection<int> GetChannelMap()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        var ptr = Sdl.GetAudioDeviceChannelMap(_id, out var count);
        if (ptr is null && count != 0)
        {
            throw new InvalidOperationException(
                $"Failed to get audio device channel map: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        try
        {
            if (count < 0)
            {
                throw new InvalidOperationException(
                    "SDL returned a negative audio channel-map size."
                );
            }
            if (ptr is null || count == 0)
            {
                return [];
            }

            var channelMap = new int[count];
            for (var i = 0; i < count; i++)
            {
                channelMap[i] = ptr[i];
            }
            return new ReadOnlyCollection<int>(channelMap);
        }
        finally
        {
            if (ptr is not null)
            {
                Sdl.Free(ptr);
            }
        }
    }

    public unsafe AudioDeviceProperties GetProperties()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        var properties = Sdl.GetAudioDeviceProperties(_id);
        return properties == 0
            ? throw new InvalidOperationException(
                $"Failed to get audio device properties: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioDeviceProperties(properties);
    }

    public unsafe AudioDevice Open(AudioSpec? spec)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        Sdl.AudioDeviceID result;
        if (spec is null)
        {
            result = Sdl.OpenAudioDevice(_id, null);
            return result == 0
                ? throw new InvalidOperationException(
                    $"Failed to open audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                )
                : new AudioDevice(result, opened: true);
        }

        var nativeSpec = new Sdl.AudioSpec
        {
            Format = spec.Format.Value,
            Channels = spec.Channels,
            Freq = spec.Frequency,
        };

        result = Sdl.OpenAudioDevice(_id, &nativeSpec);
        return result == 0
            ? throw new InvalidOperationException(
                $"Failed to open audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioDevice(result, opened: true);
    }

    public unsafe void Pause()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (!Sdl.PauseAudioDevice(_id))
        {
            throw new InvalidOperationException(
                $"Failed to pause audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe void Resume()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (!Sdl.ResumeAudioDevice(_id))
        {
            throw new InvalidOperationException(
                $"Failed to resume audio device: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe float GetGain()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        var gain = Sdl.GetAudioDeviceGain(_id);
        return gain == -1F
            ? throw new InvalidOperationException(
                $"Failed to get audio device gain: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : gain;
    }

    public unsafe void SetGain(float gain)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (!Sdl.SetAudioDeviceGain(_id, gain))
        {
            throw new InvalidOperationException(
                $"Failed to set audio device gain: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe void BindStreams(ReadOnlySpan<AudioStream> streams)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (streams.IsEmpty)
        {
            return;
        }

        var nativeStreams = new Sdl.AudioStream[streams.Length];
        var distinctStreams = new HashSet<AudioStream>();
        for (var i = 0; i < streams.Length; i++)
        {
            ArgumentNullException.ThrowIfNull(streams[i]);
            if (streams[i].IsBound)
            {
                throw new InvalidOperationException(
                    "An audio stream can only be bound to one device."
                );
            }
            if (!distinctStreams.Add(streams[i]))
            {
                throw new ArgumentException(
                    "The stream list cannot contain duplicates.",
                    nameof(streams)
                );
            }

            nativeStreams[i] = streams[i].Handle;
        }

        _boundStreams.EnsureCapacity(checked(_boundStreams.Count + streams.Length));
        fixed (Sdl.AudioStream* nativeStreamsPtr = nativeStreams)
        {
            if (!Sdl.BindAudioStreams(_id, nativeStreamsPtr, streams.Length))
            {
                throw new InvalidOperationException(
                    $"Failed to bind audio streams: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }

        _boundStreams.AddRange(streams);
        foreach (var stream in streams)
        {
            stream.SetBoundDevice(this);
        }
    }

    public unsafe void BindStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(stream);
        if (stream.IsBound)
        {
            throw new InvalidOperationException("An audio stream can only be bound to one device.");
        }

        _boundStreams.EnsureCapacity(checked(_boundStreams.Count + 1));
        if (!Sdl.BindAudioStream(_id, stream.Handle))
        {
            throw new InvalidOperationException(
                $"Failed to bind audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        _boundStreams.Add(stream);
        stream.SetBoundDevice(this);
    }

    public unsafe void UnbindStreams(ReadOnlySpan<AudioStream> streams)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (streams.IsEmpty)
        {
            return;
        }

        var nativeStreams = new Sdl.AudioStream[streams.Length];
        for (var i = 0; i < streams.Length; i++)
        {
            ArgumentNullException.ThrowIfNull(streams[i]);
            if (streams[i].BoundDevice != this)
            {
                throw new InvalidOperationException(
                    "The audio stream is not bound to this device."
                );
            }

            nativeStreams[i] = streams[i].Handle;
        }

        fixed (Sdl.AudioStream* nativeStreamsPtr = nativeStreams)
        {
            Sdl.UnbindAudioStreams(nativeStreamsPtr, streams.Length);
        }

        foreach (var stream in streams)
        {
            _boundStreams.Remove(stream);
            stream.MarkUnbound(this);
        }
    }

    public void UnbindStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(stream);
        if (stream.BoundDevice != this)
        {
            throw new InvalidOperationException("The audio stream is not bound to this device.");
        }

        Sdl.UnbindAudioStream(stream.Handle);
        _boundStreams.Remove(stream);
        stream.MarkUnbound(this);
    }

    public override unsafe string ToString()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        return Utf8StringMarshaller.ConvertToManaged(Sdl.GetAudioDeviceName(_id))
            ?? throw new InvalidOperationException(
                $"Failed to get audio device name: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
    }

    internal void ForgetBoundStream(AudioStream stream)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        _boundStreams.Remove(stream);
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this);
}
