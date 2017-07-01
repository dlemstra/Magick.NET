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

namespace ImageMagick
{
    /// <summary>
    /// Encapsulates a convolution kernel.
    /// </summary>
    public sealed class ConvolveMatrix : DoubleMatrix
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConvolveMatrix"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        public ConvolveMatrix(int order)
          : base(order, null)
        {
            CheckOrder(order);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvolveMatrix"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="values">The values to initialize the matrix with.</param>
        public ConvolveMatrix(int order, params double[] values)
          : base(order, values)
        {
            CheckOrder(order);
        }

        private static void CheckOrder(int order)
        {
            Throw.IfTrue(nameof(order), order < 1, "Invalid order specified, value has to be at least 1.");
            Throw.IfTrue(nameof(order), order % 2 == 0, "Order must be an odd number.");
        }
    }
}