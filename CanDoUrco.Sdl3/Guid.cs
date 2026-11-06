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
    [LibraryImport(DllName, EntryPoint = "SDL_GUIDToString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GuidToString(Guid guid, byte* pszGuid, int cbGuid);

    [LibraryImport(DllName, EntryPoint = "SDL_StringToGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Guid _StringToGuid(byte* pszGuid);

    public static void GuidToString(Guid guid, Span<byte> buffer, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* pBuffer = buffer)
        {
            _GuidToString(guid, pBuffer, count);
        }
    }

    public static Guid StringToGuid(ReadOnlySpan<byte> buffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* pBuffer = buffer)
        {
            return _StringToGuid(pBuffer);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Guid
    {
        public fixed byte Data[16];
    }
}
