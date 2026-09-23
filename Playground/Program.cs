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

using System.Drawing;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;

using var context = new GlfwNativeContext();
using var window = new GlfwWindow(
    new Size(640, 480),
    "OpenGL Triangle",
    options: (options) =>
    {
        options.ContextVersion = new Version(3, 3);
        options.OpenGlProfile = GlfwOpenGlProfile.Core;
    }
);

window.KeyAction += (_, args) =>
{
    if (args.Key is GlfwKey.Escape)
    {
        window.SetShouldClose(value: true);
    }
};

window.MakeContextCurrent();

// Load OpenGL as required.

GlfwContextUtilities.SetSwapInterval(interval: 1);

// Initialize whatever is required for OpenGL drawing here

while (!window.ShouldClose())
{
    var framebufferSize = window.GetFramebufferSize();

    // Draw with OpenGL, use `GlfwTimeUtilities.GetTime()` to get the current GLFW time if required.

    window.SwapBuffers();
    GlfwEvents.Poll();
}
