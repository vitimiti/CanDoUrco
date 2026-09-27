// Common utilities for OpenGL importing and generation.
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

using CanDoUrco.OpenGl.Common.Exceptions;

namespace CanDoUrco.OpenGl.Common.Utilities;

/// <summary>
/// Provides utility methods for handling OpenGL errors.
/// </summary>
public static class Error
{
    /// <summary>
    /// Throws an AggregateException if any OpenGL errors are detected after an API call.
    /// </summary>
    /// <param name="getErrorFunc">The function to retrieve the current OpenGL error code.</param>
    /// <param name="message">The error message to include in the exception if an error occurs.</param>
    /// <param name="noErrorCode">The OpenGL error code that indicates no error.</param>
    /// <exception cref="AggregateException"></exception>
    public static void PerApiCallErrorThrower(
        Func<uint> getErrorFunc,
        string message,
        uint noErrorCode
    )
    {
        List<Exception> exceptions = [];
        var code = getErrorFunc();
        while (code != noErrorCode)
        {
            exceptions.Add(new OpenGlException(message, unchecked((int)code)));
            code = getErrorFunc();
        }

        if (exceptions.Count > 0)
        {
            throw new AggregateException(exceptions);
        }
    }
}
