// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Specifies a text alignment to be applied when annotating with text.
    /// </summary>
    public sealed class DrawableTextAlignment : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextAlignment"/> class.
        /// </summary>
        /// <param name="alignment">Text alignment.</param>
        public DrawableTextAlignment(TextAlignment alignment)
        {
            Alignment = alignment;
        }

        /// <summary>
        /// Gets or sets text alignment.
        /// </summary>
        public TextAlignment Alignment { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.TextAlignment(Alignment);
    }
}