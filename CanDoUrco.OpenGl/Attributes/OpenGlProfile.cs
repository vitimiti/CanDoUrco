// A generator to import required OpenGL and its extensions.
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

namespace CanDoUrco.OpenGl.Attributes;

/// <summary>
/// The OpenGL profile.
/// </summary>
/// <seealso cref="OpenGlSpecificationAttribute"/>
public enum OpenGlProfile
{
    /// <summary>
    /// Compatibility mode.
    /// </summary>
    Compat,

    /// <summary>
    /// Core mode.
    /// </summary>
    Core,
}
