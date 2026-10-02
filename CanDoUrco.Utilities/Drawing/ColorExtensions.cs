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

using System.Drawing;
using System.Numerics;

namespace CanDoUrco.Utilities.Drawing;

/// <summary>Conversions and utilities for <see cref="Color"/>.</summary>
public static class ColorExtensions
{
    /// <summary>Extension methods for <see cref="Color"/>.</summary>
    extension(Color color)
    {
        /// <summary>Returns the color components normalized to the range <c>[0, 1]</c>.</summary>
        /// <returns>A <see cref="Vector4"/> containing the normalized RGBA components.</returns>
        public Vector4 ToNormalized() =>
            new(color.R / 255F, color.G / 255F, color.B / 255F, color.A / 255F);

        /// <summary>Returns the RGB components normalized to the range <c>[0, 1]</c>.</summary>
        /// <returns>A <see cref="Vector3"/> containing the normalized RGB components.</returns>
        public Vector3 ToNormalizedRgb() => new(color.R / 255F, color.G / 255F, color.B / 255F);
    }
}
