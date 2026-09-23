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
/// An event that happens when a unicode character has been typed with modifiers.
/// </summary>
/// <param name="codepoint">The Unicode codepoint.</param>
/// <param name="modifiers">The key modifiers.</param>
public sealed class GlfwUnicodeCharacterWithModifiersEventArgs(
    uint codepoint,
    GlfwKeyModifiers modifiers
) : EventArgs
{
    /// <summary>
    /// Gets the Unicode codepoint.
    /// </summary>
    public uint Codepoint { get; init; } = codepoint;

    /// <summary>
    /// Gets the key modifiers.
    /// </summary>
    public GlfwKeyModifiers Modifiers { get; init; } = modifiers;
}
