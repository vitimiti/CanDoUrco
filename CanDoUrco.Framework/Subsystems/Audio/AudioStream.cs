// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Framework.Properties;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems.Audio;

public sealed class AudioStream : IDisposable
{
    private Sdl.AudioStream _handle;
    private AudioDevice? _boundDevice;
    private int _activeLocks;
    private bool _disposed;

    internal Sdl.AudioStream Handle
    {
        get
        {
            ThrowIfDisposed();
            return _handle;
        }
    }

    internal bool IsBound
    {
        get
        {
            ThrowIfDisposed();
            return _boundDevice is not null;
        }
    }
    internal AudioDevice? BoundDevice
    {
        get
        {
            ThrowIfDisposed();
            return _boundDevice;
        }
    }

    public bool DevicePaused
    {
        get
        {
            ThrowIfDisposed();
            return Sdl.AudioStreamDevicePaused(Handle);
        }
    }

    internal AudioStream(Sdl.AudioStream handle)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (handle == 0)
        {
            throw new ArgumentException("Audio stream handle cannot be null.", nameof(handle));
        }

        _handle = handle;
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (_disposed)
        {
            return;
        }
        if (_activeLocks != 0)
        {
            throw new InvalidOperationException("Unlock the audio stream before disposing it.");
        }

        _disposed = true;
        var handle = _handle;
        _handle = default;
        _boundDevice?.ForgetBoundStream(this);
        _boundDevice = null;
        Sdl.DestroyAudioStream(handle);
        GC.SuppressFinalize(this);
    }

    public AudioDevice? GetDevice()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var id = Sdl.GetAudioStreamDevice(Handle);
        return id == 0 ? null : new AudioDevice(id);
    }

    public unsafe AudioStreamProperties GetProperties()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var props = Sdl.GetAudioStreamProperties(Handle);
        return props == 0
            ? throw new InvalidOperationException(
                $"Failed to get audio stream properties: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : new AudioStreamProperties(props);
    }

    public unsafe (AudioSpec Source, AudioSpec Destination) GetFormat()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        return !Sdl.GetAudioStreamFormat(Handle, out var sourceSpec, out var destSpec)
            ? throw new InvalidOperationException(
                $"Failed to get audio stream format: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : ((AudioSpec Source, AudioSpec Destination))
                (
                    new AudioSpec(
                        new AudioFormat(sourceSpec.Format),
                        sourceSpec.Channels,
                        sourceSpec.Freq
                    ),
                    new AudioSpec(
                        new AudioFormat(destSpec.Format),
                        destSpec.Channels,
                        destSpec.Freq
                    )
                );
    }

    public unsafe void SetFormat(AudioSpec? srcSpec, AudioSpec? dstSpec)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        Sdl.AudioSpec nativeSrcSpec;
        Sdl.AudioSpec nativeDstSpec;
        if (srcSpec is not null)
        {
            nativeSrcSpec = new Sdl.AudioSpec
            {
                Format = srcSpec.Format.Value,
                Channels = srcSpec.Channels,
                Freq = srcSpec.Frequency,
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

        if (
            !Sdl.SetAudioStreamFormat(
                Handle,
                srcSpec is not null ? &nativeSrcSpec : null,
                dstSpec is not null ? &nativeDstSpec : null
            )
        )
        {
            throw new InvalidOperationException(
                $"Failed to set audio stream format: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe float GetFrequencyRatio()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var ratio = Sdl.GetAudioStreamFrequencyRatio(Handle);
        return ratio == 0F
            ? throw new InvalidOperationException(
                $"Failed to get audio stream frequency ratio: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : ratio;
    }

    public unsafe void SetFrequencyRatio(float ratio)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.SetAudioStreamFrequencyRatio(Handle, ratio))
        {
            throw new InvalidOperationException(
                $"Failed to set audio stream frequency ratio: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe float GetGain()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var gain = Sdl.GetAudioStreamGain(Handle);
        return gain == -1F
            ? throw new InvalidOperationException(
                $"Failed to get audio stream gain: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : gain;
    }

    public unsafe void SetGain(float gain)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.SetAudioStreamGain(Handle, gain))
        {
            throw new InvalidOperationException(
                $"Failed to set audio stream gain: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe ReadOnlyCollection<int> GetInputChannelMap()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var ptr = Sdl.GetAudioStreamInputChannelMap(Handle, out var count);
        if (ptr is null)
        {
            return count == 0
                ? []
                : throw new InvalidOperationException(
                    $"Failed to get audio stream input channel map: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
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
            if (count == 0)
            {
                return [];
            }

            var array = new int[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = ptr[i];
            }
            return new ReadOnlyCollection<int>(array);
        }
        finally
        {
            Sdl.Free(ptr);
        }
    }

    public unsafe ReadOnlyCollection<int> GetOutputChannelMap()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var ptr = Sdl.GetAudioStreamOutputChannelMap(Handle, out var count);
        if (ptr is null)
        {
            return count == 0
                ? []
                : throw new InvalidOperationException(
                    $"Failed to get audio stream output channel map: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
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
            if (count == 0)
            {
                return [];
            }

            var array = new int[count];
            for (var i = 0; i < count; i++)
            {
                array[i] = ptr[i];
            }
            return new ReadOnlyCollection<int>(array);
        }
        finally
        {
            Sdl.Free(ptr);
        }
    }

    public unsafe void SetInputChannelMap(ReadOnlySpan<int> channelMap)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        fixed (int* ptr = channelMap)
        {
            if (!Sdl.SetAudioStreamInputChannelMap(Handle, ptr, channelMap.Length))
            {
                throw new InvalidOperationException(
                    $"Failed to set audio stream input channel map: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
    }

    public unsafe void SetOutputChannelMap(ReadOnlySpan<int> channelMap)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        fixed (int* ptr = channelMap)
        {
            if (!Sdl.SetAudioStreamOutputChannelMap(Handle, ptr, channelMap.Length))
            {
                throw new InvalidOperationException(
                    $"Failed to set audio stream output channel map: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
    }

    public unsafe void PutData(ReadOnlySpan<byte> data)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var handle = Handle;
        if (data.IsEmpty)
        {
            return;
        }

        fixed (byte* ptr = data)
        {
            if (!Sdl.PutAudioStreamData(handle, ptr, data.Length))
            {
                throw new InvalidOperationException(
                    $"Failed to put audio stream data: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
    }

    public unsafe void PutPlanarData(ReadOnlyCollection<ReadOnlyCollection<byte>> buffers)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        ArgumentNullException.ThrowIfNull(buffers);

        foreach (var buffer in buffers)
        {
            ArgumentNullException.ThrowIfNull(buffer);
        }
        if (buffers.Count == 0)
        {
            throw new ArgumentException(
                "At least one channel buffer is required.",
                nameof(buffers)
            );
        }

        using var lockedStream = Lock();
        var sourceSpec = GetFormat().Source;
        if (buffers.Count != sourceSpec.Channels)
        {
            throw new ArgumentException(
                $"Expected {sourceSpec.Channels} channel buffers, but received {buffers.Count}.",
                nameof(buffers)
            );
        }

        var bytesPerSample = sourceSpec.Format.ByteSize;
        if (bytesPerSample == 0)
        {
            throw new InvalidOperationException("The audio stream source format is unknown.");
        }

        var numBytes = buffers[0].Count;
        if (numBytes % bytesPerSample != 0)
        {
            throw new ArgumentException(
                "Each channel buffer must contain a whole number of samples.",
                nameof(buffers)
            );
        }

        for (int i = 1; i < buffers.Count; i++)
        {
            if (buffers[i].Count != numBytes)
            {
                throw new ArgumentException(
                    "All channel buffers must have the same length.",
                    nameof(buffers)
                );
            }
        }

        var numSamples = numBytes / bytesPerSample;
        if (numSamples == 0)
        {
            return;
        }

        var arraysPtr = (byte**)
            NativeMemory.AllocZeroed((nuint)buffers.Count, (nuint)sizeof(byte*));
        if (arraysPtr is null)
        {
            throw new OutOfMemoryException();
        }

        try
        {
            for (int i = 0; i < buffers.Count; i++)
            {
                var buffer = buffers[i];
                var bufferPtr = (byte*)NativeMemory.Alloc((nuint)buffer.Count);
                if (bufferPtr is null)
                {
                    throw new OutOfMemoryException();
                }

                arraysPtr[i] = bufferPtr;
                for (int j = 0; j < buffer.Count; j++)
                {
                    bufferPtr[j] = buffer[j];
                }
            }

            if (!Sdl.PutAudioStreamPlanarData(Handle, (void**)arraysPtr, buffers.Count, numSamples))
            {
                throw new InvalidOperationException(
                    $"Failed to put audio stream planar data: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            for (int i = 0; i < buffers.Count; i++)
            {
                NativeMemory.Free(arraysPtr[i]);
            }
            NativeMemory.Free(arraysPtr);
        }
    }

    public unsafe int ReadData(Span<byte> buffer)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var handle = Handle;
        if (buffer.IsEmpty)
        {
            return 0;
        }

        int bytesRead;
        fixed (byte* ptr = buffer)
        {
            bytesRead = Sdl.GetAudioStreamData(handle, ptr, buffer.Length);
        }

        return bytesRead < 0
            ? throw new InvalidOperationException(
                $"Failed to read audio stream data: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : bytesRead;
    }

    public unsafe int GetAvailable()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var available = Sdl.GetAudioStreamAvailable(Handle);
        return available < 0
            ? throw new InvalidOperationException(
                $"Failed to get available audio stream data: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : available;
    }

    public unsafe int GetQueued()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var queued = Sdl.GetAudioStreamQueued(Handle);
        return queued < 0
            ? throw new InvalidOperationException(
                $"Failed to get queued audio stream data: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            )
            : queued;
    }

    public unsafe void Flush()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.FlushAudioStream(Handle))
        {
            throw new InvalidOperationException(
                $"Failed to flush audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe void Clear()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.ClearAudioStream(Handle))
        {
            throw new InvalidOperationException(
                $"Failed to clear audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe void PauseDevice()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.PauseAudioStreamDevice(Handle))
        {
            throw new InvalidOperationException(
                $"Failed to pause audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe void ResumeDevice()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (!Sdl.ResumeAudioStreamDevice(Handle))
        {
            throw new InvalidOperationException(
                $"Failed to resume audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe LockedAudioStream Lock()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        var lockedStream = new LockedAudioStream(this);
        if (!Sdl.LockAudioStream(Handle))
        {
            throw new InvalidOperationException(
                $"Failed to lock audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        _activeLocks++;
        return lockedStream;
    }

    internal unsafe void Unlock()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();

        if (_activeLocks == 0)
        {
            throw new InvalidOperationException("The audio stream is not locked.");
        }
        if (!Sdl.UnlockAudioStream(_handle))
        {
            throw new InvalidOperationException(
                $"Failed to unlock audio stream: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }

        _activeLocks--;
    }

    internal void SetBoundDevice(AudioDevice? device)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (device is not null && _boundDevice is not null && _boundDevice != device)
        {
            throw new InvalidOperationException(
                "The audio stream is already bound to another device."
            );
        }

        _boundDevice = device;
    }

    internal void MarkUnbound(AudioDevice device)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ThrowIfDisposed();
        if (_boundDevice == device)
        {
            _boundDevice = null;
        }
    }

    private void ThrowIfDisposed() => ObjectDisposedException.ThrowIf(_disposed, this);
}
