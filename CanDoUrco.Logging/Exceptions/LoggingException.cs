// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

namespace CanDoUrco.Logging.Exceptions;

public class LoggingException : Exception
{
    public LoggingException() { }

    public LoggingException(string? message)
        : base(message) { }

    public LoggingException(string? message, Exception? innerException)
        : base(message, innerException) { }
}
