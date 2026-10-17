// A common framework for the projects and engines of Can Do Urco
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

using System.Diagnostics;

namespace CanDoUrco.Framework.Time;

public sealed class GameTime
{
    private readonly Stopwatch _stopwatch = new();

    private TimeSpan _previousElapsed = TimeSpan.Zero;

    public TimeSpan Elapsed => _stopwatch.Elapsed;

    public TimeSpan DeltaTime { get; private set; } = TimeSpan.Zero;

    public double Delta => DeltaTime.TotalSeconds;

    public int? TargetFramesPerSecond
    {
        get;
        set
        {
            if (value.HasValue)
            {
                ArgumentOutOfRangeException.ThrowIfNegativeOrZero(value.Value, nameof(value));
            }

            field = value;
        }
    }

    public void Start() => _stopwatch.Start();

    public void Stop() => _stopwatch.Stop();

    public void Reset()
    {
        _stopwatch.Reset();
        _previousElapsed = TimeSpan.Zero;
        DeltaTime = TimeSpan.Zero;
    }

    public void Update()
    {
        if (_stopwatch.IsRunning && TargetFramesPerSecond is int targetFramesPerSecond)
        {
            var targetFrameDuration = TimeSpan.FromSeconds(1D / targetFramesPerSecond);
            var elapsedSinceLastUpdate = _stopwatch.Elapsed - _previousElapsed;
            var remainingFrameTime = targetFrameDuration - elapsedSinceLastUpdate;
            if (remainingFrameTime > TimeSpan.Zero)
            {
                Thread.Sleep(remainingFrameTime);
            }
        }

        var elapsed = _stopwatch.Elapsed;
        DeltaTime = elapsed - _previousElapsed;
        _previousElapsed = elapsed;
    }
}
