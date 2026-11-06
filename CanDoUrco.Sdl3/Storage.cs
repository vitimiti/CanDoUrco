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
    [LibraryImport(DllName, EntryPoint = "SDL_OpenTitleStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenTitleStorage(byte* @override, PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenUserStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenUserStorage(byte* org, byte* app, PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenFileStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenFileStorage(byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenStorage(in StorageInterface iface, void* userdata);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CloseStorage(Storage storage);

    [LibraryImport(DllName, EntryPoint = "SDL_StorageReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _StorageReady(Storage storage);

    [LibraryImport(DllName, EntryPoint = "SDL_GetStorageFileSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetStorageFileSize(Storage storage, byte* path, out ulong length);

    [LibraryImport(DllName, EntryPoint = "SDL_ReadStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ReadStorageFile(
        Storage storage,
        byte* path,
        void* destination,
        ulong length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_WriteStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _WriteStorageFile(
        Storage storage,
        byte* path,
        void* destination,
        ulong length
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CreateStorageDirectory(Storage storage, byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_EnumerateStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _EnumerateStorageDirectory(
        Storage storage,
        byte* path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveStoragePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RemoveStoragePath(Storage storage, byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_RenameStoragePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RenameStoragePath(Storage storage, byte* oldPath, byte* newPath);

    [LibraryImport(DllName, EntryPoint = "SDL_CopyStorageFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CopyStorageFile(Storage storage, byte* oldPath, byte* newPath);

    [LibraryImport(DllName, EntryPoint = "SDL_GetStoragePathInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetStoragePathInfo(Storage storage, byte* path, out PathInfo info);

    [LibraryImport(DllName, EntryPoint = "SDL_GetStorageSpaceRemaining")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetStorageSpaceRemaining(Storage storage);

    [LibraryImport(DllName, EntryPoint = "SDL_GlobStorageDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte** _GlobStorageDirectory(
        Storage storage,
        byte* path,
        byte* pattern,
        GlobFlags flags,
        out int count
    );

    public static Storage OpenTitleStorage(byte* @override, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenTitleStorage(@override, props);
    }

    public static Storage OpenUserStorage(byte* org, byte* app, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenUserStorage(org, app, props);
    }

    public static Storage OpenFileStorage(byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenFileStorage(path);
    }

    public static Storage OpenStorage(in StorageInterface iface, void* userdata)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _OpenStorage(iface, userdata);
    }

    public static bool CloseStorage(Storage storage)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CloseStorage(storage);
    }

    public static bool StorageReady(Storage storage)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StorageReady(storage);
    }

    public static bool GetStorageFileSize(Storage storage, byte* path, out ulong length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStorageFileSize(storage, path, out length);
    }

    public static bool ReadStorageFile(Storage storage, byte* path, void* destination, ulong length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ReadStorageFile(storage, path, destination, length);
    }

    public static bool WriteStorageFile(Storage storage, byte* path, void* source, ulong length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WriteStorageFile(storage, path, source, length);
    }

    public static bool CreateStorageDirectory(Storage storage, byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateStorageDirectory(storage, path);
    }

    public static bool EnumerateStorageDirectory(
        Storage storage,
        byte* path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnumerateStorageDirectory(storage, path, callback, userdata);
    }

    public static bool RemoveStoragePath(Storage storage, byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RemoveStoragePath(storage, path);
    }

    public static bool RenameStoragePath(Storage storage, byte* oldPath, byte* newPath)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenameStoragePath(storage, oldPath, newPath);
    }

    public static bool CopyStorageFile(Storage storage, byte* oldPath, byte* newPath)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopyStorageFile(storage, oldPath, newPath);
    }

    public static bool GetStoragePathInfo(Storage storage, byte* path, out PathInfo info)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStoragePathInfo(storage, path, out info);
    }

    public static ulong GetStorageSpaceRemaining(Storage storage)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStorageSpaceRemaining(storage);
    }

    public static byte** GlobStorageDirectory(
        Storage storage,
        byte* path,
        byte* pattern,
        GlobFlags flags,
        out int count
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GlobStorageDirectory(storage, path, pattern, flags, out count);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StorageInterface
    {
        public uint Version;
        public delegate* unmanaged[Cdecl]<void*, sbyte> Close; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, sbyte> Ready; // sbyte is bool
        public delegate* unmanaged[Cdecl]<
            void*,
            byte*,
            delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult>,
            void*,
            sbyte> Enumerate; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, PathInfo, sbyte> Info; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> ReadFile; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> WriteFile; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, sbyte> MkDir; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, sbyte> Remove; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> Rename; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> Copy; // sbyte is bool
        public delegate* unmanaged[Cdecl]<void*, ulong> SpaceRemaining;
    }

    public readonly record struct Storage(nint Value)
    {
        public static implicit operator Storage(nint value) => new(value);

        public static implicit operator nint(Storage storage) => storage.Value;
    }
}
