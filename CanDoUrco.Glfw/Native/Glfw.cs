// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;

namespace CanDoUrco.Glfw.Native;

internal static unsafe partial class Glfw
{
    private const string DllName = "glfw";

    public const int TrueDefine = 1;
    public const int FalseDefine = 0;
    public const int ReleaseDefine = 0;
    public const int PressDefine = 1;
    public const int RepeatDefine = 2;
    public const int HatCenteredDefine = 0;
    public const int HatUpDefine = 1;
    public const int HatRightDefine = 2;
    public const int HatDownDefine = 4;
    public const int HatLeftDefine = 8;
    public const int HatRightUpDefine = HatRightDefine | HatUpDefine;
    public const int HatRightDownDefine = HatRightDefine | HatDownDefine;
    public const int HatLeftUpDefine = HatLeftDefine | HatUpDefine;
    public const int HatLeftDownDefine = HatLeftDefine | HatDownDefine;
    public const int KeyUnknownDefine = -1;
    public const int KeySpaceDefine = 32;
    public const int KeyApostropheDefine = 39;
    public const int KeyCommaDefine = 44;
    public const int KeyMinusDefine = 45;
    public const int KeyPeriodDefine = 46;
    public const int KeySlashDefine = 47;
    public const int Key0Define = 48;
    public const int Key1Define = 49;
    public const int Key2Define = 50;
    public const int Key3Define = 51;
    public const int Key4Define = 52;
    public const int Key5Define = 53;
    public const int Key6Define = 54;
    public const int Key7Define = 55;
    public const int Key8Define = 56;
    public const int Key9Define = 57;
    public const int KeySemicolonDefine = 59;
    public const int KeyEqualDefine = 61;
    public const int KeyADefine = 65;
    public const int KeyBDefine = 66;
    public const int KeyCDefine = 67;
    public const int KeyDDefine = 68;
    public const int KeyEDefine = 69;
    public const int KeyFDefine = 70;
    public const int KeyGDefine = 71;
    public const int KeyHDefine = 72;
    public const int KeyIDefine = 73;
    public const int KeyJDefine = 74;
    public const int KeyKDefine = 75;
    public const int KeyLDefine = 76;
    public const int KeyMDefine = 77;
    public const int KeyNDefine = 78;
    public const int KeyODefine = 79;
    public const int KeyPDefine = 80;
    public const int KeyQDefine = 81;
    public const int KeyRDefine = 82;
    public const int KeySDefine = 83;
    public const int KeyTDefine = 84;
    public const int KeyUDefine = 85;
    public const int KeyVDefine = 86;
    public const int KeyWDefine = 87;
    public const int KeyXDefine = 88;
    public const int KeyYDefine = 89;
    public const int KeyZDefine = 90;
    public const int KeyLeftBracketDefine = 91;
    public const int KeyBackslashDefine = 92;
    public const int KeyRightBracketDefine = 93;
    public const int KeyGraveAccentDefine = 96;
    public const int KeyWorld1Define = 161;
    public const int KeyWorld2Define = 162;
    public const int KeyEscapeDefine = 256;
    public const int KeyEnterDefine = 257;
    public const int KeyTabDefine = 258;
    public const int KeyBackspaceDefine = 259;
    public const int KeyInsertDefine = 260;
    public const int KeyDeleteDefine = 261;
    public const int KeyRightDefine = 262;
    public const int KeyLeftDefine = 263;
    public const int KeyDownDefine = 264;
    public const int KeyUpDefine = 265;
    public const int KeyPageUpDefine = 266;
    public const int KeyPageDownDefine = 267;
    public const int KeyHomeDefine = 268;
    public const int KeyEndDefine = 269;
    public const int KeyCapsLockDefine = 280;
    public const int KeyScrollLockDefine = 281;
    public const int KeyNumLockDefine = 282;
    public const int KeyPrintScreenDefine = 283;
    public const int KeyPauseDefine = 284;
    public const int KeyF1Define = 290;
    public const int KeyF2Define = 291;
    public const int KeyF3Define = 292;
    public const int KeyF4Define = 293;
    public const int KeyF5Define = 294;
    public const int KeyF6Define = 295;
    public const int KeyF7Define = 296;
    public const int KeyF8Define = 297;
    public const int KeyF9Define = 298;
    public const int KeyF10Define = 299;
    public const int KeyF11Define = 300;
    public const int KeyF12Define = 301;
    public const int KeyF13Define = 302;
    public const int KeyF14Define = 303;
    public const int KeyF15Define = 304;
    public const int KeyF16Define = 305;
    public const int KeyF17Define = 306;
    public const int KeyF18Define = 307;
    public const int KeyF19Define = 308;
    public const int KeyF20Define = 309;
    public const int KeyF21Define = 310;
    public const int KeyF22Define = 311;
    public const int KeyF23Define = 312;
    public const int KeyF24Define = 313;
    public const int KeyF25Define = 314;
    public const int KeyKeyPad0Define = 320;
    public const int KeyKeyPad1Define = 321;
    public const int KeyKeyPad2Define = 322;
    public const int KeyKeyPad3Define = 323;
    public const int KeyKeyPad4Define = 324;
    public const int KeyKeyPad5Define = 325;
    public const int KeyKeyPad6Define = 326;
    public const int KeyKeyPad7Define = 327;
    public const int KeyKeyPad8Define = 328;
    public const int KeyKeyPad9Define = 329;
    public const int KeyKeyPadDecimalDefine = 330;
    public const int KeyKeyPadDivideDefine = 331;
    public const int KeyKeyPadMultiplyDefine = 332;
    public const int KeyKeyPadSubtractDefine = 333;
    public const int KeyKeyPadAddDefine = 334;
    public const int KeyKeyPadEnterDefine = 335;
    public const int KeyKeyPadEqualDefine = 336;
    public const int KeyLeftShiftDefine = 340;
    public const int KeyLeftControlDefine = 341;
    public const int KeyLeftAltDefine = 342;
    public const int KeyLeftSuperDefine = 343;
    public const int KeyRightShiftDefine = 344;
    public const int KeyRightControlDefine = 345;
    public const int KeyRightAltDefine = 346;
    public const int KeyRightSuperDefine = 347;
    public const int KeyMenuDefine = 348;
    public const int ModSHiftDefine = 0x0001;
    public const int ModControlDefine = 0x0002;
    public const int ModAltDefine = 0x0004;
    public const int ModSuperDefine = 0x0008;
    public const int ModCapsLockDefine = 0x0010;
    public const int ModNumLockDefine = 0x0020;
    public const int MouseButton1Define = 0;
    public const int MouseButton2Define = 1;
    public const int MouseButton3Define = 2;
    public const int MouseButton4Define = 3;
    public const int MouseButton5Define = 4;
    public const int MouseButton6Define = 5;
    public const int MouseButton7Define = 6;
    public const int MouseButton8Define = 7;
    public const int MouseButtonLeftDefine = MouseButton1Define;
    public const int MouseButtonRightDefine = MouseButton2Define;
    public const int MouseButtonMiddleDefine = MouseButton3Define;
    public const int Joystick1Define = 0;
    public const int Joystick2Define = 1;
    public const int Joystick3Define = 2;
    public const int Joystick4Define = 3;
    public const int Joystick5Define = 4;
    public const int Joystick6Define = 5;
    public const int Joystick7Define = 6;
    public const int Joystick8Define = 7;
    public const int Joystick9Define = 8;
    public const int Joystick10Define = 9;
    public const int Joystick11Define = 10;
    public const int Joystick12Define = 11;
    public const int Joystick13Define = 12;
    public const int Joystick14Define = 13;
    public const int Joystick15Define = 14;
    public const int Joystick16Define = 15;
    public const int GamepadButtonADefine = 0;
    public const int GamepadButtonBDefine = 1;
    public const int GamepadButtonXDefine = 2;
    public const int GamepadButtonYDefine = 3;
    public const int GamepadButtonLeftBumperDefine = 4;
    public const int GamepadButtonRightBumperDefine = 5;
    public const int GamepadButtonBackDefine = 6;
    public const int GamepadButtonStartDefine = 7;
    public const int GamepadButtonGuideDefine = 8;
    public const int GamepadButtonLeftThumbDefine = 9;
    public const int GamepadButtonRightThumbDefine = 10;
    public const int GamepadButtonDpadUpDefine = 11;
    public const int GamepadButtonDpadRightDefine = 12;
    public const int GamepadButtonDpadDownDefine = 13;
    public const int GamepadButtonDpadLeftDefine = 14;
    public const int GamepadButtonCrossDefine = GamepadButtonADefine;
    public const int GamepadButtonCircleDefine = GamepadButtonBDefine;
    public const int GamepadButtonSquareDefine = GamepadButtonXDefine;
    public const int GamepadButtonTriangleDefine = GamepadButtonYDefine;
    public const int GamepadAxisLeftXDefine = 0;
    public const int GamepadAxisLeftYDefine = 1;
    public const int GamepadAxisRightXDefine = 2;
    public const int GamepadAxisRightYDefine = 3;
    public const int GamepadAxisLeftTriggerDefine = 4;
    public const int GamepadAxisRightTriggerDefine = 5;
    public const int NoErrorDefine = 0;
    public const int NotInitializedDefine = 0x00010001;
    public const int NoCurrentContextDefine = 0x00010002;
    public const int InvalidEnumDefine = 0x00010003;
    public const int InvalidValueDefine = 0x00010004;
    public const int OutOfMemoryDefine = 0x00010005;
    public const int ApiUnavailableDefine = 0x00010006;
    public const int VersionUnavailableDefine = 0x00010007;
    public const int PlatformErrorDefine = 0x00010008;
    public const int FormatUnavailableDefine = 0x00010009;
    public const int NoWindowContextDefine = 0x0001000A;
    public const int CursorUnavailableDefine = 0x0001000B;
    public const int FeatureUnavailableDefine = 0x0001000C;
    public const int FeatureUnimplementedDefine = 0x0001000D;
    public const int PlatformUnavailableDefine = 0x0001000E;
    public const int FocusedDefine = 0x00020001;
    public const int IconifiedDefine = 0x00020002;
    public const int ResizableDefine = 0x00020003;
    public const int VisibleDefine = 0x00020004;
    public const int DecoratedDefine = 0x00020005;
    public const int AutoIconifyDefine = 0x00020006;
    public const int FloatingDefine = 0x00020007;
    public const int MaximizedDefine = 0x00020008;
    public const int CenterCursorDefine = 0x00020009;
    public const int TransparentFramebufferDefine = 0x0002000A;
    public const int HoveredDefine = 0x0002000B;
    public const int FocusOnShowDefine = 0x0002000C;
    public const int MousePassthroughDefine = 0x0002000D;
    public const int PositionXDefine = 0x0002000E;
    public const int PositionYDefine = 0x0002000F;
    public const int RedBitsDefine = 0x00021001;
    public const int GreenBitsDefine = 0x00021002;
    public const int BlueBitsDefine = 0x00021003;
    public const int AlphaBitsDefine = 0x00021004;
    public const int DepthBitsDefine = 0x00021005;
    public const int StencilBitsDefine = 0x00021006;
    public const int AccumRedBitsDefine = 0x00021007;
    public const int AccumGreenBitsDefine = 0x00021008;
    public const int AccumBlueBitsDefine = 0x00021009;
    public const int AccumAlphaBitsDefine = 0x0002100A;
    public const int AuxBuffersDefine = 0x0002100B;
    public const int StereoDefine = 0x0002100C;
    public const int SamplesDefine = 0x0002100D;
    public const int SrgbCapableDefine = 0x0002100E;
    public const int RefreshRateDefine = 0x0002100F;
    public const int DoublebufferDefine = 0x00021010;
    public const int ClientApiDefine = 0x00022001;
    public const int ContextVersionMajorDefine = 0x00022002;
    public const int ContextVersionMinorDefine = 0x00022003;
    public const int ContextRevisionDefine = 0x00022004;
    public const int ContextRobustnessDefine = 0x00022005;
    public const int OpenGlFOrwardCompatDefine = 0x00022006;
    public const int ContextDebugDefine = 0x00022007;
    public const int OpenGlDebugContextDefine = ContextDebugDefine;
    public const int OpenGlProfileDefine = 0x00022008;
    public const int ContextReleaseBehaviorDefine = 0x00022009;
    public const int ContextNoErrorDefine = 0x0002200A;
    public const int ContextCreationApiDefine = 0x0002200B;
    public const int ScaleToMonitorDefine = 0x0002200C;
    public const int ScaleFramebufferDefine = 0x0002200D;
    public const int CocoaRetinaFramebufferDefine = 0x00023001;
    public const int CocoaFrameNameDefine = 0x00023002;
    public const int CocoaGraphicsSwitchingDefine = 0x00023003;
    public const int X11ClasNameDefine = 0x00024001;
    public const int X11InstanceNameDefine = 0x00024002;
    public const int Win32KeyboardMenuDefine = 0x00025001;
    public const int Win32ShowDefaultDefine = 0x00025002;
    public const int WaylandAppIdDefine = 0x00026001;
    public const int NoApiDefine = 0;
    public const int OpenGlApiDefine = 0x00030001;
    public const int OpenGlEsApiDefine = 0x00030002;
    public const int NoRobustnessDefine = 0;
    public const int NoResetNotificationDefine = 0x00031001;
    public const int LoseContextOnResetDefine = 0x00031002;
    public const int OpenGlAnyProfileDefine = 0;
    public const int OpenGlCoreProfileDefine = 0x00032001;
    public const int OpenGlCompatProfileDefine = 0x00032002;
    public const int CursorDefine = 0x00033001;
    public const int StickyKeysDefine = 0x00033002;
    public const int StickyMouseButtonsDefine = 0x00033003;
    public const int LockKeyModsDefine = 0x00033004;
    public const int RawMouseMotionDefine = 0x00033005;
    public const int UnlimitedMouseButtonsDefine = 0x00033006;
    public const int CursorNormalDefine = 0x00034001;
    public const int CursorHiddenDefine = 0x00034002;
    public const int CursorDisabledDefine = 0x00034003;
    public const int CursorCapturedDefine = 0x00034004;
    public const int AnyReleaseBehaviorDefine = 0x00035001;
    public const int ReleaseBehaviorFlushDefine = 0x00035001;
    public const int ReleaseBehaviorNoneDefine = 0x00035002;
    public const int NativeContextApiDefine = 0x00036001;
    public const int EglContextApiDefine = 0x00036002;
    public const int OsMesaContextApiDefine = 0x00036003;
    public const int AnglePlatformTypeNoneDefine = 0x00037001;
    public const int AnglePlatformTypeOpenGlDefine = 0x00037002;
    public const int AnglePlatformTypeOpenGlEsDefine = 0x00037003;
    public const int AnglePlatformTypeD3D9Define = 0x00037004;
    public const int AnglePlatformTypeD3D11Define = 0x00037005;
    public const int AnglePlatformTypeVulkanDefine = 0x00037007;
    public const int AnglePlatformTypeMetalDefine = 0x00037008;
    public const int WaylandPreferLibDecorDefine = 0x00038001;
    public const int WaylandDisableLibDecorDefine = 0x00038002;
    public const int AnyPositionDefine = unchecked((int)0x80000000);
    public const int ArrowCursorDefine = 0x00036001;
    public const int IBeamCursorDefine = 0x00036002;
    public const int CrosshairCursorDefine = 0x00036003;
    public const int PointingHandCursorDefine = 0x00036004;
    public const int ResizeEastWestCursorDefine = 0x00036005;
    public const int ResizeNorthSouthCursorDefine = 0x00036006;
    public const int ResizeNorthWestSouthEastCursorDefine = 0x00036007;
    public const int ResizeNorthEastSouthWestCursorDefine = 0x00036008;
    public const int ResizeAllCursorDefine = 0x00036009;
    public const int NotAllowedCursorDefine = 0x0003600A;
    public const int HorizontalResizeCursortDefine = ResizeEastWestCursorDefine;
    public const int VerticalResizeCursortDefine = ResizeNorthSouthCursorDefine;
    public const int HandCursorDefine = PointingHandCursorDefine;
    public const int ConnectedDefine = 0x00040001;
    public const int DisconnectedDefine = 0x00040002;
    public const int JoystickHatButtonsDefine = 0x00050001;
    public const int AnglePlatformTypeDefine = 0x00050002;
    public const int PlatfromDefine = 0x00050003;
    public const int CocoaChDirResourcesDefine = 0x00051001;
    public const int CocoaMenuBarDefine = 0x00051002;
    public const int X11XcbVulkanSurfaceDefine = 0x00052001;
    public const int WaylandLibDecorDefine = 0x00053001;
    public const int AnyPlatformDefine = 0x00060000;
    public const int PlatformWin32Define = 0x00060001;
    public const int PlatformCocoaDefine = 0x00060002;
    public const int PlatformWaylandDefine = 0x00060003;
    public const int PlatformX11Define = 0x00060004;
    public const int PlatformNullDefine = 0x00060005;
    public const int DontCareDefine = -1;

    static Glfw() =>
        NativeLibrary.SetDllImportResolver(Assembly.GetExecutingAssembly(), ResolveGlfwLibrary);

    [LibraryImport(DllName, EntryPoint = "glfwInit")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool Init();

    [LibraryImport(DllName, EntryPoint = "glfwTerminate")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void Terminate();

    [LibraryImport(DllName, EntryPoint = "glfwInitHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitHint(int hint, int value);

    [LibraryImport(DllName, EntryPoint = "glfwInitAllocator")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void InitAllocator(in Allocator allocator);

    // TODO: Add the glfwInitVulkanLoader if Vulkan is ever added to CanDoUrco.

    [LibraryImport(DllName, EntryPoint = "glfwGetVersion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetVersion(out int major, out int minor, out int rev);

    [LibraryImport(DllName, EntryPoint = "glfwGetVersionString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetVersionString();

    [LibraryImport(DllName, EntryPoint = "glfwGetError")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetError(out byte* error);

    // WARNING: glfwSetErrorCallback intentionally not included

    [LibraryImport(DllName, EntryPoint = "glfwGetPlatform")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetPlatform();

    [LibraryImport(DllName, EntryPoint = "glfwPlatformSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool PlatformSupported(int platform);

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitors")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor** GetMonitors(out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetPrimaryMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor* GetPrimaryMonitor();

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorPosition(Monitor* monitor, out int xPos, out int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorWorkarea")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorWorkArea(
        Monitor* monitor,
        out int xPos,
        out int yPos,
        out int width,
        out int height
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorPhysicalSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorPhysicalSize(
        Monitor* monitor,
        out int widthMillimeters,
        out int heightMillimeters
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetMonitorContentScale(
        Monitor* monitor,
        out float xScale,
        out float yScale
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetMonitorName(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetMonitorUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetMonitorUserPointer(Monitor* monitor, void* pointer);

    [LibraryImport(DllName, EntryPoint = "glfwGetMonitorUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* GetMonitorUserPointer(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetMonitorCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Monitor*, int, void> SetMonitorCallback(
        delegate* unmanaged[Cdecl]<Monitor*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetVideoModes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial VidMode* GetVideoModes(Monitor* monitor, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetVideoMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial VidMode* GetVideoMode(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetGamma")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGamma(Monitor* monitor, float gamma);

    [LibraryImport(DllName, EntryPoint = "glfwGetGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial GammaRamp* GetGammaRamp(Monitor* monitor);

    [LibraryImport(DllName, EntryPoint = "glfwSetGammaRamp")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetGammaRamp(Monitor* monitor, in GammaRamp ramp);

    [LibraryImport(DllName, EntryPoint = "glfwDefaultWindowHints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DefaultWindowHints();

    [LibraryImport(DllName, EntryPoint = "glfwWindowHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WindowHint(int hint, int value);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwWindowHintString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WindowHintString(int hint, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwCreateWindow",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* CreateWindow(
        int width,
        int height,
        string? title,
        Monitor* monitor,
        Window* share
    );

    [LibraryImport(DllName, EntryPoint = "glfwDestroyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwWindowShouldClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool WindowShouldClose(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowShouldClose")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowShouldClose(
        Window* window,
        [MarshalAs(UnmanagedType.I4)] int value
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowTitle")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetWindowTitle(Window* window);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwSetWindowTitle",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowTitle(Window* window, string? title);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowIcon(
        Window* window,
        int count,
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Image, Image>), CountElementName = nameof(count))]
            Image[] images
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowPos(Window* window, out int xPos, out int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowPos(Window* window, int xPos, int yPos);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowSize(Window* window, out int width, out int height);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeLimits")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowSizeLimits(
        Window* window,
        int minWidth,
        int minHeight,
        int maxWidth,
        int maxHeight
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowAspectRatio")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowAspectRatio(Window* window, int numer, int denom);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowSize(Window* window, int width, int height);

    [LibraryImport(DllName, EntryPoint = "glfwGetFramebufferSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetFramebufferSize(Window* window, out int width, out int height);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowFrameSize")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowFrameSize(
        Window* window,
        out int left,
        out int top,
        out int right,
        out int bottom
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowContentScale")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetWindowContentScale(
        Window* window,
        out float xScale,
        out float yScale
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float GetWindowOpacity(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowOpacity")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowOpacity(Window* window, float opacity);

    [LibraryImport(DllName, EntryPoint = "glfwIconifyWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void IconifyWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwRestoreWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RestoreWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwMaximizeWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MaximizeWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwShowWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ShowWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwHideWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void HideWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwFocusWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void FocusWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwRequestWindowAttentionWindow")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RequestWindowAttentionWindow(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Monitor* GetWindowMonitor(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowMonitor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowMonitor(
        Window* window,
        Monitor* monitor,
        int xPos,
        int yPos,
        int width,
        int height,
        int refreshRate
    );

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowAttrib")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetWindowAttrib(Window* window, int attrib);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowAttrib")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowAttrib(Window* window, int attrib, int value);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetWindowUserPointer(Window* window, void* pointer);

    [LibraryImport(DllName, EntryPoint = "glfwGetWindowUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* GetWindowUserPointer(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowPosCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, int, void> SetWindowPosCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowSizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, int, void> SetWindowSizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowCloseCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, void> SetWindowCloseCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowRefreshCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, void> SetWindowRefreshCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowFocusCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowFocusCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowIconifyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowIconifyCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowMaximizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetWindowMaximizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowFramebufferSizeCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        void> SetWindowFramebufferSizeCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetWindowContentScaleCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        float,
        float,
        void> SetWindowContentScaleCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, float, float, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwPollEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PollEvents();

    [LibraryImport(DllName, EntryPoint = "glfwWaitEvents")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitEvents();

    [LibraryImport(DllName, EntryPoint = "glfwWaitEventsTimeout")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void WaitEventsTimeout(double timeout);

    [LibraryImport(DllName, EntryPoint = "glfwPostEmptyEvent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void PostEmptyEvent();

    [LibraryImport(DllName, EntryPoint = "glfwGetInputMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetInputMode(Window* window, int mode);

    [LibraryImport(DllName, EntryPoint = "glfwSetInputMode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetInputMode(Window* window, int mode, int value);

    [LibraryImport(DllName, EntryPoint = "glfwRawMouseMotionSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool RawMouseMotionSupported();

    [LibraryImport(DllName, EntryPoint = "glfwGetKeyName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetKeyName(int key, int scancode);

    [LibraryImport(DllName, EntryPoint = "glfwGetKeyScancode")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetKeyScancode(int key);

    [LibraryImport(DllName, EntryPoint = "glfwGetKey")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetKey(Window* window, int key);

    [LibraryImport(DllName, EntryPoint = "glfwGetMouseButton")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial int GetMouseButton(Window* window, int button);

    [LibraryImport(DllName, EntryPoint = "glfwGetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void GetCursorPosition(Window* window, out double xPos, out double yPos);

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorPos")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCursorPos(Window* window, double xPos, double yPos);

    [LibraryImport(DllName, EntryPoint = "glfwCreateCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Cursor* CreateCursor(in Image image, int xHot, int yHot);

    [LibraryImport(DllName, EntryPoint = "glfwCreateStandardCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Cursor* CreateStandardCursor(int shape);

    [LibraryImport(DllName, EntryPoint = "glfwDestroyCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void DestroyCursor(Cursor* cursor);

    [LibraryImport(DllName, EntryPoint = "glfwSetCursor")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetCursor(Window* window, Cursor* cursor);

    [LibraryImport(DllName, EntryPoint = "glfwSetkeyCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        int,
        int,
        void> SetKeyCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCharCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, uint, void> SetCharCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, uint, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCharModsCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, uint, int, void> SetCharModsCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, uint, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetMouseButtonCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        int,
        int,
        int,
        void> SetMouseButtonCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, int, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorPosCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        double,
        double,
        void> SetCursorPosCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, double, double, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetCursorEnterCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, void> SetCursorEnterCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetScrollCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<
        Window*,
        double,
        double,
        void> SetScrollCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, double, double, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwSetDropCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<Window*, int, byte**, void> SetDropCallback(
        Window* window,
        delegate* unmanaged[Cdecl]<Window*, int, byte**, void> callback
    );

    [LibraryImport(DllName, EntryPoint = "glfwJoystickPresent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool JoystickPresent(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickAxes")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial float* GetJoystickAxes(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickButtons")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickButtons(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickHats")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickHats(int jid, out int count);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickName(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickGUID")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetJoystickGuid(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwSetJoystickUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetJoystickUserPointer(int jid, void* pointer);

    [LibraryImport(DllName, EntryPoint = "glfwGetJoystickUserPointer")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void* GetJoystickUserPointer(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwJoystickIsGamepad")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool JoystickIsGamepad(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwSetJoystickCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial delegate* unmanaged[Cdecl]<int, int, void> SetJoystickCallback(
        delegate* unmanaged[Cdecl]<int, int, void> callback
    );

    [LibraryImport(
        DllName,
        EntryPoint = "glfwUpdateGamepadMappings",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool UpdateGamepadMappings(string? @string);

    [LibraryImport(DllName, EntryPoint = "glfwGetGamepadName")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetGamepadName(int jid);

    [LibraryImport(DllName, EntryPoint = "glfwGetGamepadState")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool GetGamepadState(int jid, out GamepadState state);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwSetClipboardString",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetClipboardString(Window* window, string? @string);

    [LibraryImport(DllName, EntryPoint = "glfwGetClipboardString")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte* GetClipboardString(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial double GetTime();

    [LibraryImport(DllName, EntryPoint = "SetTime")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SetTime(double time);

    [LibraryImport(DllName, EntryPoint = "glfwGetTimerValue")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTimerValue();

    [LibraryImport(DllName, EntryPoint = "glfwGetTimerFrequency")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial ulong GetTimerFrequency();

    [LibraryImport(DllName, EntryPoint = "glfwMakeContextCurrent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void MakeContextCurrent(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwGetCurrentContext")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial Window* GetCurrentContext();

    [LibraryImport(DllName, EntryPoint = "glfwSwapBuffers")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SwapBuffers(Window* window);

    [LibraryImport(DllName, EntryPoint = "glfwSwapInterval")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void SwapInterval(int interval);

    [LibraryImport(
        DllName,
        EntryPoint = "glfwExtensionSupported",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool ExtensionSupported(string extension);

    // WARNING: glfwGetProcAddress intentionally unimplemented

    [LibraryImport(DllName, EntryPoint = "glfwVulkanSupported")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I4)]
    public static partial bool VulkanSupported();

    [LibraryImport(DllName, EntryPoint = "glfwGetRequiredInstanceExtensions")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial byte** GetRequiredInstanceExtensions(out uint count);

    // WARNING: glfwGetInstanceProcAddress intentionally unimplemented

    // TODO: Implement glfwGetPhysicalDevicePresentationSupport if Vulkan support is added to CanDoUrco

    // TODO: Implement glfwCreateWindowSurface if Vulkan support is added to CanDoUrco

    private static nint ResolveGlfwLibrary(
        string libraryName,
        Assembly assembly,
        DllImportSearchPath? searchPath
    )
    {
        if (libraryName != DllName)
        {
            return 0;
        }

        if (OperatingSystem.IsWindows())
        {
            if (NativeLibrary.TryLoad("glfw3.dll", assembly, searchPath, out var handle))
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsMacOS())
        {
            if (NativeLibrary.TryLoad("libglfw.3.dylib", assembly, searchPath, out var handle))
            {
                return handle;
            }

            if (NativeLibrary.TryLoad("libglfw.dylib", assembly, searchPath, out handle))
            {
                return handle;
            }
        }
        else if (OperatingSystem.IsLinux())
        {
            if (NativeLibrary.TryLoad("libglfw.so.3", assembly, searchPath, out var handle))
            {
                return handle;
            }

            if (NativeLibrary.TryLoad("libglfw.so", assembly, searchPath, out handle))
            {
                return handle;
            }
        }

        return 0;
    }

    public struct Monitor;

    public struct Window;

    public struct Cursor;

    [StructLayout(LayoutKind.Sequential)]
    public struct VidMode
    {
        public int Width;
        public int Height;
        public int RedBits;
        public int GreenBits;
        public int BlueBits;
        public int RefreshRate;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GammaRamp
    {
        public ushort* Red;
        public ushort* Green;
        public ushort* Blue;
        public uint Size;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Image
    {
        public int Width;
        public int Height;
        public byte* Pixels;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct GamepadState
    {
        public fixed byte Buttons[15];
        public fixed float Axes[6];
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Allocator
    {
        public delegate* unmanaged<nuint, void*, void*> Allocate;
        public delegate* unmanaged<void*, nuint, void*, void*> Reallocate;
        public delegate* unmanaged<void*, void*, void> Deallocate;
        public void* User;
    }
}
