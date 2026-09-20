// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Events;

/// <summary>
/// An event that happens when file or files are dropped onto the window.
/// </summary>
/// <param name="paths">The paths of the dropped files.</param>
public sealed class GlfwDropEventArgs(IReadOnlyCollection<string> paths) : EventArgs
{
    /// <summary>
    /// Gets whether the paths of the dropped files.
    /// </summary>
    public IReadOnlyCollection<string> Paths { get; init; } = paths;
}
