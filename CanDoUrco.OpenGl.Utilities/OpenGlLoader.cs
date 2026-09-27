// Common utilities for OpenGL importing.
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

using System.Runtime.InteropServices;

namespace CanDoUrco.OpenGl.Utilities;

public static class OpenGlLoader
{
    public static T LoadMethod<T>(string methodName)
        where T : Delegate
    {
        ArgumentNullException.ThrowIfNull(methodName);
        foreach (var library in EnumerateLibraries())
        {
            if (!NativeLibrary.TryLoad(library, out var handle))
            {
                continue;
            }

            if (!NativeLibrary.TryGetExport(handle, methodName, out var functionPointer))
            {
                continue;
            }

            return Marshal.GetDelegateForFunctionPointer<T>(functionPointer);
        }

        throw new EntryPointNotFoundException(
            $"Unable to load method '{methodName}' from any known OpenGL library."
        );
    }

    private static IEnumerable<string> EnumerateLibraries()
    {
        if (OperatingSystem.IsWindows())
        {
            yield return "opengl32.dll";
        }
        else if (OperatingSystem.IsMacOS())
        {
            yield return "/System/Library/Frameworks/OpenGL.framework/OpenGL";
        }
        else if (OperatingSystem.IsLinux())
        {
            yield return "libOpenGL.so.0";
            yield return "libOpenGL.so";
            yield return "libEGL.so.1";
            yield return "libEGL.so";
            yield return "libGL.so.1";
            yield return "libGL.so";
        }
        else
        {
            throw new PlatformNotSupportedException(RuntimeInformation.OSDescription);
        }
    }
}
