// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Starts a new sub-path at the given coordinate using absolute coordinates. The current point
    /// then becomes the specified coordinate.
    /// </summary>
    public sealed class PathMoveToAbs : IPath, IDrawingWand
    {
        private readonly PointD _coordinate;

        /// <summary>
        /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        public PathMoveToAbs(double x, double y)
         : this(new PointD(x, y))
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PathMoveToAbs"/> class.
        /// </summary>
        /// <param name="coordinate">The coordinate to use.</param>
        public PathMoveToAbs(PointD coordinate)
        {
            _coordinate = coordinate;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.PathMoveToAbs(_coordinate.X, _coordinate.Y);
    }
}