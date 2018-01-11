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
    /// Paints on the image's alpha channel in order to set effected pixels to transparent.
    /// </summary>
    public sealed class DrawableAlpha : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableAlpha"/> class.
        /// </summary>
        /// <param name="x">The X coordinate.</param>
        /// <param name="y">The Y coordinate.</param>
        /// <param name="paintMethod">The paint method to use.</param>
        public DrawableAlpha(double x, double y, PaintMethod paintMethod)
        {
            X = x;
            Y = y;
            PaintMethod = paintMethod;
        }

        /// <summary>
        /// Gets or sets the <see cref="PaintMethod"/> to use.
        /// </summary>
        public PaintMethod PaintMethod
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the X coordinate.
        /// </summary>
        public double X
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Y coordinate.
        /// </summary>
        public double Y
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
                wand.Alpha(X, Y, PaintMethod);
        }
    }
}