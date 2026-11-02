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
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl.CustomMarshallers;

[CustomMarshaller(typeof(string), MarshalMode.ManagedToUnmanagedOut, typeof(ManagedToUnmanagedOut))]
internal static class UnownedUtf8StringMarshaller
{
    public ref struct ManagedToUnmanagedOut
    {
        private string? _value;

        public unsafe void FromUnmanaged(byte* unmanaged)
        {
            LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
            _value = Marshal.PtrToStringUTF8((nint)unmanaged);
        }

        public readonly string? ToManaged()
        {
            LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
            return _value;
        }

        public readonly void Free() => LogCategory.MakeOrGet("CanDoUrco.Sdl").Trace();
    }
}
