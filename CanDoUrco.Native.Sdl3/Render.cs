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
    public enum RendererLogicalPresentation
    {
        Letterbox = 2,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Renderer;

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_CreateRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Renderer* CreateRenderer(Window* window, byte* name);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderLogicalPresentation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean SetRenderLogicalPresentation(
        Renderer* renderer,
        int w,
        int h,
        RendererLogicalPresentation presentation
    );

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_SetRenderDrawColor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean SetRenderDrawColor(
        Renderer* renderer,
        byte r,
        byte g,
        byte b,
        byte a
    );

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_RenderClear")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean RenderClear(Renderer* renderer);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_RenderPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean RenderPoints(Renderer* renderer, FPoint* points, int count);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_RenderPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Boolean RenderPresent(Renderer* renderer);

    [SuppressGCTransition]
    [LibraryImport(DllName, EntryPoint = "SDL_DestroyRenderer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyRenderer(Renderer* renderer);
}
