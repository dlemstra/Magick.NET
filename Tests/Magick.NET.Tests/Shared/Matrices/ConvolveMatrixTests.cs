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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ConvolveMatrixTests
    {
        [TestMethod]
        public void Test_Constructor()
        {
            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(-1);
            });

            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(6);
            });

            new ConvolveMatrix(1);

            ExceptionAssert.ThrowsArgumentException("values", () =>
            {
                new ConvolveMatrix(2, 1.0);
            });

            ConvolveMatrix matrix = new ConvolveMatrix(3,
              0.0, 1.0, 2.0,
              0.1, 1.1, 2.1,
              0.2, 1.2, 2.2);

            Test_Values(matrix);

            ExceptionAssert.ThrowsArgumentException("order", () =>
            {
                new ConvolveMatrix(2, null);
            });

            matrix = new ConvolveMatrix(3, new double[]
            {
                0.0, 1.0, 2.0,
                0.1, 1.1, 2.1,
                0.2, 1.2, 2.2,
            });

            Test_Values(matrix);
        }

        private static void Test_Values(ConvolveMatrix matrix)
        {
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
    }
}
