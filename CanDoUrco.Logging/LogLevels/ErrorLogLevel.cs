// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using Microsoft.Extensions.Logging;

namespace CanDoUrco.Logging.LogLevels;

public readonly struct ErrorLogLevel : ILogLevel
{
    public LogLevel Level => LogLevel.Error;
}
