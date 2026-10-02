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
using CanDoUrco.IO.Images;
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

// csharpier-ignore
var vertices = new Vertex[]
{
    new() { Position = new Vector3(.5F, .5F, 0F), Color = Color.White.ToNormalizedRgb(), TextureCoordinates = new Vector2(1F, 1F), },   // top right
    new() { Position = new Vector3(.5F, -.5F, 0F), Color = Color.White.ToNormalizedRgb(), TextureCoordinates = new Vector2(1F, 0F), },  // bottom right
    new() { Position = new Vector3(-.5F, -.5F, 0F), Color = Color.White.ToNormalizedRgb(), TextureCoordinates = new Vector2(0F, 0F), }, // bottom left
    new() { Position = new Vector3(-.5F, .5F, 0F), Color = Color.White.ToNormalizedRgb(), TextureCoordinates = new Vector2(0F, 1F), },  // top left
};

// csharpier-ignore
var indices = new uint[]
{
    0, 1, 3, // first triangle
    1, 2, 3  // second triangle
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
    Span<uint> ebo = stackalloc uint[1];
    GL.GenVertexArrays(vao);
    GL.GenBuffers(vbo);
    GL.GenBuffers(ebo);

    GL.BindVertexArray(vao[0]);

    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, vbo[0]);
    GL.BufferData(
        GL.BufferTargetARBEnum.ArrayBuffer,
        vertices.Length * Marshal.SizeOf<Vertex>(),
        MemoryMarshal.Cast<Vertex, float>(vertices),
        GL.BufferUsageARBEnum.StaticDraw
    );

    GL.BindBuffer(GL.BufferTargetARBEnum.ElementArrayBuffer, ebo[0]);
    GL.BufferData(
        GL.BufferTargetARBEnum.ElementArrayBuffer,
        indices.Length * sizeof(uint),
        indices,
        GL.BufferUsageARBEnum.StaticDraw
    );

    GL.VertexAttribPointer(
        index: 0,
        size: 3,
        GL.VertexAttribPointerTypeEnum.Float,
        normalized: false,
        stride: 8 * sizeof(float),
        pointer: 0
    );
    GL.EnableVertexAttribArray(0);

    GL.VertexAttribPointer(
        index: 1,
        size: 3,
        GL.VertexAttribPointerTypeEnum.Float,
        normalized: false,
        stride: 8 * sizeof(float),
        pointer: 3 * sizeof(float)
    );
    GL.EnableVertexAttribArray(1);

    GL.VertexAttribPointer(
        index: 2,
        size: 2,
        GL.VertexAttribPointerTypeEnum.Float,
        normalized: false,
        stride: 8 * sizeof(float),
        pointer: 6 * sizeof(float)
    );
    GL.EnableVertexAttribArray(2);

    Span<uint> textures = stackalloc uint[1];
    GL.GenTextures(textures);
    GL.BindTexture(GL.TextureTargetEnum.Texture2D, textures[0]);

    GL.TexParameteri(
        GL.TextureTargetEnum.Texture2D,
        GL.TextureParameterNameEnum.TextureWrapS,
        (int)GL.TextureWrapModeEnum.Repeat
    );
    GL.TexParameteri(
        GL.TextureTargetEnum.Texture2D,
        GL.TextureParameterNameEnum.TextureWrapT,
        (int)GL.TextureWrapModeEnum.Repeat
    );

    GL.TexParameteri(
        GL.TextureTargetEnum.Texture2D,
        GL.TextureParameterNameEnum.TextureMinFilter,
        (int)GL.TextureMinFilterEnum.LinearMipmapLinear
    );
    GL.TexParameteri(
        GL.TextureTargetEnum.Texture2D,
        GL.TextureParameterNameEnum.TextureMagFilter,
        (int)GL.TextureMagFilterEnum.Linear
    );

    using var textureStream =
        typeof(Program).Assembly.GetManifestResourceStream("Playground.Resources.Textures.Wall.jpg")
        ?? throw new InvalidOperationException("Texture not found.");

    {
        var imageData = ImageLoader.Load(textureStream);
        GL.TexImage2D(
            GL.TextureTargetEnum.Texture2D,
            level: 0,
            (int)GL.PixelFormatEnum.Rgb,
            imageData.Size.Width,
            imageData.Size.Height,
            border: 0,
            GL.PixelFormatEnum.Rgb,
            GL.PixelTypeEnum.UnsignedByte,
            imageData.Data
        );
        GL.GenerateMipmap(GL.TextureTargetEnum.Texture2D);
    }

    while (!window.ShouldClose())
    {
        ProcessInput(window);

        GL.ClearColor(.2F, .3F, .3F, 1F);
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit);

        GL.BindTexture(GL.TextureTargetEnum.Texture2D, textures[0]);

        shader.Use();

        GL.BindVertexArray(vao[0]);
        GL.DrawElements(
            GL.PrimitiveTypeEnum.Triangles,
            count: 6,
            GL.DrawElementsTypeEnum.UnsignedInt,
            indices: 0
        );

        window.SwapBuffers();
        GlfwEvents.Poll();
    }

    GL.DeleteVertexArrays(vao);
    GL.DeleteBuffers(vbo);
    GL.DeleteBuffers(ebo);
    GL.DeleteTextures(textures);
});

[StructLayout(LayoutKind.Sequential)]
struct Vertex
{
    public Vector3 Position;
    public Vector3 Color;
    public Vector2 TextureCoordinates;
}
