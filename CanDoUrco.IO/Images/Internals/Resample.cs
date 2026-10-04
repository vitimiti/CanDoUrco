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

namespace CanDoUrco.IO.Images.Internals;

internal sealed class Resample
{
    public ResampleFunc ResampleAction { get; set; } = null!;

    public byte[]? Line0 { get; set; }
    public int Line0Position { get; set; }
    public byte[]? Line1 { get; set; }
    public int Line1Position { get; set; }
    public int HS { get; set; }
    public int VS { get; set; }
    public int WLores { get; set; }
    public int YStep { get; set; }
    public int YPos { get; set; }
}
