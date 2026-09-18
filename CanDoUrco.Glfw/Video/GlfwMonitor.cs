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

    private readonly unsafe Native.Glfw.Monitor* _handle;

    private bool _disposedValue;

    internal unsafe GlfwMonitor(Native.Glfw.Monitor* handle)
    {
        _handle = handle;
        if (Handles.ContainsKey(this))
        {
            Handles[this] = (nint)_handle;
        }
        else
        {
            Handles.Add(this, (nint)_handle);
        }

        Native.Glfw.SetMonitorCallback(&HandleEvent);
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
    /// <exception cref="GlfwException">Thrown when getting the poisition failed.</exception>
    public unsafe Point GetPosition()
    {
        Native.Glfw.GetMonitorPosition(_handle, out var xPos, out var yPos);
        ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
        return new Point(xPos, yPos);
    }

    /// <summary>
    /// Gets the work area of the monitor.
    /// </summary>
    /// <returns>A new <see cref="Rectangle"/> with the monitor's work area.</returns>
    /// <exception cref="GlfwException">Thrown when getting the work area failed.</exception>
    public unsafe Rectangle GetWorkArea()
    {
        Native.Glfw.GetMonitorWorkArea(
            _handle,
            out var xPos,
            out var yPos,
            out var width,
            out var height
        );

        ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
        return new Rectangle(xPos, yPos, width, height);
    }

    /// <summary>
    /// Gets the physical size of the monitor, in millimeters.
    /// </summary>
    /// <returns>A new <see cref="Size"/> with the monitor's physical size, in millimeters</returns>
    /// <exception cref="GlfwException">Thrown when getting the monitors physical size failed.</exception>
    public unsafe Size GetPhysicalSizeInMillimeters()
    {
        Native.Glfw.GetMonitorPhysicalSize(
            _handle,
            out var widthMillimeters,
            out var heightMillimeters
        );

        ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
        return new Size(widthMillimeters, heightMillimeters);
    }

    /// <summary>
    /// Gets the content scale of the monitor.
    /// </summary>
    /// <returns>A new <see cref="PointF"/> with the monitor's content scale.</returns>
    /// <exception cref="GlfwException">Thrown when getting the monitor's content scale failed.</exception>
    public unsafe PointF GetMonitorContentScale()
    {
        Native.Glfw.GetMonitorContentScale(_handle, out var xScale, out var yScale);
        ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
        return new PointF(xScale, yScale);
    }

    /// <summary>
    /// Gets the name of the monitor.
    /// </summary>
    /// <returns>A new <see cref="string"/> with the name of the monitor.</returns>
    /// <exception cref="GlfwException">Thrown when getting the name of the monitor failed.</exception>
    public unsafe string GetName()
    {
        var result = Native.Glfw.GetMonitorName(_handle);
        if (result is null)
        {
            ErrorUtilities.CheckAndThrowErrorFromBadReturnMethod();
        }

        return Utf8StringMarshaller.ConvertToManaged(result) ?? string.Empty;
    }

    private unsafe void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            Handles.Clear();
        }

        Native.Glfw.SetMonitorCallback(null);
        _disposedValue = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleEvent(Native.Glfw.Monitor* monitor, int @event)
    {
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
