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
    /// Encapsulates a matrix of doubles.
    /// </summary>
    public interface IDoubleMatrix
    {
        /// <summary>
        /// Gets the order of the matrix.
        /// </summary>
        int Order { get; }

        /// <summary>
        /// Get or set the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        double this[int x, int y] { get; set; }

        /// <summary>
        /// Gets the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>The value at the specified x/y position.</returns>
        double GetValue(int x, int y);

        /// <summary>
        /// Set the column at the specified x position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="values">The values.</param>
        void SetColumn(int x, params double[] values);

        /// <summary>
        /// Set the row at the specified y position.
        /// </summary>
        /// <param name="y">The y position.</param>
        /// <param name="values">The values.</param>
        void SetRow(int y, params double[] values);

        /// <summary>
        /// Set the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="value">The value.</param>
        void SetValue(int x, int y, double value);

        /// <summary>
        /// Returns a string that represents the current DoubleMatrix.
        /// </summary>
        /// <returns>The double array.</returns>
        double[] ToArray();
    }
}