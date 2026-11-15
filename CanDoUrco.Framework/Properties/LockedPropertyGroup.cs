// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Properties;

public sealed class LockedPropertyGroup : IDisposable
{
    public PropertyGroup Value { get; }

    internal LockedPropertyGroup(PropertyGroup value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(value);
        Value = value;
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        Sdl.UnlockProperties(Value.Id);
        GC.SuppressFinalize(this);
    }
}
