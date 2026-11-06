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
    // Making a marshaller for this is an absolute pain :D
    [LibraryImport(DllName, EntryPoint = "SDL_GetPreferredLocales")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Locale** _GetPreferredLocales(out int count);

    public static Locale** GetPreferredLocales(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPreferredLocales(out count);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Locale
    {
        public byte* Language;
        public byte* Country;
    }
}
