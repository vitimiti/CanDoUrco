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

namespace CanDoUrco.Framework;

public sealed class App
{
    internal const string CustomLogCategory = "CanDoUrco.Framework";

    private readonly AppOptions _options;

    private bool _initialized;

    public App(Action<AppOptions>? configure = null)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var options = new AppOptions();
        configure?.Invoke(options);

        _options = options;
        LogCategory.MakeOrGet(CustomLogCategory).Debug($"App initialized with options: {_options}");
    }

    public void Run()
    {
        InitializeSdl();
        if (!_initialized)
        {
            LogCategory
                .MakeOrGet(CustomLogCategory)
                .Critical("Failed to initialize the application.");
            return;
        }
    }

    private unsafe void InitializeSdl()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();

        scoped var appNameMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        scoped var appVersionMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        scoped var appIDMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];

            appNameMarshaller.FromManaged(_options.AppName, buffer);
            appVersionMarshaller.FromManaged(_options.AppVersion, buffer);
            appIDMarshaller.FromManaged(_options.AppID, buffer);

            if (
                !Sdl.SetAppMetadata(
                    appNameMarshaller.ToUnmanaged(),
                    appVersionMarshaller.ToUnmanaged(),
                    appIDMarshaller.ToUnmanaged()
                )
            )
            {
                LogCategory
                    .MakeOrGet(CustomLogCategory)
                    .Error(
                        $"Failed to set SDL app metadata {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                    );
            }
        }
        finally
        {
            appIDMarshaller.Free();
            appVersionMarshaller.Free();
            appNameMarshaller.Free();
        }

        if (!Sdl.Init(Sdl.InitVideo | Sdl.InitEvents))
        {
            LogCategory
                .MakeOrGet(CustomLogCategory)
                .Critical(
                    $"Failed to initialize SDL {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            return;
        }

        _initialized = true;
    }
}
