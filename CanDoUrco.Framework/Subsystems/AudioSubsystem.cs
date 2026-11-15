// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Collections.ObjectModel;
using System.Runtime.InteropServices.Marshalling;
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
            // This should NEVER error, but just in case!
            drivers[i] =
                Utf8StringMarshaller.ConvertToManaged(Sdl.GetAudioDriver(i))
                ?? throw new InvalidOperationException(
                    $"Failed to get audio driver at index {i}: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            ;
        }

        return new ReadOnlyCollection<string>(drivers);
    }
}
