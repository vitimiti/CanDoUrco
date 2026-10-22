// A CHIP-8 emulator for Can Do Urco.
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

using System.Drawing;

namespace CanDoUrco.Emulator.Chip8;

public sealed class Chip8Options
{
    public bool LargeScreen { get; set; }
    public Color BackgroundColor { get; set; } = Color.Black;
    public Color ForegroundColor { get; set; } = Color.White;
    public uint InstructionsHertz { get; set; } = 500;
    public uint DisplayHertz { get; set; } = 60;
    public bool BitShiftQuirks { get; set; }
    public bool JumpWithOffsetQuirks { get; set; }
}
