// This is a small testing ground for CanDoUrco.
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

using System.Drawing;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw.Input;

public sealed class GlfwCursor : IDisposable
{
    private bool _disposedValue;

    internal unsafe Native.Glfw.Cursor* Handle { get; private init; }

    public unsafe GlfwCursor(GlfwImage image, Point hotspot)
    {
        ArgumentNullException.ThrowIfNull(image);
        var nativeImage = image.ConvertToUnmanaged();
        Handle = Native.Glfw.CreateCursor(in nativeImage, hotspot.X, hotspot.Y);
        if (Handle is null)
        {
            GlfwErrorUtilities.ThrowError();
        }
    }

    public unsafe GlfwCursor(GlfwStandardCursorShape shape)
    {
        Handle = Native.Glfw.CreateStandardCursor((int)shape);
        if (Handle is null)
        {
            GlfwErrorUtilities.ThrowError();
        }
    }

    ~GlfwCursor()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    private unsafe void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            // no-op
        }

        Native.Glfw.DestroyCursor(Handle);
        _disposedValue = true;
    }
}
