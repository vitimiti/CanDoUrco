// CanDoUrco.Framework - An unopinionated framework for building game applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;
using CanDoUrco.Sdl3;

namespace CanDoUrco.Framework.Subsystems;

public class SubsystemBase : IDisposable
{
    private readonly uint _subsystem;

    private bool _initialised;

    protected bool Disposed { get; set; }

    public unsafe SubsystemBase(AppContext app, uint subsystem)
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(app);
        _subsystem = subsystem;
        if (!Sdl.Init(subsystem))
        {
            throw new InvalidOperationException(
                $"Failed to initialize SDL subsystem {subsystem}: {Utf8StringMarshaller.ConvertToManaged(Sdl.GetError())}."
            );
        }
        _initialised = true;
    }

    public void Dispose()
    {
        LogCategory.MakeOrGet(AppContext.CustomLogCategory).Trace();
        if (_initialised)
        {
            Sdl.QuitSubSystem(_subsystem);
            _initialised = false;
        }

        Disposed = true;
        GC.SuppressFinalize(this);
    }
}
