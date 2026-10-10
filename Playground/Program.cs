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
using System.Numerics;
using System.Text;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Video;
using CanDoUrco.IO.Images;
using CanDoUrco.Utilities;
using CanDoUrco.Utilities.Drawing;
using CanDoUrco.Utilities.Mathematics;
using CanDoUrco.Utilities.Primitives;
using Playground;

var windowSize = new Size(1024, 768);

return CrashReporter.Run(() =>
{
    // csharpier-ignore
    ReadOnlySpan<float> vertices =
    [
        -1F,-1F,-1F,
        -1F,-1F, 1F,
        -1F, 1F, 1F,
        1F, 1F,-1F,
        -1F,-1F,-1F,
        -1F, 1F,-1F,
        1F,-1F, 1F,
        -1F,-1F,-1F,
        1F,-1F,-1F,
        1F, 1F,-1F,
        1F,-1F,-1F,
        -1F,-1F,-1F,
        -1F,-1F,-1F,
        -1F, 1F, 1F,
        -1F, 1F,-1F,
        1F,-1F, 1F,
        -1F,-1F, 1F,
        -1F,-1F,-1F,
        -1F, 1F, 1F,
        -1F,-1F, 1F,
        1F,-1F, 1F,
        1F, 1F, 1F,
        1F,-1F,-1F,
        1F, 1F,-1F,
        1F,-1F,-1F,
        1F, 1F, 1F,
        1F,-1F, 1F,
        1F, 1F, 1F,
        1F, 1F,-1F,
        -1F, 1F,-1F,
        1F, 1F, 1F,
        -1F, 1F,-1F,
        -1F, 1F, 1F,
        1F, 1F, 1F,
        -1F, 1F, 1F,
        1F,-1F, 1F,
    ];

    // csharpier-ignore
    ReadOnlySpan<float> uvs =
    [
        .000059F, .000004F,
        .000103F, .336048F,
        .335973F, .335903F,
        1.000023F, .000013F,
        .667979F, .335851F,
        .999958F, .336064F,
        .667979F, .335851F,
        .336024F, .671877F,
        .667969F, .671889F,
        1.000023F, .000013F,
        .668104F, .000013F,
        .667979F, .335851F,
        .000059F, .000004F,
        .335973F, .335903F,
        .336098F, .000071F,
        .667979F, .335851F,
        .335973F, .335903F,
        .336024F, .671877F,
        1.000004F, .671847F,
        .999958F, .336064F,
        .667979F, .335851F,
        .668104F, .000013F,
        .335973F, .335903F,
        .667979F, .335851F,
        .335973F, .335903F,
        .668104F, .000013F,
        .336098F, .000071F,
        .000103F, .336048F,
        .000004F, .671870F,
        .336024F, .671877F,
        .000103F, .336048F,
        .336024F, .671877F,
        .335973F, .335903F,
        .667969F, .671889F,
        1.000004F, .671847F,
        .667979F, .335851F,
    ];

    using var _ = new GlfwNativeContext();
    using var window = new GlfwWindow(
        windowSize,
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

    // Create and compile our GLSL program from the shaders
    var programID = LoadShaders("Resources/Shaders/Vertex.glsl", "Resources/Shaders/Fragment.glsl");
    GL.UseProgram(programID);
    var matrixID = GL.GetUniformLocation(programID, "MVP");

    var framebufferSize = window.GetFramebufferSize();
    GL.Viewport(0, 0, framebufferSize.Width, framebufferSize.Height);

    Span<uint> texture = stackalloc uint[1];
    LoadTextures(texture, ["Resources/Textures/UVTemplate.bmp"]);

    var projection = Matrix4x4.CreatePerspectiveFieldOfView(
        fieldOfView: 45F.ToRadians(),
        aspectRatio: framebufferSize.Width / (float)framebufferSize.Height,
        nearPlaneDistance: .1F,
        farPlaneDistance: 100F
    );

    var view = Matrix4x4.CreateLookAt(
        cameraPosition: new Vector3(4F, 3F, 3F),
        cameraTarget: Vector3.Zero,
        cameraUpVector: Vector3.UnitY
    );

    var model = Matrix4x4.Identity;
    var mvp = model * view * projection;

    window.FramebufferSizeUpdate += (_, args) =>
    {
        GL.Viewport(0, 0, args.Size.Width, args.Size.Height);
        if (args.Size.Height == 0 || args.Size.Width == 0)
        {
            return;
        }

        projection = Matrix4x4.CreatePerspectiveFieldOfView(
            fieldOfView: 45F.ToRadians(),
            aspectRatio: args.Size.Width / (float)args.Size.Height,
            nearPlaneDistance: .1F,
            farPlaneDistance: 100F
        );

        mvp = model * view * projection;
        GL.UniformMatrix4fv(matrixID, count: 1, transpose: false, value: mvp.ToArray());
    };

    // The VBO (Vertex Buffer Object)
    Span<uint> vertexBuffer = stackalloc uint[1];
    GL.GenBuffers(vertexBuffer);
    // Talk about the above buffer from here on
    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, vertexBuffer[0]);
    // Give the vertices to OpenGL
    GL.BufferData(
        GL.BufferTargetARBEnum.ArrayBuffer,
        vertices.Length * sizeof(float),
        vertices,
        GL.BufferUsageARBEnum.StaticDraw
    );

    // The UV buffer (VBO for texture coordinates)
    Span<uint> uvBuffer = stackalloc uint[1];
    GL.GenBuffers(uvBuffer);
    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, uvBuffer[0]);
    GL.BufferData(
        GL.BufferTargetARBEnum.ArrayBuffer,
        uvs.Length * sizeof(float),
        uvs,
        GL.BufferUsageARBEnum.StaticDraw
    );

    // The VAO (Vertex Array Object)
    Span<uint> vertexArray = stackalloc uint[1];
    GL.GenVertexArrays(vertexArray);
    GL.BindVertexArray(vertexArray[0]);

    // Get a handle for our "MVP" uniform
    // Send our transformation to the currently bound shader
    GL.UniformMatrix4fv(matrixID, count: 1, transpose: false, value: mvp.ToArray());

    // Bind texture
    GL.ActiveTexture(GL.TextureUnitEnum.Texture0);
    GL.BindTexture(GL.TextureTargetEnum.Texture2D, texture[0]);
    // Set the texture sampler sampler to use Texture Unit 0
    var textureSamplerID = GL.GetUniformLocation(programID, "myTextureSampler");
    GL.Uniform1i(textureSamplerID, 0);

    // Enable depth test
    GL.Enable(GL.EnableCapEnum.DepthTest);
    // Accept fragment if it is closer to the camera than the former one
    GL.DepthFunc(GL.DepthFunctionEnum.Less);

    var backgroundColor = Color.DarkBlue;
    while (window.GetKeyState(GlfwKey.Escape) is not GlfwKeyState.Pressed && !window.ShouldClose())
    {
        // Clear the screen
        backgroundColor.ClearGraphics(GL.ClearColor);
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit | GL.ClearBufferMaskEnum.DepthBufferBit);

        // Draw triangle...
        // 1st attribute buffer : vertices
        GL.EnableVertexAttribArray(0);
        GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, vertexBuffer[0]);
        GL.VertexAttribPointer(
            index: 0,
            size: 3,
            GL.VertexAttribPointerTypeEnum.Float,
            normalized: false,
            stride: 0,
            pointer: 0 // offset
        );

        // 2nd attribute buffer : texture coordinates
        GL.EnableVertexAttribArray(1);
        GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, uvBuffer[0]);
        GL.VertexAttribPointer(
            index: 1,
            size: 2,
            GL.VertexAttribPointerTypeEnum.Float,
            normalized: false,
            stride: 0,
            pointer: 0 // offset
        );

        // Draw the triangle
        GL.DrawArrays(GL.PrimitiveTypeEnum.Triangles, first: 0, count: 12 * 3); // Starting from vertex 0, 12 triangles total
        GL.DisableVertexAttribArray(0); // Disable the vertex attribute array after drawing
        GL.DisableVertexAttribArray(1); // Disable the texture coordinate attribute array after drawing

        // Swap buffers
        window.SwapBuffers();
        GlfwEvents.Poll();
    }
});

static uint LoadShaders(string vertexFilePath, string fragmentFilePath)
{
    var vertexShaderID = GL.CreateShader(GL.ShaderTypeEnum.VertexShader);
    var fragmentShaderID = GL.CreateShader(GL.ShaderTypeEnum.FragmentShader);

    Span<int> infoLogLength = stackalloc int[1];
    Span<int> result = stackalloc int[1];

    Console.WriteLine($"Compiling shader: {vertexFilePath}");
    var vertexShaderSource = File.ReadAllText(vertexFilePath);
    GL.ShaderSource(vertexShaderID, [vertexShaderSource], [vertexShaderSource.Length]);
    GL.CompileShader(vertexShaderID);
    GL.GetShaderiv(vertexShaderID, GL.ShaderParameterNameEnum.CompileStatus, result);
    GL.GetShaderiv(vertexShaderID, GL.ShaderParameterNameEnum.InfoLogLength, infoLogLength);
    if (infoLogLength[0] > 0)
    {
        Span<byte> vertexShaderErrorMessage = stackalloc byte[infoLogLength[0] + 1];
        GL.GetShaderInfoLog(vertexShaderID, ref infoLogLength[0], vertexShaderErrorMessage);
        Console.WriteLine(
            $"Vertex shader compilation error: {Encoding.UTF8.GetString(vertexShaderErrorMessage)}"
        );
    }

    Console.WriteLine($"Compiling shader: {fragmentFilePath}");
    var fragmentShaderSource = File.ReadAllText(fragmentFilePath);
    GL.ShaderSource(fragmentShaderID, [fragmentShaderSource], [fragmentShaderSource.Length]);
    GL.CompileShader(fragmentShaderID);
    GL.GetShaderiv(fragmentShaderID, GL.ShaderParameterNameEnum.CompileStatus, result);
    GL.GetShaderiv(fragmentShaderID, GL.ShaderParameterNameEnum.InfoLogLength, infoLogLength);
    if (infoLogLength[0] > 0)
    {
        Span<byte> fragmentShaderErrorMessage = stackalloc byte[infoLogLength[0] + 1];
        GL.GetShaderInfoLog(fragmentShaderID, ref infoLogLength[0], fragmentShaderErrorMessage);
        Console.WriteLine(
            $"Fragment shader compilation error: {Encoding.UTF8.GetString(fragmentShaderErrorMessage)}"
        );
    }

    Console.WriteLine("Linking program");
    var programID = GL.CreateProgram();
    GL.AttachShader(programID, vertexShaderID);
    GL.AttachShader(programID, fragmentShaderID);
    GL.LinkProgram(programID);

    GL.GetProgramiv(programID, GL.ProgramPropertyARBEnum.LinkStatus, result);
    GL.GetProgramiv(programID, GL.ProgramPropertyARBEnum.InfoLogLength, infoLogLength);
    if (infoLogLength[0] > 0)
    {
        Span<byte> programErrorMessage = stackalloc byte[infoLogLength[0] + 1];
        GL.GetProgramInfoLog(programID, ref infoLogLength[0], programErrorMessage);
        Console.WriteLine($"Program linking error: {Encoding.UTF8.GetString(programErrorMessage)}");
    }

    GL.DetachShader(programID, vertexShaderID);
    GL.DetachShader(programID, fragmentShaderID);

    GL.DeleteShader(vertexShaderID);
    GL.DeleteShader(fragmentShaderID);

    return programID;
}

static void LoadTextures(Span<uint> textures, IReadOnlyCollection<string> textureFilePaths)
{
    var count = textures.Length;
    var images = new ImageData[count];
    for (var i = 0; i < images.Length; i++)
    {
        images[i] = Image.Load(textureFilePaths.ElementAt(i), requiredChannelCount: 3);
    }

    // Create one OpenGL texture
    GL.GenTextures(textures);

    for (var i = 0; i < count; i++)
    {
        // Bind the newly created texture : all future texture functions will modify this texture
        GL.BindTexture(GL.TextureTargetEnum.Texture2D, textures[i]);

        // Give the image to OpenGL
        GL.TexImage2D(
            GL.TextureTargetEnum.Texture2D,
            level: 0,
            (int)GL.PixelFormatEnum.Rgb,
            images[i].Size.Width,
            images[i].Size.Height,
            border: 0,
            GL.PixelFormatEnum.Rgb,
            GL.PixelTypeEnum.UnsignedByte,
            images[i].Pixels
        );

        GL.TexParameteri(
            GL.TextureTargetEnum.Texture2D,
            GL.TextureParameterNameEnum.TextureMagFilter,
            (int)GL.TextureMagFilterEnum.Nearest
        );

        GL.TexParameteri(
            GL.TextureTargetEnum.Texture2D,
            GL.TextureParameterNameEnum.TextureMinFilter,
            (int)GL.TextureMinFilterEnum.Nearest
        );
    }
}
