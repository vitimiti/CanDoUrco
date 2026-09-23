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
