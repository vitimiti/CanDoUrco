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
/// Represents a 4x4 matrix.
/// </summary>
/// <param name="M11">The value at the first row and first column.</param>
/// <param name="M12">The value at the first row and second column.</param>
/// <param name="M13">The value at the first row and third column.</param>
/// <param name="M14">The value at the first row and fourth column.</param>
/// <param name="M21">The value at the second row and first column.</param>
/// <param name="M22">The value at the second row and second column.</param>
/// <param name="M23">The value at the second row and third column.</param>
/// <param name="M24">The value at the second row and fourth column.</param>
/// <param name="M31">The value at the third row and first column.</param>
/// <param name="M32">The value at the third row and second column.</param>
/// <param name="M33">The value at the third row and third column.</param>
/// <param name="M34">The value at the third row and fourth column.</param>
/// <param name="M41">The value at the fourth row and first column.</param>
/// <param name="M42">The value at the fourth row and second column.</param>
/// <param name="M43">The value at the fourth row and third column.</param>
/// <param name="M44">The value at the fourth row and fourth column.</param>
public record struct Matrix4x4(
    float M11,
    float M12,
    float M13,
    float M14,
    float M21,
    float M22,
    float M23,
    float M24,
    float M31,
    float M32,
    float M33,
    float M34,
    float M41,
    float M42,
    float M43,
    float M44
)
{
    /// <summary>
    /// Gets the identity matrix.
    /// </summary>
    public static Matrix4x4 Identity =>
        new(1F, 0F, 0F, 0F, 0F, 1F, 0F, 0F, 0F, 0F, 1F, 0F, 0F, 0F, 0F, 1F);

    /// <summary>
    /// Gets the zero matrix.
    /// </summary>
    public static Matrix4x4 Zero =>
        new(0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F, 0F);

    /// <summary>
    /// Creates a matrix from a one-dimensional array.
    /// </summary>
    /// <param name="array">The array containing the matrix elements.</param>
    /// <returns>A new Matrix4x4 instance.</returns>
    public static Matrix4x4 FromArray(ReadOnlySpan<float> array)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(array.Length, 16);
        return new Matrix4x4(
            array[0],
            array[1],
            array[2],
            array[3],
            array[4],
            array[5],
            array[6],
            array[7],
            array[8],
            array[9],
            array[10],
            array[11],
            array[12],
            array[13],
            array[14],
            array[15]
        );
    }

    /// <summary>
    /// Creates a matrix from a two-dimensional array.
    /// </summary>
    /// <param name="array">The 2D array containing the matrix elements.</param>
    /// <returns>A new Matrix4x4 instance.</returns>
    public static Matrix4x4 From2DArray(float[,] array)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.Rank, 2);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.Length, 4);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.GetLength(0), 4);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.GetLength(1), 4);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.GetLength(2), 4);
        ArgumentOutOfRangeException.ThrowIfLessThan(array.GetLength(3), 4);
        return new Matrix4x4(
            array[0, 0],
            array[0, 1],
            array[0, 2],
            array[0, 3],
            array[1, 0],
            array[1, 1],
            array[1, 2],
            array[1, 3],
            array[2, 0],
            array[2, 1],
            array[2, 2],
            array[2, 3],
            array[3, 0],
            array[3, 1],
            array[3, 2],
            array[3, 3]
        );
    }

    /// <summary>
    /// Converts the matrix to a one-dimensional array.
    /// </summary>
    /// <returns>A one-dimensional array containing the matrix elements.</returns>
    public readonly float[] ToArray() =>
        [M11, M12, M13, M14, M21, M22, M23, M24, M31, M32, M33, M34, M41, M42, M43, M44];

    /// <summary>
    /// Converts the matrix to a two-dimensional array.
    /// </summary>
    /// <returns>A two-dimensional array containing the matrix elements.</returns>
    public readonly float[,] To2DArray() =>
        new float[,]
        {
            { M11, M12, M13, M14 },
            { M21, M22, M23, M24 },
            { M31, M32, M33, M34 },
            { M41, M42, M43, M44 },
        };

    /// <summary>
    /// Gets or sets the element at the specified linear index.
    /// </summary>
    /// <param name="index">The zero-based linear index.</param>
    /// <returns>The value at the specified linear index.</returns>
    public float this[int index]
    {
        readonly get => this[index / 4, index % 4];
        set => this[index / 4, index % 4] = value;
    }

    /// <summary>
    /// Gets or sets the element at the specified row and column.
    /// </summary>
    /// <param name="row">The zero-based row index.</param>
    /// <param name="column">The zero-based column index.</param>
    /// <returns>The value at the specified row and column.</returns>
    public float this[int row, int column]
    {
        readonly get =>
            (row, column) switch
            {
                (0, 0) => M11,
                (0, 1) => M12,
                (0, 2) => M13,
                (0, 3) => M14,
                (1, 0) => M21,
                (1, 1) => M22,
                (1, 2) => M23,
                (1, 3) => M24,
                (2, 0) => M31,
                (2, 1) => M32,
                (2, 2) => M33,
                (2, 3) => M34,
                (3, 0) => M41,
                (3, 1) => M42,
                (3, 2) => M43,
                (3, 3) => M44,
                _ => throw new IndexOutOfRangeException(),
            };
        set
        {
            switch (row, column)
            {
                case (0, 0):
                    M11 = value;
                    break;
                case (0, 1):
                    M12 = value;
                    break;
                case (0, 2):
                    M13 = value;
                    break;
                case (0, 3):
                    M14 = value;
                    break;
                case (1, 0):
                    M21 = value;
                    break;
                case (1, 1):
                    M22 = value;
                    break;
                case (1, 2):
                    M23 = value;
                    break;
                case (1, 3):
                    M24 = value;
                    break;
                case (2, 0):
                    M31 = value;
                    break;
                case (2, 1):
                    M32 = value;
                    break;
                case (2, 2):
                    M33 = value;
                    break;
                case (2, 3):
                    M34 = value;
                    break;
                case (3, 0):
                    M41 = value;
                    break;
                case (3, 1):
                    M42 = value;
                    break;
                case (3, 2):
                    M43 = value;
                    break;
                case (3, 3):
                    M44 = value;
                    break;
                default:
                    throw new IndexOutOfRangeException();
            }
        }
    }
}
