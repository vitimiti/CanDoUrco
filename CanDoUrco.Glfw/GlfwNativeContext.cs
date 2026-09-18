// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;
using CanDoUrco.Glfw.Options;
using CanDoUrco.Glfw.Utilities;

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
    /// <exception cref="GlfwException">Thrown when setting the custom allocator, setting the initialization hints or initializing the library fails.</exception>
    public GlfwNativeContext(Action<GlfwNativeContextOptions>? options = null)
    {
        var opts = new GlfwNativeContextOptions();
        options?.Invoke(opts);
        SetInitHints(opts);
        if (!Native.Glfw.Init())
        {
            ErrorUtilities.CheckAndThrowErrorFromBadReturnMethod();
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
            ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
        }

        if (Native.Glfw.PlatformSupported((int)options.Platform))
        {
            SetInitHint(Native.Glfw.PlatfromDefine, (int)options.Platform);
        }

        SetInitHint(
            Native.Glfw.JoystickHatButtonsDefine,
            options.ExposeJoystickHatsAsButtons ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine
        );

        SetInitHint(Native.Glfw.AnglePlatformTypeDefine, (int)options.AnglePlatformType);

        if (OperatingSystem.IsMacOS())
        {
            SetInitHint(
                Native.Glfw.CocoaChDirResourcesDefine,
                options.CocoaChangeDirectoryToResources
                    ? Native.Glfw.TrueDefine
                    : Native.Glfw.FalseDefine
            );

            SetInitHint(
                Native.Glfw.CocoaMenuBarDefine,
                options.CocoaCreateMenuBar ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine
            );
        }

        if (
            OperatingSystem.IsLinux()
            && Native.Glfw.PlatformSupported(Native.Glfw.PlatformWaylandDefine)
        )
        {
            SetInitHint(
                Native.Glfw.WaylandLibDecorDefine,
                options.WaylandPreferLibDecor
                    ? Native.Glfw.WaylandPreferLibDecorDefine
                    : Native.Glfw.WaylandDisableLibDecorDefine
            );
        }

        if (
            (OperatingSystem.IsLinux() || OperatingSystem.IsFreeBSD())
            && Native.Glfw.PlatformSupported(Native.Glfw.PlatformX11Define)
        )
        {
            SetInitHint(
                Native.Glfw.X11XcbVulkanSurfaceDefine,
                options.X11XcbVulkanSurfaces ? Native.Glfw.TrueDefine : Native.Glfw.FalseDefine
            );
        }
    }

    private static void SetInitHint(int hint, int value)
    {
        Native.Glfw.InitHint(hint, value);
        ErrorUtilities.CheckAndThrowErrorFromVoidMethod();
    }

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
