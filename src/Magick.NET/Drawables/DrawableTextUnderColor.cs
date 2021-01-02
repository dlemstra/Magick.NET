// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Specifies the color of a background rectangle to place under text annotations.
    /// </summary>
    public sealed partial class DrawableTextUnderColor : IDrawable, IDrawingWand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableTextUnderColor"/> class.
        /// </summary>
        /// <param name="color">The color to use.</param>
        public DrawableTextUnderColor(IMagickColor<QuantumType> color)
        {
            Throw.IfNull(nameof(color), color);

            Color = color;
        }

        /// <summary>
        /// Gets or sets the color to use.
        /// </summary>
        public IMagickColor<QuantumType> Color { get; set; }

        /// <summary>
        /// Draws this instance with the drawing wand.
        /// </summary>
        /// <param name="wand">The want to draw on.</param>
        void IDrawingWand.Draw(DrawingWand wand) => wand?.TextUnderColor(Color);
    }
}