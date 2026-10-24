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

using System.Reflection;
using System.Runtime.InteropServices;

namespace CanDoUrco.Native.Sdl3;

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

        var rid = RuntimeInformation.RuntimeIdentifier;
        var path = Path.Combine("runtimes", rid, "native", DllName);
        return NativeLibrary.TryLoad(path, out var handle) ? handle : 0;
    }
}
