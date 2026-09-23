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

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw.Utilities;

/// <summary>
/// A class to manage context related functionality.
/// </summary>
public static class GlfwContextUtilities
{
    /// <summary>
    /// Sets the swap interval of the current context.
    /// </summary>
    /// <param name="interval">The minimum number of screen updates to wait for until the buffers are swapped by <see cref="GlfwWindow.SwapBuffers"/>.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <remarks>This does not apply to Vulkan.</remarks>
    public static void SetSwapInterval(int interval)
    {
        Native.Glfw.SwapInterval(interval);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets whether a given extension is available.
    /// </summary>
    /// <param name="extension">A <see cref="string"/> with the extension to check.</param>
    /// <returns><see langword="true"/> if the <paramref name="extension"/> is supported, <see langword="false"/> otherwise.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <remarks>This function does not apply to Vulkan.</remarks>
    public static bool ExtensionSupported(string extension)
    {
        var result = Native.Glfw.ExtensionSupported(extension);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gest whether the Vulkan loader and ICD have been found.
    /// </summary>
    /// <returns><see langword="true"/> if Vulkan is supported, <see langword="false"/> otherwise.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static bool IsVulkanSupported()
    {
        var result = Native.Glfw.VulkanSupported();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gets the Vulkan instance extensions required by GLFW.
    /// </summary>
    /// <returns>A new <see cref="IReadOnlyCollection{T}"/> of <see cref="string"/> with the required extensions.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static unsafe IReadOnlyCollection<string> GetRequiredVulkanInstanceExtensions()
    {
        var ptr = Native.Glfw.GetRequiredInstanceExtensions(out var count);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        if (ptr is null)
        {
            return [];
        }

        var strings = new string[count];
        for (var i = 0U; i < count; i++)
        {
            strings[i] = Utf8StringMarshaller.ConvertToManaged(ptr[i]) ?? string.Empty;
        }

        return strings;
    }
}
