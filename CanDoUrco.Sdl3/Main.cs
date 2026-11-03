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
using System.Runtime.Versioning;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_SetMainReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetMainReady();

    [LibraryImport(DllName, EntryPoint = "SDL_RunApp", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _RunApp(
        int argc,
        [In, Out]
        [MarshalUsing(typeof(ArrayMarshaller<string, nint>), CountElementName = nameof(argc))]
            string[] argv,
        delegate* unmanaged[Cdecl]<int, byte**, int> mainFunction,
        void* reserved
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_EnterAppMainCallbacks",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial int _EnterAppMainCallbacks(
        int argc,
        [In, Out]
        [MarshalUsing(typeof(ArrayMarshaller<string, nint>), CountElementName = nameof(argc))]
            string[] argv,
        delegate* unmanaged[Cdecl]<void**, int, byte**, AppResult> appInit,
        delegate* unmanaged[Cdecl]<void*, AppResult> appIter,
        delegate* unmanaged[Cdecl]<void*, Event*, AppResult> appEvent,
        delegate* unmanaged[Cdecl]<void*, AppResult, void> appQuit
    );

    [SupportedOSPlatform("windows")]
    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RegisterApp",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _RegisterApp(string? name, uint style, void* hInst);

    [SupportedOSPlatform("windows")]
    [LibraryImport(DllName, EntryPoint = "SDL_UnregisterApp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnregisterApp();

    [LibraryImport(DllName, EntryPoint = "SDL_GDKSuspendComplete")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GDKSuspendComplete();

    public static void SetMainReady()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetMainReady();
    }

    public static unsafe void RunApp(
        int argc,
        string[] argv,
        delegate* unmanaged[Cdecl]<int, byte**, int> mainFunction
    )
    {
        ArgumentNullException.ThrowIfNull(argv);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RunApp(argc, argv, mainFunction, null);
    }

    public static unsafe int EnterAppMainCallbacks(
        int argc,
        string[] argv,
        delegate* unmanaged[Cdecl]<void**, int, byte**, AppResult> appInit,
        delegate* unmanaged[Cdecl]<void*, AppResult> appIter,
        delegate* unmanaged[Cdecl]<void*, Event*, AppResult> appEvent,
        delegate* unmanaged[Cdecl]<void*, AppResult, void> appQuit
    )
    {
        ArgumentNullException.ThrowIfNull(argv);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnterAppMainCallbacks(argc, argv, appInit, appIter, appEvent, appQuit);
    }

    [SupportedOSPlatform("windows")]
    public static unsafe bool RegisterApp(string? name, uint style, void* hInst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RegisterApp(name, style, hInst);
    }

    [SupportedOSPlatform("windows")]
    public static void UnregisterApp()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnregisterApp();
    }

    public static void GdkSuspendComplete()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GDKSuspendComplete();
    }
}
