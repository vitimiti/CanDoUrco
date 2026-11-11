// CanDoUrco.Playground - A playground project for experimenting with the CanDoUrco code.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Logging;
using Microsoft.Extensions.Logging;

const string CustomLogCategory = "CanDoUrco.Playground";

CoreLog.Initialize(loggingBuilder =>
{
    loggingBuilder.AddConsole();
#if DEBUG
    loggingBuilder.SetMinimumLevel(LogLevel.Trace);
#else
    loggingBuilder.SetMinimumLevel(LogLevel.Information);
#endif
});

LogCategory.MakeOrGet(CustomLogCategory).Info("CanDoUrco.Playground logging initialized.");
