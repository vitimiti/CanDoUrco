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
    public static TrayEntryFlags TrayEntryButton => 0x0000_0001U;
    public static TrayEntryFlags TrayEntryCheckbox => 0x0000_0002U;
    public static TrayEntryFlags TrayEntrySubmenu => 0x0000_0004U;
    public static TrayEntryFlags TrayEntryDisabled => 0x8000_0000U;
    public static TrayEntryFlags TrayEntryChecked => 0x4000_0000U;

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Tray _CreateTray(Surface icon, byte* tooltip);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayIcon")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayIcon(Tray tray, Surface icon);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayTooltip")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayTooltip(Tray tray, byte* tooltip);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayMenu _CreateTrayMenu(Tray tray);

    [LibraryImport(DllName, EntryPoint = "SDL_CreateTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayMenu _CreateTraySubmenu(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayMenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayMenu _GetTrayMenu(Tray tray);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTraySubmenu")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayMenu _GetTraySubmenu(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayEntries")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayEntry* _GetTrayEntries(TrayMenu menu, out int count);

    [LibraryImport(DllName, EntryPoint = "SDL_RemoveTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _RemoveTrayEntry(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_InsertTrayEntryAt")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayEntry _InsertTrayEntryAt(
        TrayMenu menu,
        int pos,
        byte* label,
        TrayEntryFlags flags
    );

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayEntryLabel(TrayEntry entry, byte* label);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayEntryLabel")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial byte* _GetTrayEntryLabel(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayEntryChecked(TrayEntry entry, Bool @checked);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayEntryChecked")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTrayEntryChecked(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayEntryEnabled(TrayEntry entry, Bool enabled);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayEntryEnabled")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Bool _GetTrayEntryEnabled(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_SetTrayEntryCallback")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _SetTrayEntryCallback(
        TrayEntry entry,
        delegate* unmanaged[Cdecl]<void*, TrayEntry, void> callback,
        void* userdata
    );

    [LibraryImport(DllName, EntryPoint = "SDL_ClickTrayEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _ClickTrayEntry(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_DestroyTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _DestroyTray(Tray tray);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayEntryParent")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayMenu _GetTrayEntryParent(TrayEntry entry);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayMenuParentEntry")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial TrayEntry _GetTrayMenuParentEntry(TrayMenu menu);

    [LibraryImport(DllName, EntryPoint = "SDL_GetTrayMenuParentTray")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial Tray _GetTrayMenuParentTray(TrayMenu menu);

    [LibraryImport(DllName, EntryPoint = "SDL_UpdateTrays")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _UpdateTrays();

    public static Tray CreateTray(Surface icon, byte* tooltip)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTray(icon, tooltip);
    }

    public static void SetTrayIcon(Tray tray, Surface icon)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayIcon(tray, icon);
    }

    public static void SetTrayTooltip(Tray tray, byte* tooltip)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayTooltip(tray, tooltip);
    }

    public static TrayMenu CreateTrayMenu(Tray tray)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTrayMenu(tray);
    }

    public static TrayMenu CreateTraySubmenu(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _CreateTraySubmenu(entry);
    }

    public static TrayMenu GetTrayMenu(Tray tray)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayMenu(tray);
    }

    public static TrayMenu GetTraySubmenu(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTraySubmenu(entry);
    }

    public static TrayEntry* GetTrayEntries(TrayMenu menu, out int count)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayEntries(menu, out count);
    }

    public static void RemoveTrayEntry(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _RemoveTrayEntry(entry);
    }

    public static TrayEntry InsertTrayEntryAt(
        TrayMenu menu,
        int pos,
        byte* label,
        TrayEntryFlags flags
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _InsertTrayEntryAt(menu, pos, label, flags);
    }

    public static void SetTrayEntryLabel(TrayEntry entry, byte* label)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayEntryLabel(entry, label);
    }

    public static byte* GetTrayEntryLabel(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayEntryLabel(entry);
    }

    public static void SetTrayEntryChecked(TrayEntry entry, Bool @checked)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayEntryChecked(entry, @checked);
    }

    public static Bool GetTrayEntryChecked(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayEntryChecked(entry);
    }

    public static void SetTrayEntryEnabled(TrayEntry entry, Bool enabled)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayEntryEnabled(entry, enabled);
    }

    public static Bool GetTrayEntryEnabled(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayEntryEnabled(entry);
    }

    public static void SetTrayEntryCallback(
        TrayEntry entry,
        delegate* unmanaged[Cdecl]<void*, TrayEntry, void> callback,
        void* userdata
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _SetTrayEntryCallback(entry, callback, userdata);
    }

    public static void ClickTrayEntry(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _ClickTrayEntry(entry);
    }

    public static void DestroyTray(Tray tray)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _DestroyTray(tray);
    }

    public static TrayMenu GetTrayEntryParent(TrayEntry entry)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayEntryParent(entry);
    }

    public static TrayEntry GetTrayMenuParentEntry(TrayMenu menu)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayMenuParentEntry(menu);
    }

    public static Tray GetTrayMenuParentTray(TrayMenu menu)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetTrayMenuParentTray(menu);
    }

    public static void UpdateTrays()
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _UpdateTrays();
    }

    public readonly record struct Tray(nint Value)
    {
        public static implicit operator nint(Tray tray) => tray.Value;

        public static implicit operator Tray(nint value) => new(value);

        public static implicit operator bool(Tray tray) => tray.Value != 0;
    }

    public readonly record struct TrayMenu(nint Value)
    {
        public static implicit operator nint(TrayMenu menu) => menu.Value;

        public static implicit operator TrayMenu(nint value) => new(value);

        public static implicit operator bool(TrayMenu menu) => menu.Value != 0;
    }

    public readonly record struct TrayEntry(nint Value)
    {
        public static implicit operator nint(TrayEntry entry) => entry.Value;

        public static implicit operator TrayEntry(nint value) => new(value);

        public static implicit operator bool(TrayEntry entry) => entry.Value != 0;
    }

    public readonly record struct TrayEntryFlags(uint Value)
    {
        public static TrayEntryFlags operator |(TrayEntryFlags left, TrayEntryFlags right) =>
            new(left.Value | right.Value);

        public static TrayEntryFlags operator &(TrayEntryFlags left, TrayEntryFlags right) =>
            new(left.Value & right.Value);

        public static TrayEntryFlags operator ^(TrayEntryFlags left, TrayEntryFlags right) =>
            new(left.Value ^ right.Value);

        public static TrayEntryFlags operator ~(TrayEntryFlags flags) => new(~flags.Value);

        public static implicit operator uint(TrayEntryFlags flags) => flags.Value;

        public static implicit operator TrayEntryFlags(uint value) => new(value);
    }
}
