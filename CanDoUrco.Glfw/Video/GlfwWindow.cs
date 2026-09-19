// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents a GLFW window.
/// </summary>
/// <remarks>
/// <para>This class is sealed.</para>
/// <para>This class inherits from <see cref="IDisposable"/>.</para>
/// </remarks>
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

        Handles[this] = (nint)_handle;
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
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the window position.
    /// </summary>
    /// <returns>A new <see cref="Point"/> with the window position.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Point GetPosition()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetWindowPos(_handle, out var xPos, out var yPos);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Point(xPos, yPos);
    }

    /// <summary>
    /// Sets the window position.
    /// </summary>
    /// <param name="position">A new <see cref="Point"/> with the window position.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetPosition(Point position)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowPos(_handle, position.X, position.Y);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the window size.
    /// </summary>
    /// <returns>A new <see cref="Size"/> with the window size.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Size GetSize()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetWindowSize(_handle, out var width, out var height);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Size(width, height);
    }

    /// <summary>
    /// Sets the window's size limits.
    /// </summary>
    /// <param name="minimum">A <see cref="Size"/> with the minimum size.</param>
    /// <param name="maximum">A <see cref="Size"/> with the maximum size.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetSizeLimits(Size minimum, Size maximum)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowSizeLimits(
            _handle,
            minimum.Width,
            minimum.Height,
            maximum.Width,
            maximum.Height
        );

        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Sets the window's aspect ratio.
    /// </summary>
    /// <param name="numerator">An <see cref="int"/> with the numerator value.</param>
    /// <param name="denominator">An <see cref="int"/> with the denominator value.</param>
    public unsafe void SetAspectRatio(int numerator, int denominator)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowAspectRatio(_handle, numerator, denominator);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Sets the window size.
    /// </summary>
    /// <param name="size">A <see cref="Size"/> with the window's size.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetSize(Size size)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowSize(_handle, size.Width, size.Height);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the current framebuffer size.
    /// </summary>
    /// <returns>A new <see cref="Size"/> with the framebuffer size.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe Size GetFramebufferSize()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetFramebufferSize(_handle, out var width, out var height);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new Size(width, height);
    }

    /// <summary>
    /// Gets the current window's frame size.
    /// </summary>
    /// <returns>A tuple with the left, top, right and bottom sizes of the frame.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe (int Left, int Top, int Right, int Bottom) GetFrameSize()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetWindowFrameSize(
            _handle,
            out var left,
            out var top,
            out var right,
            out var bottom
        );

        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return (left, top, right, bottom);
    }

    /// <summary>
    /// Gets the scale of the window's contents.
    /// </summary>
    /// <returns>A new <see cref="PointF"/> with the window's contents.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe PointF GetContentScale()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetWindowContentScale(_handle, out var xScale, out var yScale);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new PointF(xScale, yScale);
    }

    /// <summary>
    /// Gets the opacity of the window.
    /// </summary>
    /// <returns>A new <see cref="float"/> with the opacity of the window.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe float GetOpacity()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetWindowOpacity(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Sets the window opacity.
    /// </summary>
    /// <param name="opacity">A <see cref="float"/> with the window opacity.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetOpacity(float opacity)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowOpacity(_handle, opacity);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Iconfies the window.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void Iconify()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.IconifyWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Restores the window.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void RestoreWindow()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.RestoreWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Maximizes the window.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void Maximize()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.MaximizeWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Shows the window
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void Show()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.ShowWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Hides the window.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void Hide()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.HideWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Brings the window to the front and sets input focus.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void Focus()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.FocusWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Requests user attention to the window.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void RequestAttention()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.RequestWindowAttentionWindow(_handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Gets the window monitor used for full screen mode.
    /// </summary>
    /// <returns>A new <see cref="GlfwMonitor"/> or <see langword="null"/> if the window is in windowed mode.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwMonitor? GetMonitor()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var ptr = Native.Glfw.GetWindowMonitor(_handle);
        if (ptr is null)
        {
            return null;
        }

        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new GlfwMonitor(ptr);
    }

    /// <summary>
    /// Sets the mode, monitor, video mode and placement of the window.
    /// </summary>
    /// <param name="monitor">A <see cref="GlfwMonitor"/>, or <see langword="null"/> to set windowed mode.</param>
    /// <param name="contentArea">The desired dimentions of the content area.</param>
    /// <param name="refreshRateInHertz">The desired refresh rate, in hertz, of the video mode, or <see cref="SpecialValues.DontCare"/>./param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetMonitor(
        GlfwMonitor? monitor,
        Rectangle contentArea,
        int refreshRateInHertz
    )
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetWindowMonitor(
            _handle,
            monitor is null ? null : monitor.Handle,
            contentArea.X,
            contentArea.Y,
            contentArea.Width,
            contentArea.Height,
            refreshRateInHertz
        );

        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
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
