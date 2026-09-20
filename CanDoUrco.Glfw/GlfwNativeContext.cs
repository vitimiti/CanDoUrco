// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;
using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw;

/// <summary>
/// The main library context, which manages its initialization and termination.
/// </summary>
public sealed class GlfwNativeContext : IDisposable
{
    private static Native.Glfw.Allocator s_customAllocatorStruct;

    private bool _disposedValue;

    /// <summary>
    /// Gets the current runtime numeric version of GLFW.
    /// </summary>
    public static Version Version
    {
        get
        {
            Native.Glfw.GetVersion(out var major, out var minor, out var rev);
            return new Version(major, minor, rev);
        }
    }

    /// <summary>
    /// Gets the current runtime complete version of GLFW.
    /// </summary>
    public static unsafe string VersionComplete
    {
        get
        {
            var stringPtr = Native.Glfw.GetVersionString();
            return Utf8StringMarshaller.ConvertToManaged(stringPtr) ?? string.Empty;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GlfwNativeContext"/> class.
    /// </summary>
    /// <param name="options">The <see cref="GlfwNativeContextOptions"/> to pass, or <see langword="null"/> to use the default.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public GlfwNativeContext(Action<GlfwNativeContextOptions>? options = null)
    {
        var opts = new GlfwNativeContextOptions();
        options?.Invoke(opts);
        SetInitHints(opts);
        if (!Native.Glfw.Init())
        {
            GlfwErrorUtilities.ThrowError();
        }
    }

    /// <summary>
    /// Finalizes the <see cref="GlfwNativeContext"/> instance.
    /// </summary>
    ~GlfwNativeContext()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes all managed and unmanaged resources and terminates the GLFW context.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Gets the currently in use windowing and input platform.
    /// </summary>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwNativeContext"/> instance has already been disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public GlfwPlatform GetCurrentPlatform()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = (GlfwPlatform)Native.Glfw.GetPlatform();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gets a read-only dictionary with information as to whether a windowing and input platform is supported by the GLFW runtime.
    /// </summary>
    /// <returns>A read-only dictionary with <see cref="GlfwPlatform"/>:<see cref="bool"/> pairs.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwNativeContext"/> instance has already been disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public IReadOnlyDictionary<GlfwPlatform, bool> GetRuntimeSupportedPlatforms()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var platformValues = Enum.GetValues<GlfwPlatform>();
        var result = new Dictionary<GlfwPlatform, bool>(platformValues.Length);
        foreach (var platform in platformValues)
        {
            result.Add(platform, Native.Glfw.PlatformSupported((int)platform));
            GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        }

        return result;
    }

    /// <summary>
    /// Gets a collection of the available monitors.
    /// </summary>
    /// <returns>A collection of <see cref="GlfwMonitor"/>, or an empty collection if no monitors could be found.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwNativeContext"/> instance has already been disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe ICollection<GlfwMonitor> GetMonitors()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var monitorsPtr = Native.Glfw.GetMonitors(out var count);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        if (monitorsPtr is null)
        {
            return [];
        }

        var monitors = new GlfwMonitor[count];
        for (var i = 0; i < count; i++)
        {
            monitors[i] = new GlfwMonitor(monitorsPtr[i]);
        }

        return monitors;
    }

    /// <summary>
    /// Gets the primary monitor.
    /// </summary>
    /// <returns>The primary monitor, or <see langword="null"/> if no monitor could be found.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwNativeContext"/> instance has already been disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwMonitor? GetPrimaryMonitor()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var monitorPtr = Native.Glfw.GetPrimaryMonitor();
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return monitorPtr is null ? null : new GlfwMonitor(monitorPtr);
    }

    private static unsafe void SetInitHints(GlfwNativeContextOptions options)
    {
        ArgumentNullException.ThrowIfNull(options);
        if (options.UseDotnetAsCustomAllocator)
        {
            s_customAllocatorStruct = new()
            {
                Allocate = &HandleAllocate,
                Reallocate = &HandleReallocate,
                Deallocate = &HandleDeallocate,
                User = null,
            };

            Native.Glfw.InitAllocator(in s_customAllocatorStruct);
            GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        }

        if (Native.Glfw.PlatformSupported((int)options.Platform))
        {
            SetInitHint(Native.Glfw.PlatfromDefine, (int)options.Platform);
        }

        SetInitHint(Native.Glfw.JoystickHatButtonsDefine, options.ExposeJoystickHatsAsButtons);

        SetInitHint(Native.Glfw.AnglePlatformTypeDefine, (int)options.AnglePlatformType);

        if (OperatingSystem.IsMacOS())
        {
            SetInitHint(
                Native.Glfw.CocoaChDirResourcesDefine,
                options.CocoaChangeDirectoryToResources
            );

            SetInitHint(Native.Glfw.CocoaMenuBarDefine, options.CocoaCreateMenuBar);
        }

        if (
            OperatingSystem.IsLinux()
            && Native.Glfw.PlatformSupported(Native.Glfw.PlatformWaylandDefine)
        )
        {
            SetInitHint(Native.Glfw.WaylandLibDecorDefine, options.WaylandPreferLibDecor);
        }

        if (
            (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
            && Native.Glfw.PlatformSupported(Native.Glfw.PlatformX11Define)
        )
        {
            SetInitHint(Native.Glfw.X11XcbVulkanSurfaceDefine, options.X11XcbVulkanSurfaces);
        }
    }

    private static void SetInitHint(int hint, int value)
    {
        Native.Glfw.InitHint(hint, value);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    private static void SetInitHint(int hint, bool value) =>
        SetInitHint(hint, value ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine);

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

        Native.Glfw.Terminate();
        // Ignoring errors intentionally!

        _disposedValue = true;
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void* HandleAllocate(nuint size, void* user) => NativeMemory.Alloc(size);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void* HandleReallocate(void* block, nuint size, void* user) =>
        NativeMemory.Realloc(block, size);

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void HandleDeallocate(void* block, void* user) =>
        NativeMemory.Free(block);
}
