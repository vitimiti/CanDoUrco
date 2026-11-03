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
    public static GlobFlags GlobCaseInsensitive => new(1U << 0);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetBasePath",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetBasePath();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPrefPath",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(SdlAllocatedUtf8StringMarshaller))]
    private static partial string? _GetPrefPath(string org, string app);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetUserFolder",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetUserFolder(Folder folder);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CreateDirectory",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CreateDirectory(string path);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_EnumerateDirectory",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _EnumerateDirectory(
        string path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RemovePath",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RemovePath(string path);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RenamePath",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _RenamePath(string oldPath, string newPath);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_CopyFile",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CopyFile(string src, string dst);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPathInfo",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetPathInfo(string path, out PathInfo info);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GlobDirectory",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(
        typeof(SdlAllocatedArrayMarshaller<string, nint>),
        CountElementName = nameof(count)
    )]
    private static partial string[]? _GlobDirectory(
        string path,
        string? pattern,
        GlobFlags flags,
        out int count
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetCurrentDirectory",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(SdlAllocatedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetCurrentDirectory();

    public static string? GetBasePath()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetBasePath();
    }

    public static string? GetPrefPath(string org, string app)
    {
        ArgumentNullException.ThrowIfNull(org);
        ArgumentNullException.ThrowIfNull(app);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPrefPath(org, app);
    }

    public static string? GetUserFolder(Folder folder)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetUserFolder(folder);
    }

    public static bool CreateDirectory(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateDirectory(path);
    }

    public static unsafe bool EnumerateDirectory(
        string path,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, EnumerationResult> callback,
        void* userdata
    )
    {
        ArgumentNullException.ThrowIfNull(path);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnumerateDirectory(path, callback, userdata);
    }

    public static bool RemovePath(string path)
    {
        ArgumentNullException.ThrowIfNull(path);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RemovePath(path);
    }

    public static bool RenamePath(string oldPath, string newPath)
    {
        ArgumentNullException.ThrowIfNull(oldPath);
        ArgumentNullException.ThrowIfNull(newPath);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RenamePath(oldPath, newPath);
    }

    public static bool CopyFile(string src, string dst)
    {
        ArgumentNullException.ThrowIfNull(src);
        ArgumentNullException.ThrowIfNull(dst);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopyFile(src, dst);
    }

    public static bool GetPathInfo(string path, out PathInfo info)
    {
        ArgumentNullException.ThrowIfNull(path);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPathInfo(path, out info);
    }

    public static string[]? GlobDirectory(
        string path,
        string? pattern,
        GlobFlags flags,
        out int count
    )
    {
        ArgumentNullException.ThrowIfNull(path);
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
