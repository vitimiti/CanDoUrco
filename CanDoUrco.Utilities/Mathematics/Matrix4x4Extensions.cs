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

using System.Numerics;
using System.Runtime.InteropServices;

namespace CanDoUrco.Utilities.Mathematics;

/// <summary>Conversions of <see cref="Matrix4x4"/> for graphics APIs such as OpenGL.</summary>
public static class Matrix4x4Extensions
{
    /// <summary>Copies the 16 elements in row-major order (M11, M12, ..., M44).</summary>
    /// <param name="matrix">The matrix to copy.</param>
    /// <param name="destination">A span of at least 16 floats.</param>
    public static void CopyTo(this in Matrix4x4 matrix, Span<float> destination) =>
        MemoryMarshal
            .Cast<Matrix4x4, float>(new ReadOnlySpan<Matrix4x4>(in matrix))
            .CopyTo(destination);

    /// <summary>Returns the 16 elements in row-major order as a new array.</summary>
    /// <param name="matrix">The matrix to convert.</param>
    /// <returns>A new array of 16 floats.</returns>
    public static float[] ToArray(this in Matrix4x4 matrix)
    {
        var result = new float[16];
        matrix.CopyTo(result);
        return result;
    }
}
