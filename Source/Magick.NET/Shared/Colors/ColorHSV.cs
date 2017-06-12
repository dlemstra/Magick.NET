//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

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
    /// Class that represents a HSV color.
    /// </summary>
    public sealed class ColorHSV : ColorBase
    {
        private double _Hue;
        private double _Saturation;
        private double _Value;

        private void Initialize(double red, double green, double blue)
        {
            _Hue = 0.0;
            _Saturation = 0.0;
            _Value = 0.0;

            double min = Math.Min(Math.Min(red, green), blue);
            double max = Math.Max(Math.Max(red, green), blue);

            if (Math.Abs(max) < double.Epsilon)
                return;
            double delta = max - min;
            _Saturation = delta / max;
            _Value = (1.0 / Quantum.Max) * max;
            if (Math.Abs(delta) < double.Epsilon)
                return;
            if (Math.Abs(red - max) < double.Epsilon)
                _Hue = (green - blue) / delta;
            else if (Math.Abs(green - max) < double.Epsilon)
                _Hue = 2.0 + ((blue - red) / delta);
            else
                _Hue = 4.0 + ((red - green) / delta);
            _Hue /= 6.0;
            if (_Hue < 0.0)
                _Hue += 1.0;
        }

        private ColorHSV(MagickColor color)
          : base(color)
        {
            Initialize(color.R, color.G, color.B);
        }

        /// <summary>
        /// Updates the color value in an inherited class.
        /// </summary>
        protected override void UpdateColor()
        {
            if (Math.Abs(_Saturation) < double.Epsilon)
            {
                Color.R = Color.G = Color.B = Quantum.ScaleToQuantum(_Value);
                return;
            }

            double h = 6.0 * (_Hue - Math.Floor(_Hue));
            double f = h - Math.Floor(h);
            double p = _Value * (1.0 - _Saturation);
            double q = _Value * (1.0 - (_Saturation * f));
            double t = _Value * (1.0 - (_Saturation * (1.0 - f)));
            switch ((int)h)
            {
                case 0:
                default:
                    Color.R = Quantum.ScaleToQuantum(_Value);
                    Color.G = Quantum.ScaleToQuantum(t);
                    Color.B = Quantum.ScaleToQuantum(p);
                    break;
                case 1:
                    Color.R = Quantum.ScaleToQuantum(q);
                    Color.G = Quantum.ScaleToQuantum(_Value);
                    Color.B = Quantum.ScaleToQuantum(p);
                    break;
                case 2:
                    Color.R = Quantum.ScaleToQuantum(p);
                    Color.G = Quantum.ScaleToQuantum(_Value);
                    Color.B = Quantum.ScaleToQuantum(t);
                    break;
                case 3:
                    Color.R = Quantum.ScaleToQuantum(p);
                    Color.G = Quantum.ScaleToQuantum(q);
                    Color.B = Quantum.ScaleToQuantum(_Value);
                    break;
                case 4:
                    Color.R = Quantum.ScaleToQuantum(t);
                    Color.G = Quantum.ScaleToQuantum(p);
                    Color.B = Quantum.ScaleToQuantum(_Value);
                    break;
                case 5:
                    Color.R = Quantum.ScaleToQuantum(_Value);
                    Color.G = Quantum.ScaleToQuantum(p);
                    Color.B = Quantum.ScaleToQuantum(q);
                    break;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ColorHSV"/> class.
        /// </summary>
        /// <param name="hue">Hue component value of this color.</param>
        /// <param name="saturation">Saturation component value of this color.</param>
        /// <param name="value">Value component value of this color.</param>
        public ColorHSV(double hue, double saturation, double value)
              : base(new MagickColor(0, 0, 0))
        {
            _Hue = hue;
            _Saturation = saturation;
            _Value = value;
        }

        /// <summary>
        /// Gets or sets the hue component value of this color.
        /// </summary>
        public double Hue
        {
            get
            {
                return _Hue;
            }
            set
            {
                _Hue = value;
            }
        }

        /// <summary>
        /// Gets or sets the saturation component value of this color.
        /// </summary>
        public double Saturation
        {
            get
            {
                return _Saturation;
            }
            set
            {
                _Saturation = value;
            }
        }

        /// <summary>
        /// Gets or sets the value component value of this color.
        /// </summary>
        public double Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
            }
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorHSV"/> instance.</returns>
        public static implicit operator ColorHSV(MagickColor color)
        {
            return FromMagickColor(color);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to an instance of this type.
        /// </summary>
        /// <param name="color">The color to use.</param>
        /// <returns>A <see cref="ColorHSV"/> instance.</returns>
        public static ColorHSV FromMagickColor(MagickColor color)
        {
            if (color == null)
                return null;

            return new ColorHSV(color);
        }

        /// <summary>
        /// Performs a hue shift with the specified degrees.
        /// </summary>
        /// <param name="degrees">The degrees.</param>
        public void HueShift(double degrees)
        {
            _Hue += degrees / 360.0;

            while (_Hue >= 1.0)
                _Hue -= 1.0;

            while (_Hue < 0.0)
                _Hue += 1.0;
        }
    }
}
