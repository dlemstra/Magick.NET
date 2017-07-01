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
    /// Class that represents a YUV color.
    /// </summary>
    public sealed class ColorYUV : ColorBase
    {
        private double _U;
        private double _V;
        private double _Y;

        private ColorYUV(MagickColor color)
          : base(color)
        {
            _Y = (1.0 / Quantum.Max) * ((0.298839 * color.R) + (0.586811 * color.G) + (0.11435 * color.B));
            _U = ((1.0 / Quantum.Max) * ((-0.147 * color.R) - (0.289 * color.G) + (0.436 * color.B))) + 0.5;
            _V = ((1.0 / Quantum.Max) * ((0.615 * color.R) - (0.515 * color.G) - (0.1 * color.B))) + 0.5;
        }

        /// <summary>
        /// Updates the color value in an inherited class.
        /// </summary>
        protected override void UpdateColor()
        {
            Color.R = Quantum.ScaleToQuantum(_Y - (3.945707070708279e-05 * (_U - 0.5)) + (1.1398279671717170825 * (_V - 0.5)));
            Color.G = Quantum.ScaleToQuantum(_Y - (0.3946101641414141437 * (_U - 0.5)) - (0.5805003156565656797 * (_V - 0.5)));
            Color.B = Quantum.ScaleToQuantum(_Y + (2.0319996843434342537 * (_U - 0.5)) - (4.813762626262513e-04 * (_V - 0.5)));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorYUV"/> class.
        /// </summary>
        /// <param name="y">Y component value of this color.</param>
        /// <param name="u">U component value of this color.</param>
        /// <param name="v">V component value of this color.</param>
        public ColorYUV(double y, double u, double v)
          : base(new MagickColor(0, 0, 0))
        {
            _Y = y;
            _U = u;
            _V = v;
        }

        /// <summary>
        /// Gets or sets the U component value of this color. (value beteeen -0.5 and 0.5)
        /// </summary>
        public double U
        {
            get
            {
                return _U;
            }
            set
            {
                _U = value;
            }
        }

        /// <summary>
        /// Gets or sets the V component value of this color. (value beteeen -0.5 and 0.5)
        /// </summary>
        public double V
        {
            get
            {
                return _V;
            }
            set
            {
                _V = value;
            }
        }

        /// <summary>
        /// Gets or sets the Y component value of this color. (value beteeen 0.0 and 1.0)
        /// </summary>
        public double Y
        {
            get
            {
                return _Y;
            }
            set
            {
                _Y = value;
            }
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorYUV"/> instance.</returns>
        public static implicit operator ColorYUV(MagickColor color)
        {
            return FromMagickColor(color);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorYUV"/> instance.</returns>
        public static ColorYUV FromMagickColor(MagickColor color)
        {
            if (color == null)
                return null;

            return new ColorYUV(color);
        }
    }
}
