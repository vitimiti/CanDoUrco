// An unsafe, direct binding to SDL3 without any marshalling.
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

using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.InteropServices;

namespace CanDoUrco.Native.Sdl3;

[SuppressMessage(
    "Interoperability",
    "CA1401:P/Invokes should not be visible",
    Justification = "Native SDL3 functions are intentionally exposed as public for direct binding."
)]
public static partial class Sdl
{
    private const string DllName = "SDL3";

    static Sdl() => NativeLibrary.SetDllImportResolver(typeof(Sdl).Assembly, NativeLibraryResolver);

    private static nint NativeLibraryResolver(
        string libraryName,
        Assembly assembly,
        DllImportSearchPath? searchPath
    )
    {
        if (libraryName != DllName)
        {
            return 0;
        }

        var pattern =
            OperatingSystem.IsWindows() ? "SDL3.dll"
            : OperatingSystem.IsLinux() ? "libSDL3.so*"
            : OperatingSystem.IsMacOS() ? "libSDL3*.dylib"
            : throw new PlatformNotSupportedException(
                "SDL3 native loading is not configured for this OS."
            );

        string? libraryPath = null;
        foreach (var path in Directory.EnumerateFiles(AppContext.BaseDirectory, pattern))
        {
            if ((File.GetAttributes(path) & FileAttributes.ReparsePoint) != 0)
            {
                continue;
            }

            if (libraryPath is not null)
            {
                throw new DllNotFoundException(
                    $"Multiple SDL3 libraries found in '{AppContext.BaseDirectory}'. Clean the output directory so it contains only the intended SDL3 library."
                );
            }

            libraryPath = path;
        }

        return libraryPath is null
            ? throw new DllNotFoundException(
                $"No SDL3 library matching '{pattern}' found in '{AppContext.BaseDirectory}'. Build SDL3 for the host or the selected RuntimeIdentifier."
            )
            : NativeLibrary.Load(libraryPath);
    }

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct Boolean
    {
        private readonly int _value;

        public static readonly Boolean False = new(0);
        public static readonly Boolean True = new(1);

        private Boolean(int value) => _value = value;

        public static implicit operator bool(Boolean value) => value._value != 0;

        public static implicit operator Boolean(bool value) => value ? True : False;
    }
}
