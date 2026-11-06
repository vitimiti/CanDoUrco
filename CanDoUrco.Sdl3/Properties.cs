// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropNameString => "SDL.name"u8;

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

    [LibraryImport(DllName, EntryPoint = "SDL_SetPointerPropertyWithCleanup")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetPointerPropertyWithCleanup(
        PropertiesID props,
        byte* name,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void*, void> cleanup,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetPointerProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetPointerProperty(PropertiesID props, byte* name, void* value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetStringProperty(PropertiesID props, byte* name, byte* value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetNumberProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetNumberProperty(PropertiesID props, byte* name, long value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetFloatProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetFloatProperty(PropertiesID props, byte* name, float value);

    [LibraryImport(DllName, EntryPoint = "SDL_SetBooleanProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetBooleanProperty(
        PropertiesID props,
        byte* name,
        [MarshalAs(UnmanagedType.I1)] bool value
    );

    [LibraryImport(DllName, EntryPoint = "SDL_HasProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasProperty(PropertiesID props, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetPropertyType")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial PropertyType _GetPropertyType(PropertiesID props, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetPointerProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void* _GetPointerProperty(
        PropertiesID props,
        byte* name,
        void* defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetStringProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetStringProperty(
        PropertiesID props,
        byte* name,
        byte* defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetNumberProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial long _GetNumberProperty(
        PropertiesID props,
        byte* name,
        long defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetFloatProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial float _GetFloatProperty(
        PropertiesID props,
        byte* name,
        float defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetBooleanProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetBooleanProperty(
        PropertiesID props,
        byte* name,
        [MarshalAs(UnmanagedType.I1)] bool defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ClearProperty")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ClearProperty(PropertiesID props, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_EnumerateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _EnumerateProperties(
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

    public static bool SetPointerPropertyWithCleanup(
        PropertiesID props,
        byte* name,
        void* value,
        delegate* unmanaged[Cdecl]<void*, void*, void> cleanup,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
    }

    public static bool SetPointerProperty(PropertiesID props, byte* name, void* value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPointerProperty(props, name, value);
    }

    public static bool SetStringProperty(PropertiesID props, byte* name, byte* value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetStringProperty(props, name, value);
    }

    public static bool SetNumberProperty(PropertiesID props, byte* name, long value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetNumberProperty(props, name, value);
    }

    public static bool SetFloatProperty(PropertiesID props, byte* name, float value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetFloatProperty(props, name, value);
    }

    public static bool SetBooleanProperty(PropertiesID props, byte* name, bool value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetBooleanProperty(props, name, value);
    }

    public static bool HasProperty(PropertiesID props, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasProperty(props, name);
    }

    public static PropertyType GetPropertyType(PropertiesID props, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPropertyType(props, name);
    }

    public static void* GetPointerProperty(PropertiesID props, byte* name, void* defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPointerProperty(props, name, defaultValue);
    }

    public static byte* GetStringProperty(PropertiesID props, byte* name, byte* defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStringProperty(props, name, defaultValue);
    }

    public static long GetNumberProperty(PropertiesID props, byte* name, long defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumberProperty(props, name, defaultValue);
    }

    public static float GetFloatProperty(PropertiesID props, byte* name, float defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetFloatProperty(props, name, defaultValue);
    }

    public static bool GetBooleanProperty(PropertiesID props, byte* name, bool defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetBooleanProperty(props, name, defaultValue);
    }

    public static bool ClearProperty(PropertiesID props, byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearProperty(props, name);
    }

    public static bool EnumerateProperties(
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
        public static implicit operator uint(PropertiesID props) => props.Value;

        public static implicit operator PropertiesID(uint value) => new(value);
    }
}
