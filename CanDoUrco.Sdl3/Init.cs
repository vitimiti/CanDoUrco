// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropAppMetadataNameString => "SDL.app.metadata.name"u8;
    public static ReadOnlySpan<byte> PropAppMetadataVersionString => "SDL.app.metadata.version"u8;
    public static ReadOnlySpan<byte> PropAppMetadataIdentifierString =>
        "SDL.app.metadata.identifier"u8;
    public static ReadOnlySpan<byte> PropAppMetadataCreatorString => "SDL.app.metadata.creator"u8;
    public static ReadOnlySpan<byte> PropAppMetadataCopyrightString =>
        "SDL.app.metadata.copyright"u8;
    public static ReadOnlySpan<byte> PropAppMetadataUrlString => "SDL.app.metadata.url"u8;
    public static ReadOnlySpan<byte> PropAppMetadataTypeString => "SDL.app.metadata.type"u8;

    public static InitFlags InitAudio => new(0x0000_0010U);

    public static InitFlags InitVideo => new(0x0000_0020U);

    public static InitFlags InitJoystick => new(0x0000_0200U);

    public static InitFlags InitHaptic => new(0x0000_1000U);

    public static InitFlags InitGamepad => new(0x0000_2000U);

    public static InitFlags InitEvents => new(0x0000_4000U);

    public static InitFlags InitSensor => new(0x0000_8000U);

    public static InitFlags InitCamera => new(0x0001_0000U);

    [LibraryImport(DllName, EntryPoint = "SDL_Init")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _Init(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_InitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _InitSubSystem(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_QuitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _QuitSubSystem(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_WasInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags _WasInit(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _Quit();

    [LibraryImport(DllName, EntryPoint = "SDL_IsMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _IsMainThread();

    [LibraryImport(DllName, EntryPoint = "SDL_RunOnMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _RunOnMainThread(
        delegate* unmanaged[Cdecl]<void*, void> callback,
        void* userdata,
        Bool waitComplete
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAppMetadata")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAppMetadata(
        byte* appName,
        byte* appVersion,
        byte* appIdentifier
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetAppMetadataProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetAppMetadataProperty(byte* property, byte* value);

    [LibraryImport(DllName, EntryPoint = "SDL_GetAppMetadataProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetAppMetadataProperty(byte* property);

    public static Bool Init(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Init(flags);
    }

    public static Bool InitSubSystem(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _InitSubSystem(flags);
    }

    public static Bool QuitSubSystem(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _QuitSubSystem(flags);
    }

    public static InitFlags WasInit(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WasInit(flags);
    }

    public static void Quit()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _Quit();
    }

    public static Bool IsMainThread()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsMainThread();
    }

    public static Bool RunOnMainThread(
        delegate* unmanaged[Cdecl]<void*, void> callback,
        void* userdata,
        Bool waitComplete
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RunOnMainThread(callback, userdata, waitComplete);
    }

    public static Bool SetAppMetadata(byte* appName, byte* appVersion, byte* appIdentifier)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAppMetadata(appName, appVersion, appIdentifier);
    }

    public static Bool SetAppMetadataProperty(byte* property, byte* value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAppMetadataProperty(property, value);
    }

    public static byte* GetAppMetadataProperty(byte* property)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAppMetadataProperty(property);
    }

    public enum AppResult
    {
        Continue,
        Success,
        Failure,
    }

    public readonly record struct InitFlags(uint Value)
    {
        public static InitFlags operator |(InitFlags left, InitFlags right) =>
            new(left.Value | right.Value);

        public static InitFlags operator &(InitFlags left, InitFlags right) =>
            new(left.Value & right.Value);

        public static InitFlags operator ^(InitFlags left, InitFlags right) =>
            new(left.Value ^ right.Value);

        public static InitFlags operator ~(InitFlags value) => new(~value.Value);

        public static implicit operator uint(InitFlags initFlags) => initFlags.Value;

        public static implicit operator InitFlags(uint value) => new(value);
    }
}
