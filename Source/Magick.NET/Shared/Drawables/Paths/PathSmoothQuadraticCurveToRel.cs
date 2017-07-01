// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Draws a quadratic Bezier curve (using relative coordinates) from the current point to (X, Y).
    /// The control point is assumed to be the reflection of the control point on the previous
    /// command relative to the current point. (If there is no previous command or if the previous
    /// command was not a PathQuadraticCurveToAbs, PathQuadraticCurveToRel,
    /// PathSmoothQuadraticCurveToAbs or PathSmoothQuadraticCurveToRel, assume the control point is
    /// coincident with the current point.). At the end of the command, the new current point becomes
    /// the final (X,Y) coordinate pair used in the polybezier.
    /// </summary>
    public sealed class PathSmoothQuadraticCurveToRel : IPath, IDrawingWand
    {
        private PointD _End;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSmoothQuadraticCurveToRel"/> class.
        /// </summary>
        /// <param name="x">X coordinate of final point</param>
        /// <param name="y">Y coordinate of final point</param>
        public PathSmoothQuadraticCurveToRel(double x, double y)
          : this(new PointD(x, y))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathSmoothQuadraticCurveToRel"/> class.
        /// </summary>
        /// <param name="end">Coordinate of final point</param>
        public PathSmoothQuadraticCurveToRel(PointD end)
        {
            _End = end;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.PathSmoothQuadraticCurveToRel(_End);
        }
    }
}