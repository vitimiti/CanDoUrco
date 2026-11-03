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

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3.CustomMarshallers;

[CustomMarshaller(typeof(string), MarshalMode.Default, typeof(SdlOwnedUtf8StringMarshaller))]
[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
internal static class SdlOwnedUtf8StringMarshaller
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

    public static unsafe string? ConvertToManaged(byte* unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        return Marshal.PtrToStringUTF8((nint)unmanaged);
    }

    public static unsafe void Free(byte* unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        Marshal.FreeCoTaskMem((nint)unmanaged);
    }

    public ref struct ManagedToUnmanagedOut
    {
        private unsafe byte* _unmanaged;
        private string? _managed;

        public unsafe void FromUnmanaged(byte* unmanaged)
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            _unmanaged = unmanaged;
            _managed = Marshal.PtrToStringUTF8((nint)unmanaged);
        }

        public readonly string? ToManaged()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            return _managed;
        }

        public readonly unsafe void Free()
        {
            LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
            if (_unmanaged is not null)
            {
                Sdl.Free(_unmanaged);
            }
        }
    }
}
