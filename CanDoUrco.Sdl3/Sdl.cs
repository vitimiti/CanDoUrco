// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    private const string DllName = "SDL3";

    internal const string CustomLogCategory = "CanDoUrco.Sdl";

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
