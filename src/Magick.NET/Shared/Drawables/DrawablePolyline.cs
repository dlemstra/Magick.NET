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

using System.Collections.Generic;

namespace ImageMagick
{
    /// <summary>
    /// Draws a polyline using the current stroke, stroke width, and fill color or texture, using the
    /// specified array of coordinates.
    /// </summary>
    public sealed class DrawablePolyline : IDrawable, IDrawingWand
    {
        private readonly PointDCoordinates _coordinates;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePolyline"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public DrawablePolyline(params PointD[] coordinates)
        {
            _coordinates = new PointDCoordinates(coordinates, 3);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawablePolyline"/> class.
        /// </summary>
        /// <param name="coordinates">The coordinates.</param>
        public DrawablePolyline(IEnumerable<PointD> coordinates)
        {
            _coordinates = new PointDCoordinates(coordinates, 3);
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.Polyline(_coordinates.ToList());
    }
}