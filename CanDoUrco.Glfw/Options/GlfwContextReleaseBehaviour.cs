// This is a small testing ground for CanDoUrco.
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

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// The context release behavior.
/// </summary>
public enum GlfwContextReleaseBehavior
{
    /// <summary>
    /// Use any available release method.
    /// </summary>
    Any = Native.Glfw.AnyReleaseBehaviorDefine,

    /// <summary>
    /// Flush on release.
    /// </summary>
    Flush = Native.Glfw.ReleaseBehaviorFlushDefine,

    /// <summary>
    /// Don't do anything on release.
    /// </summary>
    None = Native.Glfw.ReleaseBehaviorNoneDefine,
}
