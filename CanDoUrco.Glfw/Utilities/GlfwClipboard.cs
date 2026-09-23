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
