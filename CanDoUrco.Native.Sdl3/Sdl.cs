// CanDoUrco.Native.Sdl3 - Provides native SDL3 bindings for CanDoUrco
// Copyright (C) <year>  <name of author>
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

namespace CanDoUrco.Native.Sdl3;

public partial class Sdl
{
    private const string DllName = "SDL3";

    static Sdl() => NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, ImportResolver);

    private static nint ImportResolver(string name, Assembly assembly, DllImportSearchPath? path)
    {
        foreach (var dllName in GetDllNames())
        {
            if (NativeLibrary.TryLoad(dllName, assembly, path, out var handle))
            {
                return handle;
            }
        }

        return 0;
    }

    private static IEnumerable<string> GetDllNames()
    {
        if (OperatingSystem.IsWindows())
        {
            yield return "SDL3.dll";
            yield return Path.Combine(
                AppContext.BaseDirectory,
                "runtimes",
                RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X86 => "win-x86",
                    Architecture.X64 => "win-x64",
                    Architecture.Arm64 => "win-arm64",
                    _ => throw new PlatformNotSupportedException(),
                },
                "native",
                "SDL3.dll"
            );
        }
        else if (OperatingSystem.IsLinux())
        {
            yield return "libSDL3.so.0";
            yield return Path.Combine(
                AppContext.BaseDirectory,
                "runtimes",
                RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X86 => "linux-x86",
                    Architecture.X64 => "linux-x64",
                    Architecture.Arm64 => "linux-arm64",
                    _ => throw new PlatformNotSupportedException(),
                },
                "native",
                "libSDL3.so.0"
            );
        }
        else if (OperatingSystem.IsMacOS())
        {
            yield return "libSDL3.0.dylib";
            yield return Path.Combine(
                AppContext.BaseDirectory,
                "runtimes",
                RuntimeInformation.OSArchitecture switch
                {
                    Architecture.X64 => "osx-x64",
                    Architecture.Arm64 => "osx-arm64",
                    _ => throw new PlatformNotSupportedException(),
                },
                "native",
                "libSDL3.0.dylib"
            );
        }
        else
        {
            throw new PlatformNotSupportedException();
        }
    }
}
