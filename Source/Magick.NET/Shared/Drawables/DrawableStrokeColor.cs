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
    /// Sets the color used for stroking object outlines.
    /// </summary>
    public sealed partial class DrawableStrokeColor : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableStrokeColor"/> class.
        /// </summary>
        /// <param name="color">The color to use.</param>
        public DrawableStrokeColor(MagickColor color)
        {
            Throw.IfNull(nameof(color), color);

            Color = color;
        }

        /// <summary>
        /// Gets or sets the color to use.
        /// </summary>
        public MagickColor Color
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
                wand.StrokeColor(Color);
        }
    }
}