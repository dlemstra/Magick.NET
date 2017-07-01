// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    ///  Class that represents a gray color.
    /// </summary>
    public sealed class ColorGray : ColorBase
    {
        private double _shade;

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorGray"/> class.
        /// </summary>
        /// <param name="shade">Value between 0.0 - 1.0.</param>
        public ColorGray(double shade)
          : base(new MagickColor(0, 0, 0))
        {
            Throw.IfTrue(nameof(shade), shade < 0.0 || shade > 1.0, "Invalid shade specified");

            _shade = shade;
        }

        private ColorGray(MagickColor color)
          : base(color)
        {
            _shade = Quantum.ScaleToQuantum(color.R);
        }

        /// <summary>
        /// Gets or sets the shade of this color (value between 0.0 - 1.0).
        /// </summary>
        public double Shade
        {
            get
            {
                return _shade;
            }

            set
            {
                if (value < 0.0 || value > 1.0)
                    return;

                _shade = value;
            }
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorGray"/> instance.</returns>
        public static implicit operator ColorGray(MagickColor color)
        {
            return FromMagickColor(color);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorGray"/> instance.</returns>
        public static ColorGray FromMagickColor(MagickColor color)
        {
            if (color == null)
                return null;

            return new ColorGray(color);
        }

        /// <summary>
        /// Updates the color value in an inherited class.
        /// </summary>
        protected override void UpdateColor()
        {
            QuantumType gray = Quantum.ScaleToQuantum(_shade);
            Color.R = gray;
            Color.G = gray;
            Color.B = gray;
        }
    }
}