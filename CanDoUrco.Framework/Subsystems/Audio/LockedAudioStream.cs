// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Logging;

namespace CanDoUrco.Framework.Subsystems.Audio;

public sealed class LockedAudioStream : IDisposable
{
    public AudioStream Value { get; }
    private bool _disposed;

    internal LockedAudioStream(AudioStream value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        Value = value;
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (_disposed)
        {
            return;
        }

        Value.Unlock();
        _disposed = true;
        GC.SuppressFinalize(this);
    }
}
