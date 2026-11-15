// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

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
}
