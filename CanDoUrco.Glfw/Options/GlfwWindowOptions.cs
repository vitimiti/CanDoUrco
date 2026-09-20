// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;
using System.Runtime.Versioning;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The possible options for the window creation.
/// </summary>
/// <remarks>This record is sealed</remarks>
/// <seealso cref="GlfwWindow"/>
public sealed record GlfwWindowOptions
{
    /// <summary>
    /// Gets whether the window is resizable.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Resizable { get; init; } = true;

    /// <summary>
    /// Gets whether the window is visible.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Visible { get; init; } = true;

    /// <summary>
    /// Gets whether the window is decorated.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Decorated { get; init; } = true;

    /// <summary>
    /// Gets whether the window is focused.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Focused { get; init; } = true;

    /// <summary>
    /// Gets whether the window is automatically iconfied.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool AutoIconify { get; init; } = true;

    /// <summary>
    /// Gets whether the window is floating.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Floating { get; init; }

    /// <summary>
    /// Gets whether the window is maximized.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Maximized { get; init; }

    /// <summary>
    /// Gets whether the cursor is centered on the window.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool CenterCursor { get; init; } = true;

    /// <summary>
    /// Gets whether to use a transparent framebuffer.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool TransparentFramebuffer { get; init; }

    /// <summary>
    /// Gets whether the window is focused when shown.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool FocusOnShow { get; init; } = true;

    /// <summary>
    /// Gets whether the window should be scaled to the monitor.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool ScaleToMonitor { get; init; }

    /// <summary>
    /// Gets whether the window's frambuffer should be scaled.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool ScaleFramebuffer { get; init; } = true;

    /// <summary>
    /// Gets whether the window should support mouse passthrough.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool MousePassthrough { get; init; }

    /// <summary>
    /// Gets the window position.
    /// </summary>
    /// <remarks>The default value is { <see cref="GlfwSpecialValues.AnyPosition"/>, <see cref="GlfwSpecialValues.AnyPosition"/> }.</remarks>
    public Point Position { get; init; } =
        new Point(GlfwSpecialValues.AnyPosition, GlfwSpecialValues.AnyPosition);

    /// <summary>
    /// Gets the window color bits.
    /// </summary>
    /// <remarks>The default value is { 8, 8, 8, 8 }. You may set these values to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public (int Red, int Green, int Blue, int Alpha) ColorBits { get; init; } = (8, 8, 8, 8);

    /// <summary>
    /// Gets the window depth bits.
    /// </summary>
    /// <remarks>The default value is 24. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int DepthBits { get; init; } = 24;

    /// <summary>
    /// Gets the window stencil bits.
    /// </summary>
    /// <remarks>The default value is 8. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int StencilBits { get; init; } = 8;

    /// <summary>
    /// Gets the window accum bits.
    /// </summary>
    /// <remarks>The default value is { 0, 0, 0, 0 }. You may set these values to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public (int Red, int Green, int Blue, int Alpha) AccumBits { get; init; }

    /// <summary>
    /// Gets the window aux buffers.
    /// </summary>
    /// <remarks>The default value is 0. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int AuxBuffers { get; init; }

    /// <summary>
    /// Gets the window samples.
    /// </summary>
    /// <remarks>The default value is 0. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int Samples { get; init; }

    /// <summary>
    /// Gets the window refresh rate.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int RefreshRate { get; init; } = GlfwSpecialValues.DontCare;

    /// <summary>
    /// Gets whether the window uses stereo.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Stereo { get; init; }

    /// <summary>
    /// Gets whether the window is SRGB capable.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool SrgbCapable { get; init; }

    /// <summary>
    /// Gets whether the window uses a doublebuffer.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Doublebuffer { get; init; } = true;

    /// <summary>
    /// Gets the window's client API.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwClientApi.OpenGl"/>.</remarks>
    /// <seealso cref="GlfwClientApi"/>
    public GlfwClientApi ClientApi { get; init; } = GlfwClientApi.OpenGl;

    /// <summary>
    /// Gets the window context creation API.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextCreationApi.Native"/>.</remarks>
    /// <seealso cref="GlfwContextCreationApi"/>
    public GlfwContextCreationApi ContextCreationApi { get; init; } = GlfwContextCreationApi.Native;

    /// <summary>
    /// Gets the context version.
    /// </summary>
    /// <remarks>
    /// <para>The deafult value is 1.0.</para>
    /// <para>The revision value will always be ignored.</para>
    /// </remarks>
    public Version ContextVersion { get; init; } = new Version(1, 0);

    /// <summary>
    /// Gets the context robustness.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextRobustness.None"/>.</remarks>
    /// <seealso cref="GlfwContextRobustness"/>
    public GlfwContextRobustness ContextRobustness { get; init; } = GlfwContextRobustness.None;

    /// <summary>
    /// Gets the context release behavior.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextReleaseBehavior.Any"/>.</remarks>
    /// <seealso cref="GlfwContextReleaseBehavior"/>
    public GlfwContextReleaseBehavior ContextReleaseBehavior { get; init; } =
        GlfwContextReleaseBehavior.Any;

    /// <summary>
    /// Gets whether the OpenGL context is forward compatible.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool OpenGlForwardCompat { get; init; }

    /// <summary>
    /// Gets whether the window context is in debugging mode.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool ContextDebug { get; init; }

    /// <summary>
    /// Gets the OpenGL profile.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwOpenGlProfile.Any"/>.</remarks>
    /// <seealso cref="GlfwOpenGlProfile"/>
    public GlfwOpenGlProfile OpenGlProfile { get; init; } = GlfwOpenGlProfile.Any;

    /// <summary>
    /// Gets whether the Win32 keyboard menu is in use.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on Windows systems.</para>
    /// </remarks>
    [SupportedOSPlatform("windows")]
    public bool Win32KeyboardMenu { get; init; }

    /// <summary>
    /// Gets whether the Win32 default window should be shown.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on Windows systems.</para>
    /// </remarks>
    [SupportedOSPlatform("windows")]
    public bool Win32ShowDefault { get; init; }

    /// <summary>
    /// Gets the Cocoa frame name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public string CocoaFrameName { get; init; } = string.Empty;

    /// <summary>
    /// Gets whether to enable the Cocoa graphics switching.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaGraphicsSwitching { get; init; }

    /// <summary>
    /// Gets the Wayland app ID.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    public string WaylandAppId { get; init; } = string.Empty;

    /// <summary>
    /// Gets the X11 class name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public string X11ClassName { get; init; } = string.Empty;

    /// <summary>
    /// Gets the X11 instance name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public string X11InstanceName { get; init; } = string.Empty;
}
