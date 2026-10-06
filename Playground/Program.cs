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

// Following https://www.opengl-tutorial.org/

using System.Drawing;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Video;
using CanDoUrco.Utilities;
using Playground;

return CrashReporter.Run(() =>
{
    using var _ = new GlfwNativeContext();
    using var window = new GlfwWindow(
        new Size(1024, 768),
        "Tutorial 01",
        options: opts =>
        {
            opts.Samples = 4; // 4x Antialiasing
            opts.ContextVersion = new Version(3, 3); // OpenGL 3.3
            opts.OpenGLProfile = GlfwOpenGLProfile.Core; // Core Profile
            opts.OpenGLForwardCompat = true; // Forward compatible
        }
    );

    window.MakeContextCurrent(); // Initialize OpenGL context
    window.EnableStickKeys(enabled: true); // Ensure we can capture the escape key being pressed

    while (window.GetKeyState(GlfwKey.Escape) is not GlfwKeyState.Pressed && !window.ShouldClose())
    {
        // Clear the screen
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit);

        // Draw nothing

        // Swap buffers
        window.SwapBuffers();
        GlfwEvents.Poll();
    }
});
