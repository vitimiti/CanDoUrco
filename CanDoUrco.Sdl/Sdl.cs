// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
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

using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl;

public static partial class Sdl
{
    private const string DllName = "SDL3";
    private const string CustomLogCategory = "CanDoUrco.Sdl";

    static Sdl() =>
        NativeLibrary.SetDllImportResolver(
            typeof(Sdl).Assembly,
            (name, assembly, path) =>
                NativeLibrary.Load(
                    name switch
                    {
                        DllName when OperatingSystem.IsWindows() => "SDL3.dll",
                        DllName when OperatingSystem.IsLinux() => "libSDL3.so.0",
                        DllName when OperatingSystem.IsMacOS() => "libSDL3.0.dylib",
                        _ => name,
                    },
                    assembly,
                    path
                )
        );
}
