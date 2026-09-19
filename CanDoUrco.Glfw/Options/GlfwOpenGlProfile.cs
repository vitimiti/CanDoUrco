// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The possible OpenGL profiles.
/// </summary>
public enum GlfwOpenGlProfile
{
    /// <summary>
    /// Use any available profile.
    /// </summary>
    Any = Native.Glfw.OpenGlAnyProfileDefine,

    /// <summary>
    /// Use the compatibility profile.
    /// </summary>
    Compat = Native.Glfw.OpenGlCompatProfileDefine,

    /// <summary>
    /// Use the core profile.
    /// </summary>
    Core = Native.Glfw.OpenGlCoreProfileDefine,
}
