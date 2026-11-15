// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems;

public sealed class HapticSubsystem : SubsystemBase
{
    public HapticSubsystem(AppContext app)
        : base(app, Sdl.InitHaptic) => LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
}
