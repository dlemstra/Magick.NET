// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Encapsulates a color matrix in the order of 1 to 6 (1x1 through 6x6).
    /// </summary>
    public sealed class MagickColorMatrix : DoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColorMatrix"/> class.
        /// </summary>
        /// <param name="order">The order (1 to 6).</param>
        public MagickColorMatrix(int order)
          : base(order, null)
        {
            CheckOrder(order);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MagickColorMatrix"/> class.
        /// </summary>
        /// <param name="order">The order (1 to 6).</param>
        /// <param name="values">The values to initialize the matrix with.</param>
        public MagickColorMatrix(int order, params double[] values)
          : base(order, values)
        {
            CheckOrder(order);
        }

        private static void CheckOrder(int order)
        {
            Throw.IfTrue(nameof(order), order > 6, "Invalid order specified, range 1-6.");
        }
    }
}