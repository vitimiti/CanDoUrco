// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
