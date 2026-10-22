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
public unsafe struct Chip8Cpu
{
    public const int RamLength = 0x1000;
    public const int DisplaySize = 128 * 64; // Make it the largest screen
    public const int StackSize = 0x10;
    public const int RegisterCount = 0x10;
    public const int KeypadSize = 0x10;

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] PrimaryTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0x10];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] ZeroSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xEF];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] MathSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xF];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] SkipSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xA2];

    private static readonly delegate* unmanaged<Chip8Cpu, ushort, void>[] MiscSubTable =
        new delegate* unmanaged<Chip8Cpu, ushort, void>[0xFF];

    public Chip8Ram Ram;
    public Chip8Display Display;
    public ushort PC;
    public ushort I;
    public Chip8Stack Stack;
    public byte SP;
    public byte DT;
    public byte ST;
    public Chip8Register V;
    public Chip8Keypad Keypad;
    public bool LargeScreen;
    public bool ShiftUsesVX;
    public bool JumpUsesVXOffset;
    public bool SpriteClipping;
    public bool SetVFOnIOverflow;

    public void Reset(Chip8Options options)
    {
        PC = 0x200;
        I = 0;
        SP = 0;
        DT = 0;
        ST = 0;
        LargeScreen = options.LargeScreen;
        ShiftUsesVX = options.ShiftUsesVX;
        JumpUsesVXOffset = options.JumpUsesVXOffset;
        SpriteClipping = options.SpriteClipping;
        SetVFOnIOverflow = options.SetVFOnIOverflow;

        var ramSpan = MemoryMarshal.CreateSpan(ref Ram[0], RamLength);
        var displaySpan = MemoryMarshal.CreateSpan(ref Display[0], DisplaySize);
        var stackSpan = MemoryMarshal.CreateSpan(ref Stack[0], StackSize);
        var registerSpan = MemoryMarshal.CreateSpan(ref V[0], RegisterCount);
        var keypadSpan = MemoryMarshal.CreateSpan(ref Keypad[0], KeypadSize);

        ramSpan.Clear();
        displaySpan.Clear();
        stackSpan.Clear();
        registerSpan.Clear();
        keypadSpan.Clear();

        InitializeTables();
    }

    private static void InitializeTables()
    {
        PrimaryTable[0x0] = &RerouteOpcode0;

        ZeroSubTable[0xE0] = &Opcode00E0ClearScreen;
        ZeroSubTable[0xEE] = &Opcode00EEReturn;

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
        PrimaryTable[0xA] = &OpcodeANNNSetI;
        PrimaryTable[0xB] = &OpcodeBNNNJump;
        PrimaryTable[0xC] = &OpcodeCXNNRandom;
        PrimaryTable[0xD] = &OpcodeDXYNDraw;
        PrimaryTable[0xE] = &RerouteOpcodeE;

        SkipSubTable[0x9E] = &OpcodeEX9ESkipIfKey;
        SkipSubTable[0xA1] = &OpcodeEXA1SkipIfNotKey;

        PrimaryTable[0xF] = &RerouteOpcodeF;

        MiscSubTable[0x07] = &OpcodeFX07GetDelayTimer;
        MiscSubTable[0x15] = &OpcodeFX15SetDelayTimer;
        MiscSubTable[0x18] = &OpcodeFX18SetSoundTimer;

        // Fill in what's not used with the UnknownOpCode handler
        for (var i = 0; i < 0x10; i++)
        {
            if (PrimaryTable[i] is null)
            {
                PrimaryTable[i] = &UnknownOpCode;
            }
        }

        for (var i = 0; i < 0xEF; i++)
        {
            if (ZeroSubTable[i] is null)
            {
                ZeroSubTable[i] = &UnknownOpCode;
            }
        }

        for (var i = 0; i < 0xF; i++)
        {
            if (MathSubTable[i] is null)
            {
                MathSubTable[i] = &UnknownOpCode;
            }
        }

        for (var i = 0; i < 0xA2; i++)
        {
            if (SkipSubTable[i] is null)
            {
                SkipSubTable[i] = &UnknownOpCode;
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
        ZeroSubTable[nn](cpu, opcode);
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
        if (cpu.SP == 0)
        {
            throw new InvalidOperationException("Stack underflow");
        }

        cpu.PC = cpu.Stack[--cpu.SP];
    }

    [UnmanagedCallersOnly]
    private static void Opcode1NNNJump(Chip8Cpu cpu, ushort opcode)
    {
        var nnn = GetNNN(opcode);
        cpu.PC = nnn;
    }

    [UnmanagedCallersOnly]
    private static void Opcode2NNNCall(Chip8Cpu cpu, ushort opcode)
    {
        if (cpu.SP == 0xF)
        {
            throw new InvalidOperationException("Stack overflow");
        }

        var nnn = GetNNN(opcode);
        cpu.Stack[++cpu.SP] = cpu.PC;
        cpu.PC = nnn;
    }

    [UnmanagedCallersOnly]
    private static void Opcode3XNNSkipIfEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNN(opcode);
        if (cpu.V[x] == nn)
        {
            cpu.PC += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode4XNNSkipIfNotEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNN(opcode);
        if (cpu.V[x] != nn)
        {
            cpu.PC += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode5XY0SkipIfRegistersEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        if (cpu.V[x] == cpu.V[y])
        {
            cpu.PC += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void Opcode9XY0SkipIfRegistersNotEqual(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var y = GetY(opcode);
        if (cpu.V[x] != cpu.V[y])
        {
            cpu.PC += 2;
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
        var source = cpu.ShiftUsesVX ? x : GetY(opcode);
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
        var source = cpu.ShiftUsesVX ? x : GetY(opcode);
        var shiftedValue = cpu.V[source];
        var shiftedOutBit = (byte)((shiftedValue & 0x80) >> 7);
        cpu.V[x] = (byte)(shiftedValue << 1);
        cpu.V[0xF] = shiftedOutBit;
    }

    [UnmanagedCallersOnly]
    private static void OpcodeANNNSetI(Chip8Cpu cpu, ushort opcode) =>
        cpu.I = (ushort)(opcode & 0x0FFF);

    [UnmanagedCallersOnly]
    private static void OpcodeBNNNJump(Chip8Cpu cpu, ushort opcode)
    {
        var address = cpu.JumpUsesVXOffset
            ? GetNN(opcode) + cpu.V[GetX(opcode)]
            : GetNNN(opcode) + cpu.V[0];
        cpu.PC = (ushort)address;
    }

    [UnmanagedCallersOnly]
    private static void OpcodeCXNNRandom(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var nn = GetNN(opcode);
        cpu.V[x] = (byte)(new Random().Next(0, 256) & nn);
    }

    [UnmanagedCallersOnly]
    private static void OpcodeDXYNDraw(Chip8Cpu cpu, ushort opcode)
    {
        var displaySpan = MemoryMarshal.CreateSpan(ref cpu.Display[0], Chip8Cpu.DisplaySize);
        var x = GetX(opcode);
        var y = GetY(opcode);
        var n = GetN(opcode);

        var screenWidth = cpu.LargeScreen ? 128 : 64;
        var screenHeight = cpu.LargeScreen ? 64 : 32;

        var xCoord = cpu.V[x] % screenWidth;
        var yCoord = cpu.V[y] % screenHeight;

        cpu.V[0xF] = 0;
        for (var row = 0; row < n; row++)
        {
            var spriteByte = cpu.Ram[cpu.I + row];
            for (var col = 0; col < 8; col++)
            {
                var spritePixel = (spriteByte >> (7 - col)) & 0x1;
                var screenX = xCoord + col;
                var screenY = yCoord + row;
                if (cpu.SpriteClipping)
                {
                    if (screenX >= screenWidth || screenY >= screenHeight)
                    {
                        continue;
                    }
                }
                else
                {
                    screenX %= screenWidth;
                    screenY %= screenHeight;
                }

                var screenIndex = screenY * screenWidth + screenX;
                var screenPixel = displaySpan[screenIndex];
                displaySpan[screenIndex] ^= (byte)spritePixel;
                if (screenPixel == 1 && displaySpan[screenIndex] == 0)
                {
                    cpu.V[0xF] = 1;
                }
            }
        }
    }

    [UnmanagedCallersOnly]
    private static void RerouteOpcodeE(Chip8Cpu cpu, ushort opcode)
    {
        var subOpcode = GetNN(opcode);
        SkipSubTable[subOpcode](cpu, opcode);
    }

    [UnmanagedCallersOnly]
    private static void OpcodeEX9ESkipIfKey(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var key = cpu.V[x];
        if (cpu.Keypad[key] != 0)
        {
            cpu.PC += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void OpcodeEXA1SkipIfNotKey(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var key = cpu.V[x];
        if (cpu.Keypad[key] == 0)
        {
            cpu.PC += 2;
        }
    }

    [UnmanagedCallersOnly]
    private static void RerouteOpcodeF(Chip8Cpu cpu, ushort opcode)
    {
        var subOpcode = GetNN(opcode);
        MiscSubTable[subOpcode](cpu, opcode);
    }

    [UnmanagedCallersOnly]
    private static void OpcodeFX07GetDelayTimer(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        cpu.V[x] = cpu.DT;
    }

    [UnmanagedCallersOnly]
    private static void OpcodeFX15SetDelayTimer(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        cpu.DT = cpu.V[x];
    }

    [UnmanagedCallersOnly]
    private static void OpcodeFX18SetSoundTimer(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        cpu.ST = cpu.V[x];
    }

    [UnmanagedCallersOnly]
    private static void OpcodeFX1EAddToI(Chip8Cpu cpu, ushort opcode)
    {
        var x = GetX(opcode);
        var sum = cpu.I + cpu.V[x];
        if (sum > 0xFFF && cpu.SetVFOnIOverflow)
        {
            cpu.V[0xF] = 1;
        }

        cpu.I = (ushort)(sum & 0xFFF);
    }
}
