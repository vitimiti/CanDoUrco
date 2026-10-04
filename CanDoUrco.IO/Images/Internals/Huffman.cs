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

internal sealed class Huffman
{
    public byte[] Fast { get; } = new byte[1 << Constants.FastBits];
    public int FastPosition { get; set; }
    public ushort[] Code { get; } = new ushort[Constants.HuffmanCodeLength];
    public int CodePosition { get; set; }
    public byte[] Values { get; } = new byte[Constants.HuffmanValuesLength];
    public int ValuesPosition { get; set; }
    public byte[] Size { get; } = new byte[Constants.HuffmanSizeLength];
    public int SizePosition { get; set; }
    public uint[] MaxCode { get; } = new uint[Constants.HuffmanMaxCodeLength];
    public int MaxCodePosition { get; set; }
    public int[] Delta { get; } = new int[Constants.HuffmanDeltaLength];
    public int DeltaPosition { get; set; }

    public bool Build(ReadOnlySpan<int> count)
    {
        int j;
        var k = 0;
        for (var i = 0; i < 16; i++)
        {
            for (j = 0; j < count[i]; j++)
            {
                Size[k++] = unchecked((byte)(i + 1));
                if (k >= 257)
                {
                    return Methods.Error("Bad size list. Corrupt JPEG.");
                }
            }
        }

        Size[k] = 0;
        var code = 0U;
        k = 0;
        for (j = 1; j <= 16; j++)
        {
            Delta[j] = unchecked((int)(k - code));
            if (Size[k] == j)
            {
                while (Size[k] == j)
                {
                    Code[k++] = unchecked((ushort)code++);
                }

                if (code - 1 >= (1U << j))
                {
                    return Methods.Error("Bad code lengths. Corrupt JPEG.");
                }
            }

            MaxCode[j] = code << (16 - j);
            code <<= 1;
        }

        MaxCode[j] = 0xFFFF_FFFF;
        Array.Fill(Fast, (byte)255, 0, 1 << Constants.FastBits);
        for (var i = 0; i < k; i++)
        {
            var s = (int)Size[i];
            if (s <= Constants.FastBits)
            {
                var c = Code[i] << (Constants.FastBits - s);
                var m = 1 << (Constants.FastBits - s);
                for (j = 0; j < m; j++)
                {
                    Fast[c + j] = unchecked((byte)i);
                }
            }
        }

        return true;
    }

    public void BuildFastAC(short[] fastAC)
    {
        for (var i = 0; i < (1 << Constants.FastBits); i++)
        {
            var fast = Fast[i];
            fastAC[i] = 0;
            if (fast < 255)
            {
                var rs = Values[fast];
                var run = (rs >> 4) & 15;
                var magbits = rs & 15;
                var len = Size[fast];
                if (magbits != 0 && len + magbits <= Constants.FastBits)
                {
                    var k =
                        ((i << len) & ((1 << Constants.FastBits) - 1))
                        >> (Constants.FastBits - magbits);

                    var m = 1 << (magbits - 1);
                    if (k < m)
                    {
                        k += unchecked((int)((~0U << magbits) + 1));
                    }

                    if (k >= -128 && k <= 127)
                    {
                        fastAC[i] = unchecked((short)((k * 256) + (run * 16) + len + magbits));
                    }
                }
            }
        }
    }
}
