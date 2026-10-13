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

using System.Collections.Concurrent;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Sdl3.Exceptions;

namespace CanDoUrco.Sdl3.Internals;

internal static class SdlMainThreadCleanup
{
    private static readonly ConcurrentQueue<Action> Pending = new();
    private static readonly Timer Retry = new(
        static _ => Schedule(),
        null,
        Timeout.Infinite,
        Timeout.Infinite
    );
    private static int _scheduled;

    internal static void Enqueue(Action cleanup)
    {
        Pending.Enqueue(cleanup);
        Schedule();
    }

    private static unsafe void Schedule()
    {
        if (Pending.IsEmpty || Interlocked.CompareExchange(ref _scheduled, 1, 0) != 0)
        {
            return;
        }

        try
        {
            if (Sdl.RunOnMainThread(&ProcessPending, 0, waitComplete: false))
            {
                return;
            }

            throw new Sdl3Exception("Failed to schedule SDL resource cleanup");
        }
        catch (Exception error)
        {
            Trace.TraceError("SDL cleanup dispatch failed; retained for retry: {0}", error);
            Volatile.Write(ref _scheduled, 0);
            Retry.Change(100, Timeout.Infinite);
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void ProcessPending(nint userdata)
    {
        try
        {
            SdlSubsystemLifetime.EnsureMainThread();
            var count = Pending.Count;
            for (var i = 0; i < count && Pending.TryDequeue(out var cleanup); i++)
            {
                try
                {
                    cleanup();
                }
                catch (Exception error)
                {
                    Pending.Enqueue(cleanup);
                    Trace.TraceError(
                        "SDL resource cleanup failed; retained for retry: {0}",
                        error
                    );
                }
            }
        }
        catch (Exception error)
        {
            // Exceptions cannot cross SDL's native callback boundary.
            Trace.TraceError("SDL cleanup callback failed: {0}", error);
        }
        finally
        {
            Volatile.Write(ref _scheduled, 0);
            if (!Pending.IsEmpty)
            {
                Retry.Change(100, Timeout.Infinite);
            }
        }
    }
}
