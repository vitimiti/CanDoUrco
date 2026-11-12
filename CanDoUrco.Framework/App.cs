// CanDoUrco.Framework - A framework for building applications with rendering support.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Framework.Options;
using CanDoUrco.Logging;

namespace CanDoUrco.Framework;

public sealed class App
{
    internal const string CustomLogCategory = "CanDoUrco.Framework";

    private readonly AppOptions _options;

    public App(Action<AppOptions>? configure = null)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var options = new AppOptions();
        configure?.Invoke(options);

        _options = options;
        LogCategory.MakeOrGet(CustomLogCategory).Debug($"App initialized with options: {_options}");
    }
}
