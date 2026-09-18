// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.Versioning;

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The possible supported windowing and input platforms.
/// </summary>
/// <seealso cref="GlfwNativeContextOptions"/>
public enum GlfwPlatform
{
    /// <summary>
    /// Use any available platform.
    /// </summary>
    Any = Native.Glfw.AnyPlatformDefine,

    /// <summary>
    /// Use the Win32 platform.
    /// </summary>
    /// <remarks>This is only supported on Windows systems.</remarks>
    [SupportedOSPlatform("windows")]
    Win32 = Native.Glfw.PlatformWin32Define,

    /// <summary>
    /// Use the Cocoa platform.
    /// </summary>
    /// <remarks>This is only supported on macOS systems.</remarks>
    [SupportedOSPlatform("macos")]
    Cocoa = Native.Glfw.PlatformCocoaDefine,

    /// <summary>
    /// Use the Wayland platform.
    /// </summary>
    /// <remarks>This is only supported on Linux systems.</remarks>
    [SupportedOSPlatform("linux")]
    Wayland = Native.Glfw.PlatformWaylandDefine,

    /// <summary>
    /// Use the X11 platform.
    /// </summary>
    /// <remarks>This is only supported on Linux and FreeBSD systems.</remarks>
    [SupportedOSPlatform("linux")]
    [SupportedOSPlatform("freebsd")]
    X11 = Native.Glfw.PlatformX11Define,

    /// <summary>
    /// Use a stub platform implementation.
    /// </summary>
    Null = Native.Glfw.PlatformNullDefine,
}
