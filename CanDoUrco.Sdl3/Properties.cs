// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const string PropNameString = "SDL.name";

    [LibraryImport(DllName, EntryPoint = "SDL_GetGlobalProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _GetGlobalProperties();

    [LibraryImport(DllName, EntryPoint = "SDL_CreateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertiesID _CreateProperties();

    [LibraryImport(DllName, EntryPoint = "SDL_CopyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _CopyProperties(PropertiesID src, PropertiesID dst);

    [LibraryImport(DllName, EntryPoint = "SDL_LockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _LockProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UnlockProperties(PropertiesID props);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPointerPropertyWithCleanup",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetPointerPropertyWithCleanup(
        PropertiesID props,
        string name,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void*, void> cleanup,
        void* userdata
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPointerProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _SetPointerProperty(
        PropertiesID props,
        string name,
        void* value
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetStringProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetStringProperty(PropertiesID props, string name, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetNumberProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetNumberProperty(PropertiesID props, string name, long value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetFloatProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetFloatProperty(PropertiesID props, string name, float value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetBooleanProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetBooleanProperty(
        PropertiesID props,
        string name,
        [MarshalAs(UnmanagedType.I1)] bool value
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_HasProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasProperty(PropertiesID props, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPropertyType",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertyType _GetPropertyType(PropertiesID props, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPointerProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static unsafe partial void* _GetPointerProperty(
        PropertiesID props,
        string name,
        void* defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetStringProperty",
        StringMarshalling = StringMarshalling.Custom,
        StringMarshallingCustomType = typeof(UnownedUtf8StringMarshaller)
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial string? _GetStringProperty(
        PropertiesID props,
        string name,
        string? defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetNumberProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _GetNumberProperty(
        PropertiesID props,
        string name,
        long defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetFloatProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetFloatProperty(
        PropertiesID props,
        string name,
        float defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetBooleanProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetBooleanProperty(
        PropertiesID props,
        string name,
        [MarshalAs(UnmanagedType.I1)] bool defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_ClearProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ClearProperty(PropertiesID props, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_EnumerateProperties",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _EnumerateProperties(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void*, PropertiesID, byte*, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyProperties(PropertiesID props);

    public static PropertiesID GetGlobalProperties()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGlobalProperties();
    }

    public static PropertiesID CreateProperties()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateProperties();
    }

    public static bool CopyProperties(PropertiesID src, PropertiesID dst)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CopyProperties(src, dst);
    }

    public static bool LockProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _LockProperties(props);
    }

    public static void UnlockProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UnlockProperties(props);
    }

    public static unsafe bool SetPointerPropertyWithCleanup(
        PropertiesID props,
        string name,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void*, void> cleanup,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
    }

    public static unsafe bool SetPointerProperty(PropertiesID props, string name, void* value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetPointerProperty(props, name, value);
    }

    public static bool SetStringProperty(PropertiesID props, string name, string? value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetStringProperty(props, name, value);
    }

    public static bool SetNumberProperty(PropertiesID props, string name, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetNumberProperty(props, name, value);
    }

    public static bool SetFloatProperty(PropertiesID props, string name, float value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetFloatProperty(props, name, value);
    }

    public static bool SetBooleanProperty(PropertiesID props, string name, bool value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _SetBooleanProperty(props, name, value);
    }

    public static bool HasProperty(PropertiesID props, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _HasProperty(props, name);
    }

    public static PropertyType GetPropertyType(PropertiesID props, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetPropertyType(props, name);
    }

    public static unsafe void* GetPointerProperty(
        PropertiesID props,
        string name,
        void* defaultValue
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetPointerProperty(props, name, defaultValue);
    }

    public static string? GetStringProperty(PropertiesID props, string name, string? defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetStringProperty(props, name, defaultValue);
    }

    public static long GetNumberProperty(PropertiesID props, string name, long defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetNumberProperty(props, name, defaultValue);
    }

    public static float GetFloatProperty(PropertiesID props, string name, float defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetFloatProperty(props, name, defaultValue);
    }

    public static bool GetBooleanProperty(PropertiesID props, string name, bool defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _GetBooleanProperty(props, name, defaultValue);
    }

    public static bool ClearProperty(PropertiesID props, string name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(name);
        return _ClearProperty(props, name);
    }

    public static unsafe bool EnumerateProperties(
        PropertiesID props,
        delegate* unmanaged[Cdecl]<void*, PropertiesID, byte*, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EnumerateProperties(props, callback, userdata);
    }

    public static void DestroyProperties(PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyProperties(props);
    }

    public enum PropertyType
    {
        Invalid,
        Pointer,
        String,
        Number,
        Float,
        Boolean,
    }

    public readonly record struct PropertiesID(uint Value)
    {
        public bool IsInvalid => Value == 0;

        public static implicit operator uint(PropertiesID props) => props.Value;

        public static implicit operator PropertiesID(uint value) => new(value);
    }
}
