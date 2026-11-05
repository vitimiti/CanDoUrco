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

public static partial class Sdl
{
    // Making a marshaller for this is an absolute pain :D
    [LibraryImport(DllName, EntryPoint = "SDL_GetPreferredLocales")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial UnsafeLocale** _GetPreferredLocales(out int count);

    public static unsafe Locale?[]? GetPreferredLocales(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        var ptr = _GetPreferredLocales(out count);
        try
        {
            var result = new Locale?[count];
            for (int i = 0; i < count; i++)
            {
                result[i] = ptr[i] is null
                    ? null
                    : new Locale
                    {
                        Language = Marshal.PtrToStringUTF8((nint)ptr[i]->Language),
                        Country = Marshal.PtrToStringUTF8((nint)ptr[i]->Country),
                    };
            }

            return result;
        }
        finally
        {
            Free(ptr);
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct UnsafeLocale
    {
        public unsafe byte* Language;
        public unsafe byte* Country;
    }

    public class Locale
    {
        public string? Language { get; set; }
        public string? Country { get; set; }
    }
}
