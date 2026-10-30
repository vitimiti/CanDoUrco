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

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using System.Text;
using CanDoUrco.Engine.Options;
using CanDoUrco.Engine.Plugins;
using CanDoUrco.Native.Sdl3;

namespace CanDoUrco.Engine;

public sealed class App : IDisposable
{
    private readonly Dictionary<Type, IPlugin> _pluginMap = [];

    private LoggingPlugin? _loggingPlugin;
    private bool _isRunning;
    private bool _disposedValue;

    public App(AppOptions? options = null)
    {
        if (options is not null)
        {
            if (options.Name is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataNameString,
                    Encoding.UTF8.GetBytes(options.Name)
                );
            }

            if (options.Version is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataVersionString,
                    Encoding.UTF8.GetBytes(options.Version.ToString())
                );
            }

            if (options.Identifier is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataIdentifierString,
                    Encoding.UTF8.GetBytes(options.Identifier)
                );
            }

            if (options.Creator is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataCreatorString,
                    Encoding.UTF8.GetBytes(options.Creator)
                );
            }

            if (options.Copyright is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataCopyrightString,
                    Encoding.UTF8.GetBytes(options.Copyright)
                );
            }

            if (options.Url is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataUrlString,
                    Encoding.UTF8.GetBytes(options.Url.ToString())
                );
            }

            if (options.AppType is not null)
            {
                SetAppMetadataProperty(
                    Sdl.PropAppMetadataTypeString,
                    Encoding.UTF8.GetBytes(options.AppType)
                );
            }
        }
    }

    public void Dispose()
    {
        if (_disposedValue)
        {
            return;
        }

        Sdl.Quit();

        _disposedValue = true;
    }

    public App AddPlugin(IPlugin plugin)
    {
        _pluginMap[plugin.GetType()] = plugin;
        if (plugin is LoggingPlugin loggingPlugin)
        {
            _loggingPlugin = loggingPlugin;
        }

        return this;
    }

    public App AddPlugins(IEnumerable<IPlugin> plugins)
    {
        foreach (var plugin in plugins)
        {
            AddPlugin(plugin);
        }

        return this;
    }

    public T GetPlugin<T>()
        where T : class, IPlugin =>
        _pluginMap.TryGetValue(typeof(T), out var p)
            ? (T)p
            : throw new InvalidOperationException(
                $"{typeof(T).Name} is required but has not been added to the App."
            );

    public bool TryGetPlugin<T>([NotNullWhen(true)] out T? plugin)
        where T : class, IPlugin
    {
        plugin = _pluginMap.TryGetValue(typeof(T), out var p) ? (T)p : null;
        return plugin is not null;
    }

    public void Run()
    {
        foreach (var plugin in _pluginMap.Values)
        {
            plugin.Build(this);
        }

        _isRunning = true;
        double deltaTime;
        var startTime = Stopwatch.GetTimestamp();
        while (_isRunning)
        {
            var currentTime = Stopwatch.GetTimestamp();
            deltaTime = (currentTime - startTime) / (double)Stopwatch.Frequency;
        }
    }

    internal void Log(Sdl.LogCategory category, Sdl.LogPriority priority, string message) =>
        _loggingPlugin?.Log(category, priority, message);

    private static unsafe void SetAppMetadataProperty(
        ReadOnlySpan<byte> name,
        ReadOnlySpan<byte> value
    )
    {
        fixed (byte* namePtr = name)
        {
            fixed (byte* valuePtr = value)
            {
                if (!Sdl.SetAppMetadataProperty(namePtr, valuePtr))
                {
                    var error = Marshal.PtrToStringUTF8((nint)Sdl.GetError());
                    throw new InvalidOperationException(
                        $"Failed to set app metadata property: {error}."
                    );
                }
            }
        }
    }
}
