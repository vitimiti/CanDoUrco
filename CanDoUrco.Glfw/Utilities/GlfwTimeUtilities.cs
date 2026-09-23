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
