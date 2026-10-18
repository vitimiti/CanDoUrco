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
using CanDoUrco.Framework.Native.CustomMarshallers;
using CanDoUrco.Framework.Video;
using CanDoUrco.NativeLibraryLoaderConfigurer;

namespace CanDoUrco.Framework.Native.SDL3;

internal static partial class Sdl
{
    public sealed class WindowHandle : SafeHandle
    {
        public override bool IsInvalid => handle == 0;

        public WindowHandle()
            : base(invalidHandleValue: 0, ownsHandle: true) => SetHandle(0);

        protected override bool ReleaseHandle()
        {
            Sdl.DestroyWindow(handle);
            return true;
        }
    }

    public sealed class GLContextHandle : SafeHandle
    {
        public override bool IsInvalid => handle == 0;

        public GLContextHandle()
            : base(invalidHandleValue: 0, ownsHandle: true) => SetHandle(0);

        protected override bool ReleaseHandle()
        {
            Sdl.GLDestroyContext(handle);
            return true;
        }
    }

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
    [LibraryImport(
        CommonNativeLibraryLoader.Sdl3,
        EntryPoint = "SDL_CreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial WindowHandle CreateWindow(string title, int w, int h, ulong flags);

    [SuppressGCTransition]
    [LibraryImport(
        CommonNativeLibraryLoader.Sdl3,
        EntryPoint = "SDL_SetWindowTitle",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowTitle(WindowHandle window, string title);

    [SuppressGCTransition]
    [LibraryImport(
        CommonNativeLibraryLoader.Sdl3,
        EntryPoint = "SDL_GetWindowTitle",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial string? GetWindowTitle(WindowHandle window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowMouseGrab(
        WindowHandle window,
        [MarshalAs(UnmanagedType.I1)] bool grabbed
    );

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool FlashWindow(WindowHandle window, FlashOperation operation);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowProgressState(
        WindowHandle window,
        WindowProgressState state
    );

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_SetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetWindowProgressValue(WindowHandle window, float value);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(nint window);

    [SuppressGCTransition]
    [LibraryImport(
        CommonNativeLibraryLoader.Sdl3,
        EntryPoint = "SDL_GL_GetProcAddress",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GLGetProcAddress(string proc);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLSetAttribute(GLAttr attr, int value);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GLContextHandle GLCreateContext(WindowHandle window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_MakeCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLMakeCurrent(WindowHandle window, GLContextHandle context);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLSetSwapInterval(int interval);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_SwapWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GLSwapWindow(WindowHandle window);

    [SuppressGCTransition]
    [LibraryImport(CommonNativeLibraryLoader.Sdl3, EntryPoint = "SDL_GL_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GLDestroyContext(nint context);
}
