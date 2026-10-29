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

using System.Collections;
using CanDoUrco.Engine.Options.PluginOptions;

namespace CanDoUrco.Engine.Plugins;

public class DefaultPlugins : IEnumerable<IPlugin>
{
    private readonly List<IPlugin> _plugins = [new LoggingPlugin(new LoggingPluginOptions())];

    public DefaultPlugins Set(LoggingPlugin loggingPlugin)
    {
        ArgumentNullException.ThrowIfNull(loggingPlugin);
        var index = _plugins.FindIndex(p => p is LoggingPlugin);
        if (index >= 0)
        {
            _plugins[index] = loggingPlugin;
        }
        else
        {
            _plugins.Add(loggingPlugin);
        }

        return this;
    }

    public IEnumerator<IPlugin> GetEnumerator() => _plugins.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
