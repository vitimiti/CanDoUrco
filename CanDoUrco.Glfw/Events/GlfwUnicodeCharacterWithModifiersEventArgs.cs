// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
