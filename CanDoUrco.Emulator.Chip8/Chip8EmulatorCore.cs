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

    public IReadOnlyDictionary<string, int> GetCpuState() =>
        new Dictionary<string, int>
        {
            { "ProgramCounter", _cpu.ProgramCounter },
            { "I", _cpu.I },
            { "StackPointer", _cpu.StackPointer },
            { "DelayTimer", _cpu.DelayTimer },
            { "SoundTimer", _cpu.SoundTimer },
            { "V0", _cpu.V[0] },
            { "V1", _cpu.V[1] },
            { "V2", _cpu.V[2] },
            { "V3", _cpu.V[3] },
            { "V4", _cpu.V[4] },
            { "V5", _cpu.V[5] },
            { "V6", _cpu.V[6] },
            { "V7", _cpu.V[7] },
            { "V8", _cpu.V[8] },
            { "V9", _cpu.V[9] },
            { "VA", _cpu.V[0xA] },
            { "VB", _cpu.V[0xB] },
            { "VC", _cpu.V[0xC] },
            { "VD", _cpu.V[0xD] },
            { "VE", _cpu.V[0xE] },
            { "VF", _cpu.V[0xF] },
        };

    private void InitializeTables()
    {
        _primaryTable[0x0] = &Opcode00E0ClearScreen;
        _primaryTable[0x1] = &Opcode1NNNJump;

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

    [UnmanagedCallersOnly]
    private static void Opcode00E0ClearScreen(Chip8Cpu cpu, ushort opcode)
    {
        var displaySpan = MemoryMarshal.CreateSpan(ref cpu.Display[0], 128 * 64);
        displaySpan.Clear();
        cpu.ProgramCounter += 2;
    }

    [UnmanagedCallersOnly]
    private static void Opcode1NNNJump(Chip8Cpu cpu, ushort opcode)
    {
        var nnn = (ushort)(opcode & 0x0FFF);
        cpu.ProgramCounter = nnn;
    }
}
