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

using System.Runtime.InteropServices;
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
    /// Gets or sets the windowing and input platform.
    /// </summary>
    /// <remarks>The default is <see cref="GlfwPlatform.Any"/>.</remarks>
    /// <seealso cref="GlfwPlatform"/>
    public GlfwPlatform Platform { get; set; } = GlfwPlatform.Any;

    /// <summary>
    /// Gets or sets whether to expose the joystick hats as buttons.
    /// </summary>
    /// <remarks>The default is <see langword="true"/>.
    public bool ExposeJoystickHatsAsButtons { get; set; } = true;

    /// <summary>
    /// Gets or sets the ANGLE platform to use.
    /// </summary>
    /// <remarks>The default is <see cref="GlfwAnglePlatformType.None"/>.
    /// <seealso cref="GlfwAnglePlatformType"/>
    public GlfwAnglePlatformType AnglePlatformType { get; set; } = GlfwAnglePlatformType.None;

    /// <summary>
    /// Gets or sets whether to set the current directory to the application to the <c>Contents/Resources</c> subdirectory of the application's bundle, if present.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaChangeDirectoryToResources { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to create the menu bar and dock icon during initialization.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on macOS systems.</para>
    /// </remarks>
    [SupportedOSPlatform("macos")]
    public bool CocoaCreateMenuBar { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use <see href="https://gitlab.freedesktop.org/libdecor/libdecor">libdecor</see> for window decorations where avilable.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on Linux systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    public bool WaylandPreferLibDecor { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to prefer <c>VK_KHR_xcb_surface</c> extension for creating Vulkan surfaces instead of <c>VK_KHR_xlib_surface</c> extension.
    /// </summary>
    /// <remarks>
    /// <para>The default is <see langword="true"/>.</para>
    /// <para>This is only supported on Linux and FreeBSD systems.</para>
    /// </remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    public bool X11XcbVulkanSurfaces { get; set; } = true;

    /// <summary>
    /// Gets or sets whether to use the <see cref="NativeMemory"/> methods for managing native memory instead of GLFW's defaults.
    /// </summary>
    /// <remarks>The default value is <see langword="false"/>.</remarks>
    public bool UseDotnetAsCustomAllocator { get; set; }
}
