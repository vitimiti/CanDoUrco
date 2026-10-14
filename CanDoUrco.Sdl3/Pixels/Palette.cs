// An SDL3 interop library with dotnet safety and idioms.
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
using CanDoUrco.Sdl3.Exceptions;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3.Pixels;

public sealed class Palette : IDisposable
{
    private readonly Lock _sync = new();
    private bool _disposedValue;

    public unsafe IReadOnlyCollection<Color> Colors
    {
        get
        {
            try
            {
                lock (_sync)
                {
                    ObjectDisposedException.ThrowIf(_disposedValue, this);
                    var colors = new ReadOnlySpan<Sdl.Color>(
                        UnsafeHandle->Colors,
                        UnsafeHandle->NColors
                    );

                    var result = new Color[colors.Length];
                    for (var i = 0; i < colors.Length; i++)
                    {
                        result[i] = Color.FromArgb(
                            colors[i].A,
                            colors[i].R,
                            colors[i].G,
                            colors[i].B
                        );
                    }

                    return result;
                }
            }
            finally
            {
                GC.KeepAlive(this);
            }
        }
    }

    public unsafe uint Version
    {
        get
        {
            try
            {
                lock (_sync)
                {
                    ObjectDisposedException.ThrowIf(_disposedValue, this);
                    return UnsafeHandle->Version;
                }
            }
            finally
            {
                GC.KeepAlive(this);
            }
        }
    }

    public unsafe int ReferenceCount
    {
        get
        {
            try
            {
                lock (_sync)
                {
                    ObjectDisposedException.ThrowIf(_disposedValue, this);
                    return UnsafeHandle->RefCount;
                }
            }
            finally
            {
                GC.KeepAlive(this);
            }
        }
    }

    internal nint Handle { get; private set; }

    private unsafe Sdl.Palette* UnsafeHandle => (Sdl.Palette*)Handle;

    public Palette(int colorCount)
    {
        Handle = Sdl.CreatePalette(colorCount);
        if (Handle == 0)
        {
            throw new Sdl3Exception($"Failed to create palette with {colorCount} colors.");
        }
    }

    ~Palette()
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

    public unsafe void SetColors(ReadOnlySpan<Color> colors, int firstColor, int count)
    {
        try
        {
            lock (_sync)
            {
                ObjectDisposedException.ThrowIf(_disposedValue, this);
                var capacity = UnsafeHandle->NColors;

                ArgumentOutOfRangeException.ThrowIfNegative(firstColor);
                ArgumentOutOfRangeException.ThrowIfNegative(count);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, colors.Length);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(firstColor, capacity);
                ArgumentOutOfRangeException.ThrowIfGreaterThan(count, capacity - firstColor);

                var nativeColors = new Sdl.Color[int.Max(1, count)];
                for (var i = 0; i < count; i++)
                {
                    nativeColors[i] = new Sdl.Color
                    {
                        R = colors[i].R,
                        G = colors[i].G,
                        B = colors[i].B,
                        A = colors[i].A,
                    };
                }

                fixed (Sdl.Color* colorsPtr = nativeColors)
                {
                    if (!Sdl.SetPaletteColors(Handle, colorsPtr, firstColor, count))
                    {
                        throw new Sdl3Exception("Failed to set palette colors.");
                    }
                }
            }
        }
        finally
        {
            GC.KeepAlive(this);
        }
    }

    private void Dispose(bool disposing)
    {
        lock (_sync)
        {
            if (_disposedValue)
            {
                return;
            }

            if (disposing)
            {
                // no-op
            }

            if (Handle != 0)
            {
                Sdl.DestroyPalette(Handle);
                Handle = 0;
            }

            _disposedValue = true;
        }
    }
}
