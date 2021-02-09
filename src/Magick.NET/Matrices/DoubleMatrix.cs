// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;

namespace ImageMagick
{
    /// <summary>
    /// Encapsulates a matrix of doubles.
    /// </summary>
    public abstract partial class DoubleMatrix
    {
        private readonly double[] _values;

        /// <summary>
        /// Initializes a new instance of the <see cref="DoubleMatrix"/> class.
        /// </summary>
        /// <param name="order">The order.</param>
        /// <param name="values">The values to initialize the matrix with.</param>
        protected DoubleMatrix(int order, double[] values)
        {
            Throw.IfTrue(nameof(order), order < 1, "Invalid order specified, value has to be at least 1.");

            Order = order;

            _values = new double[Order * Order];
            if (values != null)
            {
                Throw.IfFalse(nameof(values), (Order * Order) == values.Length, "Invalid number of values specified");
                Array.Copy(values, _values, _values.Length);
            }
        }

        /// <summary>
        /// Gets the order of the matrix.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Get or set the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        public double this[int x, int y]
        {
            get => GetValue(x, y);
            set => SetValue(x, y, value);
        }

        /// <summary>
        /// Gets the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <returns>The value at the specified x/y position.</returns>
        public double GetValue(int x, int y)
            => _values[GetIndex(x, y)];

        /// <summary>
        /// Set the column at the specified x position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="values">The values.</param>
        public void SetColumn(int x, params double[] values)
        {
            Throw.IfOutOfRange(nameof(x), x, Order);
            Throw.IfNull(nameof(values), values);
            Throw.IfTrue(nameof(values), values.Length != Order, "Invalid length");

            for (int y = 0; y < Order; y++)
            {
                SetValue(x, y, values[y]);
            }
        }

        /// <summary>
        /// Set the row at the specified y position.
        /// </summary>
        /// <param name="y">The y position.</param>
        /// <param name="values">The values.</param>
        public void SetRow(int y, params double[] values)
        {
            Throw.IfOutOfRange(nameof(y), y, Order);
            Throw.IfNull(nameof(values), values);
            Throw.IfTrue(nameof(values), values.Length != Order, "Invalid length");

            for (int x = 0; x < Order; x++)
            {
                SetValue(x, y, values[x]);
            }
        }

        /// <summary>
        /// Set the value at the specified x/y position.
        /// </summary>
        /// <param name="x">The x position.</param>
        /// <param name="y">The y position.</param>
        /// <param name="value">The value.</param>
        public void SetValue(int x, int y, double value)
            => _values[GetIndex(x, y)] = value;

        /// <summary>
        /// Returns a string that represents the current DoubleMatrix.
        /// </summary>
        /// <returns>The double array.</returns>
        public double[] ToArray()
            => _values;

        private static INativeInstance CreateNativeInstance(IDoubleMatrix instance)
            => new NativeDoubleMatrix(instance.ToArray(), instance.Order);

        private int GetIndex(int x, int y)
        {
            Throw.IfOutOfRange(nameof(x), x, Order);
            Throw.IfOutOfRange(nameof(y), y, Order);

            return (y * Order) + x;
        }
    }
}