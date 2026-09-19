// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a window is focused or unfocused.
/// </summary>
/// <param name="focused">Whether the window was focused.</param>
public sealed class GlfwWindowIsFocusedEventArgs(bool focused) : EventArgs
{
    /// <summary>
    /// Gets whether the window is focused.
    /// </summary>
    public bool Focused { get; init; } = focused;
}
