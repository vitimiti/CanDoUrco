// A common framework for the projects and engines of Can Do Urco
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

using System.Text;
using CanDoUrco.Framework.Native.SDL3;
using CanDoUrco.OpenGL.Generator;

namespace CanDoUrco.Framework.Native.OpenGL;

[OpenGL(
    major: 3,
    minor: 3,
    Profile = OpenGLProfile.Core,
    LoaderMethod = "global::CanDoUrco.Framework.Native.GL.GetProcAddress"
)]
internal static partial class GL
{
    private static unsafe nint GetProcAddress(string procName)
    {
        // 256 bytes should be enough for the procedure name
        // This is still safe thanks to Span ensuring we don't write out of bounds
        var buffer = stackalloc byte[256];
        var bytesWritten = Encoding.UTF8.GetBytes(procName, new Span<byte>(buffer, 255));

        // The slot 256 is never written, so we can write the null terminator safely
        buffer[bytesWritten] = 0;
        return (nint)Sdl.GLGetProcAddress(buffer);
    }
}
