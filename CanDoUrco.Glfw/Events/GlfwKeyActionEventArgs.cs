// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using CanDoUrco.Glfw.Input;

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a keyboard key is pressed.
/// </summary>
/// <param name="key">The <see cref="GlfwKey"/> that started the event.</param>
/// <param name="scancode">The scancode of the <paramref name="key"/> that started the event.</param>
/// <param name="action">The <see cref="GlfwKeyAction"/> from the <paramref name="key"/> that started the event.</param>
/// <param name="modifiers">The <see cref="GlfwKeyModifiers"/> pressed at the same time as the <paramref name="key"/> that started the event.</param>
public sealed class GlfwKeyActionEventArgs(
    GlfwKey key,
    int scancode,
    GlfwKeyAction action,
    GlfwKeyModifiers modifiers
) : EventArgs
{
    /// <summary>
    /// Gets the <see cref="GlfwKey"/> that started the event.
    /// </summary>
    public GlfwKey Key { get; init; } = key;

    /// <summary>
    /// Gets the scancode of the key.
    /// </summary>
    public int Scancode { get; init; } = scancode;

    /// <summary>
    /// Gets the <see cref="GlfwKeyAction"/> of the key.
    /// </summary>
    public GlfwKeyAction Action { get; init; } = action;

    /// <summary>
    /// Gets the <see cref="GlfwKeyModifiers"/> of the key.
    /// </summary>
    public GlfwKeyModifiers Modifiers { get; init; } = modifiers;
}
