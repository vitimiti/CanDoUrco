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

using System.Text;
using CanDoUrco.Glfw;
using CanDoUrco.Glfw.Input;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Video;
using CanDoUrco.Graphics.Primitives;
using CanDoUrco.Utilities;
using Playground;

return CrashReporter.Run(() =>
{
    Span<float> vertices = [-1F, -1F, 0F, 1F, -1F, 0F, 0F, 1F, 0F];

    using var _ = new GlfwNativeContext();
    using var window = new GlfwWindow(
        new System.Drawing.Size(1024, 768),
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

    // The VAO (Vertex Array Object)
    Span<uint> vertexArray = stackalloc uint[1];
    GL.GenVertexArrays(vertexArray);
    GL.BindVertexArray(vertexArray[0]);

    // Create and compile our GLSL program from the shaders
    var programID = LoadShaders("Resources/Shaders/Vertex.glsl", "Resources/Shaders/Fragment.glsl");

    var backgroundColor = new Color(0F, 0F, .4F, 0F);
    while (window.GetKeyState(GlfwKey.Escape) is not GlfwKeyState.Pressed && !window.ShouldClose())
    {
        // Clear the screen
        backgroundColor.GraphicsClear(GL.ClearColor);
        GL.Clear(GL.ClearBufferMaskEnum.ColorBufferBit | GL.ClearBufferMaskEnum.DepthBufferBit);

        // Use our shader
        GL.UseProgram(programID);

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

        // Draw the triangle
        GL.DrawArrays(GL.PrimitiveTypeEnum.Triangles, first: 0, count: 3); // Starting from vertex 0, 3 vertices total
        GL.DisableVertexAttribArray(0); // Disable the vertex attribute array after drawing

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
