// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    /// Class that can be used to create various matrix instances.
    /// </summary>
    public interface IMatrixFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColorMatrix"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickColorMatrix"/> instance.</returns>
        /// <param name="order">The order (1 to 6).</param>
        IMagickColorMatrix CreateColorMatrix(int order);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickColorMatrix"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickColorMatrix"/> instance.</returns>
        /// <param name="order">The order (1 to 6).</param>
        /// <param name="values">The values to initialize the matrix with.</param>
        IMagickColorMatrix CreateColorMatrix(int order, params double[] values);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IConvolveMatrix"/>.
        /// </summary>
        /// <returns>A new <see cref="IConvolveMatrix"/> instance.</returns>
        /// <param name="order">The order (odd number).</param>
        IConvolveMatrix CreateConvolveMatrix(int order);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IConvolveMatrix"/>.
        /// </summary>
        /// <returns>A new <see cref="IConvolveMatrix"/> instance.</returns>
        /// <param name="order">The order (odd number).</param>
        /// <param name="values">The values to initialize the matrix with.</param>
        IConvolveMatrix CreateConvolveMatrix(int order, params double[] values);
    }
}
