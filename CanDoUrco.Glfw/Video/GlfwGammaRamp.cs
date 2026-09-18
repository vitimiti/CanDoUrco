// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Video;

/// <summary>
/// Represents a monitor's gamma ramp.
/// </summary>
/// <remarks>
/// <para>Each channel must be of the same size as the others for this to be valid during use.</para>
/// <para>This record is sealed.</para>
/// </remarks>
public sealed record GlfwGammaRamp
{
    /// <summary>
    /// Gets the gamma ramp for the red channel.
    /// </summary>
    public IReadOnlyCollection<ushort> Red { get; init; } = [];

    /// <summary>
    /// Gets the gamma ramp for the green channel.
    /// </summary>
    public IReadOnlyCollection<ushort> Green { get; init; } = [];

    /// <summary>
    /// Gets the gamma ramp for the blue channel.
    /// </summary>
    public IReadOnlyCollection<ushort> Blue { get; init; } = [];
}
