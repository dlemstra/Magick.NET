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
    /// Draws an ellipse on the image.
    /// </summary>
    public sealed class DrawableEllipse : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableEllipse"/> class.
        /// </summary>
        /// <param name="originX">The origin X coordinate.</param>
        /// <param name="originY">The origin Y coordinate.</param>
        /// <param name="radiusX">The X radius.</param>
        /// <param name="radiusY">The Y radius.</param>
        /// <param name="startDegrees">The starting degrees of rotation.</param>
        /// <param name="endDegrees">The ending degrees of rotation.</param>
        public DrawableEllipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees)
        {
            OriginX = originX;
            OriginY = originY;
            RadiusX = radiusX;
            RadiusY = radiusY;
            StartDegrees = startDegrees;
            EndDegrees = endDegrees;
        }

        /// <summary>
        /// Gets or sets the ending degrees of rotation.
        /// </summary>
        public double EndDegrees
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the origin X coordinate.
        /// </summary>
        public double OriginX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the origin X coordinate.
        /// </summary>
        public double OriginY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the X radius.
        /// </summary>
        public double RadiusX
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y radius.
        /// </summary>
        public double RadiusY
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the starting degrees of rotation.
        /// </summary>
        public double StartDegrees
        {
            get;
            set;
        }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand)
        {
            if (wand != null)
                wand.Ellipse(OriginX, OriginY, RadiusX, RadiusY, StartDegrees, EndDegrees);
        }
    }
}