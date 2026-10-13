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

namespace CanDoUrco.Sdl3.Video;

public sealed record WindowOptions
{
    public bool Fullscreen { get; set; }
    public bool OpenGLReady { get; set; }
    public bool Occluded { get; set; }
    public bool Hidden { get; set; }
    public bool Borderless { get; set; }
    public bool Resizable { get; set; }
    public bool Minimized { get; set; }
    public bool Maximized { get; set; }
    public bool MouseGrabbed { get; set; }
    public bool InputFocus { get; set; }
    public bool MouseFocus { get; set; }
    public bool External { get; set; }
    public bool Modal { get; set; }
    public bool HighPixelDensity { get; set; }
    public bool MouseCapture { get; set; }
    public bool MouseRelativeMode { get; set; }
    public bool AlwaysOnTop { get; set; }
    public bool Utility { get; set; }
    public bool Tooltip { get; set; }
    public bool PopupMenu { get; set; }
    public bool KeyboardGrabbed { get; set; }
    public bool FillDocument { get; set; }
    public bool VulkanReady { get; set; }
    public bool MetalReady { get; set; }
    public bool Transparent { get; set; }
    public bool NotFocusable { get; set; }
}
