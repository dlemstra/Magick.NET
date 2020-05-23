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
    /// Extension methods for the <see cref="IMagickColor{QuantumType}"/> interface.
    /// </summary>
    public static class IMagickColorExtensions
    {
        /// <summary>
        /// Converts the specified <see cref="Color"/> to a <see cref="IMagickColor{QuantumType}"/> instance.
        /// </summary>
        /// <param name="self">The color.</param>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        public static void SetFromColor(this IMagickColor<QuantumType> self, Color color) => self?.SetFromBytes(color.R, color.G, color.B, color.A);

        /// <summary>
        /// Converts the value of this instance to an equivalent Color.
        /// </summary>
        /// <param name="self">The color.</param>
        /// <returns>A <see cref="Color"/> instance.</returns>
        public static Color ToColor(this IMagickColor<QuantumType> self)
        {
            if (self == null)
                return default;

            if (!self.IsCmyk)
                return Color.FromArgb(Quantum.ScaleToByte(self.A), Quantum.ScaleToByte(self.R), Quantum.ScaleToByte(self.G), Quantum.ScaleToByte(self.B));

            var r = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(self.R) * (Quantum.Max - self.K)) + self.K));
            var g = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(self.G) * (Quantum.Max - self.K)) + self.K));
            var b = Quantum.ScaleToQuantum(Quantum.Max - ((Quantum.ScaleToDouble(self.B) * (Quantum.Max - self.K)) + self.K));

            return Color.FromArgb(Quantum.ScaleToByte(self.A), Quantum.ScaleToByte(r), Quantum.ScaleToByte(g), Quantum.ScaleToByte(b));
        }
    }
}

#endif