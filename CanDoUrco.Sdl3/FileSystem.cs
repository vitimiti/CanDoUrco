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
    public static GlobFlags GlobCaseInsensitive => new(1U << 0);

    [LibraryImport(DllName, EntryPoint = "SDL_GetBasePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetBasePath();

    [LibraryImport(DllName, EntryPoint = "SDL_GetPrefPath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetPrefPath(byte* org, byte* app);

    [LibraryImport(DllName, EntryPoint = "SDL_GetUserFolder")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetUserFolder(Folder folder);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CreateDirectory(byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_EnumerateDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _EnumerateDirectory(
        byte* path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RemovePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RemovePath(byte* path);

    [LibraryImport(DllName, EntryPoint = "SDL_RenamePath")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RenamePath(byte* oldPath, byte* newPath);

    [LibraryImport(DllName, EntryPoint = "SDL_CopyFile")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CopyFile(byte* src, byte* dst);

    [LibraryImport(DllName, EntryPoint = "SDL_GetPathInfo")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetPathInfo(byte* path, out PathInfo info);

    [LibraryImport(DllName, EntryPoint = "SDL_GlobDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte** _GlobDirectory(
        byte* path,
        byte* pattern,
        GlobFlags flags,
        out int count
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetCurrentDirectory")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetCurrentDirectory();

    public static byte* GetBasePath()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetBasePath();
    }

    public static byte* GetPrefPath(byte* org, byte* app)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPrefPath(org, app);
    }

    public static byte* GetUserFolder(Folder folder)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetUserFolder(folder);
    }

    public static bool CreateDirectory(byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateDirectory(path);
    }

    public static bool EnumerateDirectory(
        byte* path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnumerateDirectory(path, callback, userdata);
    }

    public static bool RemovePath(byte* path)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RemovePath(path);
    }

    public static bool RenamePath(byte* oldPath, byte* newPath)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenamePath(oldPath, newPath);
    }

    public static bool CopyFile(byte* src, byte* dst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopyFile(src, dst);
    }

    public static bool GetPathInfo(byte* path, out PathInfo info)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPathInfo(path, out info);
    }

    public static byte** GlobDirectory(byte* path, byte* pattern, GlobFlags flags, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GlobDirectory(path, pattern, flags, out count);
    }

    public enum Folder
    {
        Home,
        Desktop,
        Documents,
        Downloads,
        Music,
        Pictures,
        PublicShare,
        SavedGames,
        Screenshots,
        Templates,
        Videos,
    }

    public enum PathType
    {
        None,
        File,
        Directory,
        Other,
    }

    public enum EnumerationResult
    {
        Continue,
        Success,
        Failure,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PathInfo
    {
        public PathType Type;
        public ulong Size;
        public Time CreateTime;
        public Time ModifyTime;
        public Time AccessTime;
    }

    public readonly record struct GlobFlags(uint Value)
    {
        public static GlobFlags operator |(GlobFlags left, GlobFlags right) =>
            new(left.Value | right.Value);

        public static GlobFlags operator &(GlobFlags left, GlobFlags right) =>
            new(left.Value & right.Value);

        public static GlobFlags operator ^(GlobFlags left, GlobFlags right) =>
            new(left.Value ^ right.Value);

        public static GlobFlags operator ~(GlobFlags value) => new(~value.Value);

        public static implicit operator uint(GlobFlags flags) => flags.Value;

        public static implicit operator GlobFlags(uint value) => new(value);
    }
}
