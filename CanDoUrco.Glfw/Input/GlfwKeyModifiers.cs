// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// The possible key modifiers.
/// </summary>
[Flags]
public enum GlfwKeyModifiers
{
    /// <summary>
    /// No modifier keys are being held down.
    /// </summary>
    None = 0,

    /// <summary>
    /// If this is set, one ore more Shift keys were held down.
    /// </summary>
    Shift = 0x0001,

    /// <summary>
    /// If this is set, one ore more Control keys were held down.
    /// </summary>
    Control = 0x0002,

    /// <summary>
    /// If this is set, one ore more Alt keys were held down.
    /// </summary>
    Alt = 0x0004,

    /// <summary>
    /// If this is set, one ore more Super keys were held down.
    /// </summary>
    Super = 0x0008,

    /// <summary>
    /// If this is set, one ore more CapsLock keys were held down.
    /// </summary>
    CapsLock = 0x0010,

    /// <summary>
    /// If this is set, one ore more NumLock keys were held down.
    /// </summary>
    NumLock = 0x0020,
}
