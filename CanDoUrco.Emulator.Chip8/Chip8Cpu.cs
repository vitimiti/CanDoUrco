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

using System.Runtime.InteropServices;

namespace CanDoUrco.Emulator.Chip8;

[StructLayout(LayoutKind.Sequential)]
public struct Chip8Cpu
{
    public Chip8Ram Ram;
    public Chip8Display Display;
    public ushort ProgramCounter;
    public ushort I;
    public Chip8Stack Stack;
    public byte StackPointer;
    public byte DelayTimer;
    public byte SoundTimer;
    public Chip8Register V;

    public const int RamLength = 0x1000;
    public const int DisplaySize = 128 * 64; // Make it the largest screen
    public const int StackSize = 0x10;
    public const int RegisterCount = 0x10;
}
