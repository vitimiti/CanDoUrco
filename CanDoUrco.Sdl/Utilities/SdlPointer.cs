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

namespace CanDoUrco.Sdl.Utilities;

public sealed unsafe class SdlPointer(void* ptr) : IDisposable
{
    private void* _ptr = ptr;

    public ref T AsRef<T>()
        where T : unmanaged => ref *(T*)_ptr;

    public ref T AsRef<T>(int index)
        where T : unmanaged => ref ((T*)_ptr)[index];

    public void Dispose()
    {
        if (_ptr is not null)
        {
            Sdl.Free(_ptr);
            _ptr = null;
        }
    }

    public static implicit operator SdlPointer(void* ptr) => new(ptr);

    public static implicit operator void*(SdlPointer sdlPointer) => sdlPointer._ptr;

    public static implicit operator nint(SdlPointer sdlPointer) => (nint)sdlPointer._ptr;
}
