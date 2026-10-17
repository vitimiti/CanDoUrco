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
using CanDoUrco.Framework.Video;
using CanDoUrco.NativeLibraryLoaderConfigurer;

namespace CanDoUrco.Framework.Native.SDL3;

internal static unsafe partial class Sdl
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Window;

    [StructLayout(LayoutKind.Sequential)]
    public struct GLContext;

    public const ulong WindowFullscreen = 0x0000_0000_0000_0001UL;
    public const ulong WindowOpengl = 0x0000_0000_0000_0002UL;
    public const ulong WindowResizable = 0x0000_0000_0000_0020UL;
    public const ulong WindowMaximized = 0x0000_0000_0000_0080UL;
    public const ulong WindowHighPixelDensity = 0x0000_0000_0000_2000UL;
    public const ulong WindowMouseCapture = 0x0000_0000_0000_4000UL;

    public enum GLAttr
    {
        ContextMajorVersion = 17,
        ContextMinorVersion,
        ContextProfileMask = 20,
    }

    public enum FlashOperation
    {
        UntilFocused = 2,
    }

    public const uint GLContextProfileCore = 0x0001U;

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_CreateWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* CreateWindow(byte* title, int w, int h, ulong flags);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowTitle(Window* window, byte* title);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetWindowTitle(Window* window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowMouseGrab(
        Window* window,
        [MarshalAs(UnmanagedType.I1)] bool grabbed
    );

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FlashWindow(Window* window, FlashOperation operation);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowProgressState(Window* window, WindowProgressState state);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowProgressValue(Window* window, float value);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(Window* window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_GetProcAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* GLGetProcAddress(byte* proc);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLSetAttribute(GLAttr attr, int value);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GLContext* GLCreateContext(Window* window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_MakeCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLMakeCurrent(Window* window, GLContext* context);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLSetSwapInterval(int interval);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SwapWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GLSwapWindow(Window* window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLDestroyContext(GLContext* context);
}
