// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3.CustomMarshallers;

[CustomMarshaller(typeof(Sdl.Guid), MarshalMode.Default, typeof(GuidMarshaller))]
internal static class GuidMarshaller
{
    public static unsafe Sdl.Guid ConvertToManaged(Sdl.UnsafeGuid unmanaged)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        var managed = new Sdl.Guid();
        var data = new byte[16];
        for (var i = 0; i < 16; i++)
        {
            data[i] = unmanaged.Data[i];
        }

        managed.Data = data;
        return managed;
    }

    public static unsafe Sdl.UnsafeGuid ConvertToUnmanaged(Sdl.Guid managed)
    {
        LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
        var unmanaged = new Sdl.UnsafeGuid();
        for (var i = 0; i < 16; i++)
        {
            unmanaged.Data[i] = managed.Data[i];
        }

        return unmanaged;
    }

    public static void Free() => LogCategory.MakeOrGet(Sdl.CustomLogCategory).Trace();
}
