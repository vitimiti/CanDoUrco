// This is a direct mode, unsafe import of the SDL3 library.
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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CanDoUrco.Native.Sdl3;

[SuppressMessage(
    "Interoperability",
    "CA1401:P/Invokes should not be visible",
    Justification = "SDL3 P/Invokes are intentionally public for direct access"
)]
public static partial class Sdl
{
    private const string DllName = "SDL3";

    static Sdl() => NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, Resolver);

    private static nint Resolver(
        string libraryName,
        Assembly assembly,
        DllImportSearchPath? searchPath
    )
    {
        var path = Path.Combine(AppContext.BaseDirectory, "runtimes");
        string os;
        if (OperatingSystem.IsWindows())
        {
            os = "win";
        }
        else if (OperatingSystem.IsLinux())
        {
            os = "linux";
        }
        else if (OperatingSystem.IsMacOS())
        {
            os = "macos";
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        path = Path.Combine(
            path,
            os,
            RuntimeInformation.OSArchitecture switch
            {
                Architecture.X86 when OperatingSystem.IsWindows() || OperatingSystem.IsLinux() =>
                    "x86",
                Architecture.X64 => "x64",
                Architecture.Arm64 => "arm64",
                _ => throw new PlatformNotSupportedException(),
            }
        );

        string dllName;
        if (OperatingSystem.IsWindows())
        {
            dllName = "SDL3.dll";
        }
        else if (OperatingSystem.IsLinux())
        {
            dllName = "libSDL3.so.0";
        }
        else if (OperatingSystem.IsMacOS())
        {
            dllName = "libSDL3.0.dylib";
        }
        else
        {
            throw new PlatformNotSupportedException();
        }

        return NativeLibrary.Load(Path.Combine(path, dllName), assembly, searchPath);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Boolean
    {
        public sbyte Value;

        public static implicit operator bool(Boolean value) => value.Value != 0;

        public static implicit operator Boolean(bool value) =>
            new() { Value = (sbyte)(value ? 1 : 0) };
    }
}
