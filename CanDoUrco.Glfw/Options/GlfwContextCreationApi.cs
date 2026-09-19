// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The possible context creation APIs.
/// </summary>
public enum GlfwContextCreationApi
{
    /// <summary>
    /// Use the native context.
    /// </summary>
    Native = Glfw.Native.Glfw.NativeContextApiDefine,

    /// <summary>
    /// Use the EGL context.
    /// </summary>
    Egl = Glfw.Native.Glfw.EglContextApiDefine,

    /// <summary>
    /// Use the OS MESA context.
    /// </summary>
    OsMesa = Glfw.Native.Glfw.OsMesaContextApiDefine,
}
