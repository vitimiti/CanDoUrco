// Common utilities for OpenGL importing and generation.
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

namespace CanDoUrco.OpenGl.Common.Utilities;

public static class Loader
{
    public static T GetDelegate<T>(string functionName)
        where T : Delegate
    {
        foreach (var libraryName in GetLibraryNames())
        {
            if (NativeLibrary.TryLoad(libraryName, out var handle))
            {
                if (NativeLibrary.TryGetExport(handle, functionName, out var functionPointer))
                {
                    return Marshal.GetDelegateForFunctionPointer<T>(functionPointer);
                }
            }
        }

        throw new EntryPointNotFoundException(
            $"Function '{functionName}' not found in any of the OpenGL libraries."
        );
    }

    private static IEnumerable<string> GetLibraryNames()
    {
        if (OperatingSystem.IsWindows())
        {
            yield return "opengl32.dll";
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
        else if (OperatingSystem.IsMacOS())
        {
            yield return "/System/Library/Frameworks/OpenGL.framework/OpenGL";
        }
        else
        {
            throw new PlatformNotSupportedException(
                $"Unsupported operating system '{RuntimeInformation.OSDescription}' for OpenGL library loading."
            );
        }
    }
}
