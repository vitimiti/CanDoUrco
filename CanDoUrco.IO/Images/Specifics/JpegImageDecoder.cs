// These are I/O utilities for the CanDoUrco project.
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
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

namespace CanDoUrco.IO.Images.Specifics;

/// <summary>
/// A self-contained decoder for baseline and extended sequential (Huffman, 8-bit) JPEG images.
/// </summary>
internal sealed class JpegImageDecoder : IImageDecoder
{
    // csharpier-ignore
    private static readonly int[] ZigZag =
    [
        0, 1, 8, 16, 9, 2, 3, 10, 17, 24, 32, 25, 18, 11, 4, 5, 12,
        19, 26, 33, 40, 48, 41, 34, 27, 20, 13, 6, 7, 14, 21, 28, 35,
        42, 49, 56, 57, 50, 43, 36, 29, 22, 15, 23, 30, 37, 44, 51,
        58, 59, 52, 45, 38, 31, 39, 46, 53, 60, 61, 54, 47, 55, 62, 63,
    ];

    private static readonly float[] CosTable = BuildCosTable();

    public bool CanDecode(ReadOnlySpan<byte> header) =>
        header.Length >= 3 && header[0] == 0xFF && header[1] == 0xD8 && header[2] == 0xFF;

    public ImageData Decode(Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);

        using var memory = new MemoryStream();
        stream.CopyTo(memory);

        try
        {
            return new Decoder(memory.ToArray()).Decode();
        }
        catch (Exception e) when (e is IndexOutOfRangeException or ArgumentOutOfRangeException)
        {
            throw new InvalidDataException("The JPEG image is truncated or corrupt.", e);
        }
    }

    private static float[] BuildCosTable()
    {
        var table = new float[64];
        for (int x = 0; x < 8; x++)
        {
            for (int u = 0; u < 8; u++)
            {
                double c = u == 0 ? Math.Sqrt(0.5) : 1.0;
                table[(x * 8) + u] = (float)(
                    c * 0.5 * Math.Cos(((2 * x) + 1) * u * Math.PI / 16.0)
                );
            }
        }

        return table;
    }

    private sealed class HuffmanTable
    {
        private readonly int[] _minCode = new int[17];
        private readonly int[] _maxCode = new int[18];
        private readonly int[] _valPtr = new int[17];
        private readonly byte[] _values;

        public HuffmanTable(byte[] counts, byte[] values)
        {
            _values = values;
            int code = 0;
            int k = 0;
            for (int len = 1; len <= 16; len++)
            {
                _valPtr[len] = k;
                _minCode[len] = code;
                code += counts[len - 1];
                k += counts[len - 1];
                _maxCode[len] = counts[len - 1] == 0 ? -1 : code - 1;
                code <<= 1;
            }
        }

        public int Decode(BitReader reader)
        {
            int code = 0;
            for (int len = 1; len <= 16; len++)
            {
                code = (code << 1) | reader.ReadBit();
                if (_maxCode[len] >= 0 && code <= _maxCode[len] && code >= _minCode[len])
                {
                    return _values[_valPtr[len] + code - _minCode[len]];
                }
            }

            throw new InvalidDataException("Invalid Huffman code in JPEG data.");
        }
    }

    private sealed class BitReader(byte[] data, int position)
    {
        private int _bits;
        private int _count;

        public int Position { get; private set; } = position;

        public int ReadBit()
        {
            if (_count == 0)
            {
                _bits = NextByte();
                _count = 8;
            }

            _count--;
            return (_bits >> _count) & 1;
        }

        public int Receive(int n)
        {
            int v = 0;
            for (int i = 0; i < n; i++)
            {
                v = (v << 1) | ReadBit();
            }

            return v;
        }

        public void Restart()
        {
            _count = 0;
            while (
                Position + 1 < data.Length
                && !(data[Position] == 0xFF && data[Position + 1] is >= 0xD0 and <= 0xD7)
            )
            {
                Position++;
            }

            Position += 2;
        }

        public void Finish() => _count = 0;

        private int NextByte()
        {
            // At a marker or the end of data, feed zeros without consuming it.
            if (Position >= data.Length)
            {
                return 0;
            }

            byte b = data[Position];
            if (b != 0xFF)
            {
                Position++;
                return b;
            }

            if (Position + 1 < data.Length && data[Position + 1] == 0)
            {
                Position += 2;
                return 0xFF;
            }

            return 0;
        }
    }

    private sealed class Component
    {
        public int Id;
        public int H;
        public int V;
        public int QuantIndex;
        public int DcTable;
        public int AcTable;
        public int Pred;
        public int Stride;
        public byte[] Plane = [];
    }

    private sealed class Decoder(byte[] data)
    {
        private readonly int[][] _quant = new int[4][];
        private readonly HuffmanTable?[] _dc = new HuffmanTable?[4];
        private readonly HuffmanTable?[] _ac = new HuffmanTable?[4];
        private Component[] _components = [];
        private int _width;
        private int _height;
        private int _hMax;
        private int _vMax;
        private int _restartInterval;
        private int _adobeTransform = -1;
        private int _pos;

        public ImageData Decode()
        {
            if (data.Length < 4 || data[0] != 0xFF || data[1] != 0xD8)
            {
                throw new InvalidDataException("Missing JPEG SOI marker.");
            }

            _pos = 2;
            bool scanned = false;
            while (true)
            {
                int marker = NextMarker();
                if (marker == 0xD9)
                {
                    break;
                }

                if (marker is 0x01 or (>= 0xD0 and <= 0xD7))
                {
                    continue;
                }

                int length = (data[_pos] << 8) | data[_pos + 1];
                int end = _pos + length;
                int p = _pos + 2;
                switch (marker)
                {
                    case 0xDB:
                        ReadQuant(p, end);
                        break;
                    case 0xC4:
                        ReadHuffman(p, end);
                        break;
                    case 0xC0:
                    case 0xC1:
                        ReadFrame(p);
                        break;
                    case 0xC2:
                        throw new NotSupportedException(
                            "Progressive JPEG images are not supported."
                        );
                    case 0xC3 or (>= 0xC5 and <= 0xC7) or (>= 0xC9 and <= 0xCF and not 0xCC):
                        throw new NotSupportedException(
                            "This JPEG coding process is not supported."
                        );
                    case 0xDD:
                        _restartInterval = (data[p] << 8) | data[p + 1];
                        break;
                    case 0xEE:
                        if (end - p >= 12 && data[p] == (byte)'A' && data[p + 1] == (byte)'d')
                        {
                            _adobeTransform = data[p + 11];
                        }

                        break;
                    case 0xDA:
                        _pos = end;
                        ReadScan(p);
                        scanned = true;
                        continue;
                }

                _pos = end;
            }

            return !scanned || _components.Length == 0
                ? throw new InvalidDataException("JPEG image contains no image data.")
                : BuildImage();
        }

        private int NextMarker()
        {
            while (_pos < data.Length && data[_pos] != 0xFF)
            {
                _pos++;
            }

            while (_pos < data.Length && data[_pos] == 0xFF)
            {
                _pos++;
            }

            return _pos >= data.Length ? 0xD9 : data[_pos++];
        }

        private void ReadQuant(int p, int end)
        {
            while (p < end)
            {
                int pq = data[p] >> 4;
                int tq = data[p] & 15;
                p++;
                var table = new int[64];
                for (int i = 0; i < 64; i++)
                {
                    if (pq == 0)
                    {
                        table[i] = data[p++];
                    }
                    else
                    {
                        table[i] = (data[p] << 8) | data[p + 1];
                        p += 2;
                    }
                }

                _quant[tq & 3] = table;
            }
        }

        private void ReadHuffman(int p, int end)
        {
            while (p < end)
            {
                int tc = data[p] >> 4;
                int th = data[p] & 3;
                p++;
                byte[] counts = data[p..(p + 16)];
                p += 16;
                int total = counts.Sum(c => c);
                byte[] values = data[p..(p + total)];
                p += total;
                var table = new HuffmanTable(counts, values);
                if (tc == 0)
                {
                    _dc[th] = table;
                }
                else
                {
                    _ac[th] = table;
                }
            }
        }

        private void ReadFrame(int p)
        {
            if (data[p] != 8)
            {
                throw new NotSupportedException("Only 8-bit JPEG precision is supported.");
            }

            _height = (data[p + 1] << 8) | data[p + 2];
            _width = (data[p + 3] << 8) | data[p + 4];
            int n = data[p + 5];
            if (_width == 0 || _height == 0)
            {
                throw new InvalidDataException("Invalid JPEG dimensions.");
            }

            if (n is not (1 or 3))
            {
                throw new NotSupportedException(
                    "Only grayscale and three-component JPEG images are supported."
                );
            }

            _components = new Component[n];
            for (int i = 0; i < n; i++)
            {
                int q = p + 6 + (i * 3);
                _components[i] = new Component
                {
                    Id = data[q],
                    H = data[q + 1] >> 4,
                    V = data[q + 1] & 15,
                    QuantIndex = data[q + 2] & 3,
                };
                if (_components[i].H is < 1 or > 4 || _components[i].V is < 1 or > 4)
                {
                    throw new InvalidDataException("Invalid JPEG sampling factors.");
                }
            }

            _hMax = _components.Max(c => c.H);
            _vMax = _components.Max(c => c.V);
            int mcusX = (_width + (8 * _hMax) - 1) / (8 * _hMax);
            int mcusY = (_height + (8 * _vMax) - 1) / (8 * _vMax);
            foreach (Component c in _components)
            {
                c.Stride = mcusX * c.H * 8;
                c.Plane = new byte[c.Stride * mcusY * c.V * 8];
            }
        }

        private void ReadScan(int p)
        {
            if (_components.Length == 0)
            {
                throw new InvalidDataException("JPEG scan found before frame header.");
            }

            int ns = data[p++];
            var scan = new Component[ns];
            for (int i = 0; i < ns; i++)
            {
                int id = data[p++];
                int tables = data[p++];
                Component c =
                    _components.FirstOrDefault(x => x.Id == id)
                    ?? throw new InvalidDataException("Scan references an unknown component.");

                c.DcTable = (tables >> 4) & 3;
                c.AcTable = tables & 3;
                scan[i] = c;
            }

            var reader = new BitReader(data, _pos);
            foreach (Component c in scan)
            {
                c.Pred = 0;
            }

            int mcuCount;
            int mcusX = 0;
            if (ns == 1)
            {
                Component c = scan[0];
                int bw = (((_width * c.H) + _hMax - 1) / _hMax + 7) / 8;
                int bh = (((_height * c.V) + _vMax - 1) / _vMax + 7) / 8;
                mcusX = bw;
                mcuCount = bw * bh;
            }
            else
            {
                mcusX = (_width + (8 * _hMax) - 1) / (8 * _hMax);
                int mcusY = (_height + (8 * _vMax) - 1) / (8 * _vMax);
                mcuCount = mcusX * mcusY;
            }

            var block = new int[64];
            for (int mcu = 0; mcu < mcuCount; mcu++)
            {
                if (_restartInterval > 0 && mcu > 0 && mcu % _restartInterval == 0)
                {
                    reader.Restart();
                    foreach (Component c in scan)
                    {
                        c.Pred = 0;
                    }
                }

                int mx = mcu % mcusX;
                int my = mcu / mcusX;
                if (ns == 1)
                {
                    DecodeBlock(reader, scan[0], block, mx, my);
                }
                else
                {
                    foreach (Component c in scan)
                    {
                        for (int v = 0; v < c.V; v++)
                        {
                            for (int h = 0; h < c.H; h++)
                            {
                                DecodeBlock(reader, c, block, (mx * c.H) + h, (my * c.V) + v);
                            }
                        }
                    }
                }
            }

            reader.Finish();
            _pos = reader.Position;
        }

        private void DecodeBlock(BitReader reader, Component c, int[] block, int bx, int by)
        {
            HuffmanTable dc =
                _dc[c.DcTable] ?? throw new InvalidDataException("Missing DC Huffman table.");

            HuffmanTable ac =
                _ac[c.AcTable] ?? throw new InvalidDataException("Missing AC Huffman table.");

            int[] q =
                _quant[c.QuantIndex]
                ?? throw new InvalidDataException("Missing quantization table.");

            Array.Clear(block);
            int t = dc.Decode(reader);
            c.Pred += t == 0 ? 0 : Extend(reader.Receive(t), t);
            block[0] = c.Pred * q[0];

            int k = 1;
            while (k < 64)
            {
                int rs = ac.Decode(reader);
                int r = rs >> 4;
                int s = rs & 15;
                if (s == 0)
                {
                    if (r == 15)
                    {
                        k += 16;
                        continue;
                    }

                    break;
                }

                k += r;
                if (k > 63)
                {
                    break;
                }

                block[ZigZag[k]] = Extend(reader.Receive(s), s) * q[k];
                k++;
            }

            Idct(block, c.Plane, (by * 8 * c.Stride) + (bx * 8), c.Stride);
        }

        private static int Extend(int v, int t) => v < (1 << (t - 1)) ? v - (1 << t) + 1 : v;

        private static void Idct(int[] block, byte[] plane, int offset, int stride)
        {
            Span<float> tmp = stackalloc float[64];
            // Rows: tmp[y*8+x] = sum_u cos[x,u] * block[y*8+u]
            for (int y = 0; y < 8; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    float sum = 0;
                    for (int u = 0; u < 8; u++)
                    {
                        sum += CosTable[(x * 8) + u] * block[(y * 8) + u];
                    }

                    tmp[(y * 8) + x] = sum;
                }
            }

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    float sum = 0;
                    for (int v = 0; v < 8; v++)
                    {
                        sum += CosTable[(y * 8) + v] * tmp[(v * 8) + x];
                    }

                    int value = (int)MathF.Round(sum) + 128;
                    plane[offset + (y * stride) + x] = (byte)Math.Clamp(value, 0, 255);
                }
            }
        }

        private ImageData BuildImage()
        {
            int n = _components.Length;
            var output = new byte[_width * _height * n];
            bool rgb =
                n == 3
                && (
                    _adobeTransform == 0
                    || (
                        _components[0].Id == 'R'
                        && _components[1].Id == 'G'
                        && _components[2].Id == 'B'
                    )
                );

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int o = ((y * _width) + x) * n;
                    if (n == 1)
                    {
                        output[o] = Sample(_components[0], x, y);
                        continue;
                    }

                    byte c0 = Sample(_components[0], x, y);
                    byte c1 = Sample(_components[1], x, y);
                    byte c2 = Sample(_components[2], x, y);
                    if (rgb)
                    {
                        output[o] = c0;
                        output[o + 1] = c1;
                        output[o + 2] = c2;
                    }
                    else
                    {
                        float yy = c0;
                        float cb = c1 - 128f;
                        float cr = c2 - 128f;
                        output[o] = ClampByte(yy + (1.402f * cr));
                        output[o + 1] = ClampByte(yy - (0.344136f * cb) - (0.714136f * cr));
                        output[o + 2] = ClampByte(yy + (1.772f * cb));
                    }
                }
            }

            return new ImageData(output, new Size(_width, _height), n);
        }

        private byte Sample(Component c, int x, int y) =>
            c.Plane[(y * c.V / _vMax * c.Stride) + (x * c.H / _hMax)];

        private static byte ClampByte(float v) => (byte)int.Clamp((int)float.Round(v), 0, 255);
    }
}
