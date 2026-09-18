// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// The possible monitor events.
/// </summary>
public enum GlfwMonitorConnectionEvent
{
    /// <summary>
    /// The monitor has been connected.
    /// </summary>
    Connected = Native.Glfw.ConnectedDefine,

    /// <summary>
    /// The monitor has been disconnected.
    /// </summary>
    Disconnected = Native.Glfw.DisconnectedDefine,
}
