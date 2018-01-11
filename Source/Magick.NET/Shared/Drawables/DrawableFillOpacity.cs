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
    /// Sets the alpha to use when drawing using the fill color or fill texture.
    /// </summary>
    public sealed class DrawableFillOpacity : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableFillOpacity"/> class.
        /// </summary>
        /// <param name="opacity">The opacity.</param>
        public DrawableFillOpacity(Percentage opacity)
        {
            Opacity = opacity;
        }

        /// <summary>
        /// Gets or sets the alpha.
        /// </summary>
        public Percentage Opacity
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
                wand.FillOpacity(Opacity.ToDouble() / 100);
        }
    }
}