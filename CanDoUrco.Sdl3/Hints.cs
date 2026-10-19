// CanDoUrco.Sdl3 - SDL3 bindings for Can Do Urco
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

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Sdl3.CustomMarshallers;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    public const string HintAllowAltTabWhileGrabbed = "SDL_ALLOW_ALT_TAB_WHILE_GRABBED";
    public const string HintAndroidAllowRecreateActivity = "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY";
    public const string HintAndroidBlockOnPause = "SDL_ANDROID_BLOCK_ON_PAUSE";
    public const string HintAndroidLowLatencyAudio = "SDL_ANDROID_LOW_LATENCY_AUDIO";
    public const string HintAndroidAaudioInputPreset = "SDL_ANDROID_AAUDIO_INPUT_PRESET";
    public const string HintAndroidTrapBackButton = "SDL_ANDROID_TRAP_BACK_BUTTON";
    public const string HintAppID = "SDL_APP_ID";
    public const string HintAppName = "SDL_APP_NAME";
    public const string HintAppleTVControllerUIEvents = "SDL_APPLE_TV_CONTROLLER_UI_EVENTS";
    public const string HintAppleTVRemoteAllowRotation = "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION";
    public const string HintAudioAlsaDefaultDevice = "SDL_AUDIO_ALSA_DEFAULT_DEVICE";
    public const string HintAudioAlsaDefaultPlaybackDevice =
        "SDL_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE";

    public const string HintAudioAlsaDefaultRecordingDevice =
        "SDL_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE";

    public const string HintAudioCategory = "SDL_AUDIO_CATEGORY";
    public const string HintAudioChannels = "SDL_AUDIO_CHANNELS";
    public const string HintAudioDeviceAppIconName = "SDL_AUDIO_DEVICE_APP_ICON_NAME";
    public const string HintAudioDeviceSampleFrames = "SDL_AUDIO_DEVICE_SAMPLE_FRAMES";
    public const string HintAudioDeviceStreamName = "SDL_AUDIO_DEVICE_STREAM_NAME";
    public const string HintAudioDeviceStreamRole = "SDL_AUDIO_DEVICE_STREAM_ROLE";
    public const string HintAudioDeviceRawStream = "SDL_AUDIO_DEVICE_RAW_STREAM";
    public const string HintAudioDiskInputFile = "SDL_AUDIO_DISK_INPUT_FILE";
    public const string HintAudioDiskOutputFile = "SDL_AUDIO_DISK_OUTPUT_FILE";
    public const string HintAudioDiskTimescale = "SDL_AUDIO_DISK_TIMESCALE";
    public const string HintAudioDriver = "SDL_AUDIO_DRIVER";
    public const string HintAudioDummyTimescale = "SDL_AUDIO_DUMMY_TIMESCALE";
    public const string HintAudioFormat = "SDL_AUDIO_FORMAT";
    public const string HintAudioFrequency = "SDL_AUDIO_FREQUENCY";
    public const string HintAudioIncludeMonitors = "SDL_AUDIO_INCLUDE_MONITORS";
    public const string HintAutoUpdateJoysticks = "SDL_AUTO_UPDATE_JOYSTICKS";
    public const string HintAutoUpdateSensors = "SDL_AUTO_UPDATE_SENSORS";
    public const string HintBmpSaveLegacyFormat = "SDL_BMP_SAVE_LEGACY_FORMAT";
    public const string HintCameraDriver = "SDL_CAMERA_DRIVER";
    public const string HintCpuFeatureMask = "SDL_CPU_FEATURE_MASK";
    public const string HintJoystickDirectinput = "SDL_JOYSTICK_DIRECTINPUT";
    public const string HintFileDialogDriver = "SDL_FILE_DIALOG_DRIVER";
    public const string HintDisplayUsableBounds = "SDL_DISPLAY_USABLE_BOUNDS";
    public const string HintInvalidParamChecks = "SDL_INVALID_PARAM_CHECKS";
    public const string HintEmscriptenAsyncify = "SDL_EMSCRIPTEN_ASYNCIFY";
    public const string HintEmscriptenCanvasSelector = "SDL_EMSCRIPTEN_CANVAS_SELECTOR";
    public const string HintEmscriptenKeyboardElement = "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT";
    public const string HintEnableScreenKeyboard = "SDL_ENABLE_SCREEN_KEYBOARD";
    public const string HintEnableSteamScreenKeyboard = "SDL_ENABLE_STEAM_SCREEN_KEYBOARD";
    public const string HintEvdevDevices = "SDL_EVDEV_DEVICES";
    public const string HintEventLogging = "SDL_EVENT_LOGGING";
    public const string HintForceRaisewindow = "SDL_FORCE_RAISEWINDOW";
    public const string HintFramebufferAcceleration = "SDL_FRAMEBUFFER_ACCELERATION";
    public const string HintGamecontrollerconfig = "SDL_GAMECONTROLLERCONFIG";
    public const string HintGamecontrollerconfigFile = "SDL_GAMECONTROLLERCONFIG_FILE";
    public const string HintGameControllerType = "SDL_GAMECONTROLLERTYPE";
    public const string HintGameControllerIgnoreDevices = "SDL_GAMECONTROLLER_IGNORE_DEVICES";
    public const string HintGameControllerIgnoreDevicesExcept =
        "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT";

    public const string HintGameControllerSensorFusion = "SDL_GAMECONTROLLER_SENSOR_FUSION";
    public const string HintGdkTextinputDefaultText = "SDL_GDK_TEXTINPUT_DEFAULT_TEXT";
    public const string HintGdkTextinputDescription = "SDL_GDK_TEXTINPUT_DESCRIPTION";
    public const string HintGdkTextinputMaxLength = "SDL_GDK_TEXTINPUT_MAX_LENGTH";
    public const string HintGdkTextinputScope = "SDL_GDK_TEXTINPUT_SCOPE";
    public const string HintGdkTextinputTitle = "SDL_GDK_TEXTINPUT_TITLE";
    public const string HintHidapiLibUsb = "SDL_HIDAPI_LIBUSB";
    public const string HintHidapiLibUsbGamecube = "SDL_HIDAPI_LIBUSB_GAMECUBE";
    public const string HintHidapiLibUsbWhitelist = "SDL_HIDAPI_LIBUSB_WHITELIST";
    public const string HintHidapiUdev = "SDL_HIDAPI_UDEV";
    public const string HintGpuDriver = "SDL_GPU_DRIVER";
    public const string HintHidapiEnumerateOnlyControllers =
        "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS";

    public const string HintHidapiIgnoreDevices = "SDL_HIDAPI_IGNORE_DEVICES";
    public const string HintImeImplementedUI = "SDL_IME_IMPLEMENTED_UI";
    public const string HintIOsHideHomeIndicator = "SDL_IOS_HIDE_HOME_INDICATOR";
    public const string HintJoystickAllowBackgroundEvents = "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS";
    public const string HintJoystickArcadestickDevices = "SDL_JOYSTICK_ARCADESTICK_DEVICES";
    public const string HintJoystickArcadestickDevicesExcluded =
        "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED";

    public const string HintJoystickBlacklistDevices = "SDL_JOYSTICK_BLACKLIST_DEVICES";
    public const string HintJoystickBlacklistDevicesExcluded =
        "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED";

    public const string HintJoystickDevice = "SDL_JOYSTICK_DEVICE";
    public const string HintJoystickEnhancedReports = "SDL_JOYSTICK_ENHANCED_REPORTS";
    public const string HintJoystickFlightstickDevices = "SDL_JOYSTICK_FLIGHTSTICK_DEVICES";
    public const string HintJoystickFlightstickDevicesExcluded =
        "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED";

    public const string HintJoystickGameinput = "SDL_JOYSTICK_GAMEINPUT";
    public const string HintJoystickGameinputRaw = "SDL_JOYSTICK_GAMEINPUT_RAW";
    public const string HintJoystickGamecubeDevices = "SDL_JOYSTICK_GAMECUBE_DEVICES";
    public const string HintJoystickGamecubeDevicesExcluded =
        "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED";

    public const string HintJoystickHidApi = "SDL_JOYSTICK_HIDAPI";
    public const string HintJoystickHidApiCombineJoyCons = "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS";
    public const string HintJoystickHidApiGamecube = "SDL_JOYSTICK_HIDAPI_GAMECUBE";
    public const string HintJoystickHidApiGamecubeRumbleBrake =
        "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE";

    public const string HintJoystickHidApiJoyCons = "SDL_JOYSTICK_HIDAPI_JOY_CONS";
    public const string HintJoystickHidApiJoyconHomeLed = "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED";
    public const string HintJoystickHidApiLuna = "SDL_JOYSTICK_HIDAPI_LUNA";
    public const string HintJoystickHidApiNintendoClassic = "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC";
    public const string HintJoystickHidApiPs3 = "SDL_JOYSTICK_HIDAPI_PS3";
    public const string HintJoystickHidApiPs3SixaxisDriver =
        "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER";

    public const string HintJoystickHidApiPs4 = "SDL_JOYSTICK_HIDAPI_PS4";
    public const string HintJoystickHidApiPs4ReportInterval =
        "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL";

    public const string HintJoystickHidApiPs5 = "SDL_JOYSTICK_HIDAPI_PS5";
    public const string HintJoystickHidApiPs5PlayerLed = "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED";
    public const string HintJoystickHidApiShield = "SDL_JOYSTICK_HIDAPI_SHIELD";
    public const string HintJoystickHidApiStadia = "SDL_JOYSTICK_HIDAPI_STADIA";
    public const string HintJoystickHidApiSteam = "SDL_JOYSTICK_HIDAPI_STEAM";
    public const string HintJoystickHidApiSteamHomeLed = "SDL_JOYSTICK_HIDAPI_STEAM_HOME_LED";
    public const string HintJoystickHidApiSteamdeck = "SDL_JOYSTICK_HIDAPI_STEAMDECK";
    public const string HintJoystickHidApiSteamHori = "SDL_JOYSTICK_HIDAPI_STEAM_HORI";
    public const string HintJoystickHidApiLg4ff = "SDL_JOYSTICK_HIDAPI_LG4FF";
    public const string HintJoystickHidApi8BitDo = "SDL_JOYSTICK_HIDAPI_8BITDO";
    public const string HintJoystickHidApiSInput = "SDL_JOYSTICK_HIDAPI_SINPUT";
    public const string HintJoystickHidApiZuiki = "SDL_JOYSTICK_HIDAPI_ZUIKI";
    public const string HintJoystickHidApiFlyDigi = "SDL_JOYSTICK_HIDAPI_FLYDIGI";
    public const string HintJoystickHidApiSwitch = "SDL_JOYSTICK_HIDAPI_SWITCH";
    public const string HintJoystickHidApiSwitchHomeLed = "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED";
    public const string HintJoystickHidApiSwitchPlayerLed = "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED";
    public const string HintJoystickHidApiSwitch2 = "SDL_JOYSTICK_HIDAPI_SWITCH2";
    public const string HintJoystickHidApiVerticalJoyCons = "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS";
    public const string HintJoystickHidApiWii = "SDL_JOYSTICK_HIDAPI_WII";
    public const string HintJoystickHidApiWiiPlayerLed = "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED";
    public const string HintJoystickHidApiXBox = "SDL_JOYSTICK_HIDAPI_XBOX";
    public const string HintJoystickHidApiXBox360 = "SDL_JOYSTICK_HIDAPI_XBOX_360";
    public const string HintJoystickHidApiXBox360PlayerLed =
        "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED";

    public const string HintJoystickHidApiXBox360Wireless = "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS";
    public const string HintJoystickHidApiXBoxOne = "SDL_JOYSTICK_HIDAPI_XBOX_ONE";
    public const string HintJoystickHidApiXBoxOneHomeLed = "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED";
    public const string HintJoystickHidApiGip = "SDL_JOYSTICK_HIDAPI_GIP";
    public const string HintJoystickHidApiGipResetForMetadata =
        "SDL_JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA";

    public const string HintJoystickIOKit = "SDL_JOYSTICK_IOKIT";
    public const string HintJoystickLinuxClassic = "SDL_JOYSTICK_LINUX_CLASSIC";
    public const string HintJoystickLinuxDeadZones = "SDL_JOYSTICK_LINUX_DEADZONES";
    public const string HintJoystickLinuxDigitalHats = "SDL_JOYSTICK_LINUX_DIGITAL_HATS";
    public const string HintJoystickLinuxHatDeadZones = "SDL_JOYSTICK_LINUX_HAT_DEADZONES";
    public const string HintJoystickMfi = "SDL_JOYSTICK_MFI";
    public const string HintJoystickRawInput = "SDL_JOYSTICK_RAWINPUT";
    public const string HintJoystickRawInputCorrelateXInput =
        "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT";

    public const string HintJoystickRogChakram = "SDL_JOYSTICK_ROG_CHAKRAM";
    public const string HintJoystickThread = "SDL_JOYSTICK_THREAD";
    public const string HintJoystickThrottleDevices = "SDL_JOYSTICK_THROTTLE_DEVICES";
    public const string HintJoystickThrottleDevicesExcluded =
        "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED";

    public const string HintJoystickWgi = "SDL_JOYSTICK_WGI";
    public const string HintJoystickWheelDevices = "SDL_JOYSTICK_WHEEL_DEVICES";
    public const string HintJoystickWheelDevicesExcluded = "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED";
    public const string HintJoystickZeroCenteredDevices = "SDL_JOYSTICK_ZERO_CENTERED_DEVICES";
    public const string HintJoystickHapticAxes = "SDL_JOYSTICK_HAPTIC_AXES";
    public const string HintKeycodeOptions = "SDL_KEYCODE_OPTIONS";
    public const string HintKmsdrmDeviceIndex = "SDL_KMSDRM_DEVICE_INDEX";
    public const string HintKmsdrmRequireDrmMaster = "SDL_KMSDRM_REQUIRE_DRM_MASTER";
    public const string HintKmsdrmAtomic = "SDL_KMSDRM_ATOMIC";
    public const string HintLogging = "SDL_LOGGING";
    public const string HintMacBackgroundApp = "SDL_MAC_BACKGROUND_APP";
    public const string HintMacCtrlClickEmulateRightClick =
        "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK";

    public const string HintMacOpenglAsyncDispatch = "SDL_MAC_OPENGL_ASYNC_DISPATCH";
    public const string HintMacOptionAsAlt = "SDL_MAC_OPTION_AS_ALT";
    public const string HintMacScrollMomentum = "SDL_MAC_SCROLL_MOMENTUM";
    public const string HintMacPressAndHold = "SDL_MAC_PRESS_AND_HOLD";
    public const string HintMainCallbackRate = "SDL_MAIN_CALLBACK_RATE";
    public const string HintMouseAutoCapture = "SDL_MOUSE_AUTO_CAPTURE";
    public const string HintMouseDoubleClickRadius = "SDL_MOUSE_DOUBLE_CLICK_RADIUS";
    public const string HintMouseDoubleClickTime = "SDL_MOUSE_DOUBLE_CLICK_TIME";
    public const string HintMouseDefaultSystemCursor = "SDL_MOUSE_DEFAULT_SYSTEM_CURSOR";
    public const string HintMouseDpiScaleCursors = "SDL_MOUSE_DPI_SCALE_CURSORS";
    public const string HintMouseEmulateWarpWithRelative = "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE";
    public const string HintMouseFocusClickthrough = "SDL_MOUSE_FOCUS_CLICKTHROUGH";
    public const string HintMouseNormalSpeedScale = "SDL_MOUSE_NORMAL_SPEED_SCALE";
    public const string HintMouseRelativeModeCenter = "SDL_MOUSE_RELATIVE_MODE_CENTER";
    public const string HintMouseRelativeSpeedScale = "SDL_MOUSE_RELATIVE_SPEED_SCALE";
    public const string HintMouseRelativeSystemScale = "SDL_MOUSE_RELATIVE_SYSTEM_SCALE";
    public const string HintMouseRelativeWarpMotion = "SDL_MOUSE_RELATIVE_WARP_MOTION";
    public const string HintMouseRelativeCursorVisible = "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE";
    public const string HintMouseTouchEvents = "SDL_MOUSE_TOUCH_EVENTS";
    public const string HintMuteConsoleKeyboard = "SDL_MUTE_CONSOLE_KEYBOARD";
    public const string HintNoSignalHandlers = "SDL_NO_SIGNAL_HANDLERS";
    public const string HintOpenglLibrary = "SDL_OPENGL_LIBRARY";
    public const string HintEglLibrary = "SDL_EGL_LIBRARY";
    public const string HintOpenGLESDriver = "SDL_OPENGL_ES_DRIVER";
    public const string HintOpenGLForceSrgbFramebuffer = "SDL_OPENGL_FORCE_SRGB_FRAMEBUFFER";
    public const string HintOpenVRLibrary = "SDL_OPENVR_LIBRARY";
    public const string HintOrientations = "SDL_ORIENTATIONS";
    public const string HintPollSentinel = "SDL_POLL_SENTINEL";
    public const string HintPreferredLocales = "SDL_PREFERRED_LOCALES";
    public const string HintQuitOnLastWindowClose = "SDL_QUIT_ON_LAST_WINDOW_CLOSE";
    public const string HintRenderDirect3DThreadsafe = "SDL_RENDER_DIRECT3D_THREADSAFE";
    public const string HintRenderDirect3D11Debug = "SDL_RENDER_DIRECT3D11_DEBUG";
    public const string HintRenderDirect3D11Warp = "SDL_RENDER_DIRECT3D11_WARP";
    public const string HintRenderVulkanDebug = "SDL_RENDER_VULKAN_DEBUG";
    public const string HintRenderGpuDebug = "SDL_RENDER_GPU_DEBUG";
    public const string HintRenderGpuLowPower = "SDL_RENDER_GPU_LOW_POWER";
    public const string HintRenderDriver = "SDL_RENDER_DRIVER";
    public const string HintRenderLineMethod = "SDL_RENDER_LINE_METHOD";
    public const string HintRenderMetalPreferLowPowerDevice =
        "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE";

    public const string HintRenderVSync = "SDL_RENDER_VSYNC";
    public const string HintReturnKeyHidesIme = "SDL_RETURN_KEY_HIDES_IME";
    public const string HintRogGamepadMice = "SDL_ROG_GAMEPAD_MICE";
    public const string HintRogGamepadMiceExcluded = "SDL_ROG_GAMEPAD_MICE_EXCLUDED";
    public const string HintPS2GSWidth = "SDL_PS2_GS_WIDTH";
    public const string HintPS2GSHeight = "SDL_PS2_GS_HEIGHT";
    public const string HintPS2GSProgressive = "SDL_PS2_GS_PROGRESSIVE";
    public const string HintPS2GSMode = "SDL_PS2_GS_MODE";
    public const string HintRpiVideoLayer = "SDL_RPI_VIDEO_LAYER";
    public const string HintScreensaverInhibitActivityName =
        "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME";

    public const string HintShutdownDbusOnQuit = "SDL_SHUTDOWN_DBUS_ON_QUIT";
    public const string HintStorageTitleDriver = "SDL_STORAGE_TITLE_DRIVER";
    public const string HintStorageUserDriver = "SDL_STORAGE_USER_DRIVER";
    public const string HintThreadForceRealtimeTimeCritical =
        "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL";

    public const string HintThreadPriorityPolicy = "SDL_THREAD_PRIORITY_POLICY";
    public const string HintTimerResolution = "SDL_TIMER_RESOLUTION";
    public const string HintTouchMouseEvents = "SDL_TOUCH_MOUSE_EVENTS";
    public const string HintTrackpadIsTouchOnly = "SDL_TRACKPAD_IS_TOUCH_ONLY";
    public const string HintTvRemoteAsJoystick = "SDL_TV_REMOTE_AS_JOYSTICK";
    public const string HintVideoAllowScreensaver = "SDL_VIDEO_ALLOW_SCREENSAVER";
    public const string HintVideoDisplayPriority = "SDL_VIDEO_DISPLAY_PRIORITY";
    public const string HintVideoDoubleBuffer = "SDL_VIDEO_DOUBLE_BUFFER";
    public const string HintVideoDriver = "SDL_VIDEO_DRIVER";
    public const string HintVideoDummySaveFrames = "SDL_VIDEO_DUMMY_SAVE_FRAMES";
    public const string HintVideoEglAllowGetdisplayFallback =
        "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK";

    public const string HintVideoForceEgl = "SDL_VIDEO_FORCE_EGL";
    public const string HintVideoMacFullscreenSpaces = "SDL_VIDEO_MAC_FULLSCREEN_SPACES";
    public const string HintVideoMacFullscreenMenuVisibility =
        "SDL_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY";

    public const string HintVideoMetalAutoResizeDrawable = "SDL_VIDEO_METAL_AUTO_RESIZE_DRAWABLE";
    public const string HintVideoMatchExclusiveModeOnMove =
        "SDL_VIDEO_MATCH_EXCLUSIVE_MODE_ON_MOVE";

    public const string HintVideoMinimizeOnFocusLoss = "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
    public const string HintVideoOffscreenSaveFrames = "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES";
    public const string HintVideoSyncWindowOperations = "SDL_VIDEO_SYNC_WINDOW_OPERATIONS";
    public const string HintVideoWaylandAllowLibDecor = "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR";
    public const string HintVideoWaylandModeEmulation = "SDL_VIDEO_WAYLAND_MODE_EMULATION";
    public const string HintVideoWaylandModeScaling = "SDL_VIDEO_WAYLAND_MODE_SCALING";
    public const string HintVideoWaylandPreferLibDecor = "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR";
    public const string HintVideoWaylandScaleToDisplay = "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY";
    public const string HintVideoWinD3Dcompiler = "SDL_VIDEO_WIN_D3DCOMPILER";
    public const string HintVideoX11EnableXSyncExt = "SDL_VIDEO_X11_ENABLE_XSYNC_EXT";
    public const string HintVideoX11ExternalWindowInput = "SDL_VIDEO_X11_EXTERNAL_WINDOW_INPUT";
    public const string HintVideoX11NetWMBypassCompositor =
        "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR";

    public const string HintVideoX11NetWMPing = "SDL_VIDEO_X11_NET_WM_PING";
    public const string HintVideoX11NoDirectColor = "SDL_VIDEO_X11_NODIRECTCOLOR";
    public const string HintVideoX11ScalingFactor = "SDL_VIDEO_X11_SCALING_FACTOR";
    public const string HintVideoX11VisualID = "SDL_VIDEO_X11_VISUALID";
    public const string HintVideoX11WindowVisualID = "SDL_VIDEO_X11_WINDOW_VISUALID";
    public const string HintVideoX11XRandr = "SDL_VIDEO_X11_XRANDR";
    public const string HintVitaEnableBackTouch = "SDL_VITA_ENABLE_BACK_TOUCH";
    public const string HintVitaEnableFrontTouch = "SDL_VITA_ENABLE_FRONT_TOUCH";
    public const string HintVitaModulePath = "SDL_VITA_MODULE_PATH";
    public const string HintVitaPvrInit = "SDL_VITA_PVR_INIT";
    public const string HintVitaResolution = "SDL_VITA_RESOLUTION";
    public const string HintVitaPvrOpenGL = "SDL_VITA_PVR_OPENGL";
    public const string HintVitaTouchMouseDevice = "SDL_VITA_TOUCH_MOUSE_DEVICE";
    public const string HintVulkanDisplay = "SDL_VULKAN_DISPLAY";
    public const string HintVulkanLibrary = "SDL_VULKAN_LIBRARY";
    public const string HintWaveFactChunk = "SDL_WAVE_FACT_CHUNK";
    public const string HintWaveChunkLimit = "SDL_WAVE_CHUNK_LIMIT";
    public const string HintWaveRiffChunkSize = "SDL_WAVE_RIFF_CHUNK_SIZE";
    public const string HintWaveTruncation = "SDL_WAVE_TRUNCATION";
    public const string HintWindowActivateWhenRaised = "SDL_WINDOW_ACTIVATE_WHEN_RAISED";
    public const string HintWindowActivateWhenShown = "SDL_WINDOW_ACTIVATE_WHEN_SHOWN";
    public const string HintWindowAllowTopmost = "SDL_WINDOW_ALLOW_TOPMOST";
    public const string HintWindowFrameUsableWhileCursorHidden =
        "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN";

    public const string HintWindowsCloseOnAltF4 = "SDL_WINDOWS_CLOSE_ON_ALT_F4";
    public const string HintWindowsEnableMenuMnemonics = "SDL_WINDOWS_ENABLE_MENU_MNEMONICS";
    public const string HintWindowsEnableMessageLoop = "SDL_WINDOWS_ENABLE_MESSAGELOOP";
    public const string HintWindowsGameInput = "SDL_WINDOWS_GAMEINPUT";
    public const string HintWindowsRawKeyboard = "SDL_WINDOWS_RAW_KEYBOARD";
    public const string HintWindowsRawKeyboardExcludeHotkeys =
        "SDL_WINDOWS_RAW_KEYBOARD_EXCLUDE_HOTKEYS";

    public const string HintWindowsRawKeyboardInputSink = "SDL_WINDOWS_RAW_KEYBOARD_INPUTSINK";
    public const string HintWindowsForceSemaphoreKernel = "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL";
    public const string HintWindowsIntresourceIcon = "SDL_WINDOWS_INTRESOURCE_ICON";
    public const string HintWindowsIntresourceIconSmall = "SDL_WINDOWS_INTRESOURCE_ICON_SMALL";
    public const string HintWindowsUseD3D9EX = "SDL_WINDOWS_USE_D3D9EX";
    public const string HintWindowsEraseBackgroundMode = "SDL_WINDOWS_ERASE_BACKGROUND_MODE";
    public const string HintX11ForceOverrideRedirect = "SDL_X11_FORCE_OVERRIDE_REDIRECT";
    public const string HintX11WindowType = "SDL_X11_WINDOW_TYPE";
    public const string HintX11XcbLibrary = "SDL_X11_XCB_LIBRARY";
    public const string HintXInputEnabled = "SDL_XINPUT_ENABLED";
    public const string HintAssert = "SDL_ASSERT";
    public const string HintPenMouseEvents = "SDL_PEN_MOUSE_EVENTS";
    public const string HintPenTouchEvents = "SDL_PEN_TOUCH_EVENTS";

    public enum HintPriority
    {
        Default,
        Normal,
        Override,
    }

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_SetHintWithPriority",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHintWithPriority(
        string name,
        string? value,
        HintPriority priority
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetHint", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool SetHint(string name, string? value);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_ResetHint",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool ResetHint(string name);

    [LibraryImport(DllName, EntryPoint = "SDL_ResetHints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void ResetHints();

    [LibraryImport(DllName, EntryPoint = "SDL_GetHint", StringMarshalling = StringMarshalling.Utf8)]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalUsing(typeof(UnownedUtf8StringMarshaller))]
    public static partial string? GetHint(string name);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_GetHintBoolean",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool GetHintBoolean(
        string name,
        [MarshalAs(UnmanagedType.I1)] bool defaultValue
    );

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate void HintCallback(
        nint userdata,
        [MarshalAs(UnmanagedType.LPStr)] string name,
        [MarshalAs(UnmanagedType.LPStr)] string? oldValue,
        [MarshalAs(UnmanagedType.LPStr)] string? newValue
    );

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_AddHintCallback",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    public static partial bool AddHintCallback(string name, HintCallback callback, nint userdata);

    [LibraryImport(
        DllName,
        EntryPoint = "SDL_RemoveHintCallback",
        StringMarshalling = StringMarshalling.Utf8
    )]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    public static partial void RemoveHintCallback(
        string name,
        HintCallback callback,
        nint userdata
    );
}
