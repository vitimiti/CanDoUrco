// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Framework.Utilities;

internal readonly ref struct ManagedUtf8String : IDisposable
{
    private readonly Span<byte> _buffer;
    private readonly Utf8StringMarshaller.ManagedToUnmanagedIn _marshaller;

    public ManagedUtf8String(string? value)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        _buffer = new byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize];
        _marshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
        _marshaller.FromManaged(value, _buffer);
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        _marshaller.Free();
    }

    public static unsafe implicit operator byte*(ManagedUtf8String managedUtf8String)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return managedUtf8String._marshaller.ToUnmanaged();
    }
}
