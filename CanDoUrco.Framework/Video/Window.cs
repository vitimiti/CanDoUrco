// CanDoUrco.Framework - A framework for building applications with rendering support.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Framework.Options;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Video;

public sealed class Window : IDisposable
{
    private readonly Sdl.Window _sdlWindow;

    internal bool Initialized { get; private set; }

    internal unsafe Window(AppOptions options)
    {
        LogCategory.MakeOrGet(App.CustomLogCategory).Trace();

        scoped var titleMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            titleMarshaller.FromManaged(options.WindowTitle, buffer);

            Sdl.WindowFlags flags = Sdl.WindowHighPixelDensity;
            if (options.Fullscreen)
            {
                flags |= Sdl.WindowFullscreen;
            }
            if (options.Resizable)
            {
                flags |= Sdl.WindowResizable;
            }

            _sdlWindow = Sdl.CreateWindow(
                titleMarshaller.ToUnmanaged(),
                options.WindowSize.Width,
                options.WindowSize.Height,
                flags
            );
        }
        finally
        {
            titleMarshaller.Free();
        }

        if (!_sdlWindow)
        {
            LogCategory
                .MakeOrGet(App.CustomLogCategory)
                .Critical(
                    $"Failed to create SDL window {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            return;
        }

        Initialized = true;
    }

    public void Dispose()
    {
        if (_sdlWindow)
        {
            Sdl.DestroyWindow(_sdlWindow);
        }

        Initialized = false;
    }
}
