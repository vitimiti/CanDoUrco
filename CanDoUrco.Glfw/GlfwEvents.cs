// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw;

/// <summary>
/// Manage the GLFW events.
/// </summary>
public static class GlfwEvents
{
    /// <summary>
    /// Processes all pending events.
    /// </summary>
    public static void Poll() => Native.Glfw.PollEvents();

    /// <summary>
    /// Waits until events are queued and processes them.
    /// </summary>
    public static void WaitEvents() => Native.Glfw.WaitEvents();

    /// <summary>
    /// Waits with timeout until events are queued and processes them.
    /// </summary>
    /// <param name="timeout"></param>
    public static void WaitEvents(TimeSpan timeout) =>
        Native.Glfw.WaitEventsTimeout(timeout.TotalSeconds);

    /// <summary>
    /// Posts an empty event to the event queue.
    /// </summary>
    public static void PostEmpty() => Native.Glfw.PostEmptyEvent();
}
