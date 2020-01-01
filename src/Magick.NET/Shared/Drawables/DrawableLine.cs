// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Draws a line on the image using the current stroke color, stroke alpha, and stroke width.
    /// </summary>
    public sealed class DrawableLine : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableLine"/> class.
        /// </summary>
        /// <param name="startX">The starting X coordinate.</param>
        /// <param name="startY">The starting Y coordinate.</param>
        /// <param name="endX">The ending X coordinate.</param>
        /// <param name="endY">The ending Y coordinate.</param>
        public DrawableLine(double startX, double startY, double endX, double endY)
        {
            StartX = startX;
            StartY = startY;
            EndX = endX;
            EndY = endY;
        }

        /// <summary>
        /// Gets or sets the ending X coordinate.
        /// </summary>
        public double EndX { get; set; }

        /// <summary>
        /// Gets or sets the ending Y coordinate.
        /// </summary>
        public double EndY { get; set; }

        /// <summary>
        /// Gets or sets the starting X coordinate.
        /// </summary>
        public double StartX { get; set; }

        /// <summary>
        /// Gets or sets the starting Y coordinate.
        /// </summary>
        public double StartY { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.Line(StartX, StartY, EndX, EndY);
    }
}