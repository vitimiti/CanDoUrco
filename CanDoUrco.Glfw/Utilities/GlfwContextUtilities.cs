// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
