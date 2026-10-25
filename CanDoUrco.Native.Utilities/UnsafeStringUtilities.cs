// A set of native utilities to help bridge safe and unsafe code.
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

using System.Runtime.InteropServices.Marshalling;
using System.Text;

namespace CanDoUrco.Native.Utilities;

public static unsafe class UnsafeStringUtilities
{
    public static string? FromUtf8String(byte* ptr)
    {
        if (ptr is null)
        {
            return null;
        }

        var length = 0;
        while (ptr[length] != 0)
        {
            length++;
        }

        return Encoding.UTF8.GetString(ptr, length);
    }
}
