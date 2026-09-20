// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
