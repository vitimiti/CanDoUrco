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
using CanDoUrco.Emulator.Abstractions;

namespace CanDoUrco.Emulator.Chip8;

public sealed unsafe class Chip8EmulatorCore : IEmulatorCore
{
    // csharpier-ignore
    private static readonly byte[] Fonts =
    [
        0xF0, 0x90, 0x90, 0x90, 0xF0, // 0
        0x20, 0x60, 0x20, 0x20, 0x70, // 1
        0xF0, 0x10, 0xF0, 0x80, 0xF0, // 2
        0xF0, 0x10, 0xF0, 0x10, 0xF0, // 3
        0x90, 0x90, 0xF0, 0x10, 0x10, // 4
        0xF0, 0x80, 0xF0, 0x10, 0xF0, // 5
        0xF0, 0x80, 0xF0, 0x90, 0xF0, // 6
        0xF0, 0x10, 0x20, 0x40, 0x40, // 7
        0xF0, 0x90, 0xF0, 0x90, 0xF0, // 8
        0xF0, 0x90, 0xF0, 0x10, 0xF0, // 9
        0xF0, 0x90, 0xF0, 0x90, 0x90, // A
        0xE0, 0x90, 0xE0, 0x90, 0xE0, // B
        0xF0, 0x80, 0x80, 0x80, 0xF0, // C
        0xE0, 0x90, 0x90, 0x90, 0xE0, // D
        0xF0, 0x80, 0xF0, 0x80, 0xF0, // E
        0xF0, 0x80, 0xF0, 0x80, 0x80  // F
    ];

    private readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] _primaryTable;
    private readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] _mathSubTable;
    private readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] _miscSubTable;

    private Chip8Cpu _cpu;

    public Chip8EmulatorCore(Chip8Options options)
    {
        _cpu = new Chip8Cpu();

        var ramSpan = MemoryMarshal.CreateSpan(ref _cpu.Ram[0], 0x1000);
        Fonts.CopyTo(ramSpan);

        _primaryTable = new delegate* unmanaged<Chip8Cpu, ushort, void>[0x10];
        _mathSubTable = new delegate* unmanaged<Chip8Cpu, ushort, void>[0x10];
        _miscSubTable = new delegate* unmanaged<Chip8Cpu, ushort, void>[0xFF];
        InitializeTables();

        _cpu.ProgramCounter = 0x0200;
    }

    public void TickFrame(long currentTimestamp)
    {
        throw new NotImplementedException();
    }

    private void InitializeTables()
    {
        // Fill in what's not used with the UnknownOpCode handler
        for (var i = 0; i < 0x10; i++)
        {
            if (_primaryTable[i] is null)
            {
                _primaryTable[i] = &UnknownOpCode;
            }

            if (_mathSubTable[i] is null)
            {
                _mathSubTable[i] = &UnknownOpCode;
            }
        }

        // Fill in what's not used with the UnknownOpCode handler
        for (var i = 0; i < 0xFF; i++)
        {
            if (_miscSubTable[i] is null)
            {
                _miscSubTable[i] = &UnknownOpCode;
            }
        }
    }

    [UnmanagedCallersOnly]
    private static void UnknownOpCode(Chip8Cpu cpu, ushort opcode) =>
        throw new NotImplementedException($"Unknown opcode: 0x{opcode:X4}");
}
