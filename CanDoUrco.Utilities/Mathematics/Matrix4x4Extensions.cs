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
    /// <summary>Extension methods for <see cref="Matrix4x4"/>.</summary>
    extension(Matrix4x4 matrix)
    {
        /// <summary>Returns the 16 elements in row-major order as a new array.</summary>
        /// <returns>A new array of 16 floats.</returns>
        public float[] ToArray()
        {
            var result = new float[16];
            MemoryMarshal
                .Cast<Matrix4x4, float>(new ReadOnlySpan<Matrix4x4>(in matrix))
                .CopyTo(result);

            return result;
        }
    }
}
