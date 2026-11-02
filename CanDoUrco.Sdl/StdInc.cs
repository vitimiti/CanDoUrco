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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl;

public static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_malloc")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _malloc(nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_free")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _free(void* mem);

    public static unsafe void* Malloc(nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _malloc(size);
    }

    public static unsafe void Free(void* mem)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _free(mem);
    }
}
