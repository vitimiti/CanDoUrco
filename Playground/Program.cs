// This is a small testing ground for CanDoUrco.
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

// Following https://www.opengl-tutorial.org/

using CanDoUrco.Framework;
using CanDoUrco.Framework.Utilities;

const string windowTitle = "CanDoUrco Playground";

ErrorReporter.RunWithErrorReporting(
    () =>
    {
        using var game = new Game(opts => opts.WindowOptions.Title = windowTitle);
        game.UpdateEnd += gameState =>
            game.Window.Title = $"{windowTitle} - {1D / gameState.Delta:F0}FPS";

        game.Run();

        throw new Exception("Test exception");
    },
    opts => opts.LogDirectoryName = "Playground"
);
