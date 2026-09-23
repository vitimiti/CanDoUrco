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
/// The possible ANGLE platform types.
/// </summary>
/// <seealso cref="GlfwNativeContextOptions"/>
public enum GlfwAnglePlatformType
{
    /// <summary>
    /// No specified ANGLE platform type.
    /// </summary>
    None = Native.Glfw.AnglePlatformTypeNoneDefine,

    /// <summary>
    /// Request ANGLE to use OpenGL.
    /// </summary>
    OpenGl = Native.Glfw.AnglePlatformTypeOpenGlDefine,

    /// <summary>
    /// Request ANGLE to use OpenGLES.
    /// </summary>
    OpenGlEs = Native.Glfw.AnglePlatformTypeOpenGlEsDefine,

    /// <summary>
    /// Request ANGLE to use D3D9.
    /// </summary>
    /// <remarks>This is only supported on Windows systems.</remarks>
    [SupportedOSPlatform("windows")]
    D3D9 = Native.Glfw.AnglePlatformTypeD3D9Define,

    /// <summary>
    /// Request ANGLE to use D3D11.
    /// </summary>
    /// <remarks>This is only supported on Windows systems.</remarks>
    [SupportedOSPlatform("windows")]
    D3D11 = Native.Glfw.AnglePlatformTypeD3D11Define,

    /// <summary>
    /// Request ANGLE to use Vulkan.
    /// </summary>
    Vulkan = Native.Glfw.AnglePlatformTypeVulkanDefine,

    /// <summary>
    /// Request ANGLE to use Metal.
    /// </summary>
    /// <remarks>This is only supported on macOS systems.</remarks>
    [SupportedOSPlatform("macos")]
    Metal = Native.Glfw.AnglePlatformTypeMetalDefine,
}
