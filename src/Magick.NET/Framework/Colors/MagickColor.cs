// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETSTANDARD

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Class that represents a color.
    /// </summary>
    public sealed partial class MagickColor
    {
        /// <summary>
        /// Converts the specified <see cref="Color"/> to a <see cref="MagickColor"/> instance.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        /// <returns>A <see cref="MagickColor"/> instance.</returns>
        public MagickColor FromColor(Color color)
        {
            Initialize(color.R, color.G, color.B, color.A);
            return this;
        }

        /// <summary>
        /// Determines whether the specified object is equal to the current color.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to compare this color with.</param>
        /// <returns>True when the specified object is equal to the current color.</returns>
        public bool Equals(Color color)
        {
            if (ReferenceEquals(this, color))
                return true;

            return Equals(color.ToColor());
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Color.
        /// </summary>
        /// <returns>A <see cref="Color"/> instance.</returns>
        public Color ToColor()
        {
            if (!_isCmyk)
                return Color.FromArgb(Quantum.ScaleToByte(A), Quantum.ScaleToByte(R), Quantum.ScaleToByte(G), Quantum.ScaleToByte(B));

            var r = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(R) * (Quantum.Max - K)) + K));
            var g = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(G) * (Quantum.Max - K)) + K));
            var b = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(B) * (Quantum.Max - K)) + K));

            return Color.FromArgb(Quantum.ScaleToByte(A), Quantum.ScaleToByte(r), Quantum.ScaleToByte(g), Quantum.ScaleToByte(b));
        }
    }
}

#endif