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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ConvolveMatrixTests
    {
        [TestMethod]
        public void Constructor_OrderIsTooLow_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(0);
            });
        }

        [TestMethod]
        public void Constructor_OrderIsNotAnOddNumber_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(2);
            });
        }

        [TestMethod]
        public void Constructor_NotEnoughValues_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("values", () =>
            {
                new ConvolveMatrix(3, 1.0);
            });
        }

        [TestMethod]
        public void Constructor_ValidOrder_PropertiesAreSet()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(3, 0.0, 1.0, 2.0, 0.1, 1.1, 2.1, 0.2, 1.2, 2.2);

            Assert.AreEqual(3, matrix.Order);
            Assert.AreEqual(0.0, matrix.GetValue(0, 0));
            Assert.AreEqual(1.0, matrix.GetValue(1, 0));
            Assert.AreEqual(2.0, matrix.GetValue(2, 0));
            Assert.AreEqual(0.1, matrix.GetValue(0, 1));
            Assert.AreEqual(1.1, matrix.GetValue(1, 1));
            Assert.AreEqual(2.1, matrix.GetValue(2, 1));
            Assert.AreEqual(0.2, matrix.GetValue(0, 2));
            Assert.AreEqual(1.2, matrix.GetValue(1, 2));
            Assert.AreEqual(2.2, matrix.GetValue(2, 2));
        }

        [TestMethod]
        public void ConstructorWithValues_OrderIsTooLow_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(0, 1);
            });
        }

        [TestMethod]
        public void ConstructorWithValues_OrderIsNotAnOddNumber_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(2, 1, 2, 3, 4);
            });
        }

        [TestMethod]
        public void Indexer_XTooLow_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [TestMethod]
        public void Indexer_XTooHigh_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [TestMethod]
        public void Indexer_YTooLow_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [TestMethod]
        public void Indexer_YTooHigh_ThrowsException()
        {
            Indexer_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [TestMethod]
        public void Indexer_ValidIndex_ReturnValue()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1, 8);

            Assert.AreEqual(8, matrix[0, 0]);
        }

        [TestMethod]
        public void GetValue_XTooLow_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [TestMethod]
        public void GetValue_XTooHigh_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [TestMethod]
        public void GetValue_YTooLow_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [TestMethod]
        public void GetValue_YTooHigh_ThrowsException()
        {
            GetValue_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [TestMethod]
        public void GetValue_ValidIndex_ReturnValue()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1, 4);

            Assert.AreEqual(4, matrix.GetValue(0, 0));
        }

        [TestMethod]
        public void SetColumn_YTooLow_ThrowsException()
        {
            SetColumn_InvalidColumn_ThrowsException(-1);
        }

        [TestMethod]
        public void SetColumn_YTooHigh_ThrowsException()
        {
            SetColumn_InvalidColumn_ThrowsException(2);
        }

        [TestMethod]
        public void SetColumn_ValuesIsNull_ThrowsException()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentNullException("values", () =>
            {
                matrix.SetColumn(0, null);
            });
        }

        [TestMethod]
        public void SetColumn_InvalidNumberOfValues_ThrowsException()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentException("values", () =>
            {
                matrix.SetColumn(0, 1, 2, 3);
            });
        }

        [TestMethod]
        public void SetColumn_CorrectNumberOfValues_SetsColumn()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(3);

            matrix.SetColumn(1, 6, 8, 10);
            Assert.AreEqual(0, matrix.GetValue(0, 0));
            Assert.AreEqual(0, matrix.GetValue(0, 1));
            Assert.AreEqual(0, matrix.GetValue(0, 2));
            Assert.AreEqual(6, matrix.GetValue(1, 0));
            Assert.AreEqual(8, matrix.GetValue(1, 1));
            Assert.AreEqual(10, matrix.GetValue(1, 2));
            Assert.AreEqual(0, matrix.GetValue(2, 0));
            Assert.AreEqual(0, matrix.GetValue(2, 1));
            Assert.AreEqual(0, matrix.GetValue(2, 2));
        }

        [TestMethod]
        public void SetRow_XTooLow_ThrowsException()
        {
            SetRow_InvalidRow_ThrowsException(-1);
        }

        [TestMethod]
        public void SetRow_XTooHigh_ThrowsException()
        {
            SetRow_InvalidRow_ThrowsException(2);
        }

        [TestMethod]
        public void SetRowValuesIsNull_ThrowsException()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentNullException("values", () =>
            {
                matrix.SetRow(0, null);
            });
        }

        [TestMethod]
        public void SetRow_InvalidNumberOfValues_ThrowsException()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentException("values", () =>
            {
                matrix.SetRow(0, 1, 2, 3);
            });
        }

        [TestMethod]
        public void SetRow_CorrectNumberOfValues_SetsColumn()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(3);

            matrix.SetRow(1, 6, 8, 10);
            Assert.AreEqual(0, matrix.GetValue(0, 0));
            Assert.AreEqual(6, matrix.GetValue(0, 1));
            Assert.AreEqual(0, matrix.GetValue(0, 2));
            Assert.AreEqual(0, matrix.GetValue(1, 0));
            Assert.AreEqual(8, matrix.GetValue(1, 1));
            Assert.AreEqual(0, matrix.GetValue(1, 2));
            Assert.AreEqual(0, matrix.GetValue(2, 0));
            Assert.AreEqual(10, matrix.GetValue(2, 1));
            Assert.AreEqual(0, matrix.GetValue(2, 2));
        }

        [TestMethod]
        public void SetValue_XTooLow_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("x", -1, 0);
        }

        [TestMethod]
        public void SetValue_XTooHigh_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("x", 1, 0);
        }

        [TestMethod]
        public void SetValue_YTooLow_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("y", 0, -1);
        }

        [TestMethod]
        public void SetValue_YTooHigh_ThrowsException()
        {
            SetValue_InvalidIndex_ThrowsException("y", 0, 1);
        }

        [TestMethod]
        public void SetValue_ValidIndex_SetsValue()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(3);

            matrix.SetValue(1, 2, 1.5);

            Assert.AreEqual(0.0, matrix.GetValue(0, 0));
            Assert.AreEqual(0.0, matrix.GetValue(0, 1));
            Assert.AreEqual(0.0, matrix.GetValue(0, 2));
            Assert.AreEqual(0.0, matrix.GetValue(1, 0));
            Assert.AreEqual(0.0, matrix.GetValue(1, 1));
            Assert.AreEqual(1.5, matrix.GetValue(1, 2));
            Assert.AreEqual(0.0, matrix.GetValue(2, 0));
            Assert.AreEqual(0.0, matrix.GetValue(2, 1));
            Assert.AreEqual(0.0, matrix.GetValue(2, 2));
        }

        [TestMethod]
        public void ToArray_ReturnsArray()
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1, 6);

            CollectionAssert.AreEqual(new double[] { 6 }, matrix.ToArray());
        }

        private static void Indexer_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentOutOfRangeException(paramName, () =>
            {
                double foo = matrix[x, y];
            });
        }

        private static void GetValue_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentOutOfRangeException(paramName, () =>
            {
                matrix.GetValue(x, y);
            });
        }

        private static void SetColumn_InvalidColumn_ThrowsException(int x)
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentOutOfRangeException("x", () =>
            {
                matrix.SetColumn(x, 1.0, 2.0);
            });
        }

        private static void SetRow_InvalidRow_ThrowsException(int y)
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentOutOfRangeException("y", () =>
            {
                matrix.SetRow(y, 1.0, 2.0);
            });
        }

        private static void SetValue_InvalidIndex_ThrowsException(string paramName, int x, int y)
        {
            ConvolveMatrix matrix = new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentOutOfRangeException(paramName, () =>
            {
                matrix.SetValue(x, y, 1);
            });
        }
    }
}
