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
    /// Encapsulation of the DrawableDensity object.
    /// </summary>
    public sealed class DrawableDensity : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableDensity"/> class.
        /// </summary>
        /// <param name="density">The vertical and horizontal resolution.</param>
        public DrawableDensity(double density)
        {
            Density = new PointD(density);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableDensity"/> class.
        /// </summary>
        /// <param name="pointDensity">The vertical and horizontal resolution.</param>
        public DrawableDensity(PointD pointDensity)
        {
            Density = pointDensity;
        }

        /// <summary>
        /// Gets or sets the vertical and horizontal resolution.
        /// </summary>
        public PointD Density { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.Density(Density);
    }
}