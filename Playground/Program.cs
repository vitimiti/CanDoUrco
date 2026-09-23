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
using System.Text;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;
using Playground;

var vertices = new Vertex[]
{
    new() { Position = new Vec2(0.0f, 0.5f), Color = new Vec3(1.0f, 0.0f, 0.0f) },
    new() { Position = new Vec2(0.5f, -0.5f), Color = new Vec3(0.0f, 1.0f, 0.0f) },
    new() { Position = new Vec2(-0.5f, -0.5f), Color = new Vec3(0.0f, 0.0f, 1.0f) },
};

const string vertexShaderSource =
    @"
#version 330 core
uniform mat4 uMVP;
in vec3 vCol;
in vec2 vPos;
out vec3 color;
void main()
{
    gl_Position = uMVP * vec4(vPos, 0.0, 1.0);
    color = vCol;
}
";

const string fragmentShaderSource =
    @"
#version 330 core
in vec3 color;
out vec4 fragment;
void main()
{
    fragment = vec4(color, 1.0);
}
";

Console.WriteLine($"Running GLFW v{GlfwNativeContext.Version}");
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

window.SetSizeLimits(
    new Size(640 / 3, 480 / 3),
    new Size(GlfwSpecialValues.DontCare, GlfwSpecialValues.DontCare)
);

window.KeyAction += (_, args) =>
{
    if (args.Key is GlfwKey.Escape)
    {
        window.SetShouldClose(value: true);
    }
};

unsafe
{
    window.MakeContextCurrent();
    GlfwContextUtilities.SetSwapInterval(interval: 1);

    // Modern OpenGL entry points are context-dependent; static DllImport bindings
    // against a directly-loaded libGL are unreliable and can silently resolve to
    // an unbound dispatch table, so resolve every function through GLFW instead.
    Gl.LoadFunctions(GlfwContextUtilities.GetProcAddress);

    Console.WriteLine(
        $"GL_VERSION: {Marshal.PtrToStringAnsi((nint)Gl.GetString(Gl.Enums.Version))}"
    );
    Console.WriteLine($"GL_VENDOR: {Marshal.PtrToStringAnsi((nint)Gl.GetString(Gl.Enums.Vendor))}");
    Console.WriteLine(
        $"GL_RENDERER: {Marshal.PtrToStringAnsi((nint)Gl.GetString(Gl.Enums.Renderer))}"
    );

    var vertexSourceBytes = Encoding.ASCII.GetBytes(vertexShaderSource + "\0");
    var fragmentSourceBytes = Encoding.ASCII.GetBytes(fragmentShaderSource + "\0");

    uint vertexBuffer;
    Gl.GenBuffers(1, &vertexBuffer);
    Gl.BindBuffer(Gl.Enums.ArrayBuffer, vertexBuffer);

    fixed (Vertex* verticesPtr = vertices)
    {
        Gl.BufferData(
            Gl.Enums.ArrayBuffer,
            Marshal.SizeOf<Vertex>() * vertices.Length,
            verticesPtr,
            Gl.Enums.StaticDraw
        );
    }

    var vertexShader = Gl.CreateShader(Gl.Enums.VertexShader);

    if (vertexShader == 0)
    {
        throw new InvalidOperationException(
            $"glCreateShader(GL_VERTEX_SHADER) returned 0; glGetError = 0x{Gl.GetError():X}"
        );
    }

    fixed (byte* vertexSourcePtr = vertexSourceBytes)
    {
        var vertexSourcePointers = stackalloc byte*[1];
        vertexSourcePointers[0] = vertexSourcePtr;
        Gl.ShaderSource(vertexShader, 1, vertexSourcePointers, null);
    }

    Gl.CompileShader(vertexShader);
    ThrowIfShaderFailed(vertexShader);

    var fragmentShader = Gl.CreateShader(Gl.Enums.FragmentShader);

    if (fragmentShader == 0)
    {
        throw new InvalidOperationException(
            $"glCreateShader(GL_FRAGMENT_SHADER) returned 0; glGetError = 0x{Gl.GetError():X}"
        );
    }

    fixed (byte* fragmentSourcePtr = fragmentSourceBytes)
    {
        var fragmentSourcePointers = stackalloc byte*[1];
        fragmentSourcePointers[0] = fragmentSourcePtr;
        Gl.ShaderSource(fragmentShader, 1, fragmentSourcePointers, null);
    }

    Gl.CompileShader(fragmentShader);
    ThrowIfShaderFailed(fragmentShader);

    var program = Gl.CreateProgram();
    Gl.AttachShader(program, vertexShader);
    Gl.AttachShader(program, fragmentShader);
    Gl.LinkProgram(program);
    ThrowIfProgramFailed(program);

    int mvpLocation;
    int vPosLocation;
    int vColLocation;

    fixed (byte* namePtr = "uMVP\0"u8)
    {
        mvpLocation = Gl.GetUniformLocation(program, namePtr);
    }

    fixed (byte* namePtr = "vPos\0"u8)
    {
        vPosLocation = Gl.GetAttribLocation(program, namePtr);
    }

    fixed (byte* namePtr = "vCol\0"u8)
    {
        vColLocation = Gl.GetAttribLocation(program, namePtr);
    }

    if (mvpLocation < 0 || vPosLocation < 0 || vColLocation < 0)
    {
        throw new InvalidOperationException("Failed to locate one or more shader variables.");
    }

    uint vertexArray;
    Gl.GenVertexArrays(1, &vertexArray);
    Gl.BindVertexArray(vertexArray);
    Gl.EnableVertexAttribArray((uint)vPosLocation);
    Gl.VertexAttribPointer(
        (uint)vPosLocation,
        2,
        Gl.Enums.Float,
        0,
        Marshal.SizeOf<Vertex>(),
        null
    );
    Gl.EnableVertexAttribArray((uint)vColLocation);
    Gl.VertexAttribPointer(
        (uint)vColLocation,
        3,
        Gl.Enums.Float,
        0,
        Marshal.SizeOf<Vertex>(),
        (void*)Marshal.OffsetOf<Vertex>("Color")
    );

    while (!window.ShouldClose())
    {
        var framebufferSize = window.GetFramebufferSize();
        var ratio = (float)framebufferSize.Width / framebufferSize.Height;

        Gl.Viewport(0, 0, framebufferSize.Width, framebufferSize.Height);
        Gl.Clear(Gl.Enums.ColorBufferBit);

        Matrix4x4 m;
        Matrix4x4 p;
        Matrix4x4 mvp;
        m = Matrix4x4.Identity;
        m = Matrix4x4.CreateRotationZ((float)GlfwTimeUtilities.GetTime().TotalSeconds);
        p = Matrix4x4.CreateOrthographicOffCenter(-ratio, ratio, -1f, 1f, 0.1f, 100f);
        mvp = m * p;

        Gl.UseProgram(program);
        var mvpArrayOfFloats = new float[16];
        mvpArrayOfFloats[0] = mvp.M11;
        mvpArrayOfFloats[1] = mvp.M12;
        mvpArrayOfFloats[2] = mvp.M13;
        mvpArrayOfFloats[3] = mvp.M14;
        mvpArrayOfFloats[4] = mvp.M21;
        mvpArrayOfFloats[5] = mvp.M22;
        mvpArrayOfFloats[6] = mvp.M23;
        mvpArrayOfFloats[7] = mvp.M24;
        mvpArrayOfFloats[8] = mvp.M31;
        mvpArrayOfFloats[9] = mvp.M32;
        mvpArrayOfFloats[10] = mvp.M33;
        mvpArrayOfFloats[11] = mvp.M34;
        mvpArrayOfFloats[12] = mvp.M41;
        mvpArrayOfFloats[13] = mvp.M42;
        mvpArrayOfFloats[14] = mvp.M43;
        mvpArrayOfFloats[15] = mvp.M44;

        fixed (float* mvpPtr = mvpArrayOfFloats)
        {
            Gl.UniformMatrix4fv(mvpLocation, 1, 0, mvpPtr);
        }

        Gl.BindVertexArray(vertexArray);
        Gl.DrawArrays(Gl.Enums.Triangles, 0, 3);

        window.SwapBuffers();
        GlfwEvents.Poll();
    }
}

return;

static unsafe void ThrowIfShaderFailed(uint shader)
{
    int status;
    Gl.GetShaderiv(shader, Gl.Enums.CompileStatus, &status);

    if (status != 0)
    {
        return;
    }

    int logLength;
    Gl.GetShaderiv(shader, Gl.Enums.InfoLogLength, &logLength);

    var log = new byte[Math.Max(logLength, 1)];

    fixed (byte* logPtr = log)
    {
        Gl.GetShaderInfoLog(shader, log.Length, null, logPtr);
    }

    throw new InvalidOperationException(
        $"Shader {shader} compilation failed: {Encoding.UTF8.GetString(log)} "
            + $"(logLength = {logLength}, glGetError = 0x{Gl.GetError():X})"
    );
}

static unsafe void ThrowIfProgramFailed(uint program)
{
    int status;
    Gl.GetProgramiv(program, Gl.Enums.LinkStatus, &status);

    if (status != 0)
    {
        return;
    }

    int logLength;
    Gl.GetProgramiv(program, Gl.Enums.InfoLogLength, &logLength);

    var log = new byte[Math.Max(logLength, 1)];

    fixed (byte* logPtr = log)
    {
        Gl.GetProgramInfoLog(program, log.Length, null, logPtr);
    }

    throw new InvalidOperationException($"Program linking failed: {Encoding.UTF8.GetString(log)}");
}

record struct Vec2(float X, float Y);

record struct Vec3(float X, float Y, float Z);

struct Vertex
{
    public Vec2 Position;
    public Vec3 Color;
}
