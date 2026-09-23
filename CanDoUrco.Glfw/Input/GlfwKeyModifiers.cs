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
