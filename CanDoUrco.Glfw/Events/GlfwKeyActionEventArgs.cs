// This is a small testing ground for CanDoUrco.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

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
