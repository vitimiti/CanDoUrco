// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Text;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Properties;

public sealed class AudioStreamProperties : PropertyGroup
{
    public bool AutoCleanup
    {
        get
        {
            var name = Encoding.UTF8.GetString(Sdl.PropAudioStreamAutoCleanupBoolean[..^1]);
            return GetBooleanProperty(name, defaultValue: true);
        }
        set
        {
            var name = Encoding.UTF8.GetString(Sdl.PropAudioStreamAutoCleanupBoolean[..^1]);
            SetProperty(name, value);
        }
    }

    internal AudioStreamProperties(Sdl.PropertiesID props)
        : base(props, ownsProperties: false)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
    }
}
