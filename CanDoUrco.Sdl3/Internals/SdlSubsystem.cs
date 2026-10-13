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

namespace CanDoUrco.Sdl3.Internals;

[Flags]
internal enum SdlSubsystem : uint
{
    Audio = 0x0000_0010U,
    Video = 0x0000_0020U,
    Joystick = 0x0000_0200U,
    Haptic = 0x0000_1000U,
    Gamepad = 0x0000_2000U,
    Events = 0x0000_4000U,
    Sensor = 0x0000_8000U,
    Camera = 0x0001_0000U,
}
