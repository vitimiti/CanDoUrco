// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.Versioning;

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The possible <see cref="GlfwNativeContext"/> options.
/// </summary>
/// <remarks>This record is sealed.</remarks>
/// <seealso cref="GlfwNativeContext"/>
public sealed record GlfwNativeContextOptions
{
    /// <summary>
    /// The windowing and input platform.
    /// </summary>
    /// <remarks>The default is <see cref="GlfwPlatform.Any"/>.</remarks>
    /// <seealso cref="GlfwPlatform"/>
    public GlfwPlatform Platform { get; init; } = GlfwPlatform.Any;

    /// <summary>
    /// Whether to expose the joystick hats as buttons.
    /// </summary>
    /// <remarks>The default is <see langword="true"/>.
    public bool ExposeJoystickHatsAsButtons { get; init; } = true;

    /// <summary>
    /// The ANGLE platform to use.
    /// </summary>
    /// <remarks>The default is <see cref="GlfwAnglePlatformType.None"/>.
    /// <seealso cref="GlfwAnglePlatformType"/>
    public GlfwAnglePlatformType AnglePlatformType { get; init; } = GlfwAnglePlatformType.None;

    /// <summary>
    /// Whether to set the current directory to the application to the <c>Contents/Resources</c> subdirectory of the application's bundle, if present.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaChangeDirectoryToResources { get; init; } = true;

    /// <summary>
    /// Whether to create the menu bar and dock icon during initialization.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaCreateMenuBar { get; init; } = true;

    /// <summary>
    /// Whether to use <see href="https://gitlab.freedesktop.org/libdecor/libdecor">libdecor</see> for window decorations where avilable.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on Linux systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    public bool WaylandPreferLibDecor { get; init; } = true;

    /// <summary>
    /// Whether to prefer <c>VK_KHR_xcb_surface</c> extension for creating Vulkan surfaces instead of <c>VK_KHR_xlib_surface</c> extension.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public bool X11XcbVulkanSurfaces { get; init; } = true;

    /// <summary>
    /// Whether to use the <see cref="NativeMemory"/> methods for managing native memory instead of GLFW's defaults.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool UseDotnetAsCustomAllocator { get; init; }
}
