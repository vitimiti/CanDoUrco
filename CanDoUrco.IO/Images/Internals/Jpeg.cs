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

using System.Runtime.InteropServices;

namespace CanDoUrco.IO.Images.Internals;

[StructLayout(LayoutKind.Sequential)]
internal struct Jpeg
{
    public unsafe Context* S;
    public unsafe Huffman* HuffDC;
    public unsafe Huffman* HuffAC;
    public unsafe ushort** Dequant;
    public unsafe short** FastAC;
    public int ImgHMax;
    public int ImgVMax;
    public int ImgMcuX;
    public int ImgMcuY;
    public int ImgMcuW;
    public int ImgMcuH;
    public unsafe CompStruct* ImgComp;
    public uint CodeBuffer;
    public int CodeBits;
    public byte Marker;
    public int NoMore;
    public bool Progressive;
    public int SpecStart;
    public int SpecEnd;
    public int SuccHigh;
    public int SuccLow;
    public int EobRun;
    public int JFif;
    public int App14ColorTransform;
    public int Rgb;
    public int ScanN;
    public unsafe fixed int Order[4];
    public int RestartInterval;
    public int Todo;
    public unsafe delegate* managed<byte*, int, short*, void> IdctBlockKernel;
    public unsafe delegate* managed<byte*, byte*, byte*, byte*, int, int, void> YCbCrToRgbKernel;
    public unsafe delegate* managed<byte*, byte*, byte*, int, int, byte*> ResampleRowHV2Kernel;

    [StructLayout(LayoutKind.Sequential)]
    public struct CompStruct
    {
        public int ID;
        public int H;
        public int V;
        public int TQ;
        public int HD;
        public int HA;
        public int DCPred;
        public int X;
        public int Y;
        public int W2;
        public int H2;
        public unsafe byte* Data;
        public unsafe void* RawData;
        public unsafe void* RawCoeff;
        public unsafe byte* LineBuf;
        public unsafe short* Coeff;
        public int CoeffW;
        public int CoeffH;
    }
}
