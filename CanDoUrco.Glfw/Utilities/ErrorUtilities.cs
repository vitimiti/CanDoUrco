// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Exceptions;

namespace CanDoUrco.Glfw.Utilities;

internal static unsafe class ErrorUtilities
{
    public static void CheckAndThrowErrorFromVoidMethod()
    {
        var errorCode = Native.Glfw.GetError(out var errorPtr);
        if (errorCode != Native.Glfw.NoErrorDefine)
        {
            var error = Utf8StringMarshaller.ConvertToManaged(errorPtr);
            throw new GlfwException(error, errorCode);
        }
    }

    public static void CheckAndThrowErrorFromBadReturnMethod()
    {
        var errorCode = Native.Glfw.GetError(out var errorPtr);
        var error = Utf8StringMarshaller.ConvertToManaged(errorPtr);
        throw new GlfwException(error, errorCode);
    }
}
