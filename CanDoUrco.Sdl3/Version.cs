// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_GetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetVersion();

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetRevision",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string _GetRevision();

    public static int VersionNumMajor(int version) => version / 1_000_000;

    public static int VersionNumMinor(int version) => (version / 1_000) % 1_000;

    public static int VersionNumMicro(int version) => version % 1_000;

    public static int GetVersion()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetVersion();
    }

    public static string GetRevision()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRevision();
    }
}
