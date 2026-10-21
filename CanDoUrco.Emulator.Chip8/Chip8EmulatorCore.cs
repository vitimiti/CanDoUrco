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

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] PrimaryTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0x10];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] ZeroTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xEE];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] MathSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0x10];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] MiscSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xFF];

    private Chip8Cpu _cpu;

    public Chip8EmulatorCore(Chip8Options options)
    {
        _cpu = new Chip8Cpu { BitShiftQuirks = options.BitShiftQuirks };

        var ramSpan = MemoryMarshal.CreateSpan(ref _cpu.Ram[0], 0x1000);
        Fonts.CopyTo(ramSpan);

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

    private static void InitializeTables()
    {
        PrimaryTable[0x0] = &RerouteOpcode0;

        ZeroTable[0xE0] = &Opcode00E0ClearScreen;
        ZeroTable[0xEE] = &Opcode00EEReturn;

        PrimaryTable[0x1] = &Opcode1NNNJump;
        PrimaryTable[0x2] = &Opcode2NNNCall;
        PrimaryTable[0x3] = &Opcode3XNNSkipIfEqual;
        PrimaryTable[0x4] = &Opcode4XNNSkipIfNotEqual;
        PrimaryTable[0x5] = &Opcode5XY0SkipIfRegistersEqual;
        PrimaryTable[0x6] = &Opcode6XNNSetRegister;
        PrimaryTable[0x7] = &Opcode7XNNAddToRegister;
        PrimaryTable[0x8] = &RerouteOpcode8;

        MathSubTable[0x0] = &Opcode8XY0SetRegister;
        MathSubTable[0x1] = &Opcode8XY1OrRegisters;
        MathSubTable[0x2] = &Opcode8XY2AndRegisters;
        MathSubTable[0x3] = &Opcode8XY3XorRegisters;
        MathSubTable[0x4] = &Opcode8XY4AddRegisters;
        MathSubTable[0x5] = &Opcode8XY5SubRegisters;
        MathSubTable[0x6] = &Opcode8XY6ShiftRight;
        MathSubTable[0x7] = &Opcode8XY7SubRegisters;
        MathSubTable[0xE] = &Opcode8XYEShiftLeft;

        PrimaryTable[0x9] = &Opcode9XY0SkipIfRegistersNotEqual;

        // Fill in what's not used with the UnknownOpCode handler
        for (var i = 0; i < 0x10; i++)
        {
            if (PrimaryTable[i] is null)
            {
                PrimaryTable[i] = &UnknownOpCode;
            }

            if (MathSubTable[i] is null)
            {
                MathSubTable[i] = &UnknownOpCode;
            }
        }

        for (var i = 0; i < 0xEE; i++)
        {
            if (ZeroTable[i] is null)
            {
                ZeroTable[i] = &UnknownOpCode;
            }
        }

        for (var i = 0; i < 0xFF; i++)
        {
            if (MiscSubTable[i] is null)
            {
                MiscSubTable[i] = &UnknownOpCode;
            }
        }
    }

    private static ushort GetNNN(ushort opcode) => (ushort)(opcode & 0x0FFF);

    private static ushort GetNN(ushort opcode) => (ushort)(opcode & 0x00FF);

    private static byte GetNNByte(ushort opcode) => (byte)(opcode & 0x00FF);

    private static byte GetN(ushort opcode) => (byte)(opcode & 0x000F);

    private static byte GetX(ushort opcode) => (byte)((opcode & 0x0F00) >> 8);

    private static byte GetY(ushort opcode) => (byte)((opcode & 0x00F0) >> 4);

    [UnmanagedCallersOnly]
    private static void UnknownOpCode(Chip8Cpu cpu, ushort opcode) =>
        throw new NotImplementedException($"Unknown opcode: 0x{opcode:X4}");

    [UnmanagedCallersOnly]
    private static void RerouteOpcode0(Chip8Cpu cpu, ushort opcode)
    {
        var nn = GetNN(opcode);
        ZeroTable[nn](cpu, opcode);
    }

    [UnmanagedCallersOnly]
    private static void Opcode00E0ClearScreen(Chip8Cpu cpu, ushort opcode)
    {
        var displaySpan = MemoryMarshal.CreateSpan(ref cpu.Display[0], 128 * 64);
        displaySpan.Clear();
    }

    [UnmanagedCallersOnly]
    private static void Opcode00EEReturn(Chip8Cpu cpu, ushort opcode)
    {
        if (cpu.StackPointer == 0)
        {
            throw new InvalidOperationException("Stack underflow");
        }

        cpu.ProgramCounter = cpu.Stack[--cpu.StackPointer];
    }

    [UnmanagedCallersOnly]
    private static void Opcode1NNNJump(Chip8Cpu cpu, ushort opcode)
    {
        var nnn = GetNNN(opcode);
        cpu.ProgramCounter = nnn;
    }

    [UnmanagedCallersOnly]
    private static void Opcode2NNNCall(Chip8Cpu cpu, ushort opcode)
    {
        if (cpu.StackPointer == 0xF)
        {
            throw new InvalidOperationException("Stack overflow");
        }

        var nnn = GetNNN(opcode);
        cpu.Stack[++cpu.StackPointer] = cpu.ProgramCounter;
        cpu.ProgramCounter = nnn;
    }

    [UnmanagedCallersOnly]
    private static void Opcode3XNNSkipIfEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNN(opcode);
        if (cpu.V[x] == nn)
        {
            cpu.ProgramCounter += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode4XNNSkipIfNotEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNN(opcode);
        if (cpu.V[x] != nn)
        {
            cpu.ProgramCounter += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode5XY0SkipIfRegistersEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        if (cpu.V[x] == cpu.V[y])
        {
            cpu.ProgramCounter += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode9XY0SkipIfRegistersNotEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        if (cpu.V[x] != cpu.V[y])
        {
            cpu.ProgramCounter += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode6XNNSetRegister(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNNByte(opcode);
        cpu.V[x] = nn;
    }

    [UnmanagedCallersOnly]
    private static void Opcode7XNNAddToRegister(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNNByte(opcode);
        unchecked
        {
            cpu.V[x] += nn;
        }
    }

    [UnmanagedCallersOnly]
    private static void RerouteOpcode8(Chip8Cpu cpu, ushort opcode)
    {
        var n = GetN(opcode);
        MathSubTable[n](cpu, opcode);
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY0SetRegister(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[x] = cpu.V[y];
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY1OrRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[x] |= cpu.V[y];
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY2AndRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[x] &= cpu.V[y];
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY3XorRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[x] ^= cpu.V[y];
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY4AddRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        var sum = cpu.V[x] + cpu.V[y];
        cpu.V[0xF] = (byte)(sum > 0xFF ? 1 : 0);
        cpu.V[x] = (byte)sum;
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY5SubRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[0xF] = (byte)(cpu.V[x] >= cpu.V[y] ? 1 : 0);
        cpu.V[x] -= cpu.V[y];
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY6ShiftRight(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var source = cpu.BitShiftQuirks ? x : GetY(opcode);
        var shiftedValue = cpu.V[source];
        var shiftedOutBit = (byte)(shiftedValue & 0x1);
        cpu.V[x] = (byte)(shiftedValue >>> 1);
        cpu.V[0xF] = shiftedOutBit;
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XY7SubRegisters(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        cpu.V[0xF] = (byte)(cpu.V[y] > cpu.V[x] ? 1 : 0);
        cpu.V[x] = (byte)(cpu.V[y] - cpu.V[x]);
    }

    [UnmanagedCallersOnly]
    private static void Opcode8XYEShiftLeft(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var source = cpu.BitShiftQuirks ? x : GetY(opcode);
        var shiftedValue = cpu.V[source];
        var shiftedOutBit = (byte)((shiftedValue & 0x80) >> 7);
        cpu.V[x] = (byte)(shiftedValue << 1);
        cpu.V[0xF] = shiftedOutBit;
    }
}
