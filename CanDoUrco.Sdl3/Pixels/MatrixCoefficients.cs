// An SDL3 interop library with dotnet safety and idioms.
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

namespace CanDoUrco.Sdl3.Pixels;

public enum MatrixCoefficients
{
    Identity,
    BT709,
    Unspecified,
    Fcc = 4,
    BT470BG,
    BT601,
    Smpte240,
    YCgCo,
    BT2020Ncl,
    BT2020Cl,
    Smpte2085,
    ChromaDerivedNcl,
    ChromaDerivedCL,
    ICtCp,
    Custom = 31,
}
