// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The graphics client APIs.
/// </summary>
public enum GlfwClientApi
{
    /// <summary>
    /// No graphics API.
    /// </summary>
    None = Native.Glfw.NoApiDefine,

    /// <summary>
    /// Use the OpenGL API.
    /// </summary>
    OpenGl = Native.Glfw.OpenGlApiDefine,

    /// <summary>
    /// Use the OpenGLES API.
    /// </summary>
    OpenGlEs = Native.Glfw.OpenGlEsApiDefine,
}
