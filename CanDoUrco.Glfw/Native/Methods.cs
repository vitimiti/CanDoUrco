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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace CanDoUrco.Glfw.Native;

internal static unsafe partial class Glfw
{
    [LibraryImport(DllName, EntryPoint = "glfwInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool Init();

    [LibraryImport(DllName, EntryPoint = "glfwTerminate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Terminate();

    [LibraryImport(DllName, EntryPoint = "glfwInitHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitHint(int hint, int value);

    [LibraryImport(DllName, EntryPoint = "glfwInitAllocator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitAllocator(in Allocator allocator);

    // TODO: Add the glfwInitVulkanLoader if Vulkan is ever added to CanDoUrco.

    [LibraryImport(DllName, EntryPoint = "glfwGetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetVersion(out int major, out int minor, out int rev);

    [LibraryImport(DllName, EntryPoint = "glfwGetVersionString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetVersionString();

    [LibraryImport(DllName, EntryPoint = "glfwGetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetError(out byte* error);

    // WARNING: glfwSetErrorCallback intentionally not included

    [LibraryImport(DllName, EntryPoint = "glfwGetPlatform")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPlatform();

    [LibraryImport(DllName, EntryPoint = "glfwPlatformSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool PlatformSupported(int platform);

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor** GetMonitors(out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetPrimaryMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor* GetPrimaryMonitor();

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorPosition(Monitor* monitor, out int xPos, out int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorWorkarea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorWorkArea(
        Monitor* monitor,
        out int xPos,
        out int yPos,
        out int width,
        out int height
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorPhysicalSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorPhysicalSize(
        Monitor* monitor,
        out int widthMillimeters,
        out int heightMillimeters
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorContentScale(
        Monitor* monitor,
        out float xScale,
        out float yScale
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetMonitorName(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetMonitorCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Monitor*, int, void> SetMonitorCallback(
        delegate* unmanaged[Cdecl]<Monitor*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial VidMode* GetVideoModes(Monitor* monitor, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial VidMode* GetVideoMode(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetGamma")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGamma(Monitor* monitor, float gamma);

    [LibraryImport(DllName, EntryPoint = "glfwGetGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GammaRamp* GetGammaRamp(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGammaRamp(Monitor* monitor, in GammaRamp ramp);

    [LibraryImport(DllName, EntryPoint = "glfwDefaultWindowHints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DefaultWindowHints();

    [LibraryImport(DllName, EntryPoint = "glfwWindowHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WindowHint(int hint, int value);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwWindowHintString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WindowHintString(int hint, string value);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwCreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* CreateWindow(
        int width,
        int height,
        string title,
        Monitor* monitor,
        Window* share
    );

    [LibraryImport(DllName, EntryPoint = "glfwDestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwWindowShouldClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool WindowShouldClose(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowShouldClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowShouldClose(
        Window* window,
        [MarshalAs(UnmanagedType.I4)] bool value
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetWindowTitle(Window* window);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwSetWindowTitle",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowTitle(Window* window, string? title);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowIcon(
        Window* window,
        int count,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Image, Image>), CountElementName = nameof(count))]
            Image[] images
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowPos(Window* window, out int xPos, out int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowPos(Window* window, int xPos, int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowSize(Window* window, out int width, out int height);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeLimits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowSizeLimits(
        Window* window,
        int minWidth,
        int minHeight,
        int maxWidth,
        int maxHeight
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowAspectRatio(Window* window, int numer, int denom);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowSize(Window* window, int width, int height);

    [LibraryImport(DllName, EntryPoint = "glfwGetFramebufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetFramebufferSize(Window* window, out int width, out int height);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowFrameSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowFrameSize(
        Window* window,
        out int left,
        out int top,
        out int right,
        out int bottom
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowContentScale(
        Window* window,
        out float xScale,
        out float yScale
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetWindowOpacity(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowOpacity(Window* window, float opacity);

    [LibraryImport(DllName, EntryPoint = "glfwIconifyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IconifyWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwRestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RestoreWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwMaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MaximizeWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ShowWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwHideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HideWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwFocusWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FocusWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwRequestWindowAttentionWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RequestWindowAttentionWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor* GetWindowMonitor(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowMonitor(
        Window* window,
        Monitor* monitor,
        int xPos,
        int yPos,
        int width,
        int height,
        int refreshRate
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowAttrib")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowAttrib(Window* window, int attrib);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowAttrib")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowAttrib(Window* window, int attrib, int value);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowPosCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, int, void> SetWindowPosCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, int, void> SetWindowSizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowCloseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, void> SetWindowCloseCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowRefreshCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, void> SetWindowRefreshCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowFocusCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowFocusCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowIconifyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowIconifyCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowMaximizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowMaximizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetFramebufferSizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        void> SetFramebufferSizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowContentScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        float,
        float,
        void> SetWindowContentScaleCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, float, float, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwPollEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PollEvents();

    [LibraryImport(DllName, EntryPoint = "glfwWaitEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitEvents();

    [LibraryImport(DllName, EntryPoint = "glfwWaitEventsTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitEventsTimeout(double timeout);

    [LibraryImport(DllName, EntryPoint = "glfwPostEmptyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PostEmptyEvent();

    [LibraryImport(DllName, EntryPoint = "glfwGetInputMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetInputMode(Window* window, int mode);

    [LibraryImport(DllName, EntryPoint = "glfwSetInputMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInputMode(Window* window, int mode, int value);

    [LibraryImport(DllName, EntryPoint = "glfwRawMouseMotionSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool RawMouseMotionSupported();

    [LibraryImport(DllName, EntryPoint = "glfwGetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetKeyName(int key, int scancode);

    [LibraryImport(DllName, EntryPoint = "glfwGetKeyScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetKeyScancode(int key);

    [LibraryImport(DllName, EntryPoint = "glfwGetKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetKey(Window* window, int key);

    [LibraryImport(DllName, EntryPoint = "glfwGetMouseButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMouseButton(Window* window, int button);

    [LibraryImport(DllName, EntryPoint = "glfwGetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetCursorPosition(Window* window, out double xPos, out double yPos);

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCursorPos(Window* window, double xPos, double yPos);

    [LibraryImport(DllName, EntryPoint = "glfwCreateCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Cursor* CreateCursor(in Image image, int xHot, int yHot);

    [LibraryImport(DllName, EntryPoint = "glfwCreateStandardCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Cursor* CreateStandardCursor(int shape);

    [LibraryImport(DllName, EntryPoint = "glfwDestroyCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyCursor(Cursor* cursor);

    [LibraryImport(DllName, EntryPoint = "glfwSetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCursor(Window* window, Cursor* cursor);

    [LibraryImport(DllName, EntryPoint = "glfwSetKeyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        int,
        int,
        void> SetKeyCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCharCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, uint, void> SetCharCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, uint, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCharModsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, uint, int, void> SetCharModsCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, uint, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetMouseButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        int,
        void> SetMouseButtonCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorPosCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        double,
        double,
        void> SetCursorPosCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, double, double, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorEnterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetCursorEnterCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetScrollCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        double,
        double,
        void> SetScrollCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, double, double, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetDropCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, byte**, void> SetDropCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, byte**, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwJoystickPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool JoystickPresent(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float* GetJoystickAxes(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickButtons(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickHats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickHats(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickName(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickGuid(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwJoystickIsGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool JoystickIsGamepad(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwSetJoystickCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<int, int, void> SetJoystickCallback(
        delegate* unmanaged[Cdecl]<int, int, void> callback
    );

    [LibraryImport(
        DllName,
        EntryPoint = "glfwUpdateGamepadMappings",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool UpdateGamepadMappings(string? @string);

    [LibraryImport(DllName, EntryPoint = "glfwGetGamepadName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetGamepadName(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetGamepadState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetGamepadState(int jid, out GamepadState state);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwSetClipboardString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetClipboardString(Window* window, string? @string);

    [LibraryImport(DllName, EntryPoint = "glfwGetClipboardString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetClipboardString(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTime();

    [LibraryImport(DllName, EntryPoint = "SetTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTime(double time);

    [LibraryImport(DllName, EntryPoint = "glfwGetTimerValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTimerValue();

    [LibraryImport(DllName, EntryPoint = "glfwGetTimerFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTimerFrequency();

    [LibraryImport(DllName, EntryPoint = "glfwMakeContextCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MakeContextCurrent(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* GetCurrentContext();

    [LibraryImport(DllName, EntryPoint = "glfwSwapBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SwapBuffers(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SwapInterval(int interval);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwExtensionSupported",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool ExtensionSupported(string extension);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwGetProcAddress",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetProcAddress(string procname);

    [LibraryImport(DllName, EntryPoint = "glfwVulkanSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool VulkanSupported();

    [LibraryImport(DllName, EntryPoint = "glfwGetRequiredInstanceExtensions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** GetRequiredInstanceExtensions(out uint count);

    // WARNING: glfwGetInstanceProcAddress intentionally unimplemented

    // TODO: Implement glfwGetPhysicalDevicePresentationSupport if Vulkan support is added to CanDoUrco

    // TODO: Implement glfwCreateWindowSurface if Vulkan support is added to CanDoUrco
}
