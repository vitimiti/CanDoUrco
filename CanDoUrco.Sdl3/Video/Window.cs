// An SDL3 interop library with dotnet safety and idioms.
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
using CanDoUrco.Sdl3.Exceptions;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3.Video;

public sealed class Window : IDisposable
{
    private nint _handle;
    private bool _disposedValue;

    public Window(string title, Size size, Action<WindowOptions>? options = null)
    {
        ArgumentNullException.ThrowIfNull(title);
        if (Sdl.WasInit(Sdl.InitVideo) == 0)
        {
            if (!Sdl.Init(Sdl.InitVideo))
            {
                throw new Sdl3Exception("Failed to initialize SDL video subsystem");
            }
        }

        var opts = new WindowOptions();
        options?.Invoke(opts);
        var windowFlags = GetWindowFlags(opts);
        _handle = Sdl.CreateWindow(title, size.Width, size.Height, windowFlags);
        if (_handle == 0)
        {
            throw new Sdl3Exception("Failed to create SDL window");
        }
    }

    ~Window()
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

    private static ulong GetWindowFlags(WindowOptions opts)
    {
        var windowFlags = 0UL;
        if (opts.Fullscreen)
        {
            windowFlags |= Sdl.WindowFullscreen;
        }
        if (opts.OpenGLReady)
        {
            windowFlags |= Sdl.WindowOpenGL;
        }
        if (opts.Resizable)
        {
            windowFlags |= Sdl.WindowResizable;
        }
        if (opts.MouseGrabbed)
        {
            windowFlags |= Sdl.WindowMouseGrabbed;
        }
        if (opts.InputFocus)
        {
            windowFlags |= Sdl.WindowInputFocus;
        }
        if (opts.MouseFocus)
        {
            windowFlags |= Sdl.WindowMouseFocus;
        }
        if (opts.HighPixelDensity)
        {
            windowFlags |= Sdl.WindowHighPixelDensity;
        }
        if (opts.MouseCapture)
        {
            windowFlags |= Sdl.WindowMouseCapture;
        }
        if (opts.MouseRelativeMode)
        {
            windowFlags |= Sdl.WindowMouseRelativeMode;
        }
        if (opts.KeyboardGrabbed)
        {
            windowFlags |= Sdl.WindowKeyboardGrabbed;
        }
        if (opts.VulkanReady)
        {
            windowFlags |= Sdl.WindowVulkan;
        }
        if (opts.MetalReady)
        {
            windowFlags |= Sdl.WindowMetal;
        }
        return windowFlags;
    }

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            // no-op
        }

        if (_handle != 0)
        {
            Sdl.DestroyWindow(_handle);
            _handle = 0;
        }

        if (Sdl.WasInit(Sdl.InitVideo) != 0)
        {
            Sdl.QuitSubSystem(Sdl.InitVideo);
        }

        if (Sdl.WasInit(0) == 0)
        {
            Sdl.Quit();
        }

        _disposedValue = true;
    }
}
