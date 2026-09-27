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

using System.Runtime.InteropServices;

namespace CanDoUrco.OpenGl.Common.Exceptions;

/// <summary>
/// Represents an exception that occurs during OpenGL operations.
/// </summary>
/// <param name="message">The error message that explains the reason for the exception.</param>
/// <param name="errorCode">The error code associated with the exception.</param>
public sealed class OpenGlException(string? message, int errorCode = 0)
    : ExternalException(message, errorCode);
