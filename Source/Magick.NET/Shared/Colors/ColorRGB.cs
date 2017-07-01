// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
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
    /// Class that represents a RGB color.
    /// </summary>
    public sealed partial class ColorRGB : ColorBase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorRGB"/> class.
        /// </summary>
        /// <param name="value">The color to use.</param>
        public ColorRGB(MagickColor value)
          : base(value)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorRGB"/> class.
        /// </summary>
        /// <param name="red">Red component value of this color.</param>
        /// <param name="green">Green component value of this color.</param>
        /// <param name="blue">Blue component value of this color.</param>
        public ColorRGB(QuantumType red, QuantumType green, QuantumType blue)
          : base(new MagickColor(red, green, blue))
        {
        }

        /// <summary>
        /// Gets or sets the blue component value of this color.
        /// </summary>
        public QuantumType B
        {
            get
            {
                return Color.B;
            }
            set
            {
                Color.B = value;
            }
        }

        /// <summary>
        /// Gets or sets the green component value of this color.
        /// </summary>
        public QuantumType G
        {
            get
            {
                return Color.G;
            }
            set
            {
                Color.G = value;
            }
        }

        /// <summary>
        /// Gets or sets the red component value of this color.
        /// </summary>
        public QuantumType R
        {
            get
            {
                return Color.R;
            }
            set
            {
                Color.R = value;
            }
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorRGB"/> instance.</returns>
        public static implicit operator ColorRGB(MagickColor color)
        {
            return FromMagickColor(color);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorRGB"/> instance.</returns>
        public static ColorRGB FromMagickColor(MagickColor color)
        {
            if (color == null)
                return null;

            return new ColorRGB(color);
        }

        /// <summary>
        /// Returns the complementary color for this color.
        /// </summary>
        /// <returns>A <see cref="ColorRGB"/> instance.</returns>
        public ColorRGB ComplementaryColor()
        {
            ColorHSV hsv = ColorHSV.FromMagickColor(this);
            hsv.HueShift(180);
            return new ColorRGB(hsv);
        }
    }
}