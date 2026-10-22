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

public sealed class Chip8EmulatorCore : IEmulatorCore
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

    private readonly Chip8Options _options;

    private Chip8Cpu _cpu;

    public Chip8EmulatorCore(Action<Chip8Options>? options = null)
    {
        _options = new Chip8Options();
        options?.Invoke(_options);

        _cpu = new Chip8Cpu();
        Reset();
    }

    public void Reset()
    {
        _cpu.Reset(_options);

        var ramSpan = MemoryMarshal.CreateSpan(ref _cpu.Ram[0], 0x1000);
        Fonts.CopyTo(ramSpan);
    }

    public void TickFrame(long currentTimestamp)
    {
        throw new NotImplementedException();
    }

    public IReadOnlyDictionary<string, string> GetCpuState() =>
        new Dictionary<string, string>
        {
            { "PC", _cpu.PC.ToString() },
            { "I", _cpu.I.ToString() },
            { "SP", _cpu.SP.ToString() },
            { "DT", _cpu.DT.ToString() },
            { "ST", _cpu.ST.ToString() },
            { "V0", _cpu.V[0].ToString() },
            { "V1", _cpu.V[1].ToString() },
            { "V2", _cpu.V[2].ToString() },
            { "V3", _cpu.V[3].ToString() },
            { "V4", _cpu.V[4].ToString() },
            { "V5", _cpu.V[5].ToString() },
            { "V6", _cpu.V[6].ToString() },
            { "V7", _cpu.V[7].ToString() },
            { "V8", _cpu.V[8].ToString() },
            { "V9", _cpu.V[9].ToString() },
            { "VA", _cpu.V[0xA].ToString() },
            { "VB", _cpu.V[0xB].ToString() },
            { "VC", _cpu.V[0xC].ToString() },
            { "VD", _cpu.V[0xD].ToString() },
            { "VE", _cpu.V[0xE].ToString() },
            { "VF", _cpu.V[0xF].ToString() },
            { "Keypad", KeypadToString(_cpu.Keypad) },
            { "LargeScreen", _cpu.LargeScreen.ToString() },
            { "ShiftUsesVX", _cpu.ShiftUsesVX.ToString() },
            { "JumpUsesVXOffset", _cpu.JumpUsesVXOffset.ToString() },
            { "SpriteClipping", _cpu.SpriteClipping.ToString() },
        };

    private static string KeypadToString(ReadOnlySpan<byte> keypad)
    {
        // Show them as KEY:ON or KEY:OFF such as 0:ON, 1:OFF, A:ON, etc
        var result = new List<string>();
        for (int i = 0; i < keypad.Length; i++)
        {
            var key = i < 10 ? i.ToString() : ((char)('A' + i - 10)).ToString();
            var state = keypad[i] != 0 ? "ON" : "OFF";
            result.Add($"{key}:{state}");
        }

        return string.Join("|", result);
    }
}
