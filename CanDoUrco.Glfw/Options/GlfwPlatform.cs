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
