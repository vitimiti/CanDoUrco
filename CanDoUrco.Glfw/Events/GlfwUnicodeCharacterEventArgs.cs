// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when a unicode character has been typed.
/// </summary>
/// <param name="codepoint">The Unicode codepoint.</param>
public sealed class GlfwUnicodeCharacterEventArgs(uint codepoint) : EventArgs
{
    /// <summary>
    /// Gets the Unicode codepoint.
    /// </summary>
    public uint Codepoint { get; init; } = codepoint;
}
