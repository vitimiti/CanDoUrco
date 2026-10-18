// CanDoUrco.Sdl3 - SDL3 bindings for Can Do Urco
// Copyright (C) 2026  Can Do Urco (Victor Matia-Cheng)
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
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public readonly record struct PropertiesID(uint Value)
    {
        public static implicit operator PropertiesID(uint value) => new(value);

        public static implicit operator uint(PropertiesID id) => id.Value;
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

    public const string PropNameString = "SDL.name";

    [LibraryImport(DllName, EntryPoint = "SDL_GetGlobalProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertiesID GetGlobalProperties();

    [LibraryImport(DllName, EntryPoint = "SDL_CreateProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertiesID CreateProperties();

    [LibraryImport(DllName, EntryPoint = "SDL_CopyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool CopyProperties(PropertiesID src, PropertiesID dst);

    [LibraryImport(DllName, EntryPoint = "SDL_LockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool LockProperties(PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_UnlockProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void UnlockProperties(PropertiesID props);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void CleanupPropertyCallback(nint userdata, nint value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPointerPropertyWithCleanup",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPointerPropertyWithCleanup(
        PropertiesID props,
        string name,
        nint value,
        CleanupPropertyCallback? cleanup,
        nint userdata
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetPointerProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetPointerProperty(PropertiesID props, string name, nint value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetStringProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetStringProperty(PropertiesID props, string name, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetNumberProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetNumberProperty(PropertiesID props, string name, long value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetFloatProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetFloatProperty(PropertiesID props, string name, float value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetBooleanProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetBooleanProperty(
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
    public static partial bool HasProperty(PropertiesID props, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPropertyType",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial PropertyType GetPropertyType(PropertiesID props, string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetPointerProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial nint GetPointerProperty(
        PropertiesID props,
        string name,
        nint defaultValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetStringProperty",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnownedUtf8StringMarshaller))]
    public static partial string? GetStringProperty(
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
    public static partial long GetNumberProperty(
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
    public static partial float GetFloatProperty(
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
    public static partial bool GetBooleanProperty(
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
    public static partial bool ClearProperty(PropertiesID props, string name);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void EnumeratePropertiesCallback(
        nint userdata,
        PropertiesID props,
        [MarshalAs(UnmanagedType.LPUTF8Str)] string name
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_EnumerateProperties",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool EnumerateProperties(
        PropertiesID props,
        EnumeratePropertiesCallback? callback,
        nint userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyProperties(PropertiesID props);
}
