// A set of utilities to handle IO for Can Do Urco.
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

namespace CanDoUrco.IO.Images.Internals;

internal sealed class Bmp
{
    public int Bpp { get; set; }

    public int Offset { get; set; }

    public int Hsz { get; set; }

    public uint MR { get; set; }

    public uint MG { get; set; }

    public uint MB { get; set; }

    public uint MA { get; set; }

    public uint AllA { get; set; }

    public int ExtraRead { get; set; }

    public bool SetMaskDefaults(int compressLevel)
    {
        if (compressLevel == 3)
        {
            return true;
        }

        if (compressLevel == 0)
        {
            if (Bpp == 16)
            {
                MR = 31U << 10;
                MG = 31U << 5;
                MB = 31U << 0;
            }
            else if (Bpp == 32)
            {
                MR = 0xFFU << 16;
                MG = 0xFFU << 8;
                MB = 0xFFU << 0;
                MA = 0xFFU << 24;
                AllA = 0;
            }
            else
            {
                MR = MG = MB = MA = 0;
            }

            return true;
        }

        return false;
    }
}
