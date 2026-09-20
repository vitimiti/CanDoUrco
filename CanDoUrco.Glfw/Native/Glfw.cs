// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Runtime.InteropServices;

namespace CanDoUrco.Glfw.Native;

internal static partial class Glfw
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

        string platform;
        if (OperatingSystem.IsWindows())
        {
            platform = "win";
        }
        else if (OperatingSystem.IsMacOS())
        {
            platform = "osx";
        }
        else if (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
        {
            platform = "linux";
        }
        else
        {
            return 0;
        }

        var cpuid = RuntimeInformation.ProcessArchitecture switch
        {
            Architecture.X86 => "x86",
            Architecture.X64 => "x64",
            Architecture.Arm64 => "arm64",
            _ => null,
        };

        if (cpuid is null)
        {
            return 0;
        }

        var basePath = Path.Combine(AppContext.BaseDirectory, "runtimes", $"{platform}-{cpuid}");
        if (OperatingSystem.IsWindows())
        {
            if (
                NativeLibrary.TryLoad(
                    Path.Combine(basePath, "glfw3.dll"),
                    assembly,
                    searchPath,
                    out var handle
                )
            )
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsMacOS())
        {
            if (
                NativeLibrary.TryLoad(
                    Path.Combine(basePath, "libglfw.3.dylib"),
                    assembly,
                    searchPath,
                    out var handle
                )
            )
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsLinux())
        {
            if (
                NativeLibrary.TryLoad(
                    Path.Combine(basePath, "libglfw.so.3"),
                    assembly,
                    searchPath,
                    out var handle
                )
            )
            {
                return handle;
            }
        }

        return 0;
    }
}
