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

namespace ImageMagick
{
    /// <summary>
    /// Represents an argument for the SparseColor method.
    /// </summary>
    public sealed class SparseColorArg
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SparseColorArg"/> class.
        /// </summary>
        /// <param name="x">The X position.</param>
        /// <param name="y">The Y position.</param>
        /// <param name="color">The color.</param>
        public SparseColorArg(double x, double y, IMagickColor color)
        {
            Throw.IfNull(nameof(color), color);

            X = x;
            Y = y;
            Color = color;
        }

        /// <summary>
        /// Gets or sets the X position.
        /// </summary>
        public double X { get; set; }

        /// <summary>
        /// Gets or sets the Y position.
        /// </summary>
        public double Y { get; set; }

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        public IMagickColor Color { get; set; }
    }
}