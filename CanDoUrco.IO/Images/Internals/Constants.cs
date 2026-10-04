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

using V16 = System.Runtime.Intrinsics.Vector128<short>;

namespace CanDoUrco.IO.Images.Internals;

internal static class Constants
{
    public const int FastBits = 9;
    public const int BufferStartLength = 128;
    public const int HuffmanCodeLength = 256;
    public const int HuffmanValuesLength = 256;
    public const int HuffmanSizeLength = 257;
    public const int HuffmanMaxCodeLength = 18;
    public const int HuffmanDeltaLength = 17;
    public const int JpegFixedArrayLength = 4;
    public const int JpegDequantLength = 64;
    public const int JpegACLength = 1 << FastBits;
    public const int MarkerNone = 0xFF;
    public const int MaxDimensions = 1 << 24;

    public static readonly uint[] BitsMask =
        [0, 1, 3, 7, 15, 31, 63, 127, 255, 511, 1023, 2047, 4095, 8191, 16383, 32767, 65535];

    public static readonly int[] JpegBias =
        [
            0,
            -1,
            -3,
            -7,
            -15,
            -31,
            -63,
            -127,
            -255,
            -511,
            -1023,
            -2047,
            -4095,
            -8191,
            -16383,
            -32767,
        ];

    public static readonly byte[] JpegDezigzag =
        [
            0,
            1,
            8,
            16,
            9,
            2,
            3,
            10,
            17,
            24,
            32,
            25,
            18,
            11,
            4,
            5,
            12,
            19,
            26,
            33,
            40,
            48,
            41,
            34,
            27,
            20,
            13,
            6,
            7,
            14,
            21,
            28,
            35,
            42,
            49,
            56,
            57,
            50,
            43,
            36,
            29,
            22,
            15,
            23,
            30,
            37,
            44,
            51,
            58,
            59,
            52,
            45,
            38,
            31,
            39,
            46,
            53,
            60,
            61,
            54,
            47,
            55,
            62,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
            63,
        ];
    public static V16 Rot00 =>
        Methods.DctConst(
            Methods.F2F(.5411961F),
            Methods.F2F(.5411961F) + Methods.F2F(-1.847759065F)
        );

    public static V16 Rot01 =>
        Methods.DctConst(Methods.F2F(.5411961F) + Methods.F2F(.765366865F), Methods.F2F(.5411961F));

    public static V16 Rot10 =>
        Methods.DctConst(
            Methods.F2F(1.175875602F) + Methods.F2F(-.899976223F),
            Methods.F2F(1.175875602F)
        );

    public static V16 Rot11 =>
        Methods.DctConst(
            Methods.F2F(1.175875602F),
            Methods.F2F(1.175875602F) + Methods.F2F(-2.562915447F)
        );

    public static V16 Rot20 =>
        Methods.DctConst(
            Methods.F2F(-1.961570560F) + Methods.F2F(.298631336F),
            Methods.F2F(-1.961570560F)
        );

    public static V16 Rot21 =>
        Methods.DctConst(
            Methods.F2F(-1.961570560F),
            Methods.F2F(-1.961570560F) + Methods.F2F(3.072711026F)
        );

    public static V16 Rot30 =>
        Methods.DctConst(
            Methods.F2F(-.390180644F) + Methods.F2F(2.053119869F),
            Methods.F2F(-.390180644F)
        );

    public static V16 Rot31 =>
        Methods.DctConst(
            Methods.F2F(-.390180644F),
            Methods.F2F(-.390180644F) + Methods.F2F(1.501321110F)
        );

    public static bool VerticallyFlipOnLoadGlobal { get; set; }

    public static bool VerticallyFlipOnLoadLocal { get; set; }

    public static bool VerticallyFlipOnLoadSet { get; set; }

    public static bool VerticallyFlipOnLoad =>
        VerticallyFlipOnLoadSet ? VerticallyFlipOnLoadLocal : VerticallyFlipOnLoadGlobal;
}
