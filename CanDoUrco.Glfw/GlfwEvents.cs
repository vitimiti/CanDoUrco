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

using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw;

/// <summary>
/// Manage the GLFW events.
/// </summary>
public static class GlfwEvents
{
    /// <summary>
    /// Processes all pending events.
    /// </summary>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static void Poll()
    {
        Native.Glfw.PollEvents();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Waits until events are queued and processes them.
    /// </summary>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static void WaitEvents()
    {
        Native.Glfw.WaitEvents();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Waits with timeout until events are queued and processes them.
    /// </summary>
    /// <param name="timeout">A <see cref="TimeSpan"/> with the time to wait for.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static void WaitEvents(TimeSpan timeout)
    {
        Native.Glfw.WaitEventsTimeout(timeout.TotalSeconds);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Posts an empty event to the event queue.
    /// </summary>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static void PostEmpty()
    {
        Native.Glfw.PostEmptyEvent();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }
}
