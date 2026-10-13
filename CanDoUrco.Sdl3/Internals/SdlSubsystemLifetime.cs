// An SDL3 interop library with dotnet safety and idioms.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using CanDoUrco.Sdl3.Exceptions;

namespace CanDoUrco.Sdl3.Internals;

internal static class SdlSubsystemLifetime
{
    private static readonly SdlSubsystem[] Subsystems =
    [
        SdlSubsystem.Audio,
        SdlSubsystem.Video,
        SdlSubsystem.Joystick,
        SdlSubsystem.Haptic,
        SdlSubsystem.Gamepad,
        SdlSubsystem.Events,
        SdlSubsystem.Sensor,
        SdlSubsystem.Camera,
    ];

    private const SdlSubsystem All =
        SdlSubsystem.Audio
        | SdlSubsystem.Video
        | SdlSubsystem.Joystick
        | SdlSubsystem.Haptic
        | SdlSubsystem.Gamepad
        | SdlSubsystem.Events
        | SdlSubsystem.Sensor
        | SdlSubsystem.Camera;

    private static readonly int[] References = new int[Subsystems.Length];
    private static int _leases;
    private static bool _ownsGlobalShutdown;

    internal static void EnsureMainThread()
    {
        if (!Sdl.IsMainThread())
        {
            throw new InvalidOperationException(
                "SDL lifetime operations must run on SDL's main thread."
            );
        }
    }

    internal static void Acquire(SdlSubsystem requested)
    {
        if (requested == 0 || (requested & ~All) != 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(requested),
                requested,
                "Specify valid SDL subsystems."
            );
        }

        // Main-thread dispatch needs events, even for haptics which do not imply it.
        requested |= SdlSubsystem.Events;
        EnsureMainThread();
        if (_leases == int.MaxValue)
        {
            throw new InvalidOperationException("Too many active SDL subsystem leases.");
        }

        var initialize = (SdlSubsystem)0;
        for (var i = 0; i < Subsystems.Length; i++)
        {
            if ((requested & Subsystems[i]) != 0 && References[i] == 0)
            {
                initialize |= Subsystems[i];
            }
        }

        if (_leases == 0)
        {
            _ownsGlobalShutdown = Sdl.WasInit(0) == 0;
        }

        if (initialize != 0 && !Sdl.Init((uint)initialize))
        {
            // SDL rolls back the subsystems acquired by a failed initialization.
            QuitIfUnused();
            throw new Sdl3Exception($"Failed to initialize SDL subsystems '{initialize}'");
        }

        for (var i = 0; i < Subsystems.Length; i++)
        {
            if ((requested & Subsystems[i]) != 0)
            {
                References[i]++;
            }
        }

        _leases++;
    }

    internal static void Release(SdlSubsystem requested)
    {
        requested |= SdlSubsystem.Events;
        EnsureMainThread();
        var terminate = (SdlSubsystem)0;
        for (var i = 0; i < Subsystems.Length; i++)
        {
            if ((requested & Subsystems[i]) != 0 && References[i] == 1)
            {
                terminate |= Subsystems[i];
            }
        }

        if (terminate != 0)
        {
            Sdl.QuitSubSystem((uint)terminate);
        }

        for (var i = 0; i < Subsystems.Length; i++)
        {
            if ((requested & Subsystems[i]) != 0)
            {
                References[i]--;
            }
        }

        _leases--;
        QuitIfUnused();
    }

    private static void QuitIfUnused()
    {
        if (_leases == 0)
        {
            if (_ownsGlobalShutdown && Sdl.WasInit(0) == 0)
            {
                Sdl.Quit();
            }

            _ownsGlobalShutdown = false;
        }
    }
}
