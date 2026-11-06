// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static unsafe partial class Sdl
{
    public static ReadOnlySpan<byte> HintAllowAltTabWhileGrabbed =>
        "SDL_ALLOW_ALT_TAB_WHILE_GRABBED"u8;
    public static ReadOnlySpan<byte> HintAndroidAllowRecreateActivity =>
        "SDL_ANDROID_ALLOW_RECREATE_ACTIVITY"u8;
    public static ReadOnlySpan<byte> HintAndroidBlockOnPause => "SDL_ANDROID_BLOCK_ON_PAUSE"u8;
    public static ReadOnlySpan<byte> HintAndroidLowLatencyAudio =>
        "SDL_ANDROID_LOW_LATENCY_AUDIO"u8;
    public static ReadOnlySpan<byte> HintAndroidAAudioInputPreset =>
        "SDL_ANDROID_AAUDIO_INPUT_PRESET"u8;
    public static ReadOnlySpan<byte> HintAndroidTrapBackButton => "SDL_ANDROID_TRAP_BACK_BUTTON"u8;
    public static ReadOnlySpan<byte> HintAppID => "SDL_APP_ID"u8;
    public static ReadOnlySpan<byte> HintAppName => "SDL_APP_NAME"u8;
    public static ReadOnlySpan<byte> HintAppleTVControllerUIEvents =>
        "SDL_APPLE_TV_CONTROLLER_UI_EVENTS"u8;
    public static ReadOnlySpan<byte> HintAppleTVRemoteAllowRotation =>
        "SDL_APPLE_TV_REMOTE_ALLOW_ROTATION"u8;
    public static ReadOnlySpan<byte> HintAudioAlsaDefaultDevice =>
        "SDL_AUDIO_ALSA_DEFAULT_DEVICE"u8;
    public static ReadOnlySpan<byte> HintAudioAlsaDefaultPlaybackDevice =>
        "SDL_AUDIO_ALSA_DEFAULT_PLAYBACK_DEVICE"u8;
    public static ReadOnlySpan<byte> HintAudioAlsaDefaultRecordingDevice =>
        "SDL_AUDIO_ALSA_DEFAULT_RECORDING_DEVICE"u8;
    public static ReadOnlySpan<byte> HintAudioCategory => "SDL_AUDIO_CATEGORY"u8;
    public static ReadOnlySpan<byte> HintAudioChannels => "SDL_AUDIO_CHANNELS"u8;
    public static ReadOnlySpan<byte> HintAudioDeviceAppIconName =>
        "SDL_AUDIO_DEVICE_APP_ICON_NAME"u8;
    public static ReadOnlySpan<byte> HintAudioDeviceSampleFrames =>
        "SDL_AUDIO_DEVICE_SAMPLE_FRAMES"u8;
    public static ReadOnlySpan<byte> HintAudioDeviceStreamName => "SDL_AUDIO_DEVICE_STREAM_NAME"u8;
    public static ReadOnlySpan<byte> HintAudioDeviceStreamRole => "SDL_AUDIO_DEVICE_STREAM_ROLE"u8;
    public static ReadOnlySpan<byte> HintAudioDeviceRawStream => "SDL_AUDIO_DEVICE_RAW_STREAM"u8;
    public static ReadOnlySpan<byte> HintAudioDiskInputFile => "SDL_AUDIO_DISK_INPUT_FILE"u8;
    public static ReadOnlySpan<byte> HintAudioDiskOutputFile => "SDL_AUDIO_DISK_OUTPUT_FILE"u8;
    public static ReadOnlySpan<byte> HintAudioDiskTimescale => "SDL_AUDIO_DISK_TIMESCALE"u8;
    public static ReadOnlySpan<byte> HintAudioDriver => "SDL_AUDIO_DRIVER"u8;
    public static ReadOnlySpan<byte> HintAudioDummyTimescale => "SDL_AUDIO_DUMMY_TIMESCALE"u8;
    public static ReadOnlySpan<byte> HintAudioFormat => "SDL_AUDIO_FORMAT"u8;
    public static ReadOnlySpan<byte> HintAudioFrequency => "SDL_AUDIO_FREQUENCY"u8;
    public static ReadOnlySpan<byte> HintAudioIncludeMonitors => "SDL_AUDIO_INCLUDE_MONITORS"u8;
    public static ReadOnlySpan<byte> HintAutoUpdateJoysticks => "SDL_AUTO_UPDATE_JOYSTICKS"u8;
    public static ReadOnlySpan<byte> HintAutoUpdateSensors => "SDL_AUTO_UPDATE_SENSORS"u8;
    public static ReadOnlySpan<byte> HintBmpSaveLegacyFormat => "SDL_BMP_SAVE_LEGACY_FORMAT"u8;
    public static ReadOnlySpan<byte> HintCameraDriver => "SDL_CAMERA_DRIVER"u8;
    public static ReadOnlySpan<byte> HintCpuFeatureMask => "SDL_CPU_FEATURE_MASK"u8;
    public static ReadOnlySpan<byte> HintJoystickDirectInput => "SDL_JOYSTICK_DIRECTINPUT"u8;
    public static ReadOnlySpan<byte> HintFileDialogDriver => "SDL_FILE_DIALOG_DRIVER"u8;
    public static ReadOnlySpan<byte> HintDisplayUsableBounds => "SDL_DISPLAY_USABLE_BOUNDS"u8;
    public static ReadOnlySpan<byte> HintInvalidParamChecks => "SDL_INVALID_PARAM_CHECKS"u8;
    public static ReadOnlySpan<byte> HintEmscriptenAsyncify => "SDL_EMSCRIPTEN_ASYNCIFY"u8;
    public static ReadOnlySpan<byte> HintEmscriptenCanvasSelector =>
        "SDL_EMSCRIPTEN_CANVAS_SELECTOR"u8;
    public static ReadOnlySpan<byte> HintEmscriptenKeyboardElement =>
        "SDL_EMSCRIPTEN_KEYBOARD_ELEMENT"u8;
    public static ReadOnlySpan<byte> HintEnableScreenKeyboard => "SDL_ENABLE_SCREEN_KEYBOARD"u8;
    public static ReadOnlySpan<byte> HintEnableSteamScreenKeyboard =>
        "SDL_ENABLE_STEAM_SCREEN_KEYBOARD"u8;
    public static ReadOnlySpan<byte> HintEvdevDevices => "SDL_EVDEV_DEVICES"u8;
    public static ReadOnlySpan<byte> HintEventLogging => "SDL_EVENT_LOGGING"u8;
    public static ReadOnlySpan<byte> HintForceRaiseWindow => "SDL_FORCE_RAISEWINDOW"u8;
    public static ReadOnlySpan<byte> HintFramebufferAcceleration =>
        "SDL_FRAMEBUFFER_ACCELERATION"u8;
    public static ReadOnlySpan<byte> HintGameControllerConfig => "SDL_GAMECONTROLLERCONFIG"u8;
    public static ReadOnlySpan<byte> HintGameControllerConfigFile =>
        "SDL_GAMECONTROLLERCONFIG_FILE"u8;
    public static ReadOnlySpan<byte> HintGameControllerType => "SDL_GAMECONTROLLERTYPE"u8;
    public static ReadOnlySpan<byte> HintGameControllerIgnoreDevices =>
        "SDL_GAMECONTROLLER_IGNORE_DEVICES"u8;
    public static ReadOnlySpan<byte> HintGameControllerIgnoreDevicesExcept =>
        "SDL_GAMECONTROLLER_IGNORE_DEVICES_EXCEPT"u8;
    public static ReadOnlySpan<byte> HintGameControllerSensorFusion =>
        "SDL_GAMECONTROLLER_SENSOR_FUSION"u8;
    public static ReadOnlySpan<byte> HintGdkTextInputDefaultText =>
        "SDL_GDK_TEXTINPUT_DEFAULT_TEXT"u8;
    public static ReadOnlySpan<byte> HintGdkTextInputDescription =>
        "SDL_GDK_TEXTINPUT_DESCRIPTION"u8;
    public static ReadOnlySpan<byte> HintGdkTextInputMaxLength => "SDL_GDK_TEXTINPUT_MAX_LENGTH"u8;
    public static ReadOnlySpan<byte> HintGdkTextInputScope => "SDL_GDK_TEXTINPUT_SCOPE"u8;
    public static ReadOnlySpan<byte> HintGdkTextInputTitle => "SDL_GDK_TEXTINPUT_TITLE"u8;
    public static ReadOnlySpan<byte> HintHidApiLibUsb => "SDL_HIDAPI_LIBUSB"u8;
    public static ReadOnlySpan<byte> HintHidApiLibUsbGameCube => "SDL_HIDAPI_LIBUSB_GAMECUBE"u8;
    public static ReadOnlySpan<byte> HintHidApiLibUsbWhitelist => "SDL_HIDAPI_LIBUSB_WHITELIST"u8;
    public static ReadOnlySpan<byte> HintHidApiUdev => "SDL_HIDAPI_UDEV"u8;
    public static ReadOnlySpan<byte> HintGpuDriver => "SDL_GPU_DRIVER"u8;
    public static ReadOnlySpan<byte> HintHidApiEnumerateOnlyControllers =>
        "SDL_HIDAPI_ENUMERATE_ONLY_CONTROLLERS"u8;
    public static ReadOnlySpan<byte> HintHidApiIgnoreDevices => "SDL_HIDAPI_IGNORE_DEVICES"u8;
    public static ReadOnlySpan<byte> HintImeImplementedUI => "SDL_IME_IMPLEMENTED_UI"u8;
    public static ReadOnlySpan<byte> HintIosHideHomeIndicator => "SDL_IOS_HIDE_HOME_INDICATOR"u8;
    public static ReadOnlySpan<byte> HintJoystickAllowBackgroundEvents =>
        "SDL_JOYSTICK_ALLOW_BACKGROUND_EVENTS"u8;
    public static ReadOnlySpan<byte> HintJoystickArcadeStickDevices =>
        "SDL_JOYSTICK_ARCADESTICK_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickArcadeStickDevicesExcluded =>
        "SDL_JOYSTICK_ARCADESTICK_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickBlacklistDevices =>
        "SDL_JOYSTICK_BLACKLIST_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickBlacklistDevicesExcluded =>
        "SDL_JOYSTICK_BLACKLIST_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickDevice => "SDL_JOYSTICK_DEVICE"u8;
    public static ReadOnlySpan<byte> HintJoystickEnhancedReports =>
        "SDL_JOYSTICK_ENHANCED_REPORTS"u8;
    public static ReadOnlySpan<byte> HintJoystickFlightStickDevices =>
        "SDL_JOYSTICK_FLIGHTSTICK_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickFlightStickDevicesExcluded =>
        "SDL_JOYSTICK_FLIGHTSTICK_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickGameInput => "SDL_JOYSTICK_GAMEINPUT"u8;
    public static ReadOnlySpan<byte> HintJoystickGameInputRaw => "SDL_JOYSTICK_GAMEINPUT_RAW"u8;
    public static ReadOnlySpan<byte> HintJoystickGameCubeDevices =>
        "SDL_JOYSTICK_GAMECUBE_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickGameCubeDevicesExcluded =>
        "SDL_JOYSTICK_GAMECUBE_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApi => "SDL_JOYSTICK_HIDAPI"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiCombineJoyCons =>
        "SDL_JOYSTICK_HIDAPI_COMBINE_JOY_CONS"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiGameCube => "SDL_JOYSTICK_HIDAPI_GAMECUBE"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiGameCubeRumbleBrake =>
        "SDL_JOYSTICK_HIDAPI_GAMECUBE_RUMBLE_BRAKE"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiJoyCons => "SDL_JOYSTICK_HIDAPI_JOY_CONS"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiJoyConHomeLed =>
        "SDL_JOYSTICK_HIDAPI_JOYCON_HOME_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiLuna => "SDL_JOYSTICK_HIDAPI_LUNA"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiNintendoClassic =>
        "SDL_JOYSTICK_HIDAPI_NINTENDO_CLASSIC"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS3 => "SDL_JOYSTICK_HIDAPI_PS3"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS3SixAxisDriver =>
        "SDL_JOYSTICK_HIDAPI_PS3_SIXAXIS_DRIVER"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS4 => "SDL_JOYSTICK_HIDAPI_PS4"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS4ReportInterval =>
        "SDL_JOYSTICK_HIDAPI_PS4_REPORT_INTERVAL"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS5 => "SDL_JOYSTICK_HIDAPI_PS5"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiPS5PlayerLed =>
        "SDL_JOYSTICK_HIDAPI_PS5_PLAYER_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiShield => "SDL_JOYSTICK_HIDAPI_SHIELD"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiStadia => "SDL_JOYSTICK_HIDAPI_STADIA"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSteam => "SDL_JOYSTICK_HIDAPI_STEAM"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSteamHomeLed =>
        "SDL_JOYSTICK_HIDAPI_STEAM_HOME_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSteamDeck =>
        "SDL_JOYSTICK_HIDAPI_STEAMDECK"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSteamHori =>
        "SDL_JOYSTICK_HIDAPI_STEAM_HORI"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiLG4FF => "SDL_JOYSTICK_HIDAPI_LG4FF"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApi8BitDo => "SDL_JOYSTICK_HIDAPI_8BITDO"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSInput => "SDL_JOYSTICK_HIDAPI_SINPUT"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiZuiki => "SDL_JOYSTICK_HIDAPI_ZUIKI"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiFlydigi => "SDL_JOYSTICK_HIDAPI_FLYDIGI"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSwitch => "SDL_JOYSTICK_HIDAPI_SWITCH"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSwitchHomeLed =>
        "SDL_JOYSTICK_HIDAPI_SWITCH_HOME_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSwitchPlayerLed =>
        "SDL_JOYSTICK_HIDAPI_SWITCH_PLAYER_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiSwitch2 => "SDL_JOYSTICK_HIDAPI_SWITCH2"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiVerticalJoyCons =>
        "SDL_JOYSTICK_HIDAPI_VERTICAL_JOY_CONS"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiWii => "SDL_JOYSTICK_HIDAPI_WII"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiWiiPlayerLed =>
        "SDL_JOYSTICK_HIDAPI_WII_PLAYER_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXbox => "SDL_JOYSTICK_HIDAPI_XBOX"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXbox360 => "SDL_JOYSTICK_HIDAPI_XBOX_360"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXbox360PlayerLed =>
        "SDL_JOYSTICK_HIDAPI_XBOX_360_PLAYER_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXbox360Wireless =>
        "SDL_JOYSTICK_HIDAPI_XBOX_360_WIRELESS"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXboxOne => "SDL_JOYSTICK_HIDAPI_XBOX_ONE"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiXboxOneHomeLed =>
        "SDL_JOYSTICK_HIDAPI_XBOX_ONE_HOME_LED"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiGip => "SDL_JOYSTICK_HIDAPI_GIP"u8;
    public static ReadOnlySpan<byte> HintJoystickHidApiGipResetForMetadata =>
        "SDL_JOYSTICK_HIDAPI_GIP_RESET_FOR_METADATA"u8;
    public static ReadOnlySpan<byte> HintJoystickIOKit => "SDL_JOYSTICK_IOKIT"u8;
    public static ReadOnlySpan<byte> HintJoystickLinuxClassic => "SDL_JOYSTICK_LINUX_CLASSIC"u8;
    public static ReadOnlySpan<byte> HintJoystickLinuxDeadZones => "SDL_JOYSTICK_LINUX_DEADZONES"u8;
    public static ReadOnlySpan<byte> HintJoystickLinuxDigitalHats =>
        "SDL_JOYSTICK_LINUX_DIGITAL_HATS"u8;
    public static ReadOnlySpan<byte> HintJoystickLinuxHatDeadZones =>
        "SDL_JOYSTICK_LINUX_HAT_DEADZONES"u8;
    public static ReadOnlySpan<byte> HintJoystickMfi => "SDL_JOYSTICK_MFI"u8;
    public static ReadOnlySpan<byte> HintJoystickRawInput => "SDL_JOYSTICK_RAWINPUT"u8;
    public static ReadOnlySpan<byte> HintJoystickRawInputCorrelateXInput =>
        "SDL_JOYSTICK_RAWINPUT_CORRELATE_XINPUT"u8;
    public static ReadOnlySpan<byte> HintJoystickRogChakram => "SDL_JOYSTICK_ROG_CHAKRAM"u8;
    public static ReadOnlySpan<byte> HintJoystickThread => "SDL_JOYSTICK_THREAD"u8;
    public static ReadOnlySpan<byte> HintJoystickThrottleDevices =>
        "SDL_JOYSTICK_THROTTLE_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickThrottleDevicesExcluded =>
        "SDL_JOYSTICK_THROTTLE_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickWgi => "SDL_JOYSTICK_WGI"u8;
    public static ReadOnlySpan<byte> HintJoystickWheelDevices => "SDL_JOYSTICK_WHEEL_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickWheelDevicesExcluded =>
        "SDL_JOYSTICK_WHEEL_DEVICES_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintJoystickZeroCenteredDevices =>
        "SDL_JOYSTICK_ZERO_CENTERED_DEVICES"u8;
    public static ReadOnlySpan<byte> HintJoystickHapticAxes => "SDL_JOYSTICK_HAPTIC_AXES"u8;
    public static ReadOnlySpan<byte> HintKeycodeOptions => "SDL_KEYCODE_OPTIONS"u8;
    public static ReadOnlySpan<byte> HintKmsDrmDeviceIndex => "SDL_KMSDRM_DEVICE_INDEX"u8;
    public static ReadOnlySpan<byte> HintKmsDrmRequireDrmMaster =>
        "SDL_KMSDRM_REQUIRE_DRM_MASTER"u8;
    public static ReadOnlySpan<byte> HintKmsDrmAtomic => "SDL_KMSDRM_ATOMIC"u8;
    public static ReadOnlySpan<byte> HintLogging => "SDL_LOGGING"u8;
    public static ReadOnlySpan<byte> HintMacBackgroundApp => "SDL_MAC_BACKGROUND_APP"u8;
    public static ReadOnlySpan<byte> HintMacCtrlClickEmulateRightClick =>
        "SDL_MAC_CTRL_CLICK_EMULATE_RIGHT_CLICK"u8;
    public static ReadOnlySpan<byte> HintMacOpenGLAsyncDispatch =>
        "SDL_MAC_OPENGL_ASYNC_DISPATCH"u8;
    public static ReadOnlySpan<byte> HintMacOptionAsAlt => "SDL_MAC_OPTION_AS_ALT"u8;
    public static ReadOnlySpan<byte> HintMacScrollMomentum => "SDL_MAC_SCROLL_MOMENTUM"u8;
    public static ReadOnlySpan<byte> HintMacPressAndHold => "SDL_MAC_PRESS_AND_HOLD"u8;
    public static ReadOnlySpan<byte> HintMainCallbackRate => "SDL_MAIN_CALLBACK_RATE"u8;
    public static ReadOnlySpan<byte> HintMouseAutoCapture => "SDL_MOUSE_AUTO_CAPTURE"u8;
    public static ReadOnlySpan<byte> HintMouseDoubleClickRadius =>
        "SDL_MOUSE_DOUBLE_CLICK_RADIUS"u8;
    public static ReadOnlySpan<byte> HintMouseDoubleClickTime => "SDL_MOUSE_DOUBLE_CLICK_TIME"u8;
    public static ReadOnlySpan<byte> HintMouseDefaultSystemCursor =>
        "SDL_MOUSE_DEFAULT_SYSTEM_CURSOR"u8;
    public static ReadOnlySpan<byte> HintMouseDpiScaleCursors => "SDL_MOUSE_DPI_SCALE_CURSORS"u8;
    public static ReadOnlySpan<byte> HintMouseEmulateWarpWithRelative =>
        "SDL_MOUSE_EMULATE_WARP_WITH_RELATIVE"u8;
    public static ReadOnlySpan<byte> HintMouseFocusClickThrough => "SDL_MOUSE_FOCUS_CLICKTHROUGH"u8;
    public static ReadOnlySpan<byte> HintMouseNormalSpeedScale => "SDL_MOUSE_NORMAL_SPEED_SCALE"u8;
    public static ReadOnlySpan<byte> HintMouseRelativeModeCenter =>
        "SDL_MOUSE_RELATIVE_MODE_CENTER"u8;
    public static ReadOnlySpan<byte> HintMouseRelativeSpeedScale =>
        "SDL_MOUSE_RELATIVE_SPEED_SCALE"u8;
    public static ReadOnlySpan<byte> HintMouseRelativeSystemScale =>
        "SDL_MOUSE_RELATIVE_SYSTEM_SCALE"u8;
    public static ReadOnlySpan<byte> HintMouseRelativeWarpMotion =>
        "SDL_MOUSE_RELATIVE_WARP_MOTION"u8;
    public static ReadOnlySpan<byte> HintMouseRelativeCursorVisible =>
        "SDL_MOUSE_RELATIVE_CURSOR_VISIBLE"u8;
    public static ReadOnlySpan<byte> HintMouseTouchEvents => "SDL_MOUSE_TOUCH_EVENTS"u8;
    public static ReadOnlySpan<byte> HintMuteConsoleKeyboard => "SDL_MUTE_CONSOLE_KEYBOARD"u8;
    public static ReadOnlySpan<byte> HintNoSignalHandlers => "SDL_NO_SIGNAL_HANDLERS"u8;
    public static ReadOnlySpan<byte> HintOpenGLLibrary => "SDL_OPENGL_LIBRARY"u8;
    public static ReadOnlySpan<byte> HintEglLibrary => "SDL_EGL_LIBRARY"u8;
    public static ReadOnlySpan<byte> HintOpenGLESDriver => "SDL_OPENGL_ES_DRIVER"u8;
    public static ReadOnlySpan<byte> HintOpenGLForceSrgbFramebuffer =>
        "SDL_OPENGL_FORCE_SRGB_FRAMEBUFFER"u8;
    public static ReadOnlySpan<byte> HintOpenVRLibrary => "SDL_OPENVR_LIBRARY"u8;
    public static ReadOnlySpan<byte> HintOrientations => "SDL_ORIENTATIONS"u8;
    public static ReadOnlySpan<byte> HintPollSentinel => "SDL_POLL_SENTINEL"u8;
    public static ReadOnlySpan<byte> HintPreferredLocales => "SDL_PREFERRED_LOCALES"u8;
    public static ReadOnlySpan<byte> HintQuitOnLastWindowClose => "SDL_QUIT_ON_LAST_WINDOW_CLOSE"u8;
    public static ReadOnlySpan<byte> HintRenderDirect3DThreadSafe =>
        "SDL_RENDER_DIRECT3D_THREADSAFE"u8;
    public static ReadOnlySpan<byte> HintRenderDirect3D11Debug => "SDL_RENDER_DIRECT3D11_DEBUG"u8;
    public static ReadOnlySpan<byte> HintRenderDirect3D11Warp => "SDL_RENDER_DIRECT3D11_WARP"u8;
    public static ReadOnlySpan<byte> HintRenderVulkanDebug => "SDL_RENDER_VULKAN_DEBUG"u8;
    public static ReadOnlySpan<byte> HintRenderGpuDebug => "SDL_RENDER_GPU_DEBUG"u8;
    public static ReadOnlySpan<byte> HintRenderGpuLowPower => "SDL_RENDER_GPU_LOW_POWER"u8;
    public static ReadOnlySpan<byte> HintRenderDriver => "SDL_RENDER_DRIVER"u8;
    public static ReadOnlySpan<byte> HintRenderLineMethod => "SDL_RENDER_LINE_METHOD"u8;
    public static ReadOnlySpan<byte> HintRenderMetalPreferLowPowerDevice =>
        "SDL_RENDER_METAL_PREFER_LOW_POWER_DEVICE"u8;
    public static ReadOnlySpan<byte> HintRenderVSync => "SDL_RENDER_VSYNC"u8;
    public static ReadOnlySpan<byte> HintReturnKeyHidesIme => "SDL_RETURN_KEY_HIDES_IME"u8;
    public static ReadOnlySpan<byte> HintRogGamepadMice => "SDL_ROG_GAMEPAD_MICE"u8;
    public static ReadOnlySpan<byte> HintRogGamepadMiceExcluded =>
        "SDL_ROG_GAMEPAD_MICE_EXCLUDED"u8;
    public static ReadOnlySpan<byte> HintPS2GSWidth => "SDL_PS2_GS_WIDTH"u8;
    public static ReadOnlySpan<byte> HintPS2GSHeight => "SDL_PS2_GS_HEIGHT"u8;
    public static ReadOnlySpan<byte> HintPS2GSProgressive => "SDL_PS2_GS_PROGRESSIVE"u8;
    public static ReadOnlySpan<byte> HintPS2GSMode => "SDL_PS2_GS_MODE"u8;
    public static ReadOnlySpan<byte> HintRpiVideoLayer => "SDL_RPI_VIDEO_LAYER"u8;
    public static ReadOnlySpan<byte> HintScreensaverInhibitActivityName =>
        "SDL_SCREENSAVER_INHIBIT_ACTIVITY_NAME"u8;
    public static ReadOnlySpan<byte> HintShutdownDBusOnQuit => "SDL_SHUTDOWN_DBUS_ON_QUIT"u8;
    public static ReadOnlySpan<byte> HintStorageTitleDriver => "SDL_STORAGE_TITLE_DRIVER"u8;
    public static ReadOnlySpan<byte> HintStorageUserDriver => "SDL_STORAGE_USER_DRIVER"u8;
    public static ReadOnlySpan<byte> HintThreadForceRealtimeTimeCritical =>
        "SDL_THREAD_FORCE_REALTIME_TIME_CRITICAL"u8;
    public static ReadOnlySpan<byte> HintThreadPriorityPolicy => "SDL_THREAD_PRIORITY_POLICY"u8;
    public static ReadOnlySpan<byte> HintTimerResolution => "SDL_TIMER_RESOLUTION"u8;
    public static ReadOnlySpan<byte> HintTouchMouseEvents => "SDL_TOUCH_MOUSE_EVENTS"u8;
    public static ReadOnlySpan<byte> HintTrackpadIsTouchOnly => "SDL_TRACKPAD_IS_TOUCH_ONLY"u8;
    public static ReadOnlySpan<byte> HintTVRemoteAsJoystick => "SDL_TV_REMOTE_AS_JOYSTICK"u8;
    public static ReadOnlySpan<byte> HintVideoAllowScreensaver => "SDL_VIDEO_ALLOW_SCREENSAVER"u8;
    public static ReadOnlySpan<byte> HintVideoDisplayPriority => "SDL_VIDEO_DISPLAY_PRIORITY"u8;
    public static ReadOnlySpan<byte> HintVideoDoubleBuffer => "SDL_VIDEO_DOUBLE_BUFFER"u8;
    public static ReadOnlySpan<byte> HintVideoDriver => "SDL_VIDEO_DRIVER"u8;
    public static ReadOnlySpan<byte> HintVideoDummySaveFrames => "SDL_VIDEO_DUMMY_SAVE_FRAMES"u8;
    public static ReadOnlySpan<byte> HintVideoEglAllowGetDisplayFallback =>
        "SDL_VIDEO_EGL_ALLOW_GETDISPLAY_FALLBACK"u8;
    public static ReadOnlySpan<byte> HintVideoForceEgl => "SDL_VIDEO_FORCE_EGL"u8;
    public static ReadOnlySpan<byte> HintVideoMacFullscreenSpaces =>
        "SDL_VIDEO_MAC_FULLSCREEN_SPACES"u8;
    public static ReadOnlySpan<byte> HintVideoMacFullscreenMenuVisibility =>
        "SDL_VIDEO_MAC_FULLSCREEN_MENU_VISIBILITY"u8;
    public static ReadOnlySpan<byte> HintVideoMetalAutoResizeDrawable =>
        "SDL_VIDEO_METAL_AUTO_RESIZE_DRAWABLE"u8;
    public static ReadOnlySpan<byte> HintVideoMatchExclusiveModeOnMove =>
        "SDL_VIDEO_MATCH_EXCLUSIVE_MODE_ON_MOVE"u8;
    public static ReadOnlySpan<byte> HintVideoMinimizeOnFocusLoss =>
        "SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS"u8;
    public static ReadOnlySpan<byte> HintVideoOffscreenSaveFrames =>
        "SDL_VIDEO_OFFSCREEN_SAVE_FRAMES"u8;
    public static ReadOnlySpan<byte> HintVideoSyncWindowOperations =>
        "SDL_VIDEO_SYNC_WINDOW_OPERATIONS"u8;
    public static ReadOnlySpan<byte> HintVideoWaylandAllowLibdecor =>
        "SDL_VIDEO_WAYLAND_ALLOW_LIBDECOR"u8;
    public static ReadOnlySpan<byte> HintVideoWaylandModeEmulation =>
        "SDL_VIDEO_WAYLAND_MODE_EMULATION"u8;
    public static ReadOnlySpan<byte> HintVideoWaylandModeScaling =>
        "SDL_VIDEO_WAYLAND_MODE_SCALING"u8;
    public static ReadOnlySpan<byte> HintVideoWaylandPreferLibdecor =>
        "SDL_VIDEO_WAYLAND_PREFER_LIBDECOR"u8;
    public static ReadOnlySpan<byte> HintVideoWaylandScaleToDisplay =>
        "SDL_VIDEO_WAYLAND_SCALE_TO_DISPLAY"u8;
    public static ReadOnlySpan<byte> HintVideoWinD3DCompiler => "SDL_VIDEO_WIN_D3DCOMPILER"u8;
    public static ReadOnlySpan<byte> HintVideoX11EnableXSyncExt =>
        "SDL_VIDEO_X11_ENABLE_XSYNC_EXT"u8;
    public static ReadOnlySpan<byte> HintVideoX11ExternalWindowInput =>
        "SDL_VIDEO_X11_EXTERNAL_WINDOW_INPUT"u8;
    public static ReadOnlySpan<byte> HintVideoX11NetWMBypassCompositor =>
        "SDL_VIDEO_X11_NET_WM_BYPASS_COMPOSITOR"u8;
    public static ReadOnlySpan<byte> HintVideoX11NetWMPing => "SDL_VIDEO_X11_NET_WM_PING"u8;
    public static ReadOnlySpan<byte> HintVideoX11NoDirectColor => "SDL_VIDEO_X11_NODIRECTCOLOR"u8;
    public static ReadOnlySpan<byte> HintVideoX11ScalingFactor => "SDL_VIDEO_X11_SCALING_FACTOR"u8;
    public static ReadOnlySpan<byte> HintVideoX11VisualID => "SDL_VIDEO_X11_VISUALID"u8;
    public static ReadOnlySpan<byte> HintVideoX11WindowVisualID =>
        "SDL_VIDEO_X11_WINDOW_VISUALID"u8;
    public static ReadOnlySpan<byte> HintVideoX11Xrandr => "SDL_VIDEO_X11_XRANDR"u8;
    public static ReadOnlySpan<byte> HintVitaEnableBackTouch => "SDL_VITA_ENABLE_BACK_TOUCH"u8;
    public static ReadOnlySpan<byte> HintVitaEnableFrontTouch => "SDL_VITA_ENABLE_FRONT_TOUCH"u8;
    public static ReadOnlySpan<byte> HintVitaModulePath => "SDL_VITA_MODULE_PATH"u8;
    public static ReadOnlySpan<byte> HintVitaPvrInit => "SDL_VITA_PVR_INIT"u8;
    public static ReadOnlySpan<byte> HintVitaResolution => "SDL_VITA_RESOLUTION"u8;
    public static ReadOnlySpan<byte> HintVitaPvrOpenGL => "SDL_VITA_PVR_OPENGL"u8;
    public static ReadOnlySpan<byte> HintVitaTouchMouseDevice => "SDL_VITA_TOUCH_MOUSE_DEVICE"u8;
    public static ReadOnlySpan<byte> HintVulkanDisplay => "SDL_VULKAN_DISPLAY"u8;
    public static ReadOnlySpan<byte> HintVulkanLibrary => "SDL_VULKAN_LIBRARY"u8;
    public static ReadOnlySpan<byte> HintWaveFactChunk => "SDL_WAVE_FACT_CHUNK"u8;
    public static ReadOnlySpan<byte> HintWaveChunkLimit => "SDL_WAVE_CHUNK_LIMIT"u8;
    public static ReadOnlySpan<byte> HintWaveRiffChunkSize => "SDL_WAVE_RIFF_CHUNK_SIZE"u8;
    public static ReadOnlySpan<byte> HintWaveTruncation => "SDL_WAVE_TRUNCATION"u8;
    public static ReadOnlySpan<byte> HintWindowActivateWhenRaised =>
        "SDL_WINDOW_ACTIVATE_WHEN_RAISED"u8;
    public static ReadOnlySpan<byte> HintWindowActivateWhenShown =>
        "SDL_WINDOW_ACTIVATE_WHEN_SHOWN"u8;
    public static ReadOnlySpan<byte> HintWindowAllowTopmost => "SDL_WINDOW_ALLOW_TOPMOST"u8;
    public static ReadOnlySpan<byte> HintWindowFrameUsableWhileCursorHidden =>
        "SDL_WINDOW_FRAME_USABLE_WHILE_CURSOR_HIDDEN"u8;
    public static ReadOnlySpan<byte> HintWindowsCloseOnAltF4 => "SDL_WINDOWS_CLOSE_ON_ALT_F4"u8;
    public static ReadOnlySpan<byte> HintWindowsEnableMenuMnemonics =>
        "SDL_WINDOWS_ENABLE_MENU_MNEMONICS"u8;
    public static ReadOnlySpan<byte> HintWindowsEnableMessageLoop =>
        "SDL_WINDOWS_ENABLE_MESSAGELOOP"u8;
    public static ReadOnlySpan<byte> HintWindowsGameInput => "SDL_WINDOWS_GAMEINPUT"u8;
    public static ReadOnlySpan<byte> HintWindowsRawKeyboard => "SDL_WINDOWS_RAW_KEYBOARD"u8;
    public static ReadOnlySpan<byte> HintWindowsRawKeyboardExcludeHotkeys =>
        "SDL_WINDOWS_RAW_KEYBOARD_EXCLUDE_HOTKEYS"u8;
    public static ReadOnlySpan<byte> HintWindowsRawKeyboardInputSink =>
        "SDL_WINDOWS_RAW_KEYBOARD_INPUTSINK"u8;
    public static ReadOnlySpan<byte> HintWindowsForceSemaphoreKernel =>
        "SDL_WINDOWS_FORCE_SEMAPHORE_KERNEL"u8;
    public static ReadOnlySpan<byte> HintWindowsIntResourceIcon => "SDL_WINDOWS_INTRESOURCE_ICON"u8;
    public static ReadOnlySpan<byte> HintWindowsIntResourceIconSmall =>
        "SDL_WINDOWS_INTRESOURCE_ICON_SMALL"u8;
    public static ReadOnlySpan<byte> HintWindowsUseD3D9EX => "SDL_WINDOWS_USE_D3D9EX"u8;
    public static ReadOnlySpan<byte> HintWindowsEraseBackgroundMode =>
        "SDL_WINDOWS_ERASE_BACKGROUND_MODE"u8;
    public static ReadOnlySpan<byte> HintX11ForceOverrideRedirect =>
        "SDL_X11_FORCE_OVERRIDE_REDIRECT"u8;
    public static ReadOnlySpan<byte> HintX11WindowType => "SDL_X11_WINDOW_TYPE"u8;
    public static ReadOnlySpan<byte> HintX11XcbLibrary => "SDL_X11_XCB_LIBRARY"u8;
    public static ReadOnlySpan<byte> HintXInputEnabled => "SDL_XINPUT_ENABLED"u8;
    public static ReadOnlySpan<byte> HintAssert => "SDL_ASSERT"u8;
    public static ReadOnlySpan<byte> HintPenMouseEvents => "SDL_PEN_MOUSE_EVENTS"u8;
    public static ReadOnlySpan<byte> HintPenTouchEvents => "SDL_PEN_TOUCH_EVENTS"u8;

    [LibraryImport(DllName, EntryPoint = "SDL_SetHintWithPriority")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetHintWithPriority(
        byte* name,
        byte* value,
        HintPriority priority
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _SetHint(byte* name, byte* value);

    [LibraryImport(DllName, EntryPoint = "SDL_ResetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _ResetHint(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_ResetHints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ResetHints();

    [LibraryImport(DllName, EntryPoint = "SDL_GetHint")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetHint(byte* name);

    [LibraryImport(DllName, EntryPoint = "SDL_GetHintBoolean")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetHintBoolean(
        byte* name,
        [MarshalAs(UnmanagedType.I1)] bool defaultValue
    );

    [LibraryImport(DllName, EntryPoint = "SDL_AddHintCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _AddHintCallback(
        byte* name,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveHintCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _RemoveHintCallback(
        byte* name,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback,
        void* userdata
    );

    public static bool SetHintWithPriority(byte* name, byte* value, HintPriority priority)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetHintWithPriority(name, value, priority);
    }

    public static bool SetHint(byte* name, byte* value)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _SetHint(name, value);
    }

    public static bool ResetHint(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ResetHint(name);
    }

    public static void ResetHints()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ResetHints();
    }

    public static byte* GetHint(byte* name)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHint(name);
    }

    public static bool GetHintBoolean(byte* name, bool defaultValue)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetHintBoolean(name, defaultValue);
    }

    public static bool AddHintCallback(
        byte* name,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _AddHintCallback(name, callback, userdata);
    }

    public static void RemoveHintCallback(
        byte* name,
        delegate* unmanaged[Cdecl]<void*, byte*, byte*, byte*, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RemoveHintCallback(name, callback, userdata);
    }

    public enum HintPriority
    {
        Default,
        Normal,
        Override,
    }
}
