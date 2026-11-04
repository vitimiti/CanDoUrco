// CanDoUrco.Sdl - Provides SDL3 bindings for C# applications.
// Copyright (C) 2026 CanDoUrco (Victor Matia-Cheng)
//
// This Source Code Form is subject to the terms of the Mozilla Public License,
// v. 2.0. If a copy of the MPL was not distributed with this file, You can
// obtain one at <https://mozilla.org/MPL/2.0/>.

using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using CanDoUrco.Logging;

namespace CanDoUrco.Sdl3;

public static partial class Sdl
{
    [LibraryImport(DllName, EntryPoint = "SDL_HasRectIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasRectIntersection(in Rect a, in Rect b);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetRectIntersection(in Rect a, in Rect b, out Rect result);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectUnion")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetRectUnion(in Rect a, in Rect b, out Rect result);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectEnclosingPoints")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _GetRectEnclosingPoints(
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Point, Point>), CountElementName = nameof(count))]
            Point[] points,
        int count,
        Rect* clip,
        out Rect result
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectAndLineIntersection")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetRectAndLineIntersection(
        in Rect rect,
        ref int x1,
        ref int y1,
        ref int x2,
        ref int y2
    );

    [LibraryImport(DllName, EntryPoint = "SDL_HasRectIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _HasRectIntersectionFloat(in FRect a, in FRect b);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectUnionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    private static partial void _GetRectUnionFloat(in FRect a, in FRect b, out FRect result);

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectEnclosingPointsFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static unsafe partial bool _GetRectEnclosingPointsFloat(
        [In]
        [MarshalUsing(typeof(ArrayMarshaller<Point, Point>), CountElementName = nameof(count))]
            Point[] points,
        int count,
        FRect* clip,
        out FRect result
    );

    [LibraryImport(DllName, EntryPoint = "SDL_GetRectAndLineIntersectionFloat")]
    [UnmanagedCallConv(CallConvs = [typeof(CallConvCdecl)])]
    [return: MarshalAs(UnmanagedType.I1)]
    private static partial bool _GetRectAndLineIntersectionFloat(
        in FRect rect,
        ref float x1,
        ref float y1,
        ref float x2,
        ref float y2
    );

    public static void RectToFRect(in Rect rect, out FRect fRect)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        fRect = new FRect
        {
            X = rect.X,
            Y = rect.Y,
            W = rect.W,
            H = rect.H,
        };
    }

    public static bool PointInRect(in Point p, in Rect r)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return p.X >= r.X && p.X < r.X + r.W && p.Y >= r.Y && p.Y < r.Y + r.H;
    }

    public static bool RectEmpty(in Rect r)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return r.W <= 0 || r.H <= 0;
    }

    public static bool RectsEqual(in Rect a, in Rect b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return a.X == b.X && a.Y == b.Y && a.W == b.W && a.H == b.H;
    }

    public static bool HasRectIntersection(in Rect a, in Rect b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasRectIntersection(a, b);
    }

    public static bool GetRectIntersection(in Rect a, in Rect b, out Rect result)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRectIntersection(a, b, out result);
    }

    public static void GetRectUnion(in Rect a, in Rect b, out Rect result)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetRectUnion(a, b, out result);
    }

    public static unsafe bool GetRectEnclosingPoints(
        Point[] points,
        int count,
        in Rect? clip,
        out Rect result
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(points);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, points.Length);
        if (clip is null)
        {
            return _GetRectEnclosingPoints(points, count, null, out result);
        }

        var clipRect = clip.Value;
        return _GetRectEnclosingPoints(points, count, &clipRect, out result);
    }

    public static bool GetRectAndLineIntersection(
        in Rect rect,
        ref int x1,
        ref int y1,
        ref int x2,
        ref int y2
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRectAndLineIntersection(rect, ref x1, ref y1, ref x2, ref y2);
    }

    public static bool PointInRectFloat(in FPoint p, in FRect r)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return p.X >= r.X && p.X < r.X + r.W && p.Y >= r.Y && p.Y < r.Y + r.H;
    }

    public static bool RectEmptyFloat(in FRect r)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return r.W <= 0F || r.H <= 0F;
    }

    public static bool RectsEqualEpsilon(in FRect a, in FRect b, float epsilon)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return FAbsF(a.X - b.X) < epsilon
            && FAbsF(a.Y - b.Y) < epsilon
            && FAbsF(a.W - b.W) < epsilon
            && FAbsF(a.H - b.H) < epsilon;
    }

    public static bool RectsEqualFloat(in FRect a, in FRect b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return RectsEqualEpsilon(a, b, FltEpsilon);
    }

    public static bool HasRectIntersectionFloat(in FRect a, in FRect b)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _HasRectIntersectionFloat(a, b);
    }

    public static bool GetRectIntersectionFloat(in FRect a, in FRect b, out FRect result)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRectIntersectionFloat(a, b, out result);
    }

    public static void GetRectUnionFloat(in FRect a, in FRect b, out FRect result)
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        _GetRectUnionFloat(a, b, out result);
    }

    public static unsafe bool GetRectEnclosingPointsFloat(
        Point[] points,
        int count,
        in FRect? clip,
        out FRect result
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        ArgumentNullException.ThrowIfNull(points);
        ArgumentOutOfRangeException.ThrowIfNegative(count);
        ArgumentOutOfRangeException.ThrowIfGreaterThan(count, points.Length);
        if (clip is null)
        {
            return _GetRectEnclosingPointsFloat(points, count, null, out result);
        }

        var clipRect = clip.Value;
        return _GetRectEnclosingPointsFloat(points, count, &clipRect, out result);
    }

    public static bool GetRectAndLineIntersectionFloat(
        in FRect rect,
        ref float x1,
        ref float y1,
        ref float x2,
        ref float y2
    )
    {
        LogCategory.MakeOrGet(CustomLogCategory).Trace();
        return _GetRectAndLineIntersectionFloat(rect, ref x1, ref y1, ref x2, ref y2);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Point
    {
        public int X;
        public int Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FPoint
    {
        public float X;
        public float Y;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct Rect
    {
        public int X;
        public int Y;
        public int W;
        public int H;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct FRect
    {
        public float X;
        public float Y;
        public float W;
        public float H;
    }
}
