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
    /// Controls whether text is antialiased. Text is antialiased by default.
    /// </summary>
    public sealed class DrawableTextAntialias : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextAntialias"/> class.
        /// </summary>
        /// <param name="isEnabled">True if text antialiasing is enabled otherwise false.</param>
        public DrawableTextAntialias(bool isEnabled)
        {
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// Gets or sets a value indicating whether text antialiasing is enabled or disabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.TextAntialias(IsEnabled);
    }
}