// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(
        DllName,
        EntryPoint = "SDL_OpenTitleStorage",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenTitleStorage(string @override, PropertiesID props);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_OpenUserStorage",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenUserStorage(string org, string app, PropertiesID props);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_OpenFileStorage",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Storage _OpenFileStorage(string path);

    [LibraryImport(DllName, EntryPoint = "SDL_OpenStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Storage _OpenStorage(in StorageInterface iface, void* userdata);

    [LibraryImport(DllName, EntryPoint = "SDL_CloseStorage")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CloseStorage(Storage storage);

    [LibraryImport(DllName, EntryPoint = "SDL_StorageReady")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _StorageReady(Storage storage);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetStorageFileSize",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetStorageFileSize(Storage storage, string path, out ulong length);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_ReadStorageFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _ReadStorageFile(
        Storage storage,
        string path,
        void* destination,
        ulong length
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_WriteStorageFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _WriteStorageFile(
        Storage storage,
        string path,
        void* destination,
        ulong length
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateStorageDirectory",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CreateStorageDirectory(Storage storage, string path);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_EnumerateStorageDirectory",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _EnumerateStorageDirectory(
        Storage storage,
        string path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RemoveStoragePath",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RemoveStoragePath(Storage storage, string path);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RenameStoragePath",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RenameStoragePath(Storage storage, string oldPath, string newPath);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CopyStorageFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _CopyStorageFile(
        Storage storage,
        string oldPath,
        string newPath
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetStoragePathInfo",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetStoragePathInfo(
        Storage storage,
        string path,
        out PathInfo info
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetStorageSpaceRemaining")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial ulong _GetStorageSpaceRemaining(Storage storage);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GlobStorageDirectory",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<string, nint>),
        CountElementName = nameof(count)
    )]
    private static partial string[]? _GlobStorageDirectory(
        Storage storage,
        string path,
        string? pattern,
        GlobFlags flags,
        out int count
    );

    public static Storage OpenTitleStorage(string @override, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(@override);
        return _OpenTitleStorage(@override, props);
    }

    public static Storage OpenUserStorage(string org, string app, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(org);
        ArgumentNullException.ThrowIfNull(app);
        return _OpenUserStorage(org, app, props);
    }

    public static Storage OpenFileStorage(string path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _OpenFileStorage(path);
    }

    public static unsafe Storage OpenStorage(in StorageInterface iface, void* userdata)
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

    public static bool GetStorageFileSize(Storage storage, string path, out ulong length)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _GetStorageFileSize(storage, path, out length);
    }

    public static unsafe bool ReadStorageFile(
        Storage storage,
        string path,
        void* destination,
        ulong length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(destination);
        return _ReadStorageFile(storage, path, destination, length);
    }

    public static unsafe bool WriteStorageFile(
        Storage storage,
        string path,
        void* source,
        ulong length
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(source);
        return _WriteStorageFile(storage, path, source, length);
    }

    public static bool CreateStorageDirectory(Storage storage, string path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _CreateStorageDirectory(storage, path);
    }

    public static unsafe bool EnumerateStorageDirectory(
        Storage storage,
        string path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        ArgumentNullException.ThrowIfNull(callback);
        return _EnumerateStorageDirectory(storage, path, callback, userdata);
    }

    public static bool RemoveStoragePath(Storage storage, string path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _RemoveStoragePath(storage, path);
    }

    public static bool RenameStoragePath(Storage storage, string oldPath, string newPath)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(oldPath);
        ArgumentNullException.ThrowIfNull(newPath);
        return _RenameStoragePath(storage, oldPath, newPath);
    }

    public static bool CopyStorageFile(Storage storage, string oldPath, string newPath)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(oldPath);
        ArgumentNullException.ThrowIfNull(newPath);
        return _CopyStorageFile(storage, oldPath, newPath);
    }

    public static bool GetStoragePathInfo(Storage storage, string path, out PathInfo info)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _GetStoragePathInfo(storage, path, out info);
    }

    public static ulong GetStorageSpaceRemaining(Storage storage)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStorageSpaceRemaining(storage);
    }

    public static string[]? GlobStorageDirectory(
        Storage storage,
        string path,
        string? pattern,
        GlobFlags flags,
        out int count
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(path);
        return _GlobStorageDirectory(storage, path, pattern, flags, out count);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct StorageInterface
    {
        public uint Version;
        public unsafe delegate* unmanaged[Cdecl]<void*, sbyte> Close; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, sbyte> Ready; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<
            void*,
            byte*,
            delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult>,
            void*,
            sbyte> Enumerate; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, PathInfo, sbyte> Info; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> ReadFile; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, void*, ulong, sbyte> WriteFile; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, sbyte> MkDir; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, sbyte> Remove; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> Rename; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, byte*, byte*, sbyte> Copy; // sbyte is bool
        public unsafe delegate* unmanaged[Cdecl]<void*, ulong> SpaceRemaining;
    }

    public readonly record struct Storage(nint Value)
    {
        public bool IsNull => Value == 0;

        public static implicit operator Storage(nint value) => new(value);

        public static implicit operator nint(Storage storage) => storage.Value;
    }
}
