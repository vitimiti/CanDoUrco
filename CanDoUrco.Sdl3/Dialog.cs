// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropFileDialogFiltersPointer => "SDL.filedialog.filters"u8;
    public static ReadOnlySpan<byte> PropFileDialogNFiltersNumber => "SDL.filedialog.nfilters"u8;
    public static ReadOnlySpan<byte> PropFileDialogWindowPointer => "SDL.filedialog.window"u8;
    public static ReadOnlySpan<byte> PropFileDialogLocationString => "SDL.filedialog.location"u8;
    public static ReadOnlySpan<byte> PropFileDialogManyBoolean => "SDL.filedialog.many"u8;
    public static ReadOnlySpan<byte> PropFileDialogTitleString => "SDL.filedialog.title"u8;
    public static ReadOnlySpan<byte> PropFileDialogAcceptString => "SDL.filedialog.accept"u8;
    public static ReadOnlySpan<byte> PropFileDialogCancelString => "SDL.filedialog.cancel"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_ShowOpenFileDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ShowOpenFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        DialogFileFilter* filters,
        int nFilters,
        byte* defaultLocation,
        Bool allowMany
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ShowSaveFileDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ShowSaveFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        DialogFileFilter* filters,
        int nFilters,
        byte* defaultLocation
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ShowOpenFolderDialog")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ShowOpenFolderDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        byte* defaultLocation,
        Bool allowMany
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ShowFileDialogWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ShowFileDialogWithProperties(
        FileDialogType type,
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        PropertiesID props
    );

    public static void ShowOpenFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        DialogFileFilter* filters,
        int nFilters,
        byte* defaultLocation,
        Bool allowMany
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ShowOpenFileDialog(
            callback,
            userdata,
            window,
            filters,
            nFilters,
            defaultLocation,
            allowMany
        );
    }

    public static void ShowSaveFileDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        DialogFileFilter* filters,
        int nFilters,
        byte* defaultLocation
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ShowSaveFileDialog(callback, userdata, window, filters, nFilters, defaultLocation);
    }

    public static void ShowOpenFolderDialog(
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        Window window,
        byte* defaultLocation,
        Bool allowMany
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ShowOpenFolderDialog(callback, userdata, window, defaultLocation, allowMany);
    }

    public static void ShowFileDialogWithProperties(
        FileDialogType type,
        delegate* unmanaged[Cdecl]<void*, byte**, int, void> callback,
        void* userdata,
        PropertiesID props
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ShowFileDialogWithProperties(type, callback, userdata, props);
    }

    public enum FileDialogType
    {
        OpenFile,
        SaveFile,
        OpenFolder,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DialogFileFilter
    {
        public byte* Name;
        public byte* Pattern;
    }
}
