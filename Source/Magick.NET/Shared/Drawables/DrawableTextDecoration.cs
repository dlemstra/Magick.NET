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
    /// Specifies a decoration to be applied when annotating with text.
    /// </summary>
    public sealed class DrawableTextDecoration : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextDecoration"/> class.
        /// </summary>
        /// <param name="decoration">The text decoration.</param>
        public DrawableTextDecoration(TextDecoration decoration)
        {
            Decoration = decoration;
        }

        /// <summary>
        /// Gets or sets the text decoration
        /// </summary>
        public TextDecoration Decoration
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
                wand.TextDecoration(Decoration);
        }
    }
}