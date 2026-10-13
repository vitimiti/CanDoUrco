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

using CanDoUrco.Sdl3.Events;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3;

public sealed class EventPump : IDisposable
{
    private static bool _active;

    private readonly SdlSubsystemLease _lifetime;

    private bool _dispatching;
    private bool _disposedValue;

    public EventPump()
    {
        SdlSubsystemLifetime.EnsureMainThread();
        if (_active)
        {
            throw new InvalidOperationException("Only one SDL event pump may be active at a time.");
        }

        _lifetime = SdlSubsystemLease.Acquire(SdlSubsystem.Events, static () => _active = false);
        _active = true;
    }

    public void Dispose()
    {
        if (_disposedValue)
        {
            return;
        }

        SdlSubsystemLifetime.EnsureMainThread();

        if (_dispatching)
        {
            throw new InvalidOperationException("Cannot dispose the event pump during dispatch.");
        }

        _lifetime.Dispose();
        _disposedValue = true;
    }

    public int DispatchPending(Action<BaseEvent> handler)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(handler);
        SdlSubsystemLifetime.EnsureMainThread();

        if (_dispatching)
        {
            throw new InvalidOperationException("SDL event dispatch cannot be reentered.");
        }

        _dispatching = true;
        var count = 0;
        try
        {
            while (Sdl.PollEvent(out var @event))
            {
                var managed = Decode(@event);
                handler(managed);
                count++;
            }

            return count;
        }
        finally
        {
            _dispatching = false;
            GC.KeepAlive(_lifetime);
        }
    }

    private static BaseEvent Decode(Sdl.Event sdlEvent) =>
        sdlEvent.Type switch
        {
            EventType.Quit => new QuitEvent(ConvertTimestamp(sdlEvent.Quit.Timestamp)),
            _ => new UnknownEvent(sdlEvent.Type, ConvertTimestamp(sdlEvent.Common.Timestamp)),
        };

    private static TimeSpan ConvertTimestamp(ulong nanoseconds) =>
        TimeSpan.FromTicks((long)(nanoseconds / 100UL));
}
