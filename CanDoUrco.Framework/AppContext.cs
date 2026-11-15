// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Framework.Subsystems;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;
using Microsoft.Extensions.Logging;

namespace CanDoUrco.Framework;

public sealed class AppContext : IDisposable
{
    private bool _disposed;

    internal const string CustomLogCategory = "CanDoUrco.Framework";

    public AppContext(Action<ILoggingBuilder>? builder = null)
    {
        if (builder is not null)
        {
            CoreLog.Initialize(builder);
        }

        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        Sdl.SetMainReady();
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        if (_disposed)
        {
            return;
        }

        Sdl.Quit();
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    public AudioSubsystem InitAudioSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new AudioSubsystem(this);
    }

    public CameraSubsystem InitCameraSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new CameraSubsystem(this);
    }

    public EventsSubsystem InitEventsSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new EventsSubsystem(this);
    }

    public GamepadSubsystem InitGamepadSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new GamepadSubsystem(this);
    }

    public HapticSubsystem InitHapticSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new HapticSubsystem(this);
    }

    public JoystickSubsystem InitJoystickSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new JoystickSubsystem(this);
    }

    public SensorSubsystem InitSensorSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new SensorSubsystem(this);
    }

    public VideoSubsystem InitVideoSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ThrowIfDisposed();
        return new VideoSubsystem(this);
    }

    private void ThrowIfDisposed() =>
        ObjectDisposedException.ThrowIf(_disposed, nameof(AppContext));
}
