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
/// Represents a three-dimensional vector with X, Y, and Z components.
/// </summary>
/// <param name="X">The X component of the vector.</param>
/// <param name="Y">The Y component of the vector.</param>
/// <param name="Z">The Z component of the vector.</param>
public record struct Vector3(float X, float Y, float Z)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Vector3"/> struct with the specified components.
    /// </summary>
    /// <param name="X">The X component of the vector.</param>
    /// <param name="Y">The Y component of the vector.</param>
    /// <param name="Z">The Z component of the vector.</param>
    public static Vector3 Zero => new(0F, 0F, 0F);

    /// <summary>
    /// Gets a vector with all components set to one.
    /// </summary>
    public static Vector3 One => new(1F, 1F, 1F);

    /// <summary>
    /// Gets a vector with the X component set to one and the others set to zero.
    /// </summary>
    public static Vector3 UnitX => new(1F, 0F, 0F);

    /// <summary>
    /// Gets a vector with the Y component set to one and the others set to zero.
    /// </summary>
    public static Vector3 UnitY => new(0F, 1F, 0F);

    /// <summary>
    /// Gets a vector with the Z component set to one and the others set to zero.
    /// </summary>
    public static Vector3 UnitZ => new(0F, 0F, 1F);

    /// <summary>
    /// Creates a new <see cref="Vector3"/> instance from an array of floats.
    /// </summary>
    /// <param name="array">The array containing the X, Y, and Z components.</param>
    /// <returns>A new <see cref="Vector3"/> instance.</returns>
    public static Vector3 FromArray(ReadOnlySpan<float> array)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(array.Length, 3);
        return new Vector3(array[0], array[1], array[2]);
    }

    /// <summary>
    /// Converts the vector to an array of floats.
    /// </summary>
    /// <returns>An array containing the X, Y, and Z components of the vector.</returns>
    public readonly float[] ToArray() => [X, Y, Z];

    /// <summary>
    /// Gets or sets the component of the vector at the specified index.
    /// </summary>
    /// <param name="index">The index of the component (0 for X, 1 for Y, 2 for Z).</param>
    /// <returns>The value of the component at the specified index.</returns>
    public float this[int index]
    {
        readonly get =>
            index switch
            {
                0 => X,
                1 => Y,
                2 => Z,
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
            else if (index == 2)
            {
                Z = value;
            }
            else
            {
                throw new IndexOutOfRangeException();
            }
        }
    }
}
