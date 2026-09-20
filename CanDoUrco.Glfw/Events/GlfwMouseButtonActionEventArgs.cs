// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CanDoUrco.Glfw.Input;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a mouse button is pressed.
/// </summary>
/// <param name="mouseButton">The <see cref="GlfwMouseButton"/> that started the event.</param>
/// <param name="action">The <see cref="GlfwMouseButtonAction"/> from the <paramref name="mouseButton"/> that started the event.</param>
/// <param name="modifiers">The <see cref="GlfwKeyModifiers"/> pressed at the same time as the <paramref name="mouseButton"/> that started the event.</param>
public sealed class GlfwMouseButtonActionEventArgs(
    GlfwMouseButton mouseButton,
    GlfwMouseButtonAction action,
    GlfwKeyModifiers modifiers
) : EventArgs
{
    /// <summary>
    /// Gets the <see cref="GlfwMouseButton"/> that started the event.
    /// </summary>
    public GlfwMouseButton Key { get; init; } = mouseButton;

    /// <summary>
    /// Gets the <see cref="GlfwMouseButtonAction"/> of the key.
    /// </summary>
    public GlfwMouseButtonAction Action { get; init; } = action;

    /// <summary>
    /// Gets the <see cref="GlfwKeyModifiers"/> of the mouse buttons.
    /// </summary>
    public GlfwKeyModifiers Modifiers { get; init; } = modifiers;
}
