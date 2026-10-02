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
using Playground;

var vertices = new Vertex[]
{
    new(new Vector2(-.6F, -.4F), new Vector3(1F, 0F, 0F)),
    new(new Vector2(.6F, -.4F), new Vector3(0F, 1F, 0F)),
    new(new Vector2(0F, .6F), new Vector3(0F, 0F, 1F)),
};

const string vertexShaderText = """
#version 330

uniform mat4 MVP;

in vec3 vCol;
in vec2 vPos;

out vec3 color;

void main()
{
    gl_Position = MVP * vec4(vPos, 0.0, 1.0);
    color = vCol;
}
""";

const string fragmentShaderText = """
#version 330

in vec3 color;
out vec4 fragment;

void main()
{
    fragment = vec4(color, 1.0);
}
""";

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
    if (args.Key is GlfwKey.Escape && args.Action is GlfwKeyAction.Press)
    {
        window.SetShouldClose(true);
    }
};

window.MakeContextCurrent();
GlfwContextUtilities.SetSwapInterval(1);

Span<uint> vertexBuffers = stackalloc uint[1];
Gl.GenBuffers(vertexBuffers);
Gl.BindBuffer(Gl.BufferTargetARBEnum.ArrayBuffer, vertexBuffers[0]);
Gl.BufferData(
    Gl.BufferTargetARBEnum.ArrayBuffer,
    Marshal.SizeOf<Vertex>() * vertices.Length,
    vertices,
    Gl.BufferUsageARBEnum.StaticDraw
);

var vertexShader = Gl.CreateShader(Gl.ShaderTypeEnum.VertexShader);
Gl.ShaderSource(vertexShader, [vertexShaderText], []);
Gl.CompileShader(vertexShader);

var fragmentShader = Gl.CreateShader(Gl.ShaderTypeEnum.FragmentShader);
Gl.ShaderSource(fragmentShader, [fragmentShaderText], []);
Gl.CompileShader(fragmentShader);

var program = Gl.CreateProgram();
Gl.AttachShader(program, vertexShader);
Gl.AttachShader(program, fragmentShader);
Gl.LinkProgram(program);

var mvpLocation = Gl.GetUniformLocation(program, "MVP");
var vPosLocation = Gl.GetAttribLocation(program, "vPos");
var vColLocation = Gl.GetAttribLocation(program, "vCol");

Span<uint> vertexArrays = stackalloc uint[1];
Gl.GenVertexArrays(vertexArrays);
Gl.BindVertexArray(vertexArrays[0]);
Gl.EnableVertexAttribArray((uint)vPosLocation);
Gl.VertexAttribPointer(
    (uint)vPosLocation,
    size: 2,
    Gl.VertexAttribPointerTypeEnum.Float,
    normalized: false,
    stride: Marshal.SizeOf<Vertex>(),
    pointer: 0
);

Gl.EnableVertexAttribArray((uint)vColLocation);
Gl.VertexAttribPointer(
    (uint)vColLocation,
    3,
    Gl.VertexAttribPointerTypeEnum.Float,
    normalized: false,
    Marshal.SizeOf<Vertex>(),
    Marshal.OffsetOf<Vertex>(nameof(Vertex.Color))
);

while (!window.ShouldClose())
{
    var framebufferSize = window.GetFramebufferSize();
    var ratio = framebufferSize.Width / (float)framebufferSize.Height;

    Gl.Viewport(0, 0, framebufferSize.Width, framebufferSize.Height);
    Gl.Clear(Gl.ClearBufferMaskEnum.ColorBufferBit);

    var m = Matrix4x4.Identity;
    m = Matrix4x4.CreateRotationZ((float)GlfwTimeUtilities.GetTime().TotalSeconds);
    var p = Matrix4x4.CreateOrthographicOffCenter(
        left: -ratio,
        right: ratio,
        bottom: -1F,
        top: 1F,
        zNearPlane: 1F,
        zFarPlane: -1F
    );
    var mvp = m * p;

    var mvpFloats = new float[16];
    mvpFloats[0] = mvp.M11;
    mvpFloats[1] = mvp.M12;
    mvpFloats[2] = mvp.M13;
    mvpFloats[3] = mvp.M14;
    mvpFloats[4] = mvp.M21;
    mvpFloats[5] = mvp.M22;
    mvpFloats[6] = mvp.M23;
    mvpFloats[7] = mvp.M24;
    mvpFloats[8] = mvp.M31;
    mvpFloats[9] = mvp.M32;
    mvpFloats[10] = mvp.M33;
    mvpFloats[11] = mvp.M34;
    mvpFloats[12] = mvp.M41;
    mvpFloats[13] = mvp.M42;
    mvpFloats[14] = mvp.M43;
    mvpFloats[15] = mvp.M44;

    Gl.UseProgram(program);
    Gl.UniformMatrix4fv(mvpLocation, 1, transpose: false, mvpFloats);
    Gl.BindVertexArray(vertexArrays[0]);
    Gl.DrawArrays(Gl.PrimitiveTypeEnum.Triangles, 0, vertices.Length);

    window.SwapBuffers();
    GlfwEvents.Poll();
}

[StructLayout(LayoutKind.Sequential)]
struct Vertex(Vector2 position, Vector3 color)
{
    public Vector2 Position = position;
    public Vector3 Color = color;
}
