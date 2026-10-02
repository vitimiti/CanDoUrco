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

using System.Text;

namespace Playground;

internal sealed class Shader : IDisposable
{
    private bool _disposedValue;

    public uint ID { get; private init; }

    public Shader(Stream vertexStream, Stream fragmentStream)
    {
        var vertexSource = new StreamReader(vertexStream).ReadToEnd();
        var fragmentSource = new StreamReader(fragmentStream).ReadToEnd();

        var vertex = GL.CreateShader(GL.ShaderTypeEnum.VertexShader);
        GL.ShaderSource(vertex, [vertexSource], [vertexSource.Length]);
        GL.CompileShader(vertex);
        CheckShaderCompilation(vertex, "Vertex");

        var fragment = GL.CreateShader(GL.ShaderTypeEnum.FragmentShader);
        GL.ShaderSource(fragment, [fragmentSource], [fragmentSource.Length]);
        GL.CompileShader(fragment);
        CheckShaderCompilation(fragment, "Fragment");

        ID = GL.CreateProgram();
        GL.AttachShader(ID, vertex);
        GL.AttachShader(ID, fragment);
        GL.LinkProgram(ID);
        CheckProgramLinking(ID);
        GL.DetachShader(ID, vertex);
        GL.DetachShader(ID, fragment);
        GL.DeleteShader(vertex);
        GL.DeleteShader(fragment);
    }

    ~Shader()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    public void Use() => GL.UseProgram(ID);

    public void Set(string name, bool value) =>
        GL.Uniform1i(GL.GetUniformLocation(ID, name), value ? 1 : 0);

    public void Set(string name, int value) => GL.Uniform1i(GL.GetUniformLocation(ID, name), value);

    public void Set(string name, float value) =>
        GL.Uniform1f(GL.GetUniformLocation(ID, name), value);

    private static void CheckShaderCompilation(uint shader, string shaderType)
    {
        Span<int> status = stackalloc int[1];
        GL.GetShaderiv(shader, GL.ShaderParameterNameEnum.CompileStatus, status);
        if (status[0] != 0)
        {
            return;
        }

        GL.GetShaderiv(shader, GL.ShaderParameterNameEnum.InfoLogLength, status);
        var buffer = new byte[int.Max(status[0], 1)];
        var written = 0;
        GL.GetShaderInfoLog(shader, ref written, buffer);
        throw new InvalidOperationException(
            $"{shaderType} shader compilation failed: {DecodeInfoLog(buffer, written)}"
        );
    }

    private static void CheckProgramLinking(uint program)
    {
        Span<int> status = stackalloc int[1];
        GL.GetProgramiv(program, GL.ProgramPropertyARBEnum.LinkStatus, status);
        if (status[0] != 0)
        {
            return;
        }

        GL.GetProgramiv(program, GL.ProgramPropertyARBEnum.InfoLogLength, status);
        var buffer = new byte[int.Max(status[0], 1)];
        var written = 0;
        GL.GetProgramInfoLog(program, ref written, buffer);
        throw new InvalidOperationException(
            $"Program linking failed: {DecodeInfoLog(buffer, written)}"
        );
    }

    private static string DecodeInfoLog(byte[] buffer, int written) =>
        Encoding.UTF8.GetString(buffer, 0, int.Clamp(written, 0, buffer.Length)).TrimEnd();

    private void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            // no-op
        }

        GL.DeleteProgram(ID);
        _disposedValue = true;
    }
}
