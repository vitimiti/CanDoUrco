// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.ExceptionServices;
using System.Text;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Properties;

public class PropertyGroup : IDisposable
{
    private readonly bool _ownsProperties;

    internal Sdl.PropertiesID Id { get; private init; }

    public string? Name
    {
        get
        {
            var name = Encoding.UTF8.GetString(Sdl.PropNameString[..^1]);
            return GetStringProperty(name);
        }
        set
        {
            var name = Encoding.UTF8.GetString(Sdl.PropNameString[..^1]);
            SetProperty(name, value);
        }
    }

    internal PropertyGroup(Sdl.PropertiesID id, bool ownsProperties = true)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        Id = id;
        _ownsProperties = ownsProperties;
    }

    public unsafe PropertyGroup()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        Id = Sdl.CreateProperties();
        if (Id == 0)
        {
            throw new InvalidOperationException(
                $"Failed to create property group: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
        _ownsProperties = true;
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (_ownsProperties && Id != 0)
        {
            Sdl.DestroyProperties(Id);
        }
        GC.SuppressFinalize(this);
    }

    public unsafe void CopyTo(PropertyGroup destination)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (!Sdl.CopyProperties(Id, destination.Id))
        {
            throw new InvalidOperationException(
                $"Failed to copy property group: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    public unsafe LockedPropertyGroup Lock()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (!Sdl.LockProperties(Id))
        {
            throw new InvalidOperationException(
                $"Failed to lock property group: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
        return new LockedPropertyGroup(this);
    }

    public unsafe void SetProperty(string name, nint value, Action<nint>? freePtrAction)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            if (freePtrAction is not null)
            {
                var handle = GCHandle.Alloc(freePtrAction);
                var userdata = (void*)GCHandle.ToIntPtr(handle);
                if (
                    !Sdl.SetPointerPropertyWithCleanup(
                        Id,
                        managedName.ToUnmanaged(),
                        value.ToPointer(),
                        &FreePtrCallback,
                        userdata
                    )
                )
                {
                    handle.Free();
                    throw new InvalidOperationException(
                        $"Failed to set pointer property with cleanup '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                    );
                }
            }
            else
            {
                if (!Sdl.SetPointerProperty(Id, managedName.ToUnmanaged(), value.ToPointer()))
                {
                    throw new InvalidOperationException(
                        $"Failed to set pointer property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                    );
                }
            }
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe void SetProperty(string name, string? value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        scoped var managedValue = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> nameBuffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, nameBuffer);

            Span<byte> valueBuffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedValue.FromManaged(value, valueBuffer);

            if (!Sdl.SetStringProperty(Id, managedName.ToUnmanaged(), managedValue.ToUnmanaged()))
            {
                throw new InvalidOperationException(
                    $"Failed to set string property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            managedName.Free();
            managedValue.Free();
        }
    }

    public unsafe void SetProperty(string name, long value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            if (!Sdl.SetNumberProperty(Id, managedName.ToUnmanaged(), value))
            {
                throw new InvalidOperationException(
                    $"Failed to set number property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe void SetProperty(string name, float value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            if (!Sdl.SetFloatProperty(Id, managedName.ToUnmanaged(), value))
            {
                throw new InvalidOperationException(
                    $"Failed to set float property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe void SetProperty(string name, bool value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            if (!Sdl.SetBooleanProperty(Id, managedName.ToUnmanaged(), value))
            {
                throw new InvalidOperationException(
                    $"Failed to set boolean property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe bool HasProperty(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            return Sdl.HasProperty(Id, managedName.ToUnmanaged());
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe PropertyType GetPropertyType(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            return (PropertyType)Sdl.GetPropertyType(Id, managedName.ToUnmanaged());
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe nint GetPointerProperty(string name, nint defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> buffer =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, buffer);

            return (nint)
                Sdl.GetPointerProperty(Id, managedName.ToUnmanaged(), defaultValue.ToPointer());
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe string? GetStringProperty(string name, string? defaultValue = null)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        scoped var managedDefaultValue = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> bufferName =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, bufferName);

            Span<byte> bufferDefault =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedDefaultValue.FromManaged(defaultValue ?? string.Empty, bufferDefault);

            return Utf8StringMarshaller.ConvertToManaged(
                Sdl.GetStringProperty(
                    Id,
                    managedName.ToUnmanaged(),
                    managedDefaultValue.ToUnmanaged()
                )
            );
        }
        finally
        {
            managedName.Free();
            managedDefaultValue.Free();
        }
    }

    public unsafe long GetNumberProperty(string name, long defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> bufferName =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, bufferName);

            return Sdl.GetNumberProperty(Id, managedName.ToUnmanaged(), defaultValue);
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe float GetFloatProperty(string name, float defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> bufferName =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, bufferName);

            return Sdl.GetFloatProperty(Id, managedName.ToUnmanaged(), defaultValue);
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe bool GetBooleanProperty(string name, bool defaultValue = false)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> bufferName =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, bufferName);

            return Sdl.GetBooleanProperty(Id, managedName.ToUnmanaged(), defaultValue);
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe void ClearProperty(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        scoped var managedName = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        try
        {
            Span<byte> bufferName =
                stackalloc byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
            managedName.FromManaged(name, bufferName);

            if (!Sdl.ClearProperty(Id, managedName.ToUnmanaged()))
            {
                throw new InvalidOperationException(
                    $"Failed to clear the property '{name}': {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
                );
            }
        }
        finally
        {
            managedName.Free();
        }
    }

    public unsafe void EnumerateProperty(Action<string> callback)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(callback);

        var context = new PropertyEnumerationContext(callback);
        var handle = GCHandle.Alloc(context);
        bool succeeded;
        try
        {
            succeeded = Sdl.EnumerateProperties(
                Id,
                &EnumeratePropertyCallback,
                (void*)GCHandle.ToIntPtr(handle)
            );
        }
        finally
        {
            handle.Free();
        }

        if (context.Exception is not null)
        {
            ExceptionDispatchInfo.Capture(context.Exception).Throw();
        }
        if (!succeeded)
        {
            throw new InvalidOperationException(
                $"Failed to enumerate properties: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void FreePtrCallback(void* userdata, void* value)
    {
        if (userdata is null)
        {
            return;
        }

        var action = (Action<nint>)GCHandle.FromIntPtr((nint)userdata).Target!;
        action((nint)value);
        GCHandle.FromIntPtr((nint)userdata).Free();
    }

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe void EnumeratePropertyCallback(
        void* userdata,
        Sdl.PropertiesID _,
        byte* name
    )
    {
        if (userdata is null)
        {
            return;
        }

        if (name is null)
        {
            return;
        }

        var context = (PropertyEnumerationContext)GCHandle.FromIntPtr((nint)userdata).Target!;
        if (context.Exception is not null)
        {
            return;
        }

        try
        {
            context.Callback(Utf8StringMarshaller.ConvertToManaged(name)!);
        }
        catch (Exception exception)
        {
            context.Exception = exception;
        }
    }

    private sealed class PropertyEnumerationContext(Action<string> callback)
    {
        public Action<string> Callback { get; } = callback;
        public Exception? Exception { get; set; }
    }
}
