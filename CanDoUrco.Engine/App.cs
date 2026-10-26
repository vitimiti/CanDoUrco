// This is the Can Do Urco engine.
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

using System.Runtime.InteropServices;
using System.Text;
using CanDoUrco.Engine.Options;
using CanDoUrco.Native.Sdl3;

namespace CanDoUrco.Engine;

public sealed class App : IDisposable
{
    private bool _disposedValue;

    public App(AppOptions? options)
    {
        if (options is not null)
        {
            if (options.Name is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataNameString,
                    Encoding.UTF8.GetBytes(options.Name)
                );
            }

            if (options.Version is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataVersionString,
                    Encoding.UTF8.GetBytes(options.Version.ToString())
                );
            }

            if (options.Identifier is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataIdentifierString,
                    Encoding.UTF8.GetBytes(options.Identifier)
                );
            }

            if (options.Creator is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataCreatorString,
                    Encoding.UTF8.GetBytes(options.Creator)
                );
            }

            if (options.Copyright is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataCopyrightString,
                    Encoding.UTF8.GetBytes(options.Copyright)
                );
            }

            if (options.Url is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataUrlString,
                    Encoding.UTF8.GetBytes(options.Url.ToString())
                );
            }

            if (options.AppType is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataTypeString,
                    Encoding.UTF8.GetBytes(options.AppType)
                );
            }
        }
    }

    public void Dispose()
    {
        if (_disposedValue)
        {
            return;
        }

        Sdl.Quit();

        _disposedValue = true;
    }

    private static unsafe void SetAppMetadataProperty(
        ReadOnlySpan<byte> name,
        ReadOnlySpan<byte> value
    )
    {
        fixed (byte* namePtr = name)
        {
            fixed (byte* valuePtr = value)
            {
                if (!Sdl.SetAppMetadataProperty(namePtr, valuePtr))
                {
                    var error = Marshal.PtrToStringUTF8((nint)Sdl.GetError());
                    throw new InvalidOperationException(
                        $"Failed to set app metadata property: {error}."
                    );
                }
            }
        }
    }
}
