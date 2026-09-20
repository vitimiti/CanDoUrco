// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// The event arguments for the joystick events.
/// </summary>
/// <param name="event">One from <see cref="GlfwJoystickConnectionEvent"/>.</param>
public sealed class GlfwJoystickConnectionEventArgs(GlfwJoystickConnectionEvent @event) : EventArgs
{
    /// <summary>
    /// Gets the actual <see cref="GlfwJoystickConnectionEvent"/>.
    /// </summary>
    public GlfwJoystickConnectionEvent Event { get; init; } = @event;
}
