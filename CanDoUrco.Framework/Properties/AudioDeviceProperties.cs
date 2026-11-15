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

public sealed class AudioDeviceProperties : PropertyGroup
{
    public string? ContainerId
    {
        get
        {
            var name = Encoding.UTF8.GetString(Sdl.PropAudioDeviceContainerIdString[..^1]);
            return GetStringProperty(name);
        }
    }

    public string? UniqueId
    {
        get
        {
            var name = Encoding.UTF8.GetString(Sdl.PropAudioDeviceUniqueIdString[..^1]);
            return GetStringProperty(name);
        }
    }

    internal AudioDeviceProperties(Sdl.PropertiesID props)
        : base(props, ownsProperties: false)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
    }
}
