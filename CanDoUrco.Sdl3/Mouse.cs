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
    public static MouseButtonFlags ButtonLeft => 1;
    public static MouseButtonFlags ButtonMiddle => 2;
    public static MouseButtonFlags ButtonRight => 3;
    public static MouseButtonFlags ButtonX1 => 4;
    public static MouseButtonFlags ButtonX2 => 5;

    public static uint ButtonLMask => ButtonMask(ButtonLeft);
    public static uint ButtonMMask => ButtonMask(ButtonMiddle);
    public static uint ButtonRMask => ButtonMask(ButtonRight);
    public static uint ButtonX1Mask => ButtonMask(ButtonX1);
    public static uint ButtonX2Mask => ButtonMask(ButtonX2);

    [LibraryImport(DllName, EntryPoint = "SDL_HasMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasMouse();

    [LibraryImport(DllName, EntryPoint = "SDL_GetMice")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseID* _GetMice(out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMouseNameForID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetMouseNameForID(MouseID id);

    [LibraryImport(DllName, EntryPoint = "SDL_GetMouseFocus")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetMouseFocus();

    [LibraryImport(DllName, EntryPoint = "SDL_GetMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags _GetMouseState(out float x, out float y);

    [LibraryImport(DllName, EntryPoint = "SDL_GetGlobalMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags _GetGlobalMouseState(out float x, out float y);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRelativeMouseState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial MouseButtonFlags _GetRelativeMouseState(out float x, out float y);

    [LibraryImport(DllName, EntryPoint = "SDL_WarpMouseInWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _WarpMouseInWindow(Window window, float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_WarpMouseGlobal")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _WarpMouseGlobal(float x, float y);

    [LibraryImport(DllName, EntryPoint = "SDL_SetRelativeMouseTransform")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetRelativeMouseTransform(
        delegate* unmanaged[Cdecl]<void*, ulong, Window, MouseID, float*, float*, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetWindowRelativeMouseMode(Window window, Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowRelativeMouseMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetWindowRelativeMouseMode(Window window);

    [LibraryImport(DllName, EntryPoint = "SDL_CaptureMouse")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _CaptureMouse(Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _CreateCursor(
        byte* data,
        byte* mask,
        int w,
        int h,
        int hotX,
        int hotY
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateColorCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _CreateColorCursor(Surface surface, int hotX, int hotY);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateAnimatedCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _CreateAnimatedCursor(
        CursorFrameInfo* frames,
        int frameCount,
        int hotX,
        int hotY
    );

    [LibraryImport(DllName, EntryPoint = "SDL_CreateSystemCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _CreateSystemCursor(SystemCursor id);

    [LibraryImport(DllName, EntryPoint = "SDL_SetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _SetCursor(Cursor cursor);

    [LibraryImport(DllName, EntryPoint = "SDL_GetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _GetCursor();

    [LibraryImport(DllName, EntryPoint = "SDL_GetDefaultCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Cursor _GetDefaultCursor();

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyCursor(Cursor cursor);

    [LibraryImport(DllName, EntryPoint = "SDL_ShowCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ShowCursor();

    [LibraryImport(DllName, EntryPoint = "SDL_HideCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HideCursor();

    [LibraryImport(DllName, EntryPoint = "SDL_CursorVisible")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _CursorVisible();

    public static uint ButtonMask(MouseButtonFlags x)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return 1U << ((int)x.Value - 1);
    }

    public static Bool HasMouse()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasMouse();
    }

    public static MouseID* GetMice(out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMice(out count);
    }

    public static byte* GetMouseNameForID(MouseID id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMouseNameForID(id);
    }

    public static Window GetMouseFocus()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMouseFocus();
    }

    public static MouseButtonFlags GetMouseState(out float x, out float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetMouseState(out x, out y);
    }

    public static MouseButtonFlags GetGlobalMouseState(out float x, out float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetGlobalMouseState(out x, out y);
    }

    public static MouseButtonFlags GetRelativeMouseState(out float x, out float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRelativeMouseState(out x, out y);
    }

    public static void WarpMouseInWindow(Window window, float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _WarpMouseInWindow(window, x, y);
    }

    public static void WarpMouseGlobal(float x, float y)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _WarpMouseGlobal(x, y);
    }

    public static Bool SetRelativeMouseTransform(
        delegate* unmanaged[Cdecl]<void*, ulong, Window, MouseID, float*, float*, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetRelativeMouseTransform(callback, userdata);
    }

    public static Bool SetWindowRelativeMouseMode(Window window, Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetWindowRelativeMouseMode(window, enabled);
    }

    public static Bool GetWindowRelativeMouseMode(Window window)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowRelativeMouseMode(window);
    }

    public static Bool CaptureMouse(Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CaptureMouse(enabled);
    }

    public static Cursor CreateCursor(byte* data, byte* mask, int w, int h, int hotX, int hotY)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateCursor(data, mask, w, h, hotX, hotY);
    }

    public static Cursor CreateColorCursor(Surface surface, int hotX, int hotY)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateColorCursor(surface, hotX, hotY);
    }

    public static Cursor CreateAnimatedCursor(
        CursorFrameInfo* frames,
        int frameCount,
        int hotX,
        int hotY
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateAnimatedCursor(frames, frameCount, hotX, hotY);
    }

    public static Cursor CreateSystemCursor(SystemCursor id)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateSystemCursor(id);
    }

    public static Bool SetCursor(Cursor cursor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetCursor(cursor);
    }

    public static Cursor GetCursor()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetCursor();
    }

    public static Cursor GetDefaultCursor()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetDefaultCursor();
    }

    public static void DestroyCursor(Cursor cursor)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyCursor(cursor);
    }

    public static Bool ShowCursor()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShowCursor();
    }

    public static Bool HideCursor()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HideCursor();
    }

    public static Bool CursorVisible()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CursorVisible();
    }

    public enum SystemCursor
    {
        Default,
        Text,
        Wait,
        Crosshair,
        Progress,
        NwSeResize,
        NeSwResize,
        EWResize,
        NSResize,
        Move,
        NotAllowed,
        Pointer,
        NWResize,
        NResize,
        NEResize,
        EResize,
        SEResize,
        SResize,
        SWResize,
        WResize,
    }

    public enum MouseWheelDirection
    {
        Normal,
        Flipped,
    }

    public readonly record struct MouseID(uint Value)
    {
        public static implicit operator uint(MouseID id) => id.Value;

        public static implicit operator MouseID(uint value) => new(value);
    }

    public readonly record struct Cursor(nint Value)
    {
        public static implicit operator nint(Cursor cursor) => cursor.Value;

        public static implicit operator Cursor(nint value) => new(value);
    }

    public readonly record struct MouseButtonFlags(uint Value)
    {
        public static MouseButtonFlags operator |(MouseButtonFlags left, MouseButtonFlags right) =>
            new(left.Value | right.Value);

        public static MouseButtonFlags operator &(MouseButtonFlags left, MouseButtonFlags right) =>
            new(left.Value & right.Value);

        public static MouseButtonFlags operator ^(MouseButtonFlags left, MouseButtonFlags right) =>
            new(left.Value ^ right.Value);

        public static MouseButtonFlags operator ~(MouseButtonFlags flags) => new(~flags.Value);

        public static implicit operator uint(MouseButtonFlags flags) => flags.Value;

        public static implicit operator MouseButtonFlags(uint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CursorFrameInfo
    {
        public Surface Surface;
        public uint Duration;
    }
}
