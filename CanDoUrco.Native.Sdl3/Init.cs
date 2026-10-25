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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Native.Sdl3;

public static partial class Sdl
{
    [StructLayout(LayoutKind.Sequential)]
    public readonly struct InitFlags
    {
        private readonly uint _value;

        public static readonly InitFlags Video = new(0x0000_0020U);

        private InitFlags(uint value) => _value = value;

        public static implicit operator uint(InitFlags value) => value._value;

        public static implicit operator InitFlags(uint value) => new(value);
    }

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean Init(InitFlags flags);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Quit();
}
