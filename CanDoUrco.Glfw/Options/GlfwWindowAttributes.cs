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

using CanDoUrco.Glfw.Video;

namespace CanDoUrco.Glfw.Options;

/// <summary>
/// Represents the possible attributes of a GLFW window.
/// </summary>
/// <remarks>This record is sealed.</remarks>
/// <seealso cref="GlfwWindow"/>
public sealed record class GlfwWindowAttributes
{
    /// <summary>
    /// Gets whether the window is focused.
    /// </summary>
    public bool Focused { get; init; }

    /// <summary>
    /// Gets whether the window is iconified.
    /// </summary>
    public bool Iconified { get; init; }

    /// <summary>
    /// Gets whether the window is maximized.
    /// </summary>
    public bool Maximized { get; init; }

    /// <summary>
    /// Gets whether the window is hovered.
    /// </summary>
    public bool Hovered { get; init; }

    /// <summary>
    /// Gets whether the window is visible.
    /// </summary>
    public bool Visible { get; init; }

    /// <summary>
    /// Gets whether the window is resizable.
    /// </summary>
    public bool Resizable { get; init; }

    /// <summary>
    /// Gets whether the window is decorated.
    /// </summary>
    public bool Decorated { get; init; }

    /// <summary>
    /// Gets whether the window auto iconifies.
    /// </summary>
    public bool AutoIconify { get; init; }

    /// <summary>
    /// Gest whether the window is floating.
    /// </summary>
    public bool Floating { get; init; }

    /// <summary>
    /// Gets whether the window frambuffer is transparent.
    /// </summary>
    public bool TransparentFramebuffer { get; init; }

    /// <summary>
    /// Gets whether the window focuses when shown.
    /// </summary>
    public bool FocusOnShow { get; init; }

    /// <summary>
    /// Gets whether the window supports mouse passthrough.
    /// </summary>
    public bool MousePassthrough { get; init; }

    /// <summary>
    /// Gets the window's client API.
    /// </summary>
    /// <seealso cref="GlfwClientApi"/>
    public GlfwClientApi ClientApi { get; init; }

    /// <summary>
    /// Gets the window's context creation API.
    /// </summary>
    /// <seealso cref="GlfwContextCreationApi"/>
    public GlfwContextCreationApi ContextCreationApi { get; init; }

    /// <summary>
    /// Gets the window's context version.
    /// </summary>
    public required Version ContextVersion { get; init; }

    /// <summary>
    /// Gets whether the window supports forward compat mode for OpenGL.
    /// </summary>
    public bool OpenGlForwardCompat { get; init; }

    /// <summary>
    /// Gets whether the window's context is in debug mode.
    /// </summary>
    public bool ContextDebug { get; init; }

    /// <summary>
    /// Gets the OpenGL profile of the window.
    /// </summary>
    /// <seealso cref="GlfwOpenGlProfile"/>
    public GlfwOpenGlProfile OpenGlProfile { get; init; }

    /// <summary>
    /// Gets the context release behaviour of the window.
    /// </summary>
    /// <seealso cref="GlfwContextReleaseBehavior"/>
    public GlfwContextReleaseBehavior ContextReleaseBehavior { get; init; }

    /// <summary>
    /// Gets whether the window context supports no error mode.
    /// </summary>
    public bool ContextNoError { get; init; }

    /// <summary>
    /// Gets the window's context robustness.
    /// </summary>
    /// <seealso cref="GlfwContextRobustness"/>
    public GlfwContextRobustness ContextRobustness { get; init; }

    /// <summary>
    /// Gets whether the window supports the doublebuffer.
    /// </summary>
    public bool Doublebuffer { get; init; }
}
