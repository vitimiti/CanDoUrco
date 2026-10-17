// A common native library loader for the CanDoUrco project.
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

namespace CanDoUrco.NativeLibraryLoaderConfigurer;

public static class CommonNativeLibraryLoader
{
    public const string Sdl3 = "SDL3";

    public static void Set()
    {
        NativeLibrary.SetDllImportResolver(
            typeof(CommonNativeLibraryLoader).Assembly,
            static (libraryName, assembly, searchPath) =>
            {
                var path = "runtimes";
                if (OperatingSystem.IsWindows())
                {
                    path = Path.Combine(path, "win-");
                }
                else if (OperatingSystem.IsMacOS())
                {
                    path = Path.Combine(path, "osx-");
                }
                else if (OperatingSystem.IsLinux())
                {
                    path = Path.Combine(path, "linux-");
                }
                else
                {
                    throw new PlatformNotSupportedException();
                }

                path = Path.Combine(
                    path,
                    RuntimeInformation.ProcessArchitecture switch
                    {
                        Architecture.X64 => "x64",
                        Architecture.X86 => OperatingSystem.IsWindows()
                            ? "x86"
                            : throw new PlatformNotSupportedException(),
                        Architecture.Arm64 => OperatingSystem.IsMacOS()
                            ? "arm64"
                            : throw new PlatformNotSupportedException(),
                        _ => throw new PlatformNotSupportedException(),
                    }
                );

                switch (libraryName)
                {
                    case Sdl3:
                    {
                        if (OperatingSystem.IsWindows())
                        {
                            return NativeLibrary.Load(Path.Combine(path, "SDL3.dll"));
                        }
                        else if (OperatingSystem.IsMacOS())
                        {
                            return NativeLibrary.Load(Path.Combine(path, "libSDL3.0.dylib"));
                        }
                        else if (OperatingSystem.IsLinux())
                        {
                            return NativeLibrary.Load(Path.Combine(path, "libSDL3.so.0"));
                        }

                        throw new PlatformNotSupportedException();
                    }
                    default:
                        throw new InvalidDataException($"Unsupported library: {libraryName}.");
                }
            }
        );
    }
}
