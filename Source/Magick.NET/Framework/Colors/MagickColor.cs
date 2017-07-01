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

#if !NETSTANDARD1_3

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Class that represents a color.
    /// </summary>
    public sealed partial class MagickColor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColor"/> class.
        /// </summary>
        /// <param name="color">The color to use.</param>
        public MagickColor(Color color)
        {
            Initialize(color.R, color.G, color.B, color.A);
        }

        /// <summary>
        /// Converts the specified <see cref="MagickColor"/> to a <see cref="Color"/> instance.
        /// </summary>
        /// <param name="color">The <see cref="MagickColor"/> to convert.</param>
        public static implicit operator Color(MagickColor color)
        {
            if (ReferenceEquals(color, null))
                return Color.Empty;

            return color.ToColor();
        }

        /// <summary>
        /// Converts the specified <see cref="Color"/> to a <see cref="MagickColor"/> instance.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> to convert.</param>
        public static implicit operator MagickColor(Color color)
        {
            return new MagickColor(color);
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent Color.
        /// </summary>
        /// <returns>A <see cref="Color"/> instance.</returns>
        public Color ToColor()
        {
            return Color.FromArgb(Quantum.ScaleToByte(A), Quantum.ScaleToByte(R), Quantum.ScaleToByte(G), Quantum.ScaleToByte(B));
        }
    }
}

#endif