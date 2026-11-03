// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) <year>  <name of author>
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const string PropAppMetadataNameString = "SDL.app.metadata.name";
    public const string PropAppMetadataVersionString = "SDL.app.metadata.version";
    public const string PropAppMetadataIdentifierString = "SDL.app.metadata.identifier";
    public const string PropAppMetadataCreatorString = "SDL.app.metadata.creator";
    public const string PropAppMetadataCopyrightString = "SDL.app.metadata.copyright";
    public const string PropAppMetadataUrlString = "SDL.app.metadata.url";
    public const string PropAppMetadataTypeString = "SDL.app.metadata.type";

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
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _Init(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_InitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _InitSubSystem(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_QuitSubSystem")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _QuitSubSystem(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_WasInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial InitFlags _WasInit(InitFlags flags);

    [LibraryImport(DllName, EntryPoint = "SDL_Quit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _Quit();

    [LibraryImport(DllName, EntryPoint = "SDL_IsMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _IsMainThread();

    [LibraryImport(DllName, EntryPoint = "SDL_RunOnMainThread")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _RunOnMainThread(
        delegate* unmanaged[Cdecl]<void*, void> callback,
        void* userdata,
        [MarshalAs(UnmanagedType.I1)] bool waitComplete
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetAppMetadata",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetAppMetadata(
        string? appName,
        string? appVersion,
        string? appIdentifier
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetAppMetadataProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetAppMetadataProperty(string property, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetAppMetadataProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnownedUtf8StringMarshaller))]
    private static partial string? _GetAppMetadataProperty(string property);

    public static bool Init(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _Init(flags);
    }

    public static bool InitSubSystem(InitFlags flags)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _InitSubSystem(flags);
    }

    public static bool QuitSubSystem(InitFlags flags)
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

    public static bool IsMainThread()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _IsMainThread();
    }

    public static unsafe bool RunOnMainThread(
        delegate* unmanaged[Cdecl]<void*, void> callback,
        void* userdata,
        bool waitComplete
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RunOnMainThread(callback, userdata, waitComplete);
    }

    public static bool SetAppMetadata(string? appName, string? appVersion, string? appIdentifier)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAppMetadata(appName, appVersion, appIdentifier);
    }

    public static bool SetAppMetadataProperty(string property, string? value)
    {
        ArgumentNullException.ThrowIfNull(property);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetAppMetadataProperty(property, value);
    }

    public static string? GetAppMetadataProperty(string property)
    {
        ArgumentNullException.ThrowIfNull(property);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetAppMetadataProperty(property);
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
    }

    public enum AppResult
    {
        Continue,
        Success,
        Failure,
    }
}
