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
internal struct Huffman
{
    public unsafe fixed byte Fast[1 << Constants.FastBits];
    public unsafe fixed ushort Code[Constants.HuffmanCodeLength];
    public unsafe fixed byte Values[Constants.HuffmanValuesLength];
    public unsafe fixed byte Size[Constants.HuffmanSizeLength];
    public unsafe fixed uint MaxCode[Constants.HuffmanMaxCodeLength];
    public unsafe fixed int Delta[Constants.HuffmanDeltaLength];
}
