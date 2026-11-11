// CanDoUrco.Playground - A playground project for experimenting with the CanDoUrco code.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

// https://lazyfoo.net/tutorials/SDL3/01-hello-sdl3/index2.php

using System.Runtime.InteropServices;
using System.Text;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;
using Microsoft.Extensions.Logging;

unsafe
{
    const string customLogCategory = "CanDoUrco.Playground";

    CoreLog.Initialize(loggingBuilder =>
    {
        loggingBuilder.AddConsole();
#if DEBUG
        loggingBuilder.SetMinimumLevel(LogLevel.Debug);
#else
        loggingBuilder.SetMinimumLevel(LogLevel.Information);
#endif
    });

    LogCategory.MakeOrGet(customLogCategory).Trace();
    LogCategory.MakeOrGet(customLogCategory).Debug("CanDoUrco.Playground logging initialized.");

    const int screenWidth = 640;
    const int screenHeight = 480;

    Sdl.Window window = 0;
    Sdl.Surface* screenSurface = null;
    Sdl.Surface* helloWorld = null;

    if (!Init())
    {
        LogCategory.MakeOrGet(customLogCategory).Critical("Failed to initialize SDL.");
        Environment.Exit(1);
    }
    LogCategory.MakeOrGet(customLogCategory).Debug("SDL initialized successfully.");

    if (!LoadMedia())
    {
        LogCategory.MakeOrGet(customLogCategory).Critical("Failed to load media.");
        Environment.Exit(2);
    }
    LogCategory.MakeOrGet(customLogCategory).Debug("Media loaded successfully.");

    var quit = false;
    Sdl.Event e = default;
    while (!quit)
    {
        while (Sdl.PollEvent(&e))
        {
            LogCategory.MakeOrGet(customLogCategory).Trace($"{GetSdlEventStringInfo(e)}");
            if (e.Type == Sdl.EventType.Quit)
            {
                LogCategory
                    .MakeOrGet(customLogCategory)
                    .Debug(
                        $"Quit event received after {TimeSpan.FromTicks((long)(e.Quit.Timestamp / 100)).ToString(@"hh\:mm\:ss\.fff")}"
                    );
                quit = true;
            }
        }

        Sdl.FillSurfaceRect(
            screenSurface,
            null,
            Sdl.MapSurfaceRgb(screenSurface, 0xFF, 0xFF, 0xFF)
        );

        Sdl.BlitSurface(helloWorld, null, screenSurface, null);

        Sdl.UpdateWindowSurface(window);
    }

    Close();
    Environment.Exit(0);

    bool Init()
    {
        LogCategory.MakeOrGet(customLogCategory).Trace();
        Sdl.SetMainReady();
        fixed (byte* appNamePtr = "CanDoUrco.Playground"u8)
        fixed (byte* appVersionPtr = "1.0.0"u8)
        fixed (byte* appIDPtr = "playground.candourco"u8)
        {
            _ = Sdl.SetAppMetadata(appNamePtr, appVersionPtr, appIDPtr);
        }

        if (!Sdl.Init(Sdl.InitVideo))
        {
            LogCategory
                .MakeOrGet(customLogCategory)
                .Error($"Failed to initialize SDL: {GetSdlError()}.");
            return false;
        }

        fixed (byte* titlePtr = "SDL3 Tutorial: Hello SDL3"u8)
        {
            window = Sdl.CreateWindow(titlePtr, screenWidth, screenHeight, 0);
        }

        if (!window)
        {
            LogCategory
                .MakeOrGet(customLogCategory)
                .Error($"Failed to create SDL window: {GetSdlError()}.");
            return false;
        }

        screenSurface = Sdl.GetWindowSurface(window);
        return true;
    }

    bool LoadMedia()
    {
        LogCategory.MakeOrGet(customLogCategory).Trace();
        const string imagePath = "hello-sdl3.bmp";
        var imagePathBytes = new byte[Encoding.UTF8.GetByteCount(imagePath) + 1];
        Encoding.UTF8.GetBytes(imagePath, 0, imagePath.Length, imagePathBytes, 0);
        imagePathBytes[^1] = 0; // Null-terminate the byte array for C interop.
        fixed (byte* imagePathPtr = imagePathBytes)
        {
            helloWorld = Sdl.LoadBmp(imagePathPtr);
        }

        if (helloWorld == null)
        {
            LogCategory
                .MakeOrGet(customLogCategory)
                .Error($"Failed to load image {imagePath}: {GetSdlError()}.");
            return false;
        }

        return true;
    }

    void Close()
    {
        LogCategory.MakeOrGet(customLogCategory).Trace();
        Sdl.DestroySurface(helloWorld);
        helloWorld = null;

        Sdl.DestroyWindow(window);
        window = 0;
        screenSurface = null;

        Sdl.Quit();
    }

    static string GetSdlError() =>
        Marshal.PtrToStringUTF8((nint)Sdl.GetError()) ?? "Unknown SDL error";

    static string GetSdlEventStringInfo(Sdl.Event @event)
    {
        int actualLength;
        Span<byte> buffer = stackalloc byte[256];
        fixed (byte* bufferPtr = buffer)
        {
            actualLength = Sdl.GetEventDescription(&@event, bufferPtr, buffer.Length);
        }

        return Encoding.UTF8.GetString(buffer[..actualLength]) ?? "Unknown SDL event";
    }
}
