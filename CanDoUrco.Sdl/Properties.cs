// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) <year>  <name of author>
//
// This program is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// This program is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl.CustomMarshallers;

namespace CanDoUrco.Sdl;

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
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnownedUtf8StringMarshaller))]
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
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPointerPropertyWithCleanup(props, name, value, cleanup, userdata);
    }

    public static unsafe bool SetPointerProperty(PropertiesID props, string name, void* value)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetPointerProperty(props, name, value);
    }

    public static bool SetStringProperty(PropertiesID props, string name, string? value)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetStringProperty(props, name, value);
    }

    public static bool SetNumberProperty(PropertiesID props, string name, long value)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetNumberProperty(props, name, value);
    }

    public static bool SetFloatProperty(PropertiesID props, string name, float value)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetFloatProperty(props, name, value);
    }

    public static bool SetBooleanProperty(PropertiesID props, string name, bool value)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetBooleanProperty(props, name, value);
    }

    public static bool HasProperty(PropertiesID props, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasProperty(props, name);
    }

    public static PropertyType GetPropertyType(PropertiesID props, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPropertyType(props, name);
    }

    public static unsafe void* GetPointerProperty(
        PropertiesID props,
        string name,
        void* defaultValue
    )
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetPointerProperty(props, name, defaultValue);
    }

    public static string? GetStringProperty(PropertiesID props, string name, string? defaultValue)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetStringProperty(props, name, defaultValue);
    }

    public static long GetNumberProperty(PropertiesID props, string name, long defaultValue)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetNumberProperty(props, name, defaultValue);
    }

    public static float GetFloatProperty(PropertiesID props, string name, float defaultValue)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetFloatProperty(props, name, defaultValue);
    }

    public static bool GetBooleanProperty(PropertiesID props, string name, bool defaultValue)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetBooleanProperty(props, name, defaultValue);
    }

    public static bool ClearProperty(PropertiesID props, string name)
    {
        ArgumentNullException.ThrowIfNull(name);
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
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

    public readonly record struct PropertiesID(uint Value)
    {
        public bool IsInvalid => Value == 0;
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
}
