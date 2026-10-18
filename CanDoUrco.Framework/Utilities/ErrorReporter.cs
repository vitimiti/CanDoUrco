// A common framework for the projects and engines of Can Do Urco
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

using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Framework.Native.SDL3;

namespace CanDoUrco.Framework.Utilities;

public static class ErrorReporter
{
    public static unsafe int RunWithErrorReporting(Action action)
    {
        try
        {
            action();
            return 0;
        }
        catch (Exception ex)
        {
            var titleMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
            var messageMarshaller = new Utf8StringMarshaller.ManagedToUnmanagedIn();
            try
            {
                titleMarshaller.FromManaged(
                    "CDU - Error",
                    new byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
                );

                messageMarshaller.FromManaged(
                    $"A fatal error occurred: {ex.Message}.\n\nDetails:\n{ex}",
                    new byte[Utf8StringMarshaller.ManagedToUnmanagedIn.BufferSize]
                );

                _ = Sdl.ShowSimpleMessageBox(
                    Sdl.MessageBoxError,
                    titleMarshaller.ToUnmanaged(),
                    messageMarshaller.ToUnmanaged(),
                    null
                );
            }
            catch
            {
                // Ignore any exceptions that occur while reporting the error
            }
            finally
            {
                titleMarshaller.Free();
                messageMarshaller.Free();
            }

            return 1;
        }
    }
}
