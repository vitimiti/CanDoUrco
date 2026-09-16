// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Runtime.InteropServices;

namespace CanDoUrco.Glfw.Native;

internal static unsafe class Glfw
{
    private const string DllName = "glfw";

    static Glfw() =>
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), ResolveGlfwLibrary);

    private static nint ResolveGlfwLibrary(
        string libraryName,
        Assembly assembly,
        DllImportSearchPath? searchPath
    )
    {
        if (libraryName != DllName)
        {
            return 0;
        }

        if (OperatingSystem.IsWindows())
        {
            if (NativeLibrary.TryLoad("glfw3.dll", assembly, searchPath, out var handle))
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsMacOS())
        {
            if (NativeLibrary.TryLoad("libglfw.3.dylib", assembly, searchPath, out var handle))
            {
                return handle;
            }

            if (NativeLibrary.TryLoad("libglfw.dylib", assembly, searchPath, out handle))
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsLinux())
        {
            if (NativeLibrary.TryLoad("libglfw.so.3", assembly, searchPath, out var handle))
            {
                return handle;
            }

            if (NativeLibrary.TryLoad("libglfw.so", assembly, searchPath, out handle))
            {
                return handle;
            }
        }

        return 0;
    }
}
