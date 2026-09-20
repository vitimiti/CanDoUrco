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
    /// Gets or sets whether the window is resizable.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Resizable { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window is visible.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Visible { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window is decorated.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Decorated { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window is focused.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Focused { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window is automatically iconfied.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool AutoIconify { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window is floating.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Floating { get; set; }

    /// <summary>
    /// Gets or sets whether the window is maximized.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Maximized { get; set; }

    /// <summary>
    /// Gets or sets whether the cursor is centered on the window.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool CenterCursor { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use a transparent framebuffer.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool TransparentFramebuffer { get; set; }

    /// <summary>
    /// Gets or sets whether the window is focused when shown.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool FocusOnShow { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window should be scaled to the monitor.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool ScaleToMonitor { get; set; }

    /// <summary>
    /// Gets or sets whether the window's frambuffer should be scaled.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool ScaleFramebuffer { get; set; } = true;

    /// <summary>
    /// Gets or sets whether the window should support mouse passthrough.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool MousePassthrough { get; set; }

    /// <summary>
    /// Gets or sets the window position.
    /// </summary>
    /// <remarks>The default value is { <see cref="GlfwSpecialValues.AnyPosition"/>, <see cref="GlfwSpecialValues.AnyPosition"/> }.</remarks>
    public Point Position { get; set; } =
        new Point(GlfwSpecialValues.AnyPosition, GlfwSpecialValues.AnyPosition);

    /// <summary>
    /// Gets or sets the window color bits.
    /// </summary>
    /// <remarks>The default value is { 8, 8, 8, 8 }. You may set these values to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public (int Red, int Green, int Blue, int Alpha) ColorBits { get; set; } = (8, 8, 8, 8);

    /// <summary>
    /// Gets or sets the window depth bits.
    /// </summary>
    /// <remarks>The default value is 24. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int DepthBits { get; set; } = 24;

    /// <summary>
    /// Gets or sets the window stencil bits.
    /// </summary>
    /// <remarks>The default value is 8. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int StencilBits { get; set; } = 8;

    /// <summary>
    /// Gets or sets the window accum bits.
    /// </summary>
    /// <remarks>The default value is { 0, 0, 0, 0 }. You may set these values to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public (int Red, int Green, int Blue, int Alpha) AccumBits { get; set; }

    /// <summary>
    /// Gets or sets the window aux buffers.
    /// </summary>
    /// <remarks>The default value is 0. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int AuxBuffers { get; set; }

    /// <summary>
    /// Gets or sets the window samples.
    /// </summary>
    /// <remarks>The default value is 0. You may set this value to <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int Samples { get; set; }

    /// <summary>
    /// Gets or sets the window refresh rate.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwSpecialValues.DontCare"/>.</remarks>
    public int RefreshRate { get; set; } = GlfwSpecialValues.DontCare;

    /// <summary>
    /// Gets or sets whether the window uses stereo.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool Stereo { get; set; }

    /// <summary>
    /// Gets or sets whether the window is SRGB capable.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool SrgbCapable { get; set; }

    /// <summary>
    /// Gets or sets whether the window uses a doublebuffer.
    /// </summary>
    /// <remarks>The default value is <see langword="true"/>.</remarks>
    public bool Doublebuffer { get; set; } = true;

    /// <summary>
    /// Gets or sets the window's client API.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwClientApi.OpenGl"/>.</remarks>
    /// <seealso cref="GlfwClientApi"/>
    public GlfwClientApi ClientApi { get; set; } = GlfwClientApi.OpenGl;

    /// <summary>
    /// Gets or sets the window context creation API.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextCreationApi.Native"/>.</remarks>
    /// <seealso cref="GlfwContextCreationApi"/>
    public GlfwContextCreationApi ContextCreationApi { get; set; } = GlfwContextCreationApi.Native;

    /// <summary>
    /// Gets or sets the context version.
    /// </summary>
    /// <remarks>
    /// <para>The deafult value is 1.0.</para>
    /// <para>The revision value will always be ignored.</para>
    /// </remarks>
    public Version ContextVersion { get; set; } = new Version(1, 0);

    /// <summary>
    /// Gets or sets the context robustness.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextRobustness.None"/>.</remarks>
    /// <seealso cref="GlfwContextRobustness"/>
    public GlfwContextRobustness ContextRobustness { get; set; } = GlfwContextRobustness.None;

    /// <summary>
    /// Gets or sets the context release behavior.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwContextReleaseBehavior.Any"/>.</remarks>
    /// <seealso cref="GlfwContextReleaseBehavior"/>
    public GlfwContextReleaseBehavior ContextReleaseBehavior { get; set; } =
        GlfwContextReleaseBehavior.Any;

    /// <summary>
    /// Gets or sets whether the OpenGL context is forward compatible.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool OpenGlForwardCompat { get; set; }

    /// <summary>
    /// Gets or sets whether the window context is in debugging mode.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool ContextDebug { get; set; }

    /// <summary>
    /// Gets or sets the OpenGL profile.
    /// </summary>
    /// <remarks>The default value is <see cref="GlfwOpenGlProfile.Any"/>.</remarks>
    /// <seealso cref="GlfwOpenGlProfile"/>
    public GlfwOpenGlProfile OpenGlProfile { get; set; } = GlfwOpenGlProfile.Any;

    /// <summary>
    /// Gets or sets whether the Win32 keyboard menu is in use.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on Windows systems.</para>
    /// </remarks>
    [SupportedOSPlatform("windows")]
    public bool Win32KeyboardMenu { get; set; }

    /// <summary>
    /// Gets or sets whether the Win32 default window should be shown.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on Windows systems.</para>
    /// </remarks>
    [SupportedOSPlatform("windows")]
    public bool Win32ShowDefault { get; set; }

    /// <summary>
    /// Gets or sets the Cocoa frame name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public string CocoaFrameName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets whether to enable the Cocoa graphics switching.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see langword="false"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaGraphicsSwitching { get; set; }

    /// <summary>
    /// Gets or sets the Wayland app ID.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    public string WaylandAppId { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X11 class name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public string X11ClassName { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the X11 instance name.
    /// </summary>
    /// <remarks>
    /// <para>The default value is <see cref="string.Empty"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public string X11InstanceName { get; set; } = string.Empty;
}
