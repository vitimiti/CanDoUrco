// Shared utilities for CanDoUrco.
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

using System.Drawing;

namespace CanDoUrco.Utilities.Drawing;

/// <summary>Conversions and utilities for <see cref="Color"/>.</summary>
public static class ColorExtensions
{
    /// <summary>Extension methods for <see cref="Color"/>.</summary>
    extension(Color color)
    {
        /// <summary>Clears the graphics using the color's RGBA components normalized to the range <c>[0, 1]</c>.</summary>
        /// <param name="graphicsClearingAction">The action to perform the graphics clearing.</param>
        public void ClearGraphics(Action<float, float, float, float> graphicsClearingAction) =>
            graphicsClearingAction(color.R / 255F, color.G / 255F, color.B / 255F, color.A / 255F);
    }
}
