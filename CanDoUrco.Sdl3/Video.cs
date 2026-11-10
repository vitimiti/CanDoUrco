// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public const uint WindowPosUndefinedMask = 0x1FFF_0000U;
    public const uint WindowPosCenteredMask = 0x2FFF_0000U;
    public const int WindowSurfaceVSyncDisabled = 0;
    public const int WindowSurfaceVSyncAdaptive = -1;

    public static ReadOnlySpan<byte> PropGlobalVideoWaylandWLDisplayPointer =>
        "SDL.video.wayland.wl_display\0"u8;

    public static WindowFlags WindowFullscreen => 0x0000_0000_0000_0001UL;
    public static WindowFlags WindowOpenGL => 0x0000_0000_0000_0002UL;
    public static WindowFlags WindowOccluded => 0x0000_0000_0000_0004UL;
    public static WindowFlags WindowHidden => 0x0000_0000_0000_0008UL;
    public static WindowFlags WindowBorderless => 0x0000_0000_0000_0010UL;
    public static WindowFlags WindowResizable => 0x0000_0000_0000_0020UL;
    public static WindowFlags WindowMinimized => 0x0000_0000_0000_0040UL;
    public static WindowFlags WindowMaximized => 0x0000_0000_0000_0080UL;
    public static WindowFlags WindowMouseGrabbed => 0x0000_0000_0000_0100UL;
    public static WindowFlags WindowInputFocus => 0x0000_0000_0000_0200UL;
    public static WindowFlags WindowMouseFocus => 0x0000_0000_0000_0400UL;
    public static WindowFlags WindowExternal => 0x0000_0000_0000_0800UL;
    public static WindowFlags WindowModal => 0x0000_0000_0000_1000UL;
    public static WindowFlags WindowHighPixelDensity => 0x0000_0000_0000_2000UL;
    public static WindowFlags WindowMouseCapture => 0x0000_0000_0000_4000UL;
    public static WindowFlags WindowMouseRelativeMode => 0x0000_0000_0000_8000UL;
    public static WindowFlags WindowAlwaysOnTop => 0x0000_0000_0001_0000UL;
    public static WindowFlags WindowUtility => 0x0000_0000_0002_0000UL;
    public static WindowFlags WindowTooltip => 0x0000_0000_0004_0000UL;
    public static WindowFlags WindowPopupMenu => 0x0000_0000_0008_0000UL;
    public static WindowFlags WindowKeyboardGrabbed => 0x0000_0000_0010_0000UL;
    public static WindowFlags WindowFillDocument => 0x0000_0000_0020_0000UL;
    public static WindowFlags WindowVulkan => 0x0000_0000_1000_0000UL;
    public static WindowFlags WindowMetal => 0x0000_0000_2000_0000UL;
    public static WindowFlags WindowTransparent => 0x0000_0000_4000_0000UL;
    public static WindowFlags WindowNotFocusable => 0x0000_0000_8000_0000UL;

    public static uint WindowPosUndefined => WindowPosUndefinedDisplay(0);
    public static uint WindowPosCentered => WindowPosCenteredDisplay(0);

    public static GLProfile GLContextProfileCore => 0x0001;
    public static GLProfile GLContextProfileCompatibility => 0x0002;
    public static GLProfile GLContextProfileES => 0x0004;

    public static GLContextFlag GLContextDebugFlag => 0x0001;
    public static GLContextFlag GLContextForwardCompatibleFlag => 0x0002;
    public static GLContextFlag GLContextRobustAccessFlag => 0x0004;
    public static GLContextFlag GLContextResetIsolationFlag => 0x0008;

    public static GLContextReleaseFlag GLContextReleaseBehaviorNone => 0x0000;
    public static GLContextReleaseFlag GLContextReleaseBehaviorFlush => 0x0001;

    public static GLContextResetNotification GLContextResetNoNotification => 0x0000;
    public static GLContextResetNotification GLContextResetLoseContext => 0x0001;

    public static ReadOnlySpan<byte> PropDisplayHdrEnabledBoolean =>
        "SDL.displayID.HDR_enabled\0"u8;
    public static ReadOnlySpan<byte> PropDisplayKmsDrmPanelOrientationNumber =>
        "SDL.displayID.KMSDRM.panel_orientation\0"u8;
    public static ReadOnlySpan<byte> PropDisplayWaylandWLOutputPointer =>
        "SDL.displayID.wayland.wl_output\0"u8;
    public static ReadOnlySpan<byte> PropDisplayWindowsHMonitorPointer =>
        "SDL.displayID.windows.hmonitor\0"u8;

    public static ReadOnlySpan<byte> PropWindowCreateAlwaysOnTopBoolean =>
        "SDL.window.create.always_on_top\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateBorderlessBoolean =>
        "SDL.window.create.borderless\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateConstrainPopupBoolean =>
        "SDL.window.create.constrain_popup\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateFocusableBoolean =>
        "SDL.window.create.focusable\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateExternalGraphicsContextBoolean =>
        "SDL.window.create.external_graphics_context\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateFlagsNumber => "SDL.window.create.flags\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateFullscreenBoolean =>
        "SDL.window.create.fullscreen\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateHeightNumber => "SDL.window.create.height\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateHiddenBoolean =>
        "SDL.window.create.hidden\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateHighPixelDensityBoolean =>
        "SDL.window.create.high_pixel_density\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateMaximizedBoolean =>
        "SDL.window.create.maximized\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateMenuBoolean => "SDL.window.create.menu\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateMetalBoolean => "SDL.window.create.metal\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateMinimizedBoolean =>
        "SDL.window.create.minimized\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateModalBoolean => "SDL.window.create.modal\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateMouseGrabbedBoolean =>
        "SDL.window.create.mouse_grabbed\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateOpenGLBoolean =>
        "SDL.window.create.opengl\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateParentPointer =>
        "SDL.window.create.parent\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateResizableBoolean =>
        "SDL.window.create.resizable\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateTitleString => "SDL.window.create.title\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateTransparentBoolean =>
        "SDL.window.create.transparent\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateTooltipBoolean =>
        "SDL.window.create.tooltip\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateUtilityBoolean =>
        "SDL.window.create.utility\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateVulkanBoolean =>
        "SDL.window.create.vulkan\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWidthNumber => "SDL.window.create.width\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateXNumber => "SDL.window.create.x\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateYNumber => "SDL.window.create.y\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateCocoaWindowPointer =>
        "SDL.window.create.cocoa.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateCocoaViewPointer =>
        "SDL.window.create.cocoa.view\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWindowScenePointer =>
        "SDL.window.create.uikit.windowscene\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWaylandSurfaceRoleCustomBoolean =>
        "SDL.window.create.wayland.surface_role_custom\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWaylandCreateEglWindowBoolean =>
        "SDL.window.create.wayland.create_egl_window\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWaylandWLSurfacePointer =>
        "SDL.window.create.wayland.wl_surface\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWin32HwndPointer =>
        "SDL.window.create.win32.hwnd\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateWin32PixelFormatHwndPointer =>
        "SDL.window.create.win32.pixel_format_hwnd\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateX11WindowNumber =>
        "SDL.window.create.x11.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateEmscriptenCanvasIDString =>
        "SDL.window.create.emscripten.canvas_id\0"u8;
    public static ReadOnlySpan<byte> PropWindowCreateEmscriptenKeyboardElementString =>
        "SDL.window.create.emscripten.keyboard_element\0"u8;

    public static ReadOnlySpan<byte> PropWindowShapePointer => "SDL.window.shape\0"u8;
    public static ReadOnlySpan<byte> PropWindowHdrEnabledBoolean => "SDL.window.HDR_enabled\0"u8;
    public static ReadOnlySpan<byte> PropWindowSdrWhiteLevelFloat =>
        "SDL.window.SDR_white_level\0"u8;
    public static ReadOnlySpan<byte> PropWindowHdrHeadroomFloat => "SDL.window.HDR_headroom\0"u8;
    public static ReadOnlySpan<byte> PropWindowAndroidWIndowPointer =>
        "SDL.window.android.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowAndroidSurfacePointer =>
        "SDL.window.android.surface\0"u8;
    public static ReadOnlySpan<byte> PropWindowUIKitWindowPointer => "SDL.window.uikit.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowUIKitMetalViewTagNumber =>
        "SDL.window.uikit.metal_view_tag\0"u8;
    public static ReadOnlySpan<byte> PropWindowUIKitOpenGLFrameBufferNumber =>
        "SDL.window.uikit.opengl.framebuffer\0"u8;
    public static ReadOnlySpan<byte> PropWindowUIKitOpenGLRenderBufferNumber =>
        "SDL.window.uikit.opengl.renderbuffer\0"u8;
    public static ReadOnlySpan<byte> PropWindowUIKitOpenGLResolveFrameBufferNumber =>
        "SDL.window.uikit.opengl.resolve_framebuffer\0"u8;
    public static ReadOnlySpan<byte> PropWindowKmsDrmDeviceIndexNumber =>
        "SDL.window.kmsdrm.dev_index\0"u8;
    public static ReadOnlySpan<byte> PropWindowKmsDrmDrmFDNumber => "SDL.window.kmsdrm.drm_fd\0"u8;
    public static ReadOnlySpan<byte> PropWindowKmsDrmGbmDevicePointer =>
        "SDL.window.kmsdrm.gbm_dev\0"u8;
    public static ReadOnlySpan<byte> PropWindowCocoaWindowPointer => "SDL.window.cocoa.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowCocoaMetalViewTagNumber =>
        "SDL.window.cocoa.metal_view_tag\0"u8;
    public static ReadOnlySpan<byte> PropWindowOpenVROverlayIDNumber =>
        "SDL.window.openvr.overlay_id\0"u8;
    public static ReadOnlySpan<byte> PropWindowVivanteDisplayPointer =>
        "SDL.window.vivante.display\0"u8;
    public static ReadOnlySpan<byte> PropWindowVivanteWindowPointer =>
        "SDL.window.vivante.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowVivanteSurfacePointer =>
        "SDL.window.vivante.surface\0"u8;
    public static ReadOnlySpan<byte> PropWindowWin32HwndPointer => "SDL.window.win32.hwnd\0"u8;
    public static ReadOnlySpan<byte> PropWindowWin32HdcPointer => "SDL.window.win32.hdc\0"u8;
    public static ReadOnlySpan<byte> PropWindowWin32InstancePointer =>
        "SDL.window.win32.instance\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandDisplayPointer =>
        "SDL.window.wayland.display\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandSurfacePointer =>
        "SDL.window.wayland.surface\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandViewportPointer =>
        "SDL.window.wayland.viewport\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandEglWindowPointer =>
        "SDL.window.wayland.egl_window\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandXdgSurfacePointer =>
        "SDL.window.wayland.xdg_surface\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandXdgToplevelPointer =>
        "SDL.window.wayland.xdg_toplevel\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandXdgToplevelExportHandleString =>
        "SDL.window.wayland.xdg_toplevel_export_handle\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandXdgPopupPointer =>
        "SDL.window.wayland.xdg_popup\0"u8;
    public static ReadOnlySpan<byte> PropWindowWaylandXdgPositionerPointer =>
        "SDL.window.wayland.xdg_positioner\0"u8;
    public static ReadOnlySpan<byte> PropWindowX11DisplayPointer => "SDL.window.x11.display\0"u8;
    public static ReadOnlySpan<byte> PropWindowX11ScreenNumber => "SDL.window.x11.screen\0"u8;
    public static ReadOnlySpan<byte> PropWindowX11WindowNumber => "SDL.window.x11.window\0"u8;
    public static ReadOnlySpan<byte> PropWindowEmscriptenCanvasIDString =>
        "SDL.window.emscripten.canvas_id\0"u8;
    public static ReadOnlySpan<byte> PropWindowEmscriptenKeyboardElementString =>
        "SDL.window.emscripten.keyboard_element\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumVideoDrivers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetNumVideoDrivers();

    [LibraryImport(DllName, EntryPoint = "SDL_GetVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetVideoDriver(int index);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentVideoDriver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCurrentVideoDriver();

    [LibraryImport(DllName, EntryPoint = "SDL_GetSystemTheme")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial SystemTheme _GetSystemTheme();

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayID* _GetDisplays(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetPrimaryDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayID _GetPrimaryDisplay();

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetDisplayProperties(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetDisplayName(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetDisplayBounds(DisplayID displayID, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayUsableBounds")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetDisplayUsableBounds(DisplayID displayID, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetNaturalDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation _GetNaturalDisplayOrientation(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentDisplayOrientation")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayOrientation _GetCurrentDisplayOrientation(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetDisplayContentScale(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetFullscreenDisplayModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode** _GetFullscreenDisplayModes(
        DisplayID displayID,
        out int count
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetClosestFullscreenDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetClosestFullscreenDisplayMode(
        DisplayID displayID,
        int w,
        int h,
        float refreshRate,
        Bool includeHighDensityModes,
        ref DisplayMode closest
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetDesktopDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* _GetDesktopDisplayMode(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentDisplayMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* _GetCurrentDisplayMode(DisplayID displayID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayForPoint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayID _GetDisplayForPoint(in Point point);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayForRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayID _GetDisplayForRect(in Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetDisplayForWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayID _GetDisplayForWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowPixelDensity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetWindowPixelDensity(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWIndowDisplayScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetWindowDisplayScale(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowFullscreenMode(Window window, DisplayMode* mode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowFullscreenMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial DisplayMode* _GetWindowFullscreenMode(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowICCProfile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetWindowIccProfile(Window window, out nuint size);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowPixelFormat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PixelFormat _GetWindowPixelFormat(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindows")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window* _GetWindows(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _CreateWindow(byte* title, int w, int h, WindowFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_CreatePopupWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _CreatePopupWindow(
        Window parent,
        int offsetX,
        int offsetY,
        int w,
        int h,
        WindowFlags flags
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateWindowWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _CreateWindowWithProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowID _GetWindowID(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowFromID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetWindowFromID(WindowID id);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetWindowParent(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetWindowProperties(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowFlags")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial WindowFlags _GetWindowFlags(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowTitle(Window window, byte* title);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetWindowTitle(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowIcon(Window window, Surface* icon);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowPosition(Window window, int x, int y);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowPosition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowPosition(Window window, out int x, out int y);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowSize(Window window, int w, int h);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowSize(Window window, out int w, out int h);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowSafeArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowSafeArea(Window window, out Rect rect);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowAspectRatio(
        Window window,
        float minAspect,
        float maxAspect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowAspectRatio(
        Window window,
        out float minAspect,
        out float maxAspect
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowBordersSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowBordersSize(
        Window window,
        out int top,
        out int left,
        out int bottom,
        out int right
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowSizeInPixels")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowSizeInPixels(Window window, out int w, out int h);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowMinimumSize(Window window, int minW, int minH);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowMinimumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowMinimumSize(Window window, out int minW, out int minH);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowMaximumSize(Window window, int maxW, int maxH);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowMaximumSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowMaximumSize(Window window, out int maxW, out int maxH);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowBordered")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowBordered(Window window, Bool bordered);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowResizable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowResizable(Window window, Bool resizable);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowAlwaysOnTop")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowAlwaysOnTop(Window window, Bool onTop);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowFillDocument")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowFillDocument(Window window, Bool fill);

    [LibraryImport(DllName, EntryPoint = "SDL_ShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ShowWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_HideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HideWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_RaiseWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RaiseWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_MaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _MaximizeWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_MinimizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _MinimizeWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_RestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RestoreWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowFullscreen")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowFullscreen(Window window, Bool fullscreen);

    [LibraryImport(DllName, EntryPoint = "SDL_SyncWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SyncWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_WindowHasSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WindowHasSurface(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Surface* _GetWindowSurface(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowSurfaceVSync(Window window, int vsync);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowSurfaceVSync")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowSurfaceVSync(Window window, out int vsync);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateWindowSurface(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateWindowSurfaceRects")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _UpdateWindowSurfaceRects(Window window, Rect* rects, int numRects);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _DestroyWindowSurface(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowKeyboardGrab(Window window, Bool grabbed);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowMouseGrab(Window window, Bool grabbed);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowKeyboardGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowKeyboardGrab(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowMouseGrab")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowMouseGrab(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGrabbedWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetGrabbedWindow();

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowMouseRect(Window window, Rect* rect);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowMouseRect")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Rect* _GetWindowMouseRect(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowOpacity(Window window, float opacity);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetWindowOpacity(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowParent(Window window, Window parent);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowModal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowModal(Window window, Bool modal);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowFocusable")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowFocusable(Window window, Bool focusable);

    [LibraryImport(DllName, EntryPoint = "SDL_ShowWindowSystemMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ShowWindowSystemMenu(Window window, int x, int y);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowHitTest")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowHitTest(
        Window window,
        delegate* unmanaged[Cdecl]<Window, Point*, void*, HitTestResult> callback,
        void* callbackData
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowShape")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowShape(Window window, Surface* shape);

    [LibraryImport(DllName, EntryPoint = "SDL_FlashWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _FlashWindow(Window window, FlashOperation operation);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowProgressState(Window window, ProgressState state);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowProgressState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ProgressState _GetWindowProgressState(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowProgressValue(Window window, float value);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowProgressValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetWindowProgressValue(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_ScreenSaverEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ScreenSaverEnabled();

    [LibraryImport(DllName, EntryPoint = "SDL_EnableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _EnableScreenSaver();

    [LibraryImport(DllName, EntryPoint = "SDL_DisableScreenSaver")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _DisableScreenSaver();

    [LibraryImport(DllName, EntryPoint = "SDL_GL_LoadLibrary")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLLoadLibrary(byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_GetProcAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint _GLGetProcAddress(byte* proc);

    [LibraryImport(DllName, EntryPoint = "SDL_EGL_GetProcAddress")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial nint _EglGetProcAddress(byte* proc);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_UnloadLibrary")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GLUnloadLibrary();

    [LibraryImport(DllName, EntryPoint = "SDL_GL_ExtensionSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLExtensionSupported(byte* extension);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_ResetAttributes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GLResetAttributes();

    [LibraryImport(DllName, EntryPoint = "SDL_GL_SetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLSetAttribute(int attr, int value);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_GetAttribute")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLGetAttribute(int attr, out int value);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_CreateContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GLContext _GLCreateContext(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_MakeCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLMakeCurrent(Window window, GLContext context);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_GetCurrentWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GLGetCurrentWindow();

    [LibraryImport(DllName, EntryPoint = "SDL_GL_GetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial GLContext _GLGetCurrentContext();

    [LibraryImport(DllName, EntryPoint = "SDL_EGL_GetCurrentDisplay")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial EglDisplay _EglGetCurrentDisplay();

    [LibraryImport(DllName, EntryPoint = "SDL_EGL_GetCurrentConfig")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial EglConfig _EglGetCurrentConfig();

    [LibraryImport(DllName, EntryPoint = "SDL_EGL_GetWindowSurface")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial EglSurface _EglGetWindowSurface(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_EGL_SetAttributeCallbacks")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _EglSetAttributeCallbacks(
        delegate* unmanaged[Cdecl]<void*, EglAttrib*> platformAttribCallback,
        delegate* unmanaged[Cdecl]<void*, EglDisplay, EglConfig, EglInt*> surfaceAttribCallback,
        delegate* unmanaged[Cdecl]<void*, EglDisplay, EglConfig, EglInt*> contextAttribCallback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GL_SetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLSetSwapInterval(int interval);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_GetSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLGetSwapInterval(out int interval);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_SwapWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLSwapWindow(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_GL_DestroyContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GLDestroyContext(GLContext context);

    public static uint WindowPosUndefinedDisplay(DisplayID x) => WindowPosUndefinedMask | x;

    public static Bool WindowPosIsUndefined(int x) => (x & 0xFFFF_0000) == WindowPosUndefinedMask;

    public static uint WindowPosCenteredDisplay(DisplayID x) => WindowPosCenteredMask | x;

    public static Bool WindowPosIsCentered(int x) => (x & 0xFFFF_0000) == WindowPosCenteredMask;

    public static int GetNumVideoDrivers()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumVideoDrivers();
    }

    public static byte* GetVideoDriver(int index)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetVideoDriver(index);
    }

    public static byte* GetCurrentVideoDriver()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentVideoDriver();
    }

    public static SystemTheme GetSystemTheme()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetSystemTheme();
    }

    public static DisplayID* GetDisplays(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplays(out count);
    }

    public static DisplayID GetPrimaryDisplay()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPrimaryDisplay();
    }

    public static PropertiesID GetDisplayProperties(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayProperties(displayID);
    }

    public static byte* GetDisplayName(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayName(displayID);
    }

    public static Bool GetDisplayBounds(DisplayID displayID, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayBounds(displayID, out rect);
    }

    public static Bool GetDisplayUsableBounds(DisplayID displayID, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayUsableBounds(displayID, out rect);
    }

    public static DisplayOrientation GetNaturalDisplayOrientation(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNaturalDisplayOrientation(displayID);
    }

    public static DisplayOrientation GetCurrentDisplayOrientation(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentDisplayOrientation(displayID);
    }

    public static float GetDisplayContentScale(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayContentScale(displayID);
    }

    public static DisplayMode** GetFullscreenDisplayModes(DisplayID displayID, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetFullscreenDisplayModes(displayID, out count);
    }

    public static Bool GetClosestFullscreenDisplayMode(
        DisplayID displayID,
        int w,
        int h,
        float refreshRate,
        Bool includeHighDensityModes,
        ref DisplayMode closest
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetClosestFullscreenDisplayMode(
            displayID,
            w,
            h,
            refreshRate,
            includeHighDensityModes,
            ref closest
        );
    }

    public static DisplayMode* GetDesktopDisplayMode(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDesktopDisplayMode(displayID);
    }

    public static DisplayMode* GetCurrentDisplayMode(DisplayID displayID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCurrentDisplayMode(displayID);
    }

    public static DisplayID GetDisplayForPoint(in Point point)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayForPoint(in point);
    }

    public static DisplayID GetDisplayForRect(in Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayForRect(in rect);
    }

    public static DisplayID GetDisplayForWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDisplayForWindow(window);
    }

    public static float GetWindowPixelDensity(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowPixelDensity(window);
    }

    public static float GetWindowDisplayScale(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowDisplayScale(window);
    }

    public static Bool SetWindowFullscreenMode(Window window, DisplayMode* mode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowFullscreenMode(window, mode);
    }

    public static DisplayMode* GetWindowFullscreenMode(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowFullscreenMode(window);
    }

    public static void* GetWindowIccProfile(Window window, out nuint size)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowIccProfile(window, out size);
    }

    public static PixelFormat GetWindowPixelFormat(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowPixelFormat(window);
    }

    public static Window* GetWindows(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindows(out count);
    }

    public static Window CreateWindow(byte* title, int w, int h, WindowFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateWindow(title, w, h, flags);
    }

    public static Window CreatePopupWindow(
        Window parent,
        int offsetX,
        int offsetY,
        int w,
        int h,
        WindowFlags flags
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreatePopupWindow(parent, offsetX, offsetY, w, h, flags);
    }

    public static Window CreateWindowWithProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateWindowWithProperties(props);
    }

    public static WindowID GetWindowID(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowID(window);
    }

    public static Window GetWindowFromID(WindowID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowFromID(id);
    }

    public static Window GetWindowParent(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowParent(window);
    }

    public static PropertiesID GetWindowProperties(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowProperties(window);
    }

    public static WindowFlags GetWindowFlags(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowFlags(window);
    }

    public static Bool SetWindowTitle(Window window, byte* title)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowTitle(window, title);
    }

    public static byte* GetWindowTitle(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowTitle(window);
    }

    public static Bool SetWindowIcon(Window window, Surface* icon)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowIcon(window, icon);
    }

    public static Bool SetWindowPosition(Window window, int x, int y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowPosition(window, x, y);
    }

    public static Bool GetWindowPosition(Window window, out int x, out int y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowPosition(window, out x, out y);
    }

    public static Bool SetWindowSize(Window window, int w, int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowSize(window, w, h);
    }

    public static Bool GetWindowSize(Window window, out int w, out int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowSize(window, out w, out h);
    }

    public static Bool GetWindowSafeArea(Window window, out Rect rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowSafeArea(window, out rect);
    }

    public static Bool SetWindowAspectRatio(Window window, float minAspect, float maxAspect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowAspectRatio(window, minAspect, maxAspect);
    }

    public static Bool GetWindowAspectRatio(Window window, out float minAspect, out float maxAspect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowAspectRatio(window, out minAspect, out maxAspect);
    }

    public static Bool GetWindowBordersSize(
        Window window,
        out int top,
        out int left,
        out int bottom,
        out int right
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowBordersSize(window, out top, out left, out bottom, out right);
    }

    public static Bool GetWindowSizeInPixels(Window window, out int w, out int h)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowSizeInPixels(window, out w, out h);
    }

    public static Bool SetWindowMinimumSize(Window window, int minW, int minH)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowMinimumSize(window, minW, minH);
    }

    public static Bool GetWindowMinimumSize(Window window, out int minW, out int minH)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowMinimumSize(window, out minW, out minH);
    }

    public static Bool SetWindowMaximumSize(Window window, int maxW, int maxH)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowMaximumSize(window, maxW, maxH);
    }

    public static Bool GetWindowMaximumSize(Window window, out int maxW, out int maxH)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowMaximumSize(window, out maxW, out maxH);
    }

    public static Bool SetWindowBordered(Window window, Bool bordered)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowBordered(window, bordered);
    }

    public static Bool SetWindowResizable(Window window, Bool resizable)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowResizable(window, resizable);
    }

    public static Bool SetWindowAlwaysOnTop(Window window, Bool onTop)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowAlwaysOnTop(window, onTop);
    }

    public static Bool SetWindowFillDocument(Window window, Bool fill)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowFillDocument(window, fill);
    }

    public static Bool ShowWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShowWindow(window);
    }

    public static Bool HideWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HideWindow(window);
    }

    public static Bool RaiseWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RaiseWindow(window);
    }

    public static Bool MaximizeWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MaximizeWindow(window);
    }

    public static Bool MinimizeWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _MinimizeWindow(window);
    }

    public static Bool RestoreWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RestoreWindow(window);
    }

    public static Bool SetWindowFullscreen(Window window, Bool fullscreen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowFullscreen(window, fullscreen);
    }

    public static Bool SyncWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SyncWindow(window);
    }

    public static Bool WindowHasSurface(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WindowHasSurface(window);
    }

    public static Surface* GetWindowSurface(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowSurface(window);
    }

    public static Bool SetWindowSurfaceVSync(Window window, int vsync)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowSurfaceVSync(window, vsync);
    }

    public static Bool GetWindowSurfaceVSync(Window window, out int vsync)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowSurfaceVSync(window, out vsync);
    }

    public static Bool UpdateWindowSurface(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateWindowSurface(window);
    }

    public static Bool UpdateWindowSurfaceRects(Window window, Rect* rects, int numRects)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _UpdateWindowSurfaceRects(window, rects, numRects);
    }

    public static Bool DestroyWindowSurface(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DestroyWindowSurface(window);
    }

    public static Bool SetWindowKeyboardGrab(Window window, Bool grabbed)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowKeyboardGrab(window, grabbed);
    }

    public static Bool SetWindowMouseGrab(Window window, Bool grabbed)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowMouseGrab(window, grabbed);
    }

    public static Bool GetWindowKeyboardGrab(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowKeyboardGrab(window);
    }

    public static Bool GetWindowMouseGrab(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowMouseGrab(window);
    }

    public static Window GetGrabbedWindow()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGrabbedWindow();
    }

    public static Bool SetWindowMouseRect(Window window, Rect* rect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowMouseRect(window, rect);
    }

    public static Rect* GetWindowMouseRect(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowMouseRect(window);
    }

    public static Bool SetWindowOpacity(Window window, float opacity)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowOpacity(window, opacity);
    }

    public static float GetWindowOpacity(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowOpacity(window);
    }

    public static Bool SetWindowParent(Window window, Window parent)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowParent(window, parent);
    }

    public static Bool SetWindowModal(Window window, Bool modal)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowModal(window, modal);
    }

    public static Bool SetWindowFocusable(Window window, Bool focusable)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowFocusable(window, focusable);
    }

    public static Bool ShowWindowSystemMenu(Window window, int x, int y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShowWindowSystemMenu(window, x, y);
    }

    public static Bool SetWindowHitTest(
        Window window,
        delegate* unmanaged[Cdecl]<Window, Point*, void*, HitTestResult> callback,
        void* callbackData
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowHitTest(window, callback, callbackData);
    }

    public static Bool SetWindowShape(Window window, Surface* shape)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowShape(window, shape);
    }

    public static Bool FlashWindow(Window window, FlashOperation operation)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _FlashWindow(window, operation);
    }

    public static Bool SetWindowProgressState(Window window, ProgressState state)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowProgressState(window, state);
    }

    public static ProgressState GetWindowProgressState(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowProgressState(window);
    }

    public static Bool SetWindowProgressValue(Window window, float value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowProgressValue(window, value);
    }

    public static float GetWindowProgressValue(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowProgressValue(window);
    }

    public static void DestroyWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyWindow(window);
    }

    public static Bool ScreenSaverEnabled()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScreenSaverEnabled();
    }

    public static Bool EnableScreenSaver()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnableScreenSaver();
    }

    public static Bool DisableScreenSaver()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _DisableScreenSaver();
    }

    public static Bool GLLoadLibrary(byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLLoadLibrary(path);
    }

    public static nint GLGetProcAddress(byte* proc)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLGetProcAddress(proc);
    }

    public static nint EglGetProcAddress(byte* proc)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EglGetProcAddress(proc);
    }

    public static void GLUnloadLibrary()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GLUnloadLibrary();
    }

    public static Bool GLExtensionSupported(byte* extension)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLExtensionSupported(extension);
    }

    public static void GLResetAttributes()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GLResetAttributes();
    }

    public static Bool GLSetAttribute(GLAttr attr, int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLSetAttribute((int)attr, value);
    }

    public static Bool GLGetAttribute(GLAttr attr, out int value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLGetAttribute((int)attr, out value);
    }

    public static GLContext GLCreateContext(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLCreateContext(window);
    }

    public static Bool GLMakeCurrent(Window window, GLContext context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLMakeCurrent(window, context);
    }

    public static Window GLGetCurrentWindow()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLGetCurrentWindow();
    }

    public static GLContext GLGetCurrentContext()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLGetCurrentContext();
    }

    public static EglDisplay EglGetCurrentDisplay()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EglGetCurrentDisplay();
    }

    public static EglConfig EglGetCurrentConfig()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EglGetCurrentConfig();
    }

    public static EglSurface EglGetWindowSurface(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EglGetWindowSurface(window);
    }

    public static void EglSetAttributeCallbacks(
        delegate* unmanaged[Cdecl]<void*, EglAttrib*> platformAttribCallback,
        delegate* unmanaged[Cdecl]<void*, EglDisplay, EglConfig, EglInt*> surfaceAttribCallback,
        delegate* unmanaged[Cdecl]<void*, EglDisplay, EglConfig, EglInt*> contextAttribCallback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _EglSetAttributeCallbacks(
            platformAttribCallback,
            surfaceAttribCallback,
            contextAttribCallback,
            userdata
        );
    }

    public static Bool GLSetSwapInterval(int interval)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLSetSwapInterval(interval);
    }

    public static Bool GLGetSwapInterval(out int interval)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLGetSwapInterval(out interval);
    }

    public static Bool GLSwapWindow(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLSwapWindow(window);
    }

    public static Bool GLDestroyContext(GLContext context)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GLDestroyContext(context);
    }

    public enum SystemTheme
    {
        Unknown,
        Light,
        Dark,
    }

    public enum DisplayOrientation
    {
        Unknown,
        Landscape,
        LandscapeFlipped,
        Portrait,
        PortraitFlipped,
    }

    public enum FlashOperation
    {
        Cancel,
        Briefly,
        UntilFocused,
    }

    public enum ProgressState
    {
        Invalid = -1,
        None,
        Indeterminate,
        Normal,
        Paused,
        Error,
    }

    public enum GLAttr
    {
        RedSize,
        GreenSize,
        BlueSize,
        AlphaSize,
        BufferSize,
        DoubleBuffer,
        DepthSize,
        StencilSize,
        AccumRedSize,
        AccumGreenSize,
        AccumBlueSize,
        AccumAlphaSize,
        Stereo,
        MultisampleBuffers,
        MultisampleSamples,
        AcceleratedVisual,
        RetainedBacking,
        ContextMajorVersion,
        ContextMinorVersion,
        ContextFlags,
        ContextProfileMask,
        ShareWithCurrentContext,
        FrameBufferSrgbCapable,
        ContextReleaseBehavior,
        ContextResetNotification,
        ContextNoError,
        FloatBuffers,
        EglPlatform,
    }

    public enum HitTestResult
    {
        Normal,
        Draggable,
        ResizeTopLeft,
        ResizeTop,
        ResizeTopRight,
        ResizeRight,
        ResizeBottomRight,
        ResizeBottom,
        ResizeBottomLeft,
        ResizeLeft,
    }

    public readonly record struct DisplayID(uint Value)
    {
        public static implicit operator DisplayID(uint value) => new(value);

        public static implicit operator uint(DisplayID displayID) => displayID.Value;
    }

    public readonly record struct WindowID(uint Value)
    {
        public static implicit operator WindowID(uint value) => new(value);

        public static implicit operator uint(WindowID windowID) => windowID.Value;
    }

    public readonly record struct Window(nint Value)
    {
        public static implicit operator Window(nint value) => new(value);

        public static implicit operator nint(Window window) => window.Value;
    }

    public readonly record struct WindowFlags(ulong Value)
    {
        public static WindowFlags operator |(WindowFlags left, WindowFlags right) =>
            new(left.Value | right.Value);

        public static WindowFlags operator &(WindowFlags left, WindowFlags right) =>
            new(left.Value & right.Value);

        public static WindowFlags operator ^(WindowFlags left, WindowFlags right) =>
            new(left.Value ^ right.Value);

        public static WindowFlags operator ~(WindowFlags value) => new(~value.Value);

        public static implicit operator WindowFlags(ulong value) => new(value);

        public static implicit operator ulong(WindowFlags windowFlags) => windowFlags.Value;
    }

    public readonly record struct GLContext(nint Value)
    {
        public static implicit operator GLContext(nint value) => new(value);

        public static implicit operator nint(GLContext glContext) => glContext.Value;
    }

    public readonly record struct EglDisplay(nint Value)
    {
        public static implicit operator EglDisplay(nint value) => new(value);

        public static implicit operator nint(EglDisplay eglDisplay) => eglDisplay.Value;
    }

    public readonly record struct EglConfig(nint Value)
    {
        public static implicit operator EglConfig(nint value) => new(value);

        public static implicit operator nint(EglConfig eglConfig) => eglConfig.Value;
    }

    public readonly record struct EglSurface(nint Value)
    {
        public static implicit operator EglSurface(nint value) => new(value);

        public static implicit operator nint(EglSurface eglSurface) => eglSurface.Value;
    }

    public readonly record struct EglAttrib(nint Value)
    {
        public static implicit operator EglAttrib(nint value) => new(value);

        public static implicit operator nint(EglAttrib eglAttrib) => eglAttrib.Value;
    }

    public readonly record struct EglInt(int Value)
    {
        public static implicit operator EglInt(int value) => new(value);

        public static implicit operator int(EglInt eglInt) => eglInt.Value;
    }

    public readonly record struct GLProfile(uint Value)
    {
        public static implicit operator GLProfile(uint value) => new(value);

        public static implicit operator uint(GLProfile glProfile) => glProfile.Value;
    }

    public readonly record struct GLContextFlag(uint Value)
    {
        public static implicit operator GLContextFlag(uint value) => new(value);

        public static implicit operator uint(GLContextFlag glContextFlag) => glContextFlag.Value;
    }

    public readonly record struct GLContextReleaseFlag(uint Value)
    {
        public static implicit operator GLContextReleaseFlag(uint value) => new(value);

        public static implicit operator uint(GLContextReleaseFlag glContextReleaseFlag) =>
            glContextReleaseFlag.Value;
    }

    public readonly record struct GLContextResetNotification(uint Value)
    {
        public static implicit operator GLContextResetNotification(uint value) => new(value);

        public static implicit operator uint(
            GLContextResetNotification glContextResetNotification
        ) => glContextResetNotification.Value;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayMode
    {
        public DisplayID DisplayID;
        public PixelFormat Format;
        public int W;
        public int H;
        public float PixelDensity;
        public float RefreshRate;
        public int RefreshRateNumerator;
        public int RefreshRateDenominator;
        private readonly unsafe void* _internal;
    }
}
