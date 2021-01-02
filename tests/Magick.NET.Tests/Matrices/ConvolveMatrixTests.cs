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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ConvolveMatrixTests
    {
        [Fact]
        public void Constructor_OrderIsTooLow_ThrowsException()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(0);
            });
        }

        [Fact]
        public void Constructor_OrderIsNotAnOddNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(2);
            });
        }

        [Fact]
        public void Constructor_NotEnoughValues_ThrowsException()
        {
            Assert.Throws<ArgumentException>("values", () =>
            {
                new ConvolveMatrix(3, 1.0);
            });
        }

        [Fact]
        public void Constructor_ValidOrder_PropertiesAreSet()
        {
            var matrix = new ConvolveMatrix(3, 0.0, 1.0, 2.0, 0.1, 1.1, 2.1, 0.2, 1.2, 2.2);

            Assert.Equal(3, matrix.Order);
            Assert.Equal(0.0, matrix.GetValue(0, 0));
            Assert.Equal(1.0, matrix.GetValue(1, 0));
            Assert.Equal(2.0, matrix.GetValue(2, 0));
            Assert.Equal(0.1, matrix.GetValue(0, 1));
            Assert.Equal(1.1, matrix.GetValue(1, 1));
            Assert.Equal(2.1, matrix.GetValue(2, 1));
            Assert.Equal(0.2, matrix.GetValue(0, 2));
            Assert.Equal(1.2, matrix.GetValue(1, 2));
            Assert.Equal(2.2, matrix.GetValue(2, 2));
        }

        [Fact]
        public void ConstructorWithValues_OrderIsTooLow_ThrowsException()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(0, 1);
            });
        }

        [Fact]
        public void ConstructorWithValues_OrderIsNotAnOddNumber_ThrowsException()
        {
            Assert.Throws<ArgumentException>("order", () =>
            {
                new ConvolveMatrix(2, 1, 2, 3, 4);
            });
        }

        [Fact]
        public void Indexer_XTooLow_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [Fact]
        public void Indexer_XTooHigh_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [Fact]
        public void Indexer_YTooLow_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [Fact]
        public void Indexer_YTooHigh_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [Fact]
        public void Indexer_ValidIndex_ReturnValue()
        {
            var matrix = new ConvolveMatrix(1, 8);

            Assert.Equal(8, matrix[0, 0]);
        }

        [Fact]
        public void GetValue_XTooLow_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [Fact]
        public void GetValue_XTooHigh_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [Fact]
        public void GetValue_YTooLow_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [Fact]
        public void GetValue_YTooHigh_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [Fact]
        public void GetValue_ValidIndex_ReturnValue()
        {
            var matrix = new ConvolveMatrix(1, 4);

            Assert.Equal(4, matrix.GetValue(0, 0));
        }

        [Fact]
        public void SetColumn_YTooLow_ThrowsException()
        {
            SetColumn_InvalidColumn_ThrowsException(-1);
        }

        [Fact]
        public void SetColumn_YTooHigh_ThrowsException()
        {
            SetColumn_InvalidColumn_ThrowsException(2);
        }

        [Fact]
        public void SetColumn_ValuesIsNull_ThrowsException()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentNullException>("values", () =>
            {
                matrix.SetColumn(0, null);
            });
        }

        [Fact]
        public void SetColumn_InvalidNumberOfValues_ThrowsException()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentException>("values", () =>
            {
                matrix.SetColumn(0, 1, 2, 3);
            });
        }

        [Fact]
        public void SetColumn_CorrectNumberOfValues_SetsColumn()
        {
            var matrix = new ConvolveMatrix(3);

            matrix.SetColumn(1, 6, 8, 10);
            Assert.Equal(0, matrix.GetValue(0, 0));
            Assert.Equal(0, matrix.GetValue(0, 1));
            Assert.Equal(0, matrix.GetValue(0, 2));
            Assert.Equal(6, matrix.GetValue(1, 0));
            Assert.Equal(8, matrix.GetValue(1, 1));
            Assert.Equal(10, matrix.GetValue(1, 2));
            Assert.Equal(0, matrix.GetValue(2, 0));
            Assert.Equal(0, matrix.GetValue(2, 1));
            Assert.Equal(0, matrix.GetValue(2, 2));
        }

        [Fact]
        public void SetRow_XTooLow_ThrowsException()
        {
            SetRow_InvalidRow_ThrowsException(-1);
        }

        [Fact]
        public void SetRow_XTooHigh_ThrowsException()
        {
            SetRow_InvalidRow_ThrowsException(2);
        }

        [Fact]
        public void SetRowValuesIsNull_ThrowsException()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentNullException>("values", () =>
            {
                matrix.SetRow(0, null);
            });
        }

        [Fact]
        public void SetRow_InvalidNumberOfValues_ThrowsException()
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentException>("values", () =>
            {
                matrix.SetRow(0, 1, 2, 3);
            });
        }

        [Fact]
        public void SetRow_CorrectNumberOfValues_SetsColumn()
        {
            var matrix = new ConvolveMatrix(3);

            matrix.SetRow(1, 6, 8, 10);
            Assert.Equal(0, matrix.GetValue(0, 0));
            Assert.Equal(6, matrix.GetValue(0, 1));
            Assert.Equal(0, matrix.GetValue(0, 2));
            Assert.Equal(0, matrix.GetValue(1, 0));
            Assert.Equal(8, matrix.GetValue(1, 1));
            Assert.Equal(0, matrix.GetValue(1, 2));
            Assert.Equal(0, matrix.GetValue(2, 0));
            Assert.Equal(10, matrix.GetValue(2, 1));
            Assert.Equal(0, matrix.GetValue(2, 2));
        }

        [Fact]
        public void SetValue_XTooLow_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [Fact]
        public void SetValue_XTooHigh_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [Fact]
        public void SetValue_YTooLow_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [Fact]
        public void SetValue_YTooHigh_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [Fact]
        public void SetValue_ValidIndex_SetsValue()
        {
            var matrix = new ConvolveMatrix(3);

            matrix.SetValue(1, 2, 1.5);

            Assert.Equal(0.0, matrix.GetValue(0, 0));
            Assert.Equal(0.0, matrix.GetValue(0, 1));
            Assert.Equal(0.0, matrix.GetValue(0, 2));
            Assert.Equal(0.0, matrix.GetValue(1, 0));
            Assert.Equal(0.0, matrix.GetValue(1, 1));
            Assert.Equal(1.5, matrix.GetValue(1, 2));
            Assert.Equal(0.0, matrix.GetValue(2, 0));
            Assert.Equal(0.0, matrix.GetValue(2, 1));
            Assert.Equal(0.0, matrix.GetValue(2, 2));
        }

        [Fact]
        public void ToArray_ReturnsArray()
        {
            var matrix = new ConvolveMatrix(1, 6);

            Assert.Equal(new double[] { 6 }, matrix.ToArray());
        }

        private static void Indexer_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () =>
            {
                double foo = matrix[x, y];
            });
        }

        private static void GetValue_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () =>
            {
                matrix.GetValue(x, y);
            });
        }

        private static void SetColumn_InvalidColumn_ThrowsException(int x)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>("x", () =>
            {
                matrix.SetColumn(x, 1.0, 2.0);
            });
        }

        private static void SetRow_InvalidRow_ThrowsException(int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>("y", () =>
            {
                matrix.SetRow(y, 1.0, 2.0);
            });
        }

        private static void SetValue_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            var matrix = new ConvolveMatrix(1);

            Assert.Throws<ArgumentOutOfRangeException>(paramName, () =>
            {
                matrix.SetValue(x, y, 1);
            });
        }
    }
}
