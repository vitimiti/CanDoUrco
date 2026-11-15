// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Collections.ObjectModel;
using CanDoUrco.Framework.Utilities;
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
        if (AudioDriversCount == 0)
        {
            return [];
        }

        var drivers = new string[AudioDriversCount];
        for (int i = 0; i < AudioDriversCount; i++)
        {
            string? value = new UnownedUtf8String(Sdl.GetAudioDriver(i));
            if (value is null)
            {
                // This should NEVER happen, but just in case!
                var error = new UnownedUtf8String(Sdl.GetError());
                throw new InvalidOperationException(
                    $"Failed to get audio driver at index {i}: {error}."
                );
            }
            drivers[i] = value;
        }

        return new ReadOnlyCollection<string>(drivers);
    }
}
