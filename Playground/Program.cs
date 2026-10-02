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
using System.Numerics;
using System.Runtime.InteropServices;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;
using CanDoUrco.Utilities;
using CanDoUrco.Utilities.Drawing;
using Playground;

// Following LearnOpenGL (https://learnOpenGL.com/book/book_pdf.pdf)

static void ProcessInput(GlfwWindow window)
{
    if (window.GetKeyState(GlfwKey.Escape) is GlfwKeyState.Pressed)
    {
        window.SetShouldClose(true);
    }
}

const int windowWidth = 800;
const int windowHeight = 600;

var vertices = new Vertex[]
{
    new() { Position = new Vector3(.5F, -.5F, 0F), Color = Color.Red.ToNormalizedRgb() },
    new() { Position = new Vector3(-.5F, -.5F, 0F), Color = Color.Green.ToNormalizedRgb() },
    new() { Position = new Vector3(0F, .5F, 0F), Color = Color.Blue.ToNormalizedRgb() },
};

return CrashReporter.Run(() =>
{
    using var _ = new GlfwNativeContext();
    using var window = new GlfwWindow(
        new Size(windowWidth, windowHeight),
        "LearnOpenGL",
        options: (options) =>
        {
            options.ContextVersion = new Version(3, 3);
            options.OpenGLProfile = GlfwOpenGLProfile.Core;
            if (OperatingSystem.IsMacOS())
            {
                options.OpenGLForwardCompat = true;
            }
        }
    );

    window.MakeContextCurrent();

    using var vertexShaderStream =
        typeof(Program).Assembly.GetManifestResourceStream(
            "Playground.Resources.Shaders.Core.Vertex.glsl"
        ) ?? throw new InvalidOperationException("Vertex shader resource not found.");

    using var fragmentShaderStream =
        typeof(Program).Assembly.GetManifestResourceStream(
            "Playground.Resources.Shaders.Core.Fragment.glsl"
        ) ?? throw new InvalidOperationException("Fragment shader resource not found.");

    using var shader = new Shader(vertexShaderStream, fragmentShaderStream);
    window.FramebufferSizeUpdate += (_, args) =>
        GL.Viewport(0, 0, args.Size.Width, args.Size.Height);

    Span<uint> vbo = stackalloc uint[1];
    Span<uint> vao = stackalloc uint[1];
    GL.GenVertexArrays(vao);
    GL.GenBuffers(vbo);
    GL.BindVertexArray(vao[0]);

    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, vbo[0]);
    GL.BufferData(
        GL.BufferTargetARBEnum.ArrayBuffer,
        vertices.Length * Marshal.SizeOf<Vertex>(),
        MemoryMarshal.Cast<Vertex, float>(vertices),
        GL.BufferUsageARBEnum.StaticDraw
    );

    GL.VertexAttribPointer(
        index: 0,
        size: 3,
        GL.VertexAttribPointerTypeEnum.Float,
        normalized: false,
        stride: 6 * sizeof(float),
        pointer: 0
    );
    GL.EnableVertexAttribArray(0);

    GL.VertexAttribPointer(
        index: 1,
        size: 3,
        GL.VertexAttribPointerTypeEnum.Float,
        normalized: false,
        stride: 6 * sizeof(float),
        pointer: 3 * sizeof(float)
    );
    GL.EnableVertexAttribArray(1);

    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, 0);
    GL.BindVertexArray(0);

    while (!window.ShouldClose())
    {
        ProcessInput(window);

        GL.ClearColor(.2F, .3F, .3F, 1F);
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit);

        shader.Use();
        var timeValue = (float)GlfwTimeUtilities.GetTime().TotalSeconds;
        var greenValue = float.Sin(timeValue) / 2F + .5F;
        shader.Set("ourColor", greenValue);

        GL.BindVertexArray(vao[0]);
        GL.DrawArrays(GL.PrimitiveTypeEnum.Triangles, first: 0, count: vertices.Length);

        window.SwapBuffers();
        GlfwEvents.Poll();
    }

    GL.DeleteVertexArrays(vao);
    GL.DeleteBuffers(vbo);
});

[StructLayout(LayoutKind.Sequential)]
struct Vertex
{
    public Vector3 Position;
    public Vector3 Color;
}
