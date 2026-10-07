// A set of graphics utilities for Can Do Urco.
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

namespace CanDoUrco.Graphics.Primitives;

/// <summary>
/// Represents a color with red, green, blue, and alpha components.
/// </summary>
/// <param name="R">The red component of the color, clamped between <c>0</c> and <c>1</c>.</param>
/// <param name="G">The green component of the color, clamped between <c>0</c> and <c>1</c>.</param>
/// <param name="B">The blue component of the color, clamped between <c>0</c> and <c>1</c>.</param>
/// <param name="A">The alpha component of the color, clamped between <c>0</c> and <c>1</c>. The default value is <c>1</c>.</param>
public record struct Color(float R, float G, float B, float A = 1F)
{
    /// <summary>
    /// Gets a color representing white.
    /// </summary>
    public static Color White => FromPackedRgba(0xFFFFFFFF);

    /// <summary>
    /// Gets a color representing black.
    /// </summary>
    public static Color Black => FromPackedRgba(0x000000FF);

    /// <summary>
    /// Gets a color representing a fully transparent color.
    /// </summary>
    public static Color Transparent => FromPackedRgba(0x00000000);

    /// <summary>
    /// Gets a color representing red.
    /// </summary>
    public static Color Red => FromPackedRgba(0xFF0000FF);

    /// <summary>
    /// Gets a color representing green.
    /// </summary>
    public static Color Green => FromPackedRgba(0x00FF00FF);

    /// <summary>
    /// Gets a color representing blue.
    /// </summary>
    public static Color Blue => FromPackedRgba(0x0000FFFF);

    /// <summary>
    /// Gets a color representing yellow.
    /// </summary>
    public static Color Yellow => FromPackedRgba(0xFFFF00FF);

    /// <summary>
    /// Gets a color representing magenta.
    /// </summary>
    public static Color Magenta => FromPackedRgba(0xFF00FFFF);

    /// <summary>
    /// Gets a color representing cyan.
    /// </summary>
    public static Color Cyan => FromPackedRgba(0x00FFFFFF);

    /// <summary>
    /// Gets or sets the red component of the color, clamped between <c>0</c> and <c>1</c>.
    /// </summary>
    public float R
    {
        get;
        set => field = float.Clamp(value, 0F, 1F);
    } = float.Clamp(R, 0F, 1F);

    /// <summary>
    /// Gets or sets the green component of the color, clamped between <c>0</c> and <c>1</c>.
    /// </summary>
    public float G
    {
        get;
        set => field = float.Clamp(value, 0F, 1F);
    } = float.Clamp(G, 0F, 1F);

    /// <summary>
    /// Gets or sets the blue component of the color, clamped between <c>0</c> and <c>1</c>.
    /// </summary>
    public float B
    {
        get;
        set => field = float.Clamp(value, 0F, 1F);
    } = float.Clamp(B, 0F, 1F);

    /// <summary>
    /// Gets or sets the alpha component of the color, clamped between <c>0</c> and <c>1</c>.
    /// </summary>
    public float A
    {
        get;
        set => field = float.Clamp(value, 0F, 1F);
    } = float.Clamp(A, 0F, 1F);

    /// <summary>
    /// Creates a <see cref="Color"/> instance from a packed RGBA value.
    /// </summary>
    /// <param name="rgba">The packed RGBA value.</param>
    /// <returns>A <see cref="Color"/> instance representing the specified RGBA value.</returns>
    public static Color FromPackedRgba(uint rgba) =>
        new(
            ((rgba >> 24) & 0xFF) / 255F,
            ((rgba >> 16) & 0xFF) / 255F,
            ((rgba >> 8) & 0xFF) / 255F,
            (rgba & 0xFF) / 255F
        );

    /// <summary>
    /// Creates a <see cref="Color"/> instance from a packed ARGB value.
    /// </summary>
    /// <param name="argb">The packed ARGB value.</param>
    /// <returns>A <see cref="Color"/> instance representing the specified ARGB value.</returns>
    public static Color FromPackedArgb(uint argb) =>
        new(
            ((argb >> 16) & 0xFF) / 255F,
            ((argb >> 8) & 0xFF) / 255F,
            (argb & 0xFF) / 255F,
            ((argb >> 24) & 0xFF) / 255F
        );

    /// <summary>
    /// Clears the graphics context using the specified clear action.
    /// </summary>
    /// <param name="clearAction">The action to perform for clearing the graphics context, taking the red, green, blue, and alpha components as parameters.</param>
    /// <example>
    /// <code>
    /// color.GraphicsClear(GL.ClearColor);
    /// </code>
    /// </example>
    public readonly void GraphicsClear(Action<float, float, float, float> clearAction) =>
        clearAction(R, G, B, A);

    /// <summary>
    /// Converts the color to an array of its components (red, green, blue, alpha).
    /// </summary>
    /// <returns>An array containing the red, green, blue, and alpha components of the color.</returns>
    public readonly float[] ToArrayRgba() => [R, G, B, A];

    /// <summary>
    /// Converts the color to an array of its components (alpha, red, green, blue).
    /// </summary>
    /// <returns>An array containing the alpha, red, green, and blue components of the color.</returns>
    public readonly float[] ToArrayArgb() => [A, R, G, B];

    /// <summary>
    /// Gets the color component at the specified index.
    /// </summary>
    /// <param name="index">The index of the color component (0 = red, 1 = green, 2 = blue, 3 = alpha).</param>
    /// <returns>The value of the color component at the specified index.</returns>
    public float this[int index]
    {
        readonly get =>
            index switch
            {
                0 => R,
                1 => G,
                2 => B,
                3 => A,
                _ => throw new IndexOutOfRangeException(),
            };
        set
        {
            if (index == 0)
            {
                R = value;
            }
            else if (index == 1)
            {
                G = value;
            }
            else if (index == 2)
            {
                B = value;
            }
            else if (index == 3)
            {
                A = value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
