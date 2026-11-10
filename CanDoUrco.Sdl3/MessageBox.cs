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
    public static MessageBoxFlags MessageBoxError => 0x0000_0010U;
    public static MessageBoxFlags MessageBoxWarning => 0x0000_0020U;
    public static MessageBoxFlags MessageBoxInformation => 0x0000_0040U;
    public static MessageBoxFlags MessageBoxButtonsLeftToRight => 0x0000_0080U;
    public static MessageBoxFlags MessageBoxButtonsRightToLeft => 0x0000_0100U;

    public static MessageBoxButtonFlags MessageBoxButtonReturnKeyDefault => 0x0000_0001U;
    public static MessageBoxButtonFlags MessageBoxButtonEscapeKeyDefault => 0x0000_0002U;

    [LibraryImport(DllName, EntryPoint = "SDL_ShowMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ShowMessageBox(in MessageBoxData messageBoxData, out int buttonID);

    [LibraryImport(DllName, EntryPoint = "SDL_ShowSimpleMessageBox")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _ShowSimpleMessageBox(
        MessageBoxFlags flags,
        byte* title,
        byte* message,
        Window window
    );

    public static Bool ShowMessageBox(in MessageBoxData messageBoxData, out int buttonID)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShowMessageBox(messageBoxData, out buttonID);
    }

    public static Bool ShowSimpleMessageBox(
        MessageBoxFlags flags,
        byte* title,
        byte* message,
        Window window
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _ShowSimpleMessageBox(flags, title, message, window);
    }

    public enum MessageBoxColorType
    {
        Background,
        Text,
        ButtonBorder,
        ButtonBackground,
        ButtonSelected,
    }

    public readonly record struct MessageBoxFlags(uint Value)
    {
        public static MessageBoxFlags operator |(MessageBoxFlags left, MessageBoxFlags right) =>
            new(left.Value | right.Value);

        public static MessageBoxFlags operator &(MessageBoxFlags left, MessageBoxFlags right) =>
            new(left.Value & right.Value);

        public static MessageBoxFlags operator ^(MessageBoxFlags left, MessageBoxFlags right) =>
            new(left.Value ^ right.Value);

        public static MessageBoxFlags operator ~(MessageBoxFlags value) => new(~value.Value);

        public static implicit operator uint(MessageBoxFlags flags) => flags.Value;

        public static implicit operator MessageBoxFlags(uint value) => new(value);
    }

    public readonly record struct MessageBoxButtonFlags(uint Value)
    {
        public static MessageBoxButtonFlags operator |(
            MessageBoxButtonFlags left,
            MessageBoxButtonFlags right
        ) => new(left.Value | right.Value);

        public static MessageBoxButtonFlags operator &(
            MessageBoxButtonFlags left,
            MessageBoxButtonFlags right
        ) => new(left.Value & right.Value);

        public static MessageBoxButtonFlags operator ^(
            MessageBoxButtonFlags left,
            MessageBoxButtonFlags right
        ) => new(left.Value ^ right.Value);

        public static MessageBoxButtonFlags operator ~(MessageBoxButtonFlags value) =>
            new(~value.Value);

        public static implicit operator uint(MessageBoxButtonFlags flags) => flags.Value;

        public static implicit operator MessageBoxButtonFlags(uint value) => new(value);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxButtonData
    {
        public MessageBoxButtonFlags Flags;
        public int ButtonID;
        public byte* Text;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColor
    {
        public byte R;
        public byte G;
        public byte B;
    }

    [InlineArray(5)]
    public struct MessageBoxColorArray
    {
        private MessageBoxColor _element0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxColorScheme
    {
        public MessageBoxColorArray Colors;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MessageBoxData
    {
        public MessageBoxFlags Flags;
        public Window Window;
        public byte* Title;
        public byte* Message;
        public int NumButtons;
        public MessageBoxButtonData* Buttons;
        public MessageBoxColorScheme* ColorScheme;
    }
}
