// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// The event arguments for the monitor events.
/// </summary>
/// <param name="event">One from <see cref="GlfwMonitorConnectionEvent"/>.</param>
public sealed class GlfwMonitorConnectionEventArgs(GlfwMonitorConnectionEvent @event) : EventArgs
{
    /// <summary>
    /// Gets the actual <see cref="GlfwMonitorConnectionEvent"/>.
    /// </summary>
    public GlfwMonitorConnectionEvent Event { get; init; } = @event;
}
