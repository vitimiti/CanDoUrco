// A common framework for the projects and engines of Can Do Urco
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
using CanDoUrco.NativeLibraryLoaderConfigurer;

namespace CanDoUrco.Framework.Native.SDL3;

internal static partial class Sdl
{
    public enum EventType : uint
    {
        Quit = 0x100,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
    }

    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct Event
    {
        [FieldOffset(0)]
        public EventType Type;

        [FieldOffset(0)]
        public QuitEvent Quit;
    }

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool PollEvent(out Event @event);
}
