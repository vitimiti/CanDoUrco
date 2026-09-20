// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
