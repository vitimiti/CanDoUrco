// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Framework.Utilities;

internal sealed unsafe class UnownedUtf8String
{
    private readonly byte* _ptr;

    public UnownedUtf8String(byte* ptr)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        _ptr = ptr;
    }

    public static implicit operator UnownedUtf8String(byte* ptr)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return new(ptr);
    }

    public static implicit operator string?(UnownedUtf8String unowned)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        return Utf8StringMarshaller.ConvertToManaged(unowned._ptr);
    }
}
