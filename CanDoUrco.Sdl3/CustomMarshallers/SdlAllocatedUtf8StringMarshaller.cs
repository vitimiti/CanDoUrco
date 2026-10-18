// CanDoUrco.Sdl3 - SDL3 bindings for Can Do Urco
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
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

using System.Runtime.InteropServices.Marshalling;

namespace CanDoUrco.Sdl3.CustomMarshallers;

[CustomMarshaller(
    typeof(string),
    MarshalMode.ManagedToUnmanagedOut,
    typeof(SdlAllocatedUtf8StringMarshaller)
)]
internal static class SdlAllocatedUtf8StringMarshaller
{
    public static unsafe string? ConvertToManaged(byte* unmanaged) =>
        Utf8StringMarshaller.ConvertToManaged(unmanaged);

    public static unsafe void Free(byte* unmanaged)
    {
        if (unmanaged is not null)
        {
            Sdl.Free((nint)unmanaged);
        }
    }
}
