// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

namespace CanDoUrco.Glfw.Utilities;

/// <summary>
/// These are special values accepted by certain GLFW data.
/// </summary>
public static class GlfwSpecialValues
{
    /// <summary>
    /// Gets a value indicating that any position is acceptable.
    /// </summary>
    public static int AnyPosition => Native.Glfw.AnyPositionDefine;

    /// <summary>
    /// Gets a value indicating that we don't care about a value and GLFW should choose for us.
    /// </summary>
    public static int DontCare => Native.Glfw.DontCareDefine;
}
