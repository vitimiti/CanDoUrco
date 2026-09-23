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
/// An attribute to indicate an extension desired during generation.
/// </summary>
/// <param name="extension">The extension to generate code for.</param>
/// <remarks>Each extension will have its own attribute, and adding lists either space separated, comma separated or otherwise is not supported and will be treated as the full name of the extension.</remarks>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
public sealed class OpenGlExtensionAttribute(string extension) : Attribute
{
    /// <summary>
    /// Gets or sets the OpenGL extension required.
    /// </summary>
    public string Extension { get; set; } = extension;
}
