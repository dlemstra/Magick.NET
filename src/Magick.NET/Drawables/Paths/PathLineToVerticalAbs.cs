// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Draws a vertical line path from the current point to the target point using absolute
    /// coordinates. The target point then becomes the new current point.
    /// </summary>
    public sealed class PathLineToVerticalAbs : IPath, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PathLineToVerticalAbs"/> class.
        /// </summary>
        /// <param name="y">The Y coordinate.</param>
        public PathLineToVerticalAbs(double y)
        {
            Y = y;
        }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.PathLineToVerticalAbs(Y);
    }
}