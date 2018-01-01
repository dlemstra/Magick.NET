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
    /// Controls whether stroked outlines are antialiased. Stroked outlines are antialiased by default.
    /// When antialiasing is disabled stroked pixels are thresholded to determine if the stroke color
    /// or underlying canvas color should be used.
    /// </summary>
    public sealed class DrawableStrokeAntialias : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableStrokeAntialias"/> class.
        /// </summary>
        /// <param name="isEnabled">True if stroke antialiasing is enabled otherwise false.</param>
        public DrawableStrokeAntialias(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether stroke antialiasing is enabled or disabled.
        /// </summary>
        public bool IsEnabled
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
                wand.StrokeAntialias(IsEnabled);
        }
    }
}