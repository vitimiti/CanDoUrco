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
    [LibraryImport(DllName, EntryPoint = "SDL_GUIDToString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void _GuidToString(Guid guid, byte* pszGuid, int cbGuid);

    [LibraryImport(DllName, EntryPoint = "SDL_StringToGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial Guid _StringToGuid(byte* pszGuid);

    public static unsafe void GuidToString(Guid guid, Span<byte> buffer, int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(buffer.Length, count);
        fixed (byte* pBuffer = buffer)
        {
            _GuidToString(guid, pBuffer, count);
        }
    }

    public static unsafe Guid StringToGuid(ReadOnlySpan<byte> buffer)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fixed (byte* pBuffer = buffer)
        {
            return _StringToGuid(pBuffer);
        }
    }

    [NativeMarshalling(typeof(GuidMarshaller))]
    [StructLayout(LayoutKind.Sequential)]
    public struct Guid
    {
        public byte[] Data
        {
            readonly get;
            set
            {
                LogCategory.MakeOrGet(CustomLogCategory).Trace();
                ArgumentOutOfRangeException.ThrowIfGreaterThan(value.Length, 16);
                field = new byte[16];
                Array.Copy(value, field, value.Length);
            }
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct UnsafeGuid
    {
        public unsafe fixed byte Data[16];
    }
}
