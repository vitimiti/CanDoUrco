// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The context release behavior.
/// </summary>
public enum GlfwContextReleaseBehavior
{
    /// <summary>
    /// Use any available release method.
    /// </summary>
    Any = Native.Glfw.AnyReleaseBehaviorDefine,

    /// <summary>
    /// Flush on release.
    /// </summary>
    Flush = Native.Glfw.ReleaseBehaviorFlushDefine,

    /// <summary>
    /// Don't do anything on release.
    /// </summary>
    None = Native.Glfw.ReleaseBehaviorNoneDefine,
}
