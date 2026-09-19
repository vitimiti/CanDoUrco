// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The context robustness modes.
/// </summary>
public enum GlfwContextRobustness
{
    /// <summary>
    /// No context robustuness.
    /// </summary>
    None = Native.Glfw.NoRobustnessDefine,

    /// <summary>
    /// Don't notify on reset.
    /// </summary>
    NoResetNotification = Native.Glfw.NoResetNotificationDefine,

    /// <summary>
    /// Lose the context on reset.
    /// </summary>
    LoseContextOnReset = Native.Glfw.LoseContextOnResetDefine,
}
