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

using System;

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
    /// Class that represents a monochrome color.
    /// </summary>
    public sealed class ColorMono : ColorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorMono"/> class.
        /// </summary>
        /// <param name="isBlack">Specifies if the color is black or white.</param>
        public ColorMono(bool isBlack)
          : base(isBlack ? MagickColors.Black : MagickColors.White)
        {
            IsBlack = isBlack;
        }

        private ColorMono(MagickColor color)
          : base(color)
        {
            if (color == MagickColors.Black)
                IsBlack = true;
            else if (color == MagickColors.White)
                IsBlack = false;
            else
                throw new ArgumentException("Invalid color specified.", nameof(color));
        }

        /// <summary>
        /// Gets or sets a value indicating whether the color is black or white.
        /// </summary>
        public bool IsBlack
        {
            get;
            set;
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorMono"/> instance.</returns>
        public static implicit operator ColorMono(MagickColor color)
        {
            return FromMagickColor(color);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorMono"/> instance.</returns>
        public static ColorMono FromMagickColor(MagickColor color)
        {
            if (color == null)
                return null;

            return new ColorMono(color);
        }

        /// <summary>
        /// Updates the color value in an inherited class.
        /// </summary>
        protected override void UpdateColor()
        {
            QuantumType color = IsBlack ? (QuantumType)0.0 : Quantum.Max;
            Color.R = color;
            Color.G = color;
            Color.B = color;
        }
    }
}