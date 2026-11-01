// Platform.cs - Defines platform-specific utilities for the CanDoUrco framework.
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

namespace CanDoUrco.Core.Utilities;

public static class Platform
{
    public static readonly PlatformFamily CurrentFamily;

    public static bool IsWindows => CurrentFamily == PlatformFamily.Windows;
    public static bool IsLinux => CurrentFamily == PlatformFamily.Linux;
    public static bool IsMacOS => CurrentFamily == PlatformFamily.MacOS;
    public static bool IsUnix => CurrentFamily is PlatformFamily.Linux or PlatformFamily.MacOS;

    static Platform() =>
        CurrentFamily =
            OperatingSystem.IsWindows() ? PlatformFamily.Windows
            : OperatingSystem.IsLinux() ? PlatformFamily.Linux
            : OperatingSystem.IsMacOS() ? PlatformFamily.MacOS
            : throw new PlatformNotSupportedException("Unsupported platform");
}
