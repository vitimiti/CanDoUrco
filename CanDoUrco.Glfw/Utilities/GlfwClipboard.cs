// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;

namespace CanDoUrco.Glfw.Utilities;

/// <summary>
/// A class to manage the global clipboard.
/// </summary>
public static class GlfwClipboard
{
    /// <summary>
    /// Sets the global clipboard to the specified string.
    /// </summary>
    /// <param name="string">A <see cref="string"/> to set the clipboard to.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static unsafe void SetString(string @string)
    {
        Native.Glfw.SetClipboardString(null, @string);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the global clipboard string.
    /// </summary>
    /// <returns>A new <see cref="string"/> with the clipboard text.</returns>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public static unsafe string GetString()
    {
        var ptr = Native.Glfw.GetClipboardString(null);
        if (ptr is null)
        {
            GlfwErrorUtilities.ThrowError();
        }

        return Utf8StringMarshaller.ConvertToManaged(ptr) ?? string.Empty;
    }
}
