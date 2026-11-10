// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// oBTain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> PropTextInputTypeNumber => "SDL.textinput.type\0"u8;
    public static ReadOnlySpan<byte> PropTextInputCapitalizationNumber =>
        "SDL.textinput.capitalization\0"u8;
    public static ReadOnlySpan<byte> PropTextInputAutocorrectBoolean =>
        "SDL.textinput.autocorrect\0"u8;
    public static ReadOnlySpan<byte> PropTextInputMultilineBoolean => "SDL.textinput.multiline\0"u8;
    public static ReadOnlySpan<byte> PropTextInputAndroidInputTypeNumber =>
        "SDL.textinput.android.inputtype\0"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_HasKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasKeyboard();

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyboards")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial KeyboardID* _GetKeyboards(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyboardNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetKeyboardNameForID(KeyboardID instanceID);

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyboardFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetKeyboardFocus();

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyboardState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool* _GetKeyboardState(out int numKeys);

    [LibraryImport(DllName, EntryPoint = "SDL_ResetKeyboard")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ResetKeyboard();

    [LibraryImport(DllName, EntryPoint = "SDL_GetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keymod _GetModState();

    [LibraryImport(DllName, EntryPoint = "SDL_SetModState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetModState(Keymod mod);

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyFromScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode _GetKeyFromScancode(
        Scancode scancode,
        Keymod modState,
        Bool keyEvent
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetScancodeFromKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode _GetScancodeFromKey(Keycode key, out Keymod modState);

    [LibraryImport(DllName, EntryPoint = "SDL_SetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetScancodeName(Scancode scancode, byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetScancodeName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetScancodeName(Scancode scancode);

    [LibraryImport(DllName, EntryPoint = "SDL_GetScancodeFromName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Scancode _GetScancodeFromName(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetKeyName(Keycode key);

    [LibraryImport(DllName, EntryPoint = "SDL_GetKeyFromName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Keycode _GetKeyFromName(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_StartTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StartTextInput(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_StartTextInputWithProperties")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StartTextInputWithProperties(Window window, PropertiesID props);

    [LibraryImport(DllName, EntryPoint = "SDL_TextInputActive")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _TextInputActive(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_StopTextInput")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _StopTextInput(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_ClearComposition")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ClearComposition(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTextInputArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetTextInputArea(Window window, in Rect rect, int cursor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTextInputArea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTextInputArea(Window window, out Rect rect, out int cursor);

    [LibraryImport(DllName, EntryPoint = "SDL_HasScreenKeyboardSupport")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasScreenKeyboardSupport();

    [LibraryImport(DllName, EntryPoint = "SDL_ScreenKeyboardShown")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ScreenKeyboardShown(Window window);

    public static Bool HasKeyboard()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasKeyboard();
    }

    public static KeyboardID* GetKeyboards(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyboards(out count);
    }

    public static byte* GetKeyboardNameForID(KeyboardID instanceID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyboardNameForID(instanceID);
    }

    public static Window GetKeyboardFocus()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyboardFocus();
    }

    public static Bool* GetKeyboardState(out int numKeys)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyboardState(out numKeys);
    }

    public static void ResetKeyboard()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ResetKeyboard();
    }

    public static Keymod GetModState()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetModState();
    }

    public static void SetModState(Keymod mod)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetModState(mod);
    }

    public static Keycode GetKeyFromScancode(Scancode scancode, Keymod modState, Bool keyEvent)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyFromScancode(scancode, modState, keyEvent);
    }

    public static Scancode GetScancodeFromKey(Keycode key, out Keymod modState)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetScancodeFromKey(key, out modState);
    }

    public static byte* GetScancodeName(Scancode scancode)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetScancodeName(scancode);
    }

    public static Scancode GetScancodeFromName(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetScancodeFromName(name);
    }

    public static byte* GetKeyName(Keycode key)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyName(key);
    }

    public static Keycode GetKeyFromName(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetKeyFromName(name);
    }

    public static Bool StartTextInput(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StartTextInput(window);
    }

    public static Bool StartTextInputWithProperties(Window window, PropertiesID props)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StartTextInputWithProperties(window, props);
    }

    public static Bool TextInputActive(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _TextInputActive(window);
    }

    public static Bool StopTextInput(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _StopTextInput(window);
    }

    public static Bool ClearComposition(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ClearComposition(window);
    }

    public static Bool SetTextInputArea(Window window, in Rect rect, int cursor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetTextInputArea(window, in rect, cursor);
    }

    public static Bool GetTextInputArea(Window window, out Rect rect, out int cursor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTextInputArea(window, out rect, out cursor);
    }

    public static Bool HasScreenKeyboardSupport()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasScreenKeyboardSupport();
    }

    public static Bool ScreenKeyboardShown(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ScreenKeyboardShown(window);
    }

    public enum TextInputType
    {
        Text,
        TextName,
        TextEmail,
        TextUsername,
        TextPasswordHidden,
        TextPasswordVisible,
        Number,
        NumberPasswordHidden,
        NumberPasswordVisible,
    }

    public enum Capitalization
    {
        None,
        Sentences,
        Words,
        Letters,
    }

    public readonly record struct KeyboardID(uint Value)
    {
        public static implicit operator uint(KeyboardID id) => id.Value;

        public static implicit operator KeyboardID(uint value) => new(value);
    }
}
