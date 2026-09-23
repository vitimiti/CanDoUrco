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

namespace CanDoUrco.OpenGl.Common.Attributes;

/// <summary>
/// An attribute that goes on a partial class to indicate what version and OpenGL mode to use.
/// </summary>
/// <param name="version">The major and minor versions of the OpenGL specification, in the form <c>"Major.Minor"</c>.</param>
/// <param name="profile">The <see cref="OpenGlProfile"/> for the given OpenGL specification.</param>
/// <remarks>The <paramref name="profile"/> will be ignored on versions that don't have it. Its value is <see cref="OpenGlProfile.Compat"/> by default.</remarks>
/// <seealso cref="OpenGlProfile"/>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public sealed class OpenGlSpecificationAttribute(
    string version,
    OpenGlProfile profile = OpenGlProfile.Compat
) : Attribute
{
    /// <summary>
    /// Gets or sets the OpenGL specification version.
    /// </summary>
    public string Version { get; set; } = version;

    /// <summary>
    /// Gets or sets the OpenGL profile mode.
    /// </summary>
    public OpenGlProfile Profile { get; set; } = profile;
}
