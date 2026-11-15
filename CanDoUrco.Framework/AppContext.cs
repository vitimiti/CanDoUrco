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

        Sdl.Quit();
        GC.SuppressFinalize(this);
    }

    public AudioSubsystem InitAudioSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new AudioSubsystem(this);
    }

    public CameraSubsystem InitCameraSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new CameraSubsystem(this);
    }

    public EventsSubsystem InitEventsSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new EventsSubsystem(this);
    }

    public GamepadSubsystem InitGamepadSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new GamepadSubsystem(this);
    }

    public HapticSubsystem InitHapticSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new HapticSubsystem(this);
    }

    public JoystickSubsystem InitJoystickSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new JoystickSubsystem(this);
    }

    public SensorSubsystem InitSensorSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new SensorSubsystem(this);
    }

    public VideoSubsystem InitVideoSubsystem()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return new VideoSubsystem(this);
    }
}
