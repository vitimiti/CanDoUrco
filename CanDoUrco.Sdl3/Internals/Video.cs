// An SDL3 interop library with dotnet safety and idioms.
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace CanDoUrco.Sdl3.Internals;

internal static partial class Sdl
{
    public const ulong WindowFullscreen = 0x0000_0000_0000_0001UL;
    public const ulong WindowOpenGL = 0x0000_0000_0000_0002UL;
    public const ulong WindowResizable = 0x0000_0000_0000_0020UL;
    public const ulong WindowMouseGrabbed = 0x0000_0000_0000_0100UL;
    public const ulong WindowInputFocus = 0x0000_0000_0000_0200UL;
    public const ulong WindowMouseFocus = 0x0000_0000_0000_0400UL;
    public const ulong WindowHighPixelDensity = 0x0000_0000_0002_0000UL;
    public const ulong WindowMouseCapture = 0x0000_0000_0004_0000UL;
    public const ulong WindowMouseRelativeMode = 0x0000_0000_0008_0000UL;
    public const ulong WindowKeyboardGrabbed = 0x0000_0000_0010_0000UL;
    public const ulong WindowVulkan = 0x0000_0000_1000_0000UL;
    public const ulong WindowMetal = 0x0000_0000_2000_0000UL;

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint CreateWindow(string title, int w, int h, ulong flags);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(nint window);
}
