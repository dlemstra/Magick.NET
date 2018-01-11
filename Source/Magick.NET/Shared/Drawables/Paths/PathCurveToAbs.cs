// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Draws a cubic Bezier curve from the current point to (x, y) using (x1, y1) as the control point
    /// at the beginning of the curve and (x2, y2) as the control point at the end of the curve using
    /// absolute coordinates. At the end of the command, the new current point becomes the final (x, y)
    /// coordinate pair used in the polybezier.
    /// </summary>
    public sealed class PathCurveToAbs : IPath, IDrawingWand
    {
        private readonly PointD _controlPointStart;
        private readonly PointD _controlPointEnd;
        private readonly PointD _end;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathCurveToAbs"/> class.
        /// </summary>
        /// <param name="x1">X coordinate of control point for curve beginning</param>
        /// <param name="y1">Y coordinate of control point for curve beginning</param>
        /// <param name="x2">X coordinate of control point for curve ending</param>
        /// <param name="y2">Y coordinate of control point for curve ending</param>
        /// <param name="x">X coordinate of the end of the curve</param>
        /// <param name="y">Y coordinate of the end of the curve</param>
        public PathCurveToAbs(double x1, double y1, double x2, double y2, double x, double y)
          : this(new PointD(x1, y1), new PointD(x2, y2), new PointD(x, y))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathCurveToAbs"/> class.
        /// </summary>
        /// <param name="controlPointStart">Coordinate of control point for curve beginning</param>
        /// <param name="controlPointEnd">Coordinate of control point for curve ending</param>
        /// <param name="end">Coordinate of the end of the curve</param>
        public PathCurveToAbs(PointD controlPointStart, PointD controlPointEnd, PointD end)
        {
            _controlPointStart = controlPointStart;
            _controlPointEnd = controlPointEnd;
            _end = end;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.PathCurveToAbs(_controlPointStart, _controlPointEnd, _end);
        }
    }
}