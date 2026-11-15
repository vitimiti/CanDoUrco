// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using CanDoUrco.Framework.Utilities;
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
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to create property group: {error}.");
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
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to copy property group: {error}.");
        }
    }

    public unsafe LockedPropertyGroup Lock()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (!Sdl.LockProperties(Id))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to lock property group: {error}.");
        }
        return new LockedPropertyGroup(this);
    }

    public unsafe void SetProperty(string name, nint value, Action<nint>? freePtrAction)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        if (freePtrAction is not null)
        {
            var handle = GCHandle.Alloc(freePtrAction);
            var userdata = (void*)GCHandle.ToIntPtr(handle);
            if (
                !Sdl.SetPointerPropertyWithCleanup(
                    Id,
                    managedName,
                    value.ToPointer(),
                    &FreePtrCallback,
                    userdata
                )
            )
            {
                handle.Free();
                var error = new UnownedUtf8String(Sdl.GetError());
                throw new InvalidOperationException(
                    $"Failed to set pointer property with cleanup '{name}': {error}."
                );
            }
        }
        else
        {
            if (!Sdl.SetPointerProperty(Id, managedName, value.ToPointer()))
            {
                var error = new UnownedUtf8String(Sdl.GetError());
                throw new InvalidOperationException(
                    $"Failed to set pointer property '{name}': {error}."
                );
            }
        }
    }

    public unsafe void SetProperty(string name, string? value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        using var managedValue = new ManagedUtf8String(value);
        if (!Sdl.SetStringProperty(Id, managedName, managedValue))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException(
                $"Failed to set string property '{name}': {error}."
            );
        }
    }

    public unsafe void SetProperty(string name, long value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        if (!Sdl.SetNumberProperty(Id, managedName, value))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException(
                $"Failed to set number property '{name}': {error}."
            );
        }
    }

    public unsafe void SetProperty(string name, float value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        if (!Sdl.SetFloatProperty(Id, managedName, value))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to set float property '{name}': {error}.");
        }
    }

    public unsafe void SetProperty(string name, bool value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        if (!Sdl.SetBooleanProperty(Id, managedName, value))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException(
                $"Failed to set boolean property '{name}': {error}."
            );
        }
    }

    public unsafe bool HasProperty(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return Sdl.HasProperty(Id, managedName);
    }

    public unsafe PropertyType GetPropertyType(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return (PropertyType)Sdl.GetPropertyType(Id, managedName);
    }

    public unsafe nint GetPointerProperty(string name, nint defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return (nint)Sdl.GetPointerProperty(Id, managedName, defaultValue.ToPointer());
    }

    public unsafe string? GetStringProperty(string name, string? defaultValue = null)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        using var managedDefaultValue = new ManagedUtf8String(defaultValue ?? string.Empty);
        var result = new UnownedUtf8String(
            Sdl.GetStringProperty(Id, managedName, managedDefaultValue)
        );
        return result;
    }

    public unsafe long GetNumberProperty(string name, long defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return Sdl.GetNumberProperty(Id, managedName, defaultValue);
    }

    public unsafe float GetFloatProperty(string name, float defaultValue = 0)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return Sdl.GetFloatProperty(Id, managedName, defaultValue);
    }

    public unsafe bool GetBooleanProperty(string name, bool defaultValue = false)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        return Sdl.GetBooleanProperty(Id, managedName, defaultValue);
    }

    public unsafe void ClearProperty(string name)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);

        using var managedName = new ManagedUtf8String(name);
        if (!Sdl.ClearProperty(Id, managedName))
        {
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to clear the property '{name}': {error}.");
        }
    }

    public unsafe void EnumerateProperty(Action<string> callback)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(callback);

        var handle = GCHandle.Alloc(callback);
        if (
            !Sdl.EnumerateProperties(
                Id,
                &EnumeratePropertyCallback,
                (void*)GCHandle.ToIntPtr(handle)
            )
        )
        {
            handle.Free();
            var error = new UnownedUtf8String(Sdl.GetError());
            throw new InvalidOperationException($"Failed to enumerate properties: {error}.");
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

        var action = (Action<string>)GCHandle.FromIntPtr((nint)userdata).Target!;
        string? nameStr = new UnownedUtf8String(name);
        action(nameStr ?? string.Empty);
        GCHandle.FromIntPtr((nint)userdata).Free();
    }
}
