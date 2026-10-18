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

using CanDoUrco.Framework.Native.OpenGL;
using CanDoUrco.Framework.Native.SDL3;
using CanDoUrco.Framework.Options;

namespace CanDoUrco.Framework.Video;

public sealed class Window : IDisposable
{
    private unsafe Sdl.Window* _window;
    private unsafe Sdl.GLContext* _glContext;

    private bool _disposedValue;

    public unsafe string Title
    {
        get
        {
            ObjectDisposedException.ThrowIf(_disposedValue, this);
            return Sdl.GetWindowTitle(_window) ?? string.Empty;
        }
        set
        {
            ObjectDisposedException.ThrowIf(_disposedValue, this);
            Sdl.SetWindowTitle(_window, value);
        }
    }

    internal unsafe Window(WindowOptions options)
    {
        var flags = GetWindowFlags(options);
        try
        {
            _window = Sdl.CreateWindow(
                options.Title,
                options.Size.Width,
                options.Size.Height,
                flags
            );

            if (_window is null)
            {
                throw new InvalidOperationException(
                    $"Failed to create SDL window: {Sdl.GetError()}."
                );
            }

            if (
                !Sdl.GLSetAttribute(Sdl.GLAttr.ContextMajorVersion, 3)
                || !Sdl.GLSetAttribute(Sdl.GLAttr.ContextMinorVersion, 3)
                || !Sdl.GLSetAttribute(Sdl.GLAttr.ContextProfileMask, (int)Sdl.GLContextProfileCore)
            )
            {
                throw new InvalidOperationException(
                    $"Failed to set SDL GL attributes: {Sdl.GetError()}."
                );
            }

            _glContext = Sdl.GLCreateContext(_window);
            if (_glContext is null)
            {
                throw new InvalidOperationException(
                    $"Failed to create SDL GL context: {Sdl.GetError()}."
                );
            }

            if (!Sdl.GLMakeCurrent(_window, _glContext))
            {
                throw new InvalidOperationException(
                    $"Failed to make SDL GL context current: {Sdl.GetError()}."
                );
            }

            if (!Sdl.GLSetSwapInterval((int)options.SwapInterval))
            {
                throw new InvalidOperationException(
                    $"Failed to set SDL GL swap interval: {Sdl.GetError()}."
                );
            }

            GL.ClearColor(
                options.BackgroundColor.R / 255F,
                options.BackgroundColor.G / 255F,
                options.BackgroundColor.B / 255F,
                options.BackgroundColor.A / 255F
            );
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public unsafe void Dispose()
    {
        if (_disposedValue)
        {
            return;
        }
        if (_glContext is not null)
        {
            Sdl.GLDestroyContext(_glContext);
            _glContext = null;
        }

        if (_window is not null)
        {
            Sdl.DestroyWindow(_window);
            _window = null;
        }

        _disposedValue = true;
    }

    public unsafe void GrabMouse()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        try
        {
            if (!Sdl.SetWindowMouseGrab(_window, true))
            {
                throw new InvalidOperationException($"Failed to grab mouse: {Sdl.GetError()}.");
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public unsafe void ReleaseMouse()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        try
        {
            if (!Sdl.SetWindowMouseGrab(_window, false))
            {
                throw new InvalidOperationException($"Failed to release mouse: {Sdl.GetError()}.");
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public unsafe void Flash()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        try
        {
            if (!Sdl.FlashWindow(_window, Sdl.FlashOperation.UntilFocused))
            {
                throw new InvalidOperationException($"Failed to flash window: {Sdl.GetError()}.");
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public unsafe void SetProgressState(WindowProgressState state)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        try
        {
            if (!Sdl.SetWindowProgressState(_window, state))
            {
                throw new InvalidOperationException(
                    $"Failed to set window progress state: {Sdl.GetError()}."
                );
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    public unsafe void SetProgressValue(float value)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        try
        {
            if (!Sdl.SetWindowProgressValue(_window, value))
            {
                throw new InvalidOperationException(
                    $"Failed to set window progress value: {Sdl.GetError()}."
                );
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    internal unsafe void StartFrame()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit | GL.ClearBufferMaskEnum.DepthBufferBit);
    }

    internal unsafe void EndFrame()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Sdl.GLSwapWindow(_window);
    }

    private static ulong GetWindowFlags(WindowOptions options)
    {
        var flags = Sdl.WindowHighPixelDensity | Sdl.WindowOpengl;
        if (options.IsResizable)
        {
            flags |= Sdl.WindowResizable;
        }
        if (options.IsMaximized)
        {
            flags |= Sdl.WindowMaximized;
        }
        if (options.IsFullscreen)
        {
            flags |= Sdl.WindowFullscreen;
        }
        if (options.MouseCaptured)
        {
            flags |= Sdl.WindowMouseCapture;
        }
        return flags;
    }
}
