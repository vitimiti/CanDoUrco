// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CanDoUrco.Glfw.Exceptions;

namespace CanDoUrco.Glfw.Utilities;

/// <summary>
/// A class to manage time-related functionality.
/// </summary>
public static class GlfwTimeUtilities
{
    /// <summary>
    /// Gets the current GLFW time.
    /// </summary>
    /// <returns>A new <see cref="TimeSpan"/> with the current GLFW time.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static TimeSpan GetTime()
    {
        var value = TimeSpan.FromSeconds(Native.Glfw.GetTime());
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return value;
    }

    /// <summary>
    /// Sets the current GLFW time.
    /// </summary>
    /// <param name="time">A <see cref="TimeSpan"/> with the new GLFW time.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static void SetTime(TimeSpan time)
    {
        Native.Glfw.SetTime(time.TotalSeconds);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the current value of the raw timer.
    /// </summary>
    /// <returns>A new <see cref="ulong"/> with the raw timer value.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static ulong GetTimerValue()
    {
        var result = Native.Glfw.GetTimerValue();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gets the frequency, in hertz, of the raw timer.
    /// </summary>
    /// <returns>A new <see cref="ulong"/> with the frequency, in hertz, of the raw timer.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static ulong GetTimerFrequenc()
    {
        var result = Native.Glfw.GetTimerFrequency();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }
}
