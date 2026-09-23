// This is a small testing ground for CanDoUrco.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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
