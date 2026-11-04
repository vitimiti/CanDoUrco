// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3.CustomMarshallers;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(UnownedUtf8StringMarshaller))]
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
internal static class UnownedUtf8StringMarshaller
{
    public static unsafe byte* ConvertToUnmanaged(string? managed)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        if (managed is null)
        {
            return null;
        }

        int exactByteCount = checked(Encoding.UTF8.GetByteCount(managed) + 1);
        byte* mem = (byte*)Marshal.AllocCoTaskMem(exactByteCount);
        Span<byte> buffer = new(mem, exactByteCount);

        int byteCount = Encoding.UTF8.GetBytes(managed, buffer);
        buffer[byteCount] = 0;
        return mem;
    }

    public static unsafe string? ConvertToManaged(byte* unmanaged) =>
        Marshal.PtrToStringUTF8((nint)unmanaged);

    public static unsafe void Free(byte* unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        Marshal.FreeCoTaskMem((nint)unmanaged);
    }

    public ref struct ManagedToUnmanagedOut
    {
        private string? _value;

        public unsafe void FromUnmanaged(byte* unmanaged)
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            _value = Marshal.PtrToStringUTF8((nint)unmanaged);
        }

        public readonly string? ToManaged()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _value;
        }

        public readonly void Free() => LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
    }
}
