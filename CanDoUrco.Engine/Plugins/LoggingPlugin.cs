// This is the Can Do Urco engine.
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

using System.Runtime.InteropServices;
using CanDoUrco.Engine.Options.PluginOptions;
using CanDoUrco.Native.Sdl3;

namespace CanDoUrco.Engine.Plugins;

public class LoggingPlugin(LoggingPluginOptions? options = null) : IPlugin
{
    private readonly LoggingPluginOptions? _options = options;

    public void Build(App app)
    {
        if (_options is not null)
        {
            Sdl.SetLogPriorities(
                _options.LoggingLevel switch
                {
                    LoggingLevel.Trace => Sdl.LogPriority.Trace,
                    LoggingLevel.Debug => Sdl.LogPriority.Debug,
                    LoggingLevel.Info => Sdl.LogPriority.Info,
                    LoggingLevel.Warn => Sdl.LogPriority.Warn,
                    LoggingLevel.Error => Sdl.LogPriority.Error,
                    LoggingLevel.Critical => Sdl.LogPriority.Critical,
                    _ => Sdl.LogPriority.Info,
                }
            );
        }
    }

    internal unsafe void Log(Sdl.LogCategory category, Sdl.LogPriority priority, string message)
    {
        if (_options is null || !_options.EnableLogging || string.IsNullOrEmpty(message))
        {
            return;
        }

        var messagePtr = (byte*)Marshal.StringToCoTaskMemUTF8(message);
        try
        {
            Sdl.LogMessage(category, priority, messagePtr);
        }
        finally
        {
            Marshal.FreeCoTaskMem((nint)messagePtr);
        }
    }
}
