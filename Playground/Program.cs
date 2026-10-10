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
using CanDoUrco.Utilities;
using CanDoUrco.Utilities.Drawing;
using CanDoUrco.Utilities.Mathematics;
using CanDoUrco.Utilities.Primitives;
using Playground;

var windowSize = new Size(1024, 768);

return CrashReporter.Run(() =>
{
    // csharpier-ignore
    Span<float> vertices =
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
        1F,-1F, 1F
    ];

    // csharpier-ignore
    Span<float> colors =
    [
        .583F,  .771F,  .014F,
        .609F,  .115F,  .436F,
        .327F,  .483F,  .844F,
        .822F,  .569F,  .201F,
        .435F,  .602F,  .223F,
        .310F,  .747F,  .185F,
        .597F,  .770F,  .761F,
        .559F,  .436F,  .730F,
        .359F,  .583F,  .152F,
        .483F,  .596F,  .789F,
        .559F,  .861F,  .639F,
        .195F,  .548F,  .859F,
        .014F,  .184F,  .576F,
        .771F,  .328F,  .970F,
        .406F,  .615F,  .116F,
        .676F,  .977F,  .133F,
        .971F,  .572F,  .833F,
        .140F,  .616F,  .489F,
        .997F,  .513F,  .064F,
        .945F,  .719F,  .592F,
        .543F,  .021F,  .978F,
        .279F,  .317F,  .505F,
        .167F,  .620F,  .077F,
        .347F,  .857F,  .137F,
        .055F,  .953F,  .042F,
        .714F,  .505F,  .345F,
        .783F,  .290F,  .734F,
        .722F,  .645F,  .174F,
        .302F,  .455F,  .848F,
        .225F,  .587F,  .040F,
        .517F,  .713F,  .338F,
        .053F,  .959F,  .120F,
        .393F,  .621F,  .362F,
        .673F,  .211F,  .457F,
        .820F,  .883F,  .371F,
        .982F,  .099F,  .879F
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

    var projection = Matrix4x4.CreatePerspectiveFieldOfView(
        fieldOfView: 45F.ToRadians(),
        aspectRatio: windowSize.Width / (float)windowSize.Height,
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

    // The color buffer (VBO for colors)
    Span<uint> colorBuffer = stackalloc uint[1];
    GL.GenBuffers(colorBuffer);
    GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, colorBuffer[0]);
    GL.BufferData(
        GL.BufferTargetARBEnum.ArrayBuffer,
        colors.Length * sizeof(float),
        colors,
        GL.BufferUsageARBEnum.StaticDraw
    );

    // The VAO (Vertex Array Object)
    Span<uint> vertexArray = stackalloc uint[1];
    GL.GenVertexArrays(vertexArray);
    GL.BindVertexArray(vertexArray[0]);

    // Create and compile our GLSL program from the shaders
    var programID = LoadShaders("Resources/Shaders/Vertex.glsl", "Resources/Shaders/Fragment.glsl");
    GL.UseProgram(programID);

    // Get a handle for our "MVP" uniform
    var matrixID = GL.GetUniformLocation(programID, "MVP");
    // Send our transformation to the currently bound shader
    GL.UniformMatrix4fv(matrixID, count: 1, transpose: false, value: mvp.ToArray());

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

        // 2nd attribute buffer : colors
        GL.EnableVertexAttribArray(1);
        GL.BindBuffer(GL.BufferTargetARBEnum.ArrayBuffer, colorBuffer[0]);
        GL.VertexAttribPointer(
            index: 1,
            size: 3,
            GL.VertexAttribPointerTypeEnum.Float,
            normalized: false,
            stride: 0,
            pointer: 0 // offset
        );

        // Draw the triangle
        GL.DrawArrays(GL.PrimitiveTypeEnum.Triangles, first: 0, count: 12 * 3); // Starting from vertex 0, 12 triangles total
        GL.DisableVertexAttribArray(0); // Disable the vertex attribute array after drawing
        GL.DisableVertexAttribArray(1); // Disable the color attribute array after drawing

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
