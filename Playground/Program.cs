// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
