// CanDoUrco.OpenGl10 - Provides OpenGL 1.0 bindings for the CanDoUrco project.
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

using CanDoUrco.OpenGl.Common.Utilities;
using CanDoUrco.OpenGl10.Native;

namespace CanDoUrco.OpenGl10;

internal static partial class Gl10
{
    public static void CullFace(CullFaceMode mode)
    {
        Gl.CullFace ??= Loader.GetDelegate<Gl.CullFaceDelegate>("glCullFace");
        Gl.CullFace((uint)mode);
        Error.PerApiCallErrorThrower(
            GetError,
            $"Error occurred while calling {nameof(CullFace)} with args: [{mode}]",
            Gl.Enums.NoError
        );
    }

    public static void FrontFace(FrontFaceOrientation orientation)
    {
        Gl.FrontFace ??= Loader.GetDelegate<Gl.FrontFaceDelegate>("glFrontFace");
        Gl.FrontFace((uint)orientation);
        Error.PerApiCallErrorThrower(
            GetError,
            $"Error occurred while calling {nameof(FrontFace)} with args: [{orientation}]",
            Gl.Enums.NoError
        );
    }

    public static void Hint(HintTarget target, HintMode mode)
    {
        Gl.Hint ??= Loader.GetDelegate<Gl.HintDelegate>("glHint");
        Gl.Hint((uint)target, (uint)mode);
        Error.PerApiCallErrorThrower(
            GetError,
            $"Error occurred while calling {nameof(Hint)} with args: [{target}, {mode}]",
            Gl.Enums.NoError
        );
    }
}
