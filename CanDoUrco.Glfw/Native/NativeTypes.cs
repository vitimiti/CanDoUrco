// This is a small testing ground for CanDoUrco.
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

namespace CanDoUrco.Glfw.Native;

internal static unsafe partial class Glfw
{
    [StructLayout(LayoutKind.Sequential)]
    public struct VidMode
    {
        public int Width;
        public int Height;
        public int RedBits;
        public int GreenBits;
        public int BlueBits;
        public int RefreshRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GammaRamp
    {
        public ushort* Red;
        public ushort* Green;
        public ushort* Blue;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Image
    {
        public int Width;
        public int Height;
        public byte* Pixels;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadState
    {
        public fixed byte Buttons[15];
        public fixed float Axes[6];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Allocator
    {
        public delegate* unmanaged[Cdecl]<nuint, void*, void*> Allocate;
        public delegate* unmanaged[Cdecl]<void*, nuint, void*, void*> Reallocate;
        public delegate* unmanaged[Cdecl]<void*, void*, void> Deallocate;
        public void* User;
    }
}
