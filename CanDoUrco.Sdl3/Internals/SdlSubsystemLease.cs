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

namespace CanDoUrco.Sdl3.Internals;

internal sealed class SdlSubsystemLease : IDisposable
{
    private readonly ReleaseState _state;

    private SdlSubsystemLease(SdlSubsystem subsystems, Action? releaseResource)
    {
        _state = new ReleaseState(subsystems, releaseResource);
    }

    public static SdlSubsystemLease Acquire(SdlSubsystem subsystems, Action? releaseResource = null)
    {
        var lease = new SdlSubsystemLease(subsystems, releaseResource);
        SdlSubsystemLifetime.Acquire(subsystems);
        lease._state.Acquired = true;
        return lease;
    }

    ~SdlSubsystemLease()
    {
        if (_state is { Acquired: true })
        {
            SdlMainThreadCleanup.Enqueue(_state.Release);
        }
    }

    public void Dispose()
    {
        _state.Release();
        GC.SuppressFinalize(this);
    }

    private sealed class ReleaseState(SdlSubsystem subsystems, Action? releaseResource)
    {
        public bool Acquired { get; set; }

        public void Release()
        {
            if (!Acquired)
            {
                return;
            }

            SdlSubsystemLifetime.EnsureMainThread();
            releaseResource?.Invoke();
            SdlSubsystemLifetime.Release(subsystems);
            Acquired = false;
        }
    }
}
