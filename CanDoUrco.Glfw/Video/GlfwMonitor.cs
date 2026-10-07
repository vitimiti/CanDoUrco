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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Events;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents the physical monitor.
/// </summary>
/// <remarks>
/// <para>This class is sealed.</para>
/// <para>This class inherits from <see cref="IDisposable"/>.</para>
/// </remarks>
public sealed class GlfwMonitor : IDisposable
{
    /// <summary>
    /// This event represents monitor connection events.
    /// </summary>
    public event EventHandler<GlfwMonitorConnectionEventArgs>? ConnectionEvent;

    private static readonly Dictionary<GlfwMonitor, nint> Handles = [];

    private bool _disposedValue;

    internal unsafe Native.Glfw.Monitor* Handle { get; private init; }

    internal unsafe GlfwMonitor(Native.Glfw.Monitor* handle)
    {
        Handle = handle;
        Handles[this] = (nint)Handle;
        Native.Glfw.SetMonitorCallback(&HandleEvent);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Finalizes the instance of <see cref="GlfwMonitor"/>.
    /// </summary>
    ~GlfwMonitor()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the managed and unmanaged resources held by <see cref="GlfwMonitor"/>.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the position of the monitor's viewport on the virtual screen.
    /// </summary>
    /// <returns>A new <see cref="Point"/> with the monitor's viewport position.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Point GetPosition()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetMonitorPosition(Handle, out var xPos, out var yPos);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Point(xPos, yPos);
    }

    /// <summary>
    /// Gets the work area of the monitor.
    /// </summary>
    /// <returns>A new <see cref="Rectangle"/> with the monitor's work area.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Rectangle GetWorkArea()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetMonitorWorkArea(
            Handle,
            out var xPos,
            out var yPos,
            out var width,
            out var height
        );

        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Rectangle(xPos, yPos, width, height);
    }

    /// <summary>
    /// Gets the physical size of the monitor, in millimeters.
    /// </summary>
    /// <returns>A new <see cref="Size"/> with the monitor's physical size, in millimeters</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Size GetPhysicalSizeInMillimeters()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetMonitorPhysicalSize(
            Handle,
            out var widthMillimeters,
            out var heightMillimeters
        );

        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Size(widthMillimeters, heightMillimeters);
    }

    /// <summary>
    /// Gets the content scale of the monitor.
    /// </summary>
    /// <returns>A new <see cref="PointF"/> with the monitor's content scale.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe PointF GetMonitorContentScale()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetMonitorContentScale(Handle, out var xScale, out var yScale);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new PointF(xScale, yScale);
    }

    /// <summary>
    /// Gets the name of the monitor.
    /// </summary>
    /// <returns>A new <see cref="string"/> with the name of the monitor.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe string GetName()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetMonitorName(Handle);
        if (result is null)
        {
            GlfwErrorUtilities.ThrowError();
        }

        return Utf8StringMarshaller.ConvertToManaged(result) ?? string.Empty;
    }

    /// <summary>
    /// Gets a collection of video modes available for the monitor.
    /// </summary>
    /// <returns>A new <see cref="ICollection{T}"/> with the available video modes.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe ICollection<GlfwVideoMode> GetVideoModes()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var arrayPtr = Native.Glfw.GetVideoModes(Handle, out var count);
        if (arrayPtr is null)
        {
            GlfwErrorUtilities.ThrowError();
        }

        var videoModes = new GlfwVideoMode[count];
        for (var i = 0; i < count; i++)
        {
            videoModes[i] = FromUnmanaged(arrayPtr[i]);
        }

        return videoModes;
    }

    /// <summary>
    /// Gets the current video mode for the monitor.
    /// </summary>
    /// <returns>A new <see cref="GlfwVideoMode"/> with the current video mode.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwVideoMode GetCurrentVideoMode()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var ptr = Native.Glfw.GetVideoMode(Handle);
        if (ptr is null)
        {
            GlfwErrorUtilities.ThrowError();
        }

        return FromUnmanaged(*ptr);
    }

    /// <summary>
    /// Generates a gamma ramp and sets it.
    /// </summary>
    /// <param name="gamma">The desired exponent.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetGamma(float gamma)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetGamma(Handle, gamma);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the current monitor's gamma ramp.
    /// </summary>
    /// <returns>A new <see cref="GlfwGammaRamp"/> with the monitor's gamma ramp.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when getting the gamma ramp fails</exception>
    public unsafe GlfwGammaRamp GetGammaRamp()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var ptr = Native.Glfw.GetGammaRamp(Handle);
        if (ptr is null)
        {
            GlfwErrorUtilities.ThrowError();
        }

        var channelSize = ptr->Size;
        var redChannel = new Span<ushort>(ptr->Red, (int)channelSize);
        var greenChannel = new Span<ushort>(ptr->Green, (int)channelSize);
        var blueChannel = new Span<ushort>(ptr->Blue, (int)channelSize);
        return new GlfwGammaRamp()
        {
            Red = redChannel.ToArray(),
            Green = greenChannel.ToArray(),
            Blue = blueChannel.ToArray(),
        };
    }

    /// <summary>
    /// Sets the current gamma ramp for the monitor.
    /// </summary>
    /// <param name="ramp">The <see cref="GlfwGammaRamp"/> to set.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwMonitor"/> instance is already disposed.</exception>
    /// <exception cref="ArgumentException">Thrown when the <paramref name="ramp"/> contains channesl of differnt sizes.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetGammaRamp(GlfwGammaRamp ramp)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(ramp);
        if (
            ramp.Red.Count != ramp.Green.Count
            || ramp.Red.Count != ramp.Blue.Count
            || ramp.Green.Count != ramp.Blue.Count
        )
        {
            throw new ArgumentException(
                "All channels of the gamma ramp must be the same size.",
                nameof(ramp)
            );
        }

        var rampSize = ramp.Red.Count;
        Native.Glfw.GammaRamp nativeRamp = default;
        nativeRamp.Size = (uint)rampSize;
        var redSpan = ramp.Red.ToArray().AsSpan();
        var greenSpan = ramp.Green.ToArray().AsSpan();
        var blueSPan = ramp.Blue.ToArray().AsSpan();
        fixed (ushort* redPtr = redSpan)
        {
            nativeRamp.Red = redPtr;
        }

        fixed (ushort* greenPtr = greenSpan)
        {
            nativeRamp.Green = greenPtr;
        }

        fixed (ushort* bluePtr = blueSPan)
        {
            nativeRamp.Blue = bluePtr;
        }

        Native.Glfw.SetGammaRamp(Handle, in nativeRamp);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static GlfwVideoMode FromUnmanaged(Native.Glfw.VidMode mode) =>
        new()
        {
            Size = new Size(mode.Width, mode.Height),
            BitDepth = (mode.RedBits, mode.GreenBits, mode.BlueBits),
            RefreshRateInHertz = mode.RefreshRate,
        };

    private unsafe void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            Handles.Remove(this);
        }

        if (Handles.Count == 0)
        {
            Native.Glfw.SetMonitorCallback(null);
            // Ignore errors intentionally
        }

        _disposedValue = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleEvent(Native.Glfw.Monitor* monitor, int @event)
    {
        if (monitor is null || Handles.Count == 0)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)monitor)
            {
                continue;
            }

            instance.ConnectionEvent?.Invoke(
                instance,
                new GlfwMonitorConnectionEventArgs(
                    @event switch
                    {
                        Native.Glfw.ConnectedDefine => GlfwMonitorConnectionEvent.Connected,
                        _ => GlfwMonitorConnectionEvent.Disconnected,
                    }
                )
            );
        }
    }
}
