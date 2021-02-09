// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

namespace ImageMagick
{
    /// <summary>
    /// Sets the spacing between line in text.
    /// </summary>
    public sealed class DrawableTextInterlineSpacing : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextInterlineSpacing"/> class.
        /// </summary>
        /// <param name="spacing">Spacing to use.</param>
        public DrawableTextInterlineSpacing(double spacing)
        {
            Spacing = spacing;
        }

        /// <summary>
        /// Gets or sets the spacing to use.
        /// </summary>
        public double Spacing { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.TextInterlineSpacing(Spacing);
    }
}