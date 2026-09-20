// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Glfw.Events;
using CanDoUrco.Glfw.Utilities;

namespace CanDoUrco.Glfw.Input;

/// <summary>
/// Represents a joystick.
/// </summary>
/// <remarks>
/// <para>This class is sealed.</para>
/// <para>This class inherits from <see cref="IDisposable"/>.</para>
public sealed class GlfwJoystick : IDisposable
{
    /// <summary>
    /// An event that happens when a given joystick is connected or disconnected.
    /// </summary>
    public event EventHandler<GlfwJoystickConnectionEventArgs>? ConnectionEvent;

    private static readonly Dictionary<GlfwJoystick, GlfwJoystickId> Ids = [];

    private readonly GlfwJoystickId _id;
    private bool _disposedValue;

    /// <summary>
    /// Creates a new instance of the <see cref="GlfwJoystick"/> class.
    /// </summary>
    /// <param name="id">The <see cref="GlfwJoystickId"/> to use for this joystick.</param>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public unsafe GlfwJoystick(GlfwJoystickId id)
    {
        _id = id;
        Ids[this] = _id;
        Native.Glfw.SetJoystickCallback(&HandleEvent);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
    }

    /// <summary>
    /// Finalizes an instance of the <see cref="GlfwJoystick"/> class.
    /// </summary>
    ~GlfwJoystick()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <summary>
    /// Disposes the managed and unmanaged resources held by <see cref="GlfwJoystick"/>.
    /// </summary>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Checks whether the joystick is actually present and ready for use.
    /// </summary>
    /// <returns><see langword="true"/> if the joystick is present, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <remarks>This should be called first to avoid certain errors, although if you skip this call exceptions will be raised if required by other methods.</remarks>
    public bool IsPresent()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.JoystickPresent((int)_id);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result;
    }

    /// <summary>
    /// Gets the values of the joystick axes.
    /// </summary>
    /// <returns>A <see cref="IReadOnlyCollection{T}"/> with <see cref="float"/> values indicating the axes values.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public unsafe IReadOnlyCollection<float> GetAxes()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetJoystickAxes((int)_id, out var count);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result is null
            ? throw ExceptionIfNotPresent()
            : new Span<float>(result, count).ToArray();
    }

    /// <summary>
    /// Gets the states of the joystick buttons.
    /// </summary>
    /// <returns>A new <see cref="IReadOnlyCollection{T}"/> of <see cref="GlfwJoystickButtonState"/> with the state of the joystick buttons.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public unsafe IReadOnlyCollection<GlfwJoystickButtonState> GetButtons()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetJoystickButtons((int)_id, out var count);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result is null
            ? throw ExceptionIfNotPresent()
            : new Span<GlfwJoystickButtonState>((GlfwJoystickButtonState*)result, count).ToArray();
    }

    /// <summary>
    /// Gets the states of the joystick hats.
    /// </summary>
    /// <returns>A <see cref="IReadOnlyCollection{T}"/> of <see cref="GlfwJoystickHatStates"/> with the joystick hats values.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public unsafe IReadOnlyCollection<GlfwJoystickHatStates> GetHats()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetJoystickHats((int)_id, out var count);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return result is null
            ? throw ExceptionIfNotPresent()
            : new Span<GlfwJoystickHatStates>((GlfwJoystickHatStates*)result, count).ToArray();
    }

    /// <summary>
    /// Gets the joystick name.
    /// </summary>
    /// <returns>A <see cref="string"/> with the joystick's name.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public unsafe string GetName()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetJoystickName((int)_id);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return Utf8StringMarshaller.ConvertToManaged(result) ?? throw ExceptionIfNotPresent();
    }

    /// <summary>
    /// Gets the joystick GUID.
    /// </summary>
    /// <returns>A new <see cref="Guid"/> for the joystick.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public unsafe Guid GetGuid()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetJoystickGuid((int)_id);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        var str = Utf8StringMarshaller.ConvertToManaged(result) ?? throw ExceptionIfNotPresent();

        return new Guid(str);
    }

    /// <summary>
    /// Gets whether the joystick is a gamepad.
    /// </summary>
    /// <returns><see langword="true"/> if the joystick is a gamepad, <see langword="false"/> otherwise.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use.</exception>
    public bool IsGamepad()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.JoystickIsGamepad((int)_id);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return !result ? throw ExceptionIfNotPresent() : result;
    }

    /// <summary>
    /// Updates the gamepad mappings.
    /// </summary>
    /// <param name="string">A <see cref="string"/> with the gamepad mappings.</param>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    public void UpdateGamepadMappings(string @string)
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        ArgumentNullException.ThrowIfNull(@string);
        if (!Native.Glfw.UpdateGamepadMappings(@string))
        {
            GlfwErrorUtilities.ThrowError();
        }
    }

    /// <summary>
    /// Gets the gamepad name.
    /// </summary>
    /// <returns>A <see cref="string"/> with the gamepad name.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use or there were no available gamepad mappings.</exception>
    public unsafe string GetGamepadName()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var ptr = Native.Glfw.GetGamepadName((int)_id);
        GlfwErrorUtilities.CheckErrorCodeAndMaybeThrowError();
        return Utf8StringMarshaller.ConvertToManaged(ptr)
            ?? throw ExceptionIfNotPresentOrNoMappings();
    }

    /// <summary>
    /// Gets the current gamepad state.
    /// </summary>
    /// <returns>A new <see cref="GlfwGamepadState"/> with the current gamepad state.</returns>
    /// <exception cref="ObjectDisposedException">Thrown when the <see cref="GlfwWindow"/> instance was already disposed.</exception>
    /// <exception cref="GlfwException">Thrown when an internal GLFW error happens.</exception>
    /// <exception cref="InvalidOperationException">Thrown when the joystick wasn't present and ready for use or there were no available gamepad mappings.</exception>
    public unsafe GlfwGamepadState GetGamepadState()
    {
        ObjectDisposedException.ThrowIf(_disposedValue, this);
        var result = Native.Glfw.GetGamepadState((int)_id, out var state);
        if (!result)
        {
            throw ExceptionIfNotPresentOrNoMappings();
        }

        var buttons = new Span<GlfwGamepadButtonState>((GlfwGamepadButtonState*)state.Buttons, 15);
        var axes = new Span<float>(state.Axes, 6);
        return new GlfwGamepadState() { Buttons = buttons.ToArray(), Axes = axes.ToArray() };
    }

    private unsafe void Dispose(bool disposing)
    {
        if (_disposedValue)
        {
            return;
        }

        if (disposing)
        {
            Ids.Remove(this);
        }

        if (Ids.Count == 0)
        {
            Native.Glfw.SetJoystickCallback(null);
            // Ignore errors intentionally
        }

        _disposedValue = true;
    }

    private InvalidOperationException ExceptionIfNotPresent() =>
        new($"The joystick {_id} is not present.");

    private InvalidOperationException ExceptionIfNotPresentOrNoMappings() =>
        new($"The joystick {_id} is either not present or it has no gamepad mappings.");

    [UnmanagedCallersOnly(CallConvs = [typeof(CallConvCdecl)])]
    private static void HandleEvent(int jid, int @event)
    {
        if (Ids.Count == 0)
        {
            return;
        }

        foreach (var (instance, id) in Ids)
        {
            if (jid != (int)id)
            {
                continue;
            }

            instance.ConnectionEvent?.Invoke(
                instance,
                new GlfwJoystickConnectionEventArgs((GlfwJoystickConnectionEvent)@event)
            );
        }
    }
}
