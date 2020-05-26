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

using System.Drawing;

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
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        public static void SetFromColor<TQuantumType>(this IMagickColor<TQuantumType> self, Color color)
            => self?.SetFromBytes(color.R, color.G, color.B, color.A);

        /// <summary>
        /// Converts the value of this instance to an equivalent Color.
        /// </summary>
        /// <param name="self">The color.</param>
        /// <returns>A <see cref="Color"/> instance.</returns>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        public static Color ToColor<TQuantumType>(this IMagickColor<TQuantumType> self)
        {
            if (self == null)
                return default;

            var bytes = self.ToByteArray();

            if (!self.IsCmyk)
                return Color.FromArgb(bytes[3], bytes[0], bytes[1], bytes[2]);

            byte r = CmykToRgb(bytes, 0);
            byte g = CmykToRgb(bytes, 1);
            byte b = CmykToRgb(bytes, 2);

            return Color.FromArgb(bytes[4], r, g, b);
        }

        private static byte CmykToRgb(byte[] bytes, int index)
            => (byte)(255 - (((1.0 / 255) * bytes[index] * (255 - bytes[3])) + bytes[3]));
    }
}