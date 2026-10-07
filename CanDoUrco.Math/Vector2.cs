// A set of math utilities for Can Do Urco.
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

namespace CanDoUrco.Math;

/// <summary>
/// Represents a two-dimensional vector with X and Y components.
/// </summary>
/// <param name="X">The X component of the vector.</param>
/// <param name="Y">The Y component of the vector.</param>
public record struct Vector2(float X, float Y)
{
    /// <summary>
    /// Gets a vector with both components set to zero.
    /// </summary>
    public static Vector2 Zero => new(0F, 0F);

    /// <summary>
    /// Gets a vector with both components set to one.
    /// </summary>
    public static Vector2 One => new(1F, 1F);

    /// <summary>
    /// Gets a vector with the X component set to one and the Y component set to zero.
    /// </summary>
    public static Vector2 UnitX => new(1F, 0F);

    /// <summary>
    /// Gets a vector with the X component set to zero and the Y component set to one.
    /// </summary>
    public static Vector2 UnitY => new(0F, 1F);

    /// <summary>
    /// Creates a <see cref="Vector2"/> instance from an array of components.
    /// </summary>
    /// <param name="array">An array containing the X and Y components of the vector.</param>
    /// <returns>A <see cref="Vector2"/> instance representing the specified components.</returns>
    public static Vector2 FromArray(ReadOnlySpan<float> array)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(array.Length, 2);
        return new Vector2(array[0], array[1]);
    }

    /// <summary>
    /// Converts the vector to an array of its components (X, Y).
    /// </summary>
    /// <returns>An array containing the X and Y components of the vector.</returns>
    public readonly float[] ToArray() => [X, Y];

    /// <summary>
    /// Gets the component of the vector at the specified index.
    /// </summary>
    /// <param name="index">The index of the component (0 = X, 1 = Y).</param>
    /// <returns>The value of the component at the specified index.</returns>
    public float this[int index]
    {
        readonly get =>
            index switch
            {
                0 => X,
                1 => Y,
                _ => throw new IndexOutOfRangeException(),
            };
        set
        {
            if (index == 0)
            {
                X = value;
            }
            else if (index == 1)
            {
                Y = value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
