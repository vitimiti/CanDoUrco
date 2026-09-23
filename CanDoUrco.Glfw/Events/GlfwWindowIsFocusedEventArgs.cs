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
