// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents a GLFW window.
/// </summary>
public sealed class GlfwWindow : IDisposable
{
    private static readonly Dictionary<GlfwWindow, nint> Handles = [];

    private readonly unsafe Native.Glfw.Window* _handle;

    private bool _disposedValue;

    /// <summary>
    /// Initializes a new instance of <see cref="GlfwWindow"/>.
    /// </summary>
    /// <param name="size">A <see cref="Size"/> with the window dimensions.</param>
    /// <param name="title">A <see cref="string"/> with the window title.</param>
    /// <param name="monitor">A <see cref="GlfwMonitor"/> where the window must be placed, or <see langword="null"/> for the default monitor.</param>
    /// <param name="share">A <see cref="GlfwWindow"/> to share context with, or <see langword="null"/> to have a unique context.</param>
    /// <param name="options">The <see cref="GlfwWindowOptions"/> to pass, or <see langword="null"/> to use the default options.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="title"/> is <see langword="null"/>.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwWindow(
        Size size,
        string title,
        GlfwMonitor? monitor = null,
        GlfwWindow? share = null,
        Action<GlfwWindowOptions>? options = null
    )
    {
        ArgumentNullException.ThrowIfNull(title);
        var opts = new GlfwWindowOptions();
        options?.Invoke(opts);
        SetHints(opts);
        _handle = Native.Glfw.CreateWindow(
            size.Width,
            size.Height,
            title,
            monitor is null ? null : monitor.Handle,
            share is null ? null : share._handle
        );

        if (_handle is null)
        {
            ErrorUtilities.ThrowError();
        }

        if (Handles.ContainsKey(this))
        {
            Handles[this] = (nint)_handle;
        }
        else
        {
            Handles.Add(this, (nint)_handle);
        }
    }

    /// <summary>
    /// Finalises the <see cref="GlfwWindow"/> instance.
    /// </summary>
    ~GlfwWindow()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes all managed and unmanaged resources.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets whether the window should be closed.
    /// </summary>
    /// <returns><see langword="true"/> if the window should be closed, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool ShouldClose()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.WindowShouldClose(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Sets whether the window should be closed.
    /// </summary>
    /// <param name="value">A <see cref="bool"/> indicating whether the window should be closed.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetShouldClose(bool value)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowShouldClose(_handle, value);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the window title.
    /// </summary>
    /// <returns>A <see cref="string"/> with the current window title.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe string GetTitle()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var ptr = Native.Glfw.GetWindowTitle(_handle);
        if (ptr is null)
        {
            ErrorUtilities.ThrowError();
        }

        return Utf8StringMarshaller.ConvertToManaged(ptr) ?? string.Empty;
    }

    /// <summary>
    /// Sets the window title.
    /// </summary>
    /// <param name="title">A <see cref="string"/> with the new window title.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="title"/> is <see langword="null"/>.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetTitle(string title)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(title);
        Native.Glfw.SetWindowTitle(_handle, title);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Sets the given images as the window icon.
    /// </summary>
    /// <param name="images">A <see cref="IReadOnlyCollection{T}"/> of <see cref="GlfwImage"/> with the window icon.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="ArgumentNullException">Thrown when the <paramref name="images"/> are <see langword="null"/>.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetIcon(IReadOnlyCollection<GlfwImage> images)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(images);
        var count = images.Count;
        var nativeImages = new Native.Glfw.Image[count];
        for (var i = 0; i < count; i++)
        {
            nativeImages[i].Width = images.ElementAt(i).Size.Width;
            nativeImages[i].Height = images.ElementAt(i).Size.Height;
            var pixelsSpan = images.ElementAt(i).PixelData.ToArray().AsSpan();
            fixed (byte* pixelsPtr = pixelsSpan)
            {
                nativeImages[i].Pixels = pixelsPtr;
            }
        }

        Native.Glfw.SetWindowIcon(_handle, count, nativeImages);
    }

    private unsafe void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Handles.Clear();
            }

            Native.Glfw.DestroyWindow(_handle);
            _disposedValue = true;
        }
    }

    private static void SetHints(GlfwWindowOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        SetHint(Native.Glfw.ResizableDefine, options.Resizable);
        SetHint(Native.Glfw.VisibleDefine, options.Visible);
        SetHint(Native.Glfw.DecoratedDefine, options.Decorated);
        SetHint(Native.Glfw.FocusedDefine, options.Focused);
        SetHint(Native.Glfw.AutoIconifyDefine, options.AutoIconify);
        SetHint(Native.Glfw.FloatingDefine, options.Floating);
        SetHint(Native.Glfw.MaximizedDefine, options.Maximized);
        SetHint(Native.Glfw.CenterCursorDefine, options.CenterCursor);
        SetHint(Native.Glfw.TransparentFramebufferDefine, options.TransparentFramebuffer);
        SetHint(Native.Glfw.FocusOnShowDefine, options.FocusOnShow);
        SetHint(Native.Glfw.ScaleToMonitorDefine, options.ScaleToMonitor);
        SetHint(Native.Glfw.ScaleFramebufferDefine, options.ScaleFramebuffer);
        SetHint(Native.Glfw.MousePassthroughDefine, options.MousePassthrough);
        SetHint(Native.Glfw.PositionXDefine, options.Position.X);
        SetHint(Native.Glfw.PositionYDefine, options.Position.Y);
        SetHint(Native.Glfw.RedBitsDefine, options.ColorBits.Red);
        SetHint(Native.Glfw.GreenBitsDefine, options.ColorBits.Green);
        SetHint(Native.Glfw.BlueBitsDefine, options.ColorBits.Blue);
        SetHint(Native.Glfw.AlphaBitsDefine, options.ColorBits.Alpha);
        SetHint(Native.Glfw.DepthBitsDefine, options.DepthBits);
        SetHint(Native.Glfw.StencilBitsDefine, options.StencilBits);
        SetHint(Native.Glfw.AccumRedBitsDefine, options.AccumBits.Red);
        SetHint(Native.Glfw.AccumGreenBitsDefine, options.AccumBits.Green);
        SetHint(Native.Glfw.AccumBlueBitsDefine, options.AccumBits.Blue);
        SetHint(Native.Glfw.AccumAlphaBitsDefine, options.AccumBits.Alpha);
        SetHint(Native.Glfw.AuxBuffersDefine, options.AuxBuffers);
        SetHint(Native.Glfw.SamplesDefine, options.Samples);
        SetHint(Native.Glfw.RefreshRateDefine, options.RefreshRate);
        SetHint(Native.Glfw.StereoDefine, options.Stereo);
        SetHint(Native.Glfw.SrgbCapableDefine, options.SrgbCapable);
        SetHint(Native.Glfw.DoublebufferDefine, options.Doublebuffer);
        SetHint(Native.Glfw.ClientApiDefine, options.ClientApi);
        SetHint(Native.Glfw.ContextCreationApiDefine, options.ContextCreationApi);
        SetHint(Native.Glfw.ContextVersionMajorDefine, options.ContextVersion.Major);
        SetHint(Native.Glfw.ContextVersionMinorDefine, options.ContextVersion.Minor);
        SetHint(Native.Glfw.ContextRobustnessDefine, options.ContextRobustness);
        SetHint(Native.Glfw.ContextReleaseBehaviorDefine, options.ContextReleaseBehavior);
        SetHint(Native.Glfw.OpenGlForwardCompatDefine, options.OpenGlForwardCompat);
        SetHint(Native.Glfw.ContextDebugDefine, options.ContextDebug);
        SetHint(Native.Glfw.OpenGlProfileDefine, options.OpenGlProfile);

        if (
            OperatingSystem.IsWindows()
            && Native.Glfw.GetPlatform() == Native.Glfw.PlatformWin32Define
        )
        {
            SetHint(Native.Glfw.Win32KeyboardMenuDefine, options.Win32KeyboardMenu);
            SetHint(Native.Glfw.Win32ShowDefaultDefine, options.Win32ShowDefault);
        }

        if (
            OperatingSystem.IsMacOS()
            && Native.Glfw.GetPlatform() == Native.Glfw.PlatformCocoaDefine
        )
        {
            SetHint(Native.Glfw.CocoaFrameNameDefine, options.CocoaFrameName);
            SetHint(Native.Glfw.CocoaGraphicsSwitchingDefine, options.CocoaGraphicsSwitching);
        }

        if (
            OperatingSystem.IsLinux()
            && Native.Glfw.GetPlatform() == Native.Glfw.PlatformWaylandDefine
        )
        {
            SetHint(Native.Glfw.WaylandAppIdDefine, options.WaylandAppId);
        }

        if (
            (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
            && Native.Glfw.GetPlatform() == Native.Glfw.PlatformX11Define
        )
        {
            SetHint(Native.Glfw.X11ClasNameDefine, options.X11ClassName);
            SetHint(Native.Glfw.X11InstanceNameDefine, options.X11InstanceName);
        }
    }

    private static void SetHint(int hint, int value)
    {
        Native.Glfw.WindowHint(hint, value);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static void SetHint(int hint, string value)
    {
        ArgumentNullException.ThrowIfNull(value);
        Native.Glfw.WindowHintString(hint, value);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static void SetHint(int hint, bool value) =>
        SetHint(hint, value ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine);

    private static void SetHint<T>(int hint, T value)
        where T : Enum => SetHint(hint, Convert.ToInt32(value));
}
