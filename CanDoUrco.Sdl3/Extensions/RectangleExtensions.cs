// An SDL3 interop library with dotnet safety and idioms.
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

using System.Drawing;
using CanDoUrco.Sdl3.Internals;

namespace CanDoUrco.Sdl3.Extensions;

public static class RectangleExtensions
{
    extension(Rectangle rect)
    {
        public static unsafe bool TryGetEnclosingPoints(
            IReadOnlyCollection<Point> points,
            out Rectangle result
        )
        {
            var count = points.Count;
            Span<Sdl.Point> sdlPoints = stackalloc Sdl.Point[count];
            for (var i = 0; i < count; i++)
            {
                sdlPoints[i] = new Sdl.Point
                {
                    X = points.ElementAt(i).X,
                    Y = points.ElementAt(i).Y,
                };
            }

            fixed (Sdl.Point* pPoints = sdlPoints)
            {
                var valid = Sdl.GetRectEnclosingPoints(pPoints, count, null, out var res);
                result = new Rectangle(res.X, res.Y, res.W, res.H);
                return valid;
            }
        }

        public RectangleF ToRectangleF() => new(rect.X, rect.Y, rect.Width, rect.Height);

        public unsafe bool TryGetEnclosingPointsClipped(
            IReadOnlyCollection<Point> points,
            out Rectangle result
        )
        {
            var count = points.Count;
            Span<Sdl.Point> sdlPoints = stackalloc Sdl.Point[count];
            for (var i = 0; i < count; i++)
            {
                sdlPoints[i] = new Sdl.Point
                {
                    X = points.ElementAt(i).X,
                    Y = points.ElementAt(i).Y,
                };
            }

            var clip = new Sdl.Rect
            {
                X = rect.X,
                Y = rect.Y,
                W = rect.Width,
                H = rect.Height,
            };

            fixed (Sdl.Point* pPoints = sdlPoints)
            {
                var valid = Sdl.GetRectEnclosingPoints(pPoints, count, &clip, out var res);
                result = new Rectangle(res.X, res.Y, res.W, res.H);
                return valid;
            }
        }

        public bool TryGetLineIntersection(
            (Point P1, Point P2) line,
            out (Point P1, Point P2) result
        )
        {
            var nativeRect = new Sdl.Rect
            {
                X = rect.X,
                Y = rect.Y,
                W = rect.Width,
                H = rect.Height,
            };

            var x1 = line.P1.X;
            var y1 = line.P1.Y;
            var x2 = line.P2.X;
            var y2 = line.P2.Y;

            var valid = Sdl.GetRectAndLineIntersection(
                in nativeRect,
                ref x1,
                ref y1,
                ref x2,
                ref y2
            );

            result = (new Point(x1, y1), new Point(x2, y2));
            return valid;
        }
    }
}
