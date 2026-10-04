// A set of utilities to handle IO for Can Do Urco.
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

namespace CanDoUrco.IO.Images.Internals;

[StructLayout(LayoutKind.Sequential)]
internal struct Resample
{
    public unsafe delegate* managed<byte*, byte*, byte*, int, int, byte*> ResampleFunc { get; set; }
    public unsafe byte* Line0 { get; set; }
    public unsafe byte* Line1 { get; set; }
    public int HS { get; set; }
    public int VS { get; set; }
    public int WLores { get; set; }
    public int YStep { get; set; }
    public int YPos { get; set; }
}
