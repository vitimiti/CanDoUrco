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

internal delegate void IdctKernel(Span<byte> output, int outStride, ReadOnlySpan<short> data);

internal delegate void YCbCrToRgbKernelFunc(
    Span<byte> output,
    ReadOnlySpan<byte> y,
    ReadOnlySpan<byte> cb,
    ReadOnlySpan<byte> cr,
    int count,
    int step
);

internal delegate (byte[] Data, int Offset) ResampleFunc(
    byte[] output,
    byte[] inNear,
    int inNearPos,
    byte[] inFar,
    int inFarPos,
    int w,
    int hs
);

internal sealed class Jpeg
{
    public Context S { get; set; } = null!;
    public Huffman[] HuffDC { get; set; } = null!;
    public Huffman[] HuffAC { get; set; } = null!;
    public ushort[][] Dequant { get; set; } = null!;
    public int DequantPosition1 { get; set; }
    public int DequantPosition2 { get; set; }
    public short[][] FastAC { get; set; } = null!;
    public int FastACPosition1 { get; set; }
    public int FastACPosition2 { get; set; }
    public int ImgHMax { get; set; }
    public int ImgVMax { get; set; }
    public int ImgMcuX { get; set; }
    public int ImgMcuY { get; set; }
    public int ImgMcuW { get; set; }
    public int ImgMcuH { get; set; }
    public CompStruct[] ImgComp { get; set; } = null!;
    public uint CodeBuffer { get; set; }
    public int CodeBits { get; set; }
    public byte Marker { get; set; }
    public int NoMore { get; set; }
    public bool Progressive { get; set; }
    public int SpecStart { get; set; }
    public int SpecEnd { get; set; }
    public int SuccHigh { get; set; }
    public int SuccLow { get; set; }
    public int EobRun { get; set; }
    public int JFif { get; set; }
    public int App14ColorTransform { get; set; }
    public int Rgb { get; set; }
    public int ScanN { get; set; }
    public int[] Order { get; } = new int[4];
    public int OrderPosition { get; set; }
    public int RestartInterval { get; set; }
    public int Todo { get; set; }
    public IdctKernel IdctBlockKernel { get; set; } = null!;
    public YCbCrToRgbKernelFunc YCbCrToRgbKernel { get; set; } = null!;
    public ResampleFunc ResampleRowHV2Kernel { get; set; } = null!;

    public sealed class CompStruct
    {
        public int ID { get; set; }
        public int H { get; set; }
        public int V { get; set; }
        public int TQ { get; set; }
        public int HD { get; set; }
        public int HA { get; set; }
        public int DCPred { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int W2 { get; set; }
        public int H2 { get; set; }
        public byte[]? Data { get; set; }
        public byte[]? RawData { get; set; }
        public object? RawCoeff { get; set; }
        public byte[]? LineBuf { get; set; }
        public short[]? Coeff { get; set; }
        public int CoeffW { get; set; }
        public int CoeffH { get; set; }
    }
}
