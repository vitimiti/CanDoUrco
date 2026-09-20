// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Events;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Input;
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
    /// <summary>
    /// An event that happens when the window position is updated.
    /// </summary>
    public event EventHandler<GlfwWindowPositionEventArgs>? PositionUpdate;

    /// <summary>
    /// An event that happens when the window size is updated.
    /// </summary>
    public event EventHandler<GlfwWindowSizeEventArgs>? SizeUpdate;

    /// <summary>
    /// An event that happens when the window is closed.
    /// </summary>
    public event EventHandler<GlfwWindowCloseEventArgs>? Close;

    /// <summary>
    /// An event that happens when the window is refreshed.
    /// </summary>
    public event EventHandler<GlfwWindowRefreshEventArgs>? Refresh;

    /// <summary>
    /// An event that happens when the window is focused or unfocused.
    /// </summary>
    public event EventHandler<GlfwWindowIsFocusedEventArgs>? IsFocused;

    /// <summary>
    /// An event that happens when the window is iconified or not.
    /// </summary>
    public event EventHandler<GlfwWindowIsIconifiedEventArgs>? IsIconified;

    /// <summary>
    /// An event that happens when the window is maximized or not.
    /// </summary>
    public event EventHandler<GlfwWindowIsMaximizedEventArgs>? IsMaximized;

    /// <summary>
    /// An event that happens when the window framebuffer size is updated.
    /// </summary>
    public event EventHandler<GlfwWindowFramebufferSizeEventArgs>? FramebufferSizeUpdate;

    /// <summary>
    /// An event that happens when the window content scale is updated.
    /// </summary>
    public event EventHandler<GlfwWindowContentScaleEventArgs>? ContentScaleUpdate;

    /// <summary>
    /// An event that happens when a keyboard key action happens.
    /// </summary>
    public event EventHandler<GlfwKeyActionEventArgs>? KeyAction;

    /// <summary>
    /// An event that happens when a Unicode character is input.
    /// </summary>
    public event EventHandler<GlfwUnicodeCharacterEventArgs>? UnicodeCharacterInput;

    /// <summary>
    /// An event that happens when a Unicode character is input with key modifiers.
    /// </summary>
    public event EventHandler<GlfwUnicodeCharacterWithModifiersEventArgs>? UnicodeCharacterWithKeyModifiersInput;

    /// <summary>
    /// An event that happens when a mouse button action happens.
    /// </summary>
    public event EventHandler<GlfwMouseButtonActionEventArgs>? MouseButtonAction;

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
        SetCallbacks();
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
            nativeImages[i] = images.ElementAt(i).ConvertToUnmanaged();
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

    /// <summary>
    /// Gets the current window attributes.
    /// </summary>
    /// <returns>A new <see cref="GlfwWindowAttributes"/>.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwWindowAttributes GetAttributes()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return new GlfwWindowAttributes()
        {
            Focused = GetAttributeBool(_handle, Native.Glfw.FocusedDefine),
            Iconified = GetAttributeBool(_handle, Native.Glfw.IconifiedDefine),
            Maximized = GetAttributeBool(_handle, Native.Glfw.MaximizedDefine),
            Hovered = GetAttributeBool(_handle, Native.Glfw.HoveredDefine),
            Visible = GetAttributeBool(_handle, Native.Glfw.VisibleDefine),
            Resizable = GetAttributeBool(_handle, Native.Glfw.ResizableDefine),
            Decorated = GetAttributeBool(_handle, Native.Glfw.DecoratedDefine),
            AutoIconify = GetAttributeBool(_handle, Native.Glfw.AutoIconifyDefine),
            Floating = GetAttributeBool(_handle, Native.Glfw.FloatingDefine),
            TransparentFramebuffer = GetAttributeBool(
                _handle,
                Native.Glfw.TransparentFramebufferDefine
            ),
            FocusOnShow = GetAttributeBool(_handle, Native.Glfw.FocusOnShowDefine),
            MousePassthrough = GetAttributeBool(_handle, Native.Glfw.MousePassthroughDefine),
            ClientApi = GetAttributeEnum<GlfwClientApi>(_handle, Native.Glfw.ClientApiDefine),
            ContextCreationApi = GetAttributeEnum<GlfwContextCreationApi>(
                _handle,
                Native.Glfw.ContextCreationApiDefine
            ),
            ContextVersion = new Version(
                GetAttribute(_handle, Native.Glfw.ContextVersionMajorDefine),
                GetAttribute(_handle, Native.Glfw.ContextVersionMinorDefine),
                GetAttribute(_handle, Native.Glfw.ContextRevisionDefine)
            ),
            OpenGlForwardCompat = GetAttributeBool(_handle, Native.Glfw.OpenGlForwardCompatDefine),
            ContextDebug = GetAttributeBool(_handle, Native.Glfw.ContextDebugDefine),
            OpenGlProfile = GetAttributeEnum<GlfwOpenGlProfile>(
                _handle,
                Native.Glfw.OpenGlProfileDefine
            ),
            ContextReleaseBehavior = GetAttributeEnum<GlfwContextReleaseBehavior>(
                _handle,
                Native.Glfw.ContextReleaseBehaviorDefine
            ),
            ContextNoError = GetAttributeBool(_handle, Native.Glfw.ContextNoErrorDefine),
            ContextRobustness = GetAttributeEnum<GlfwContextRobustness>(
                _handle,
                Native.Glfw.ContextRobustnessDefine
            ),
            Doublebuffer = GetAttributeBool(_handle, Native.Glfw.DoublebufferDefine),
        };
    }

    /// <summary>
    /// Sets the window attributes.
    /// </summary>
    /// <param name="attributes">The <see cref="GlfwWindowAttributes"/> to set.</param>
    /// <remarks>
    /// If you wish to only set a few attributes and not all, you may do the following:
    /// <code>
    /// window.SetAttributes(window.GetAttributes() with { Resizable = false });
    /// </code>
    /// If you are going to change attributes more often than once, cache the original attributes and update them as required.
    /// </remarks>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetAttributes(GlfwWindowAttributes attributes)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(attributes);
        SetAttribute(_handle, Native.Glfw.FocusedDefine, attributes.Focused);
        SetAttribute(_handle, Native.Glfw.IconifiedDefine, attributes.Iconified);
        SetAttribute(_handle, Native.Glfw.MaximizedDefine, attributes.Maximized);
        SetAttribute(_handle, Native.Glfw.HoveredDefine, attributes.Hovered);
        SetAttribute(_handle, Native.Glfw.VisibleDefine, attributes.Visible);
        SetAttribute(_handle, Native.Glfw.ResizableDefine, attributes.Resizable);
        SetAttribute(_handle, Native.Glfw.DecoratedDefine, attributes.Decorated);
        SetAttribute(_handle, Native.Glfw.AutoIconifyDefine, attributes.AutoIconify);
        SetAttribute(_handle, Native.Glfw.FloatingDefine, attributes.Floating);
        SetAttribute(
            _handle,
            Native.Glfw.TransparentFramebufferDefine,
            attributes.TransparentFramebuffer
        );

        SetAttribute(_handle, Native.Glfw.FocusOnShowDefine, attributes.FocusOnShow);
        SetAttribute(_handle, Native.Glfw.MousePassthroughDefine, attributes.MousePassthrough);
        SetAttribute(_handle, Native.Glfw.ClientApiDefine, attributes.ClientApi);
        SetAttribute(_handle, Native.Glfw.ContextCreationApiDefine, attributes.ContextCreationApi);
        SetAttribute(
            _handle,
            Native.Glfw.ContextVersionMajorDefine,
            attributes.ContextVersion.Major
        );

        SetAttribute(
            _handle,
            Native.Glfw.ContextVersionMinorDefine,
            attributes.ContextVersion.Minor
        );

        SetAttribute(
            _handle,
            Native.Glfw.ContextRevisionDefine,
            attributes.ContextVersion.Revision
        );

        SetAttribute(
            _handle,
            Native.Glfw.OpenGlForwardCompatDefine,
            attributes.OpenGlForwardCompat
        );

        SetAttribute(_handle, Native.Glfw.ContextDebugDefine, attributes.ContextDebug);
        SetAttribute(_handle, Native.Glfw.OpenGlProfileDefine, attributes.OpenGlProfile);
        SetAttribute(
            _handle,
            Native.Glfw.ContextReleaseBehaviorDefine,
            attributes.ContextReleaseBehavior
        );

        SetAttribute(_handle, Native.Glfw.ContextNoErrorDefine, attributes.ContextNoError);
        SetAttribute(_handle, Native.Glfw.ContextRobustnessDefine, attributes.ContextRobustness);
        SetAttribute(_handle, Native.Glfw.DoublebufferDefine, attributes.Doublebuffer);
    }

    /// <summary>
    /// Gets the current cursor mode.
    /// </summary>
    /// <returns>A value from <see cref="GlfwCursorMode"/> with the current cursor mode.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwCursorMode GetCursorMode()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeEnum<GlfwCursorMode>(_handle, Native.Glfw.CursorDefine);
    }

    /// <summary>
    /// Gets whether the sticky keys input mode is enabled.
    /// </summary>
    /// <returns><see langword="true"/> if the sticky keys are enabled, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool StickyKeysEnabled()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeBool(_handle, Native.Glfw.StickyKeysDefine);
    }

    /// <summary>
    /// Gets whether the sticky mouse buttons input mode is enabled.
    /// </summary>
    /// <returns><see langword="true"/> if the sticky mouse buttons are enabled, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool StickyMouseButtonsEnabled()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeBool(_handle, Native.Glfw.StickyMouseButtonsDefine);
    }

    /// <summary>
    /// Gets whether the lock key modifier bits are enabled.
    /// </summary>
    /// <returns><see langword="true"/> if the lock key modifier bits are enabled, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool LockKeyModifiersEnabled()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeBool(_handle, Native.Glfw.LockKeyModsDefine);
    }

    /// <summary>
    /// Gets whether the raw mouse motion is enabled.
    /// </summary>
    /// <returns><see langword="true"/> if the raw mouse motion is enabled, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool RawMouseMotionEnabled()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeBool(_handle, Native.Glfw.RawMouseMotionDefine);
    }

    /// <summary>
    /// Gets whether the unlimited mouse buttons input mode is enabled.
    /// </summary>
    /// <returns><see langword="true"/> if the unlimited mouse buttons input mode is enabled, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe bool UnlimitedMouseButtonsEnabled()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        return GetInputModeBool(_handle, Native.Glfw.UnlimitedMouseButtonsDefine);
    }

    /// <summary>
    /// Sets the cursor input mode.
    /// </summary>
    /// <param name="cursorMode">The <see cref="GlfwCursorMode"/> to set.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetCursorMode(GlfwCursorMode cursorMode)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.CursorDefine, cursorMode);
    }

    /// <summary>
    /// Sets whether the sticky keys input mode should be enabled.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable the sticky keys input mode, <see langword="false"/> to disable it.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void EnableStickKeys(bool enabled)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.StickyKeysDefine, enabled);
    }

    /// <summary>
    /// Sets whether the sticky mouse buttons input mode should be enabled.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable the sticky mouse buttons input mode, <see langword="false"/> to disable it.</param>
    public unsafe void EnableStickyMouseButtons(bool enabled)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.StickyMouseButtonsDefine, enabled);
    }

    /// <summary>
    /// Sets whether the lock key modifier bits are enabled.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable the lock key modifier bits, <see langword="false"/> to disable it.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void EnableLockKeyModifiers(bool enabled)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.LockKeyModsDefine, enabled);
    }

    /// <summary>
    /// Sets whether the raw mouse motion input mode is enabled.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable the raw mouse motion input mode, <see langword="false"/> otherwise.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void EnableRawMouseMotion(bool enabled)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.RawMouseMotionDefine, enabled);
    }

    /// <summary>
    /// Sets whether the unlimited mouse buttons input mode is enabled.
    /// </summary>
    /// <param name="enabled"><see langword="true"/> to enable the unlimited mouse buttons input mode, <see langword="false"/> to disable it.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void EnableUnlimitedMouseButtons(bool enabled)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        SetInputMode(_handle, Native.Glfw.UnlimitedMouseButtonsDefine, enabled);
    }

    /// <summary>
    /// Gets the las reported state of a keyboard key.
    /// </summary>
    /// <param name="key">The <see cref="GlfwKey"/> to get the state from.</param>
    /// <returns>One of the <see cref="GlfwKeyState"/> values.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwKeyState GetKeyState(GlfwKey key)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetKey(_handle, (int)key);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return (GlfwKeyState)result;
    }

    /// <summary>
    /// Gets the last reported state of a mouse button.
    /// </summary>
    /// <param name="button">The <see cref="GlfwMouseButton"/> to get the state from.</param>
    /// <returns>One of the <see cref="GlfwMouseButtonState"/> values.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <remarks>If <see cref="EnableUnlimitedMouseButtons(bool)"/> has been set to <see langword="true"/>, you can add extra buttons to this enum such as: <c>(GlfwMouseButton)25</c>.
    /// </remarks>
    public unsafe GlfwMouseButtonState GetMouseButtonState(GlfwMouseButton button)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetMouseButton(_handle, (int)button);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return (GlfwMouseButtonState)result;
    }

    /// <summary>
    /// Gets the current position of the cursor relative to the upper-left corner of the content area of the window.
    /// </summary>
    /// <returns>A new <see cref="PointF"/> with the cursor position.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe PointF GetCursorPosition()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.GetCursorPosition(_handle, out var xPos, out var yPos);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return new PointF((float)xPos, (float)yPos);
    }

    /// <summary>
    /// Sets the position of the cursor, relative to the upper-left corner of the content area of the window.
    /// </summary>
    /// <param name="cursorPosition">The <see cref="PointF"/> with the new cursor position.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetCursorPosition(PointF cursorPosition)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetCursorPos(_handle, cursorPosition.X, cursorPosition.Y);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Sets the cursor of the window.
    /// </summary>
    /// <param name="cursor">The <see cref="GlfwCursor"/> to set, or <see langword="null"/> to switch back to the default arrow cursor.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe void SetCursor(GlfwCursor? cursor)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        Native.Glfw.SetCursor(_handle, cursor is null ? null : cursor.Handle);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static void SetCallback(Action registerCallback)
    {
        ArgumentNullException.ThrowIfNull(registerCallback);
        registerCallback();
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
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

    private static unsafe int GetAttribute(Native.Glfw.Window* window, int attrib)
    {
        var result = Native.Glfw.GetWindowAttrib(window, attrib);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    private static unsafe bool GetAttributeBool(Native.Glfw.Window* window, int attrib) =>
        GetAttribute(window, attrib) == Native.Glfw.TrueDefine;

    private static unsafe T GetAttributeEnum<T>(Native.Glfw.Window* window, int attrib)
        where T : Enum => (T)Enum.ToObject(typeof(T), GetAttribute(window, attrib));

    private static unsafe void SetAttribute(Native.Glfw.Window* window, int attrib, int value)
    {
        Native.Glfw.SetWindowAttrib(window, attrib, value);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static unsafe void SetAttribute(Native.Glfw.Window* window, int attrib, bool value) =>
        SetAttribute(window, attrib, value ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine);

    private static unsafe void SetAttribute<T>(Native.Glfw.Window* window, int attrib, T value)
        where T : Enum => SetAttribute(window, attrib, Convert.ToInt32(value));

    private static unsafe int GetInputMode(Native.Glfw.Window* window, int mode)
    {
        var result = Native.Glfw.GetInputMode(window, mode);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    private static unsafe T GetInputModeEnum<T>(Native.Glfw.Window* window, int mode)
        where T : Enum => (T)Enum.ToObject(typeof(T), GetInputMode(window, mode));

    private static unsafe bool GetInputModeBool(Native.Glfw.Window* window, int mode) =>
        GetInputMode(window, mode) == Native.Glfw.TrueDefine;

    private static unsafe void SetInputMode(Native.Glfw.Window* window, int mode, int value)
    {
        Native.Glfw.SetInputMode(window, mode, value);
        ErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static unsafe void SetInputMode<T>(Native.Glfw.Window* window, int mode, T value)
        where T : Enum => SetInputMode(window, mode, Convert.ToInt32(value));

    private static unsafe void SetInputMode(Native.Glfw.Window* window, int mode, bool value) =>
        SetInputMode(window, mode, value ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine);

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                Handles.Clear();
            }

            UnsetCallbacks();
            // Ignore errors intentionally

            _disposedValue = true;
        }
    }

    private unsafe void SetCallbacks()
    {
        SetCallback(() => Native.Glfw.SetWindowPosCallback(_handle, &HandlePositionEvent));
        SetCallback(() => Native.Glfw.SetWindowSizeCallback(_handle, &HandleSizeEvent));
        SetCallback(() => Native.Glfw.SetWindowCloseCallback(_handle, &HandleCloseEvent));
        SetCallback(() => Native.Glfw.SetWindowRefreshCallback(_handle, &HandleRefreshEvent));
        SetCallback(() => Native.Glfw.SetWindowFocusCallback(_handle, &HandleIsFocusedEvent));
        SetCallback(() => Native.Glfw.SetWindowIconifyCallback(_handle, &HandleIsIconifiedEvent));
        SetCallback(() => Native.Glfw.SetWindowMaximizeCallback(_handle, &HandleIsMaximizedEvent));
        SetCallback(() =>
            Native.Glfw.SetWindowFramebufferSizeCallback(_handle, &HandleFramebufferSizeEvent)
        );

        SetCallback(() =>
            Native.Glfw.SetWindowContentScaleCallback(_handle, &HandleContentScaleEvent)
        );

        SetCallback(() => Native.Glfw.SetKeyCallback(_handle, &HandleKeyAction));
        SetCallback(() => Native.Glfw.SetCharCallback(_handle, &HandleUnicodeInput));
        SetCallback(() => Native.Glfw.SetCharModsCallback(_handle, &HandleUnicodeWithModsInput));
        SetCallback(() => Native.Glfw.SetMouseButtonCallback(_handle, &HandleMouseButton));
    }

    private unsafe void UnsetCallbacks()
    {
        Native.Glfw.DestroyWindow(_handle);
        Native.Glfw.SetWindowPosCallback(_handle, null);
        Native.Glfw.SetWindowSizeCallback(_handle, null);
        Native.Glfw.SetWindowCloseCallback(_handle, null);
        Native.Glfw.SetWindowRefreshCallback(_handle, null);
        Native.Glfw.SetWindowFocusCallback(_handle, null);
        Native.Glfw.SetWindowIconifyCallback(_handle, null);
        Native.Glfw.SetWindowMaximizeCallback(_handle, null);
        Native.Glfw.SetWindowFramebufferSizeCallback(_handle, null);
        Native.Glfw.SetWindowContentScaleCallback(_handle, null);
        Native.Glfw.SetKeyCallback(_handle, null);
        Native.Glfw.SetCharCallback(_handle, null);
        Native.Glfw.SetCharModsCallback(_handle, null);
        Native.Glfw.SetMouseButtonCallback(_handle, null);
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandlePositionEvent(Native.Glfw.Window* window, int xPos, int yPos)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.PositionUpdate?.Invoke(
                instance,
                new GlfwWindowPositionEventArgs(new Point(xPos, yPos))
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleSizeEvent(Native.Glfw.Window* window, int width, int height)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.SizeUpdate?.Invoke(
                instance,
                new GlfwWindowSizeEventArgs(new Size(width, height))
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleCloseEvent(Native.Glfw.Window* window)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.Close?.Invoke(instance, new GlfwWindowCloseEventArgs());
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleRefreshEvent(Native.Glfw.Window* window)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.Refresh?.Invoke(instance, new GlfwWindowRefreshEventArgs());
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleIsFocusedEvent(Native.Glfw.Window* window, int focused)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.IsFocused?.Invoke(
                instance,
                new GlfwWindowIsFocusedEventArgs(focused == Native.Glfw.TrueDefine)
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleIsIconifiedEvent(Native.Glfw.Window* window, int iconified)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.IsIconified?.Invoke(
                instance,
                new GlfwWindowIsIconifiedEventArgs(iconified == Native.Glfw.TrueDefine)
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleIsMaximizedEvent(Native.Glfw.Window* window, int maximized)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.IsMaximized?.Invoke(
                instance,
                new GlfwWindowIsMaximizedEventArgs(maximized == Native.Glfw.TrueDefine)
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleFramebufferSizeEvent(
        Native.Glfw.Window* window,
        int width,
        int height
    )
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.FramebufferSizeUpdate?.Invoke(
                instance,
                new GlfwWindowFramebufferSizeEventArgs(new Size(width, height))
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleContentScaleEvent(
        Native.Glfw.Window* window,
        float xScale,
        float yScale
    )
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.ContentScaleUpdate?.Invoke(
                instance,
                new GlfwWindowContentScaleEventArgs(new PointF(xScale, yScale))
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleKeyAction(
        Native.Glfw.Window* window,
        int key,
        int scancode,
        int action,
        int mods
    )
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.KeyAction?.Invoke(
                instance,
                new GlfwKeyActionEventArgs(
                    (GlfwKey)key,
                    scancode,
                    (GlfwKeyAction)action,
                    (GlfwKeyModifiers)mods
                )
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleUnicodeInput(Native.Glfw.Window* window, uint codepoint)
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.UnicodeCharacterInput?.Invoke(
                instance,
                new GlfwUnicodeCharacterEventArgs(codepoint)
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleUnicodeWithModsInput(
        Native.Glfw.Window* window,
        uint codepoint,
        int mods
    )
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.UnicodeCharacterWithKeyModifiersInput?.Invoke(
                instance,
                new GlfwUnicodeCharacterWithModifiersEventArgs(codepoint, (GlfwKeyModifiers)mods)
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleMouseButton(
        Native.Glfw.Window* window,
        int button,
        int action,
        int mods
    )
    {
        if (window is null)
        {
            return;
        }

        foreach (var (instance, handle) in Handles)
        {
            if (handle != (nint)window)
            {
                continue;
            }

            instance.MouseButtonAction?.Invoke(
                instance,
                new GlfwMouseButtonActionEventArgs(
                    (GlfwMouseButton)button,
                    (GlfwMouseButtonAction)action,
                    (GlfwKeyModifiers)mods
                )
            );
        }
    }
}
