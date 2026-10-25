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

public static unsafe partial class Sdl
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Window;

    [StructLayout(LayoutKind.Sequential)]
    public readonly struct WindowFlags
    {
        private readonly ulong _value;

        public static readonly WindowFlags Fullscreen = new(0x0000_0000_0000_0001UL);

        private WindowFlags(ulong value) => _value = value;

        public static implicit operator ulong(WindowFlags value) => value._value;

        public static implicit operator WindowFlags(ulong value) => new(value);
    }

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_CreateWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* CreateWindow(byte* title, int w, int h, WindowFlags flags);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(Window* window);
}
