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
    [LibraryImport(DllName, EntryPoint = "SDL_PumpEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _PumpEvents();

    [LibraryImport(DllName, EntryPoint = "SDL_PeepEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _PeepEvents(
        Event* events,
        int numEvents,
        EventAction action,
        uint minType,
        uint maxType
    );

    [LibraryImport(DllName, EntryPoint = "SDL_HasEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasEvent(uint type);

    [LibraryImport(DllName, EntryPoint = "SDL_HasEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _HasEvents(uint minType, uint maxType);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _FlushEvent(uint type);

    [LibraryImport(DllName, EntryPoint = "SDL_FlushEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _FlushEvents(uint minType, uint maxType);

    [LibraryImport(DllName, EntryPoint = "SDL_PollEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PollEvent(Event* @event);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitEvent(Event* @event);

    [LibraryImport(DllName, EntryPoint = "SDL_WaitEventTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _WaitEventTimeout(Event* @event, int timeoutMS);

    [LibraryImport(DllName, EntryPoint = "SDL_PushEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _PushEvent(ref Event @event);

    [LibraryImport(DllName, EntryPoint = "SDL_SetEventFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetEventFilter(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetEventFilter")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetEventFilter(
        out delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        out void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_AddEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _AddEventWatch(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveEventWatch")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _RemoveEventWatch(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_FilterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _FilterEvents(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetEventEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetEventEnabled(uint type, Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_EventEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _EventEnabled(uint type);

    [LibraryImport(DllName, EntryPoint = "SDL_RegisterEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial uint _RegisterEvents(int numEvents);

    [LibraryImport(DllName, EntryPoint = "SDL_GetWindowFromEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Window _GetWindowFromEvent(in Event @event);

    [LibraryImport(DllName, EntryPoint = "SDL_GetEventDescription")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial int _GetEventDescription(Event* @event, byte* buf, int bufLen);

    public static void PumpEvents()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _PumpEvents();
    }

    public static int PeepEvents(
        Event* events,
        int numEvents,
        EventAction action,
        uint minType,
        uint maxType
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PeepEvents(events, numEvents, action, minType, maxType);
    }

    public static Bool HasEvent(uint type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasEvent(type);
    }

    public static Bool HasEvents(uint minType, uint maxType)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasEvents(minType, maxType);
    }

    public static void FlushEvent(uint type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _FlushEvent(type);
    }

    public static void FlushEvents(uint minType, uint maxType)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _FlushEvents(minType, maxType);
    }

    public static Bool PollEvent(Event* @event)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PollEvent(@event);
    }

    public static Bool WaitEvent(Event* @event)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitEvent(@event);
    }

    public static Bool WaitEventTimeout(Event* @event, int timeoutMS)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _WaitEventTimeout(@event, timeoutMS);
    }

    public static Bool PushEvent(ref Event @event)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _PushEvent(ref @event);
    }

    public static void SetEventFilter(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetEventFilter(filter, userdata);
    }

    public static Bool GetEventFilter(
        out delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        out void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEventFilter(out filter, out userdata);
    }

    public static void AddEventWatch(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _AddEventWatch(filter, userdata);
    }

    public static void RemoveEventWatch(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RemoveEventWatch(filter, userdata);
    }

    public static void FilterEvents(
        delegate* unmanaged[Cdecl]<void*, Event*, Bool> filter,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _FilterEvents(filter, userdata);
    }

    public static void SetEventEnabled(uint type, Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetEventEnabled(type, enabled);
    }

    public static Bool EventEnabled(uint type)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _EventEnabled(type);
    }

    public static uint RegisterEvents(int numEvents)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _RegisterEvents(numEvents);
    }

    public static Window GetWindowFromEvent(in Event @event)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetWindowFromEvent(in @event);
    }

    public static int GetEventDescription(Event* @event, byte* buf, int bufLen)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetEventDescription(@event, buf, bufLen);
    }

    public enum EventType : uint
    {
        First,
        Quit = 0x0100,
        Terminating,
        LowMemory,
        WillEnterBackground,
        DidEnterBackground,
        WillEnterForeground,
        DidEnterForeground,
        LocaleChanged,
        SystemThemeChanged,
        DisplayOrientation = 0x0151,
        DisplayAdded,
        DisplayRemoved,
        DisplayMoved,
        DisplayDesktopModeChanged,
        DisplayCurrentModeChanged,
        DisplayContentScaleChanged,
        DisplayUsableBoundsChanged,
        DisplayFirst = DisplayOrientation,
        DisplayLast = DisplayUsableBoundsChanged,
        WindowShown = 0x0202,
        WindowHidden,
        WindowExposed,
        WindowMoved,
        WindowResized,
        WindowPixelSizeChanged,
        WindowMetalViewResized,
        WindowMInimized,
        WindowMaximized,
        WindowRestored,
        WindowMouseEnter,
        WindowMouseLeave,
        WindowFocusGained,
        WindowFocusLost,
        WindowCloseRequested,
        WindowHitTest,
        WindowIccProfChanged,
        WindowDisplayChanged,
        WindowDisplayScaleChanged,
        WindowSafeAreaChanged,
        WindowOccluded,
        WindowEnterFullscreen,
        WindowLeaveFullscreen,
        WindowDestroyed,
        WindowHdrStateChanged,
        WindowFirst = WindowShown,
        WindowLast = WindowHdrStateChanged,
        KeyDown = 0x0300,
        KeyUp,
        TextEditing,
        TextInput,
        KeymapChanged,
        KeyboardAdded,
        KeyboardRemoved,
        TextEditingCandidates,
        ScreenKeyboardShown,
        ScreenKeyboardHidden,
        MouseMotion = 0x0400,
        MouseButtonDown,
        MouseButtonUp,
        MouseWheel,
        MouseAdded,
        MouseRemoved,
        JoystickAxisMotion = 0x0600,
        JoystickBallMotion,
        JoystickHatMotion,
        JoystickButtonDown,
        JoystickButtonUp,
        JoystickAdded,
        JoystickRemoved,
        JoystickBatteryUpdated,
        JoystickUpdateComplete,
        GamepadAxisMotion = 0x0650,
        GamepadButtonDown,
        GamepadButtonUp,
        GamepadAdded,
        GamepadRemoved,
        GamepadRemapped,
        GamepadTouchpadDown,
        GamepadTouchpadUp,
        GamepadSensorUpdate,
        GamepadUpdateComplete,
        GamepadSteamHandleUpdated,
        FingerDown = 0x0700,
        FingerUp,
        FingerMotion,
        FingerCanceled,
        PinchBegin = 0x0710,
        PinchUpdate,
        PinchEnd,
        ClipboardUpdate = 0x0900,
        DropFile = 0x1000,
        DropText,
        DropBegin,
        DropComplete,
        DropPosition,
        AudioDeviceAdded = 0x1100,
        AudioDeviceRemoved,
        AudioDeviceFormatChanged,
        SensorUpdate = 0x1200,
        PenProximityIn = 0x1300,
        PenProximityOut,
        PenDown,
        PenUp,
        PenButtonDown,
        PenButtonUp,
        PenMotion,
        PenAxis,
        CameraDeviceAdded = 0x1400,
        CameraDeviceRemoved,
        CameraDeviceApproved,
        CameraDeviceDenied,
        RenderTargetsReset = 0x2000,
        RenderDeviceReset,
        RenderDeviceLost,
        Private0 = 0x4000,
        Private1,
        Private2,
        Private3,
        PollSentinel = 0x7F00,
        User = 0x8000,
        Last = 0xFFFF,
    }

    public enum EventAction
    {
        AddEvent,
        PeekEvent,
        GetEvent,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CommonEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DisplayEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public DisplayID DisplayID;
        public int Data1;
        public int Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct WindowEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public int Data1;
        public int Data2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public KeyboardID Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KeyboardEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public KeyboardID Which;
        public Scancode Scancode;
        public Keycode Key;
        public Keymod Mod;
        public ushort Raw;
        public Bool Down;
        public Bool Repeat;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public byte* Text;
        public int Start;
        public int Length;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TextEditingCandidatesEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public byte** Candidates;
        public int NumCandidates;
        public int SelectedCandidate;
        public Bool Horizontal;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TextInputEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public byte* Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public MouseID Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseMotionEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public MouseID Which;
        public MouseButtonFlags State;
        public float X;
        public float Y;
        public float XRel;
        public float YRel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseButtonEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public MouseID Which;
        public byte Button;
        public Bool Down;
        public byte Clicks;
        private readonly byte _padding;
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MouseWheelEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public MouseID Which;
        public float X;
        public float Y;
        public MouseWheelDirection Direction;
        public float MouseX;
        public float MouseY;
        public int IntegerX;
        public int IntegerY;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyAxisEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Axis;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
        public short Value;
        private readonly ushort _padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBallEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Ball;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
        public short XRel;
        public short YRel;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyHatEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Hat;
        public byte Value;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyButtonEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Button;
        public Bool Down;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct JoyBatteryEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public PowerState State;
        public int Percent;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadAxisEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Axis;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
        public short Value;
        private readonly ushort _padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadButtonEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public byte Button;
        public Bool Down;
        private readonly byte _padding1;
        private readonly byte _padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadTouchpadEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public int Touchpad;
        public int Finger;
        public float X;
        public float Y;
        public float Pressure;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadSensorEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public JoystickID Which;
        public int Sensor;
        public fixed float Data[3];
        public ulong SensorTimestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct AudioDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public AudioDeviceID Which;
        public Bool Recording;
        private readonly byte _padding1;
        private readonly byte _padding2;
        private readonly byte _padding3;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct CameraDeviceEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public CameraID Which;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct RenderEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct TouchFingerEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public TouchID TouchID;
        public FingerID FingerID;
        public float X;
        public float Y;
        public float DX;
        public float DY;
        public float Pressure;
        public WindowID WindowID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PinchFingerEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public float Scale;
        public WindowID WindowID;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PenProximityEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public PenID Which;
        public PenInputFlags PenState;
        public PenDeviceType DeviceType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PenMotionEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public PenID Which;
        public PenInputFlags PenState;
        public float X;
        public float Y;
        public PenDeviceType DeviceType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PenTouchEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public PenID Which;
        public PenInputFlags PenState;
        public float X;
        public float Y;
        public Bool Eraser;
        public Bool Down;
        public PenDeviceType DeviceType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PenButtonEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public PenID Which;
        public PenInputFlags PenState;
        public float X;
        public float Y;
        public byte Button;
        public Bool Down;
        public PenDeviceType DeviceType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct PenAxisEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public PenID Which;
        public PenInputFlags PenState;
        public float X;
        public float Y;
        public PenAxis Axis;
        public float Value;
        public PenDeviceType DeviceType;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct DropEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public float X;
        public float Y;
        public byte* Source;
        public byte* Data;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ClipboardEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public Bool Owner;
        public int NumMimeTypes;
        public byte** MimeTypes;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SensorEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public SensorID Which;
        public fixed float Data[6];
        public ulong SensorTimestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct QuitEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct UserEvent
    {
        public EventType Type;
        private readonly uint _reserved;
        public ulong Timestamp;
        public WindowID WindowID;
        public int Code;
        public void* Data1;
        public void* Data2;
    }

    [StructLayout(LayoutKind.Explicit, Size = 128)]
    public struct Event
    {
        [FieldOffset(0)]
        public EventType Type;

        [FieldOffset(0)]
        public CommonEvent Common;

        [FieldOffset(0)]
        public DisplayEvent Display;

        [FieldOffset(0)]
        public WindowEvent Window;

        [FieldOffset(0)]
        public KeyboardDeviceEvent KDevice;

        [FieldOffset(0)]
        public KeyboardEvent Key;

        [FieldOffset(0)]
        public TextEditingEvent Edit;

        [FieldOffset(0)]
        public TextEditingCandidatesEvent EditCandidates;

        [FieldOffset(0)]
        public TextInputEvent Text;

        [FieldOffset(0)]
        public MouseDeviceEvent MDevice;

        [FieldOffset(0)]
        public MouseMotionEvent Motion;

        [FieldOffset(0)]
        public MouseButtonEvent Button;

        [FieldOffset(0)]
        public MouseWheelEvent Wheel;

        [FieldOffset(0)]
        public JoyDeviceEvent JDevice;

        [FieldOffset(0)]
        public JoyAxisEvent JAxis;

        [FieldOffset(0)]
        public JoyBallEvent JBall;

        [FieldOffset(0)]
        public JoyHatEvent JHat;

        [FieldOffset(0)]
        public JoyButtonEvent JButton;

        [FieldOffset(0)]
        public JoyBatteryEvent JBattery;

        [FieldOffset(0)]
        public GamepadDeviceEvent GDevice;

        [FieldOffset(0)]
        public GamepadAxisEvent GAxis;

        [FieldOffset(0)]
        public GamepadButtonEvent GButton;

        [FieldOffset(0)]
        public GamepadTouchpadEvent GTouchpad;

        [FieldOffset(0)]
        public GamepadSensorEvent GSensor;

        [FieldOffset(0)]
        public AudioDeviceEvent ADevice;

        [FieldOffset(0)]
        public CameraDeviceEvent CDevice;

        [FieldOffset(0)]
        public SensorEvent Sensor;

        [FieldOffset(0)]
        public QuitEvent Quit;

        [FieldOffset(0)]
        public UserEvent User;

        [FieldOffset(0)]
        public TouchFingerEvent TFinger;

        [FieldOffset(0)]
        public PinchFingerEvent Pinch;

        [FieldOffset(0)]
        public PenProximityEvent PProximity;

        [FieldOffset(0)]
        public PenTouchEvent PTouch;

        [FieldOffset(0)]
        public PenMotionEvent PMotion;

        [FieldOffset(0)]
        public PenButtonEvent PButton;

        [FieldOffset(0)]
        public PenAxisEvent PAxis;

        [FieldOffset(0)]
        public RenderEvent Render;

        [FieldOffset(0)]
        public DropEvent Drop;

        [FieldOffset(0)]
        public ClipboardEvent Clipboard;
    }
}
