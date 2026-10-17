// A common framework for the projects and engines of Can Do Urco
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

namespace CanDoUrco.Framework.Options;

public sealed class WindowOptions
{
    public string Title { get; set; } = "Can Do Urco";
    public Size Size { get; set; } = new Size(800, 600);
    public SwapInterval SwapInterval { get; set; } = SwapInterval.VSync;
    public Color BackgroundColor { get; set; } = Color.CornflowerBlue;
    public bool IsResizable { get; set; } = true;
    public bool IsMaximized { get; set; }
    public bool IsFullscreen { get; set; }
    public bool MouseCaptured { get; set; }
}
