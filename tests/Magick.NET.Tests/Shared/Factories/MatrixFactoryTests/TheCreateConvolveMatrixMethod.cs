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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MatrixFactoryTests
    {
        [TestClass]
        public class TheCreateConvolveMatrixMethod
        {
            [TestMethod]
            public void ShouldCreateInstance()
            {
                var factory = new MatrixFactory();

                var matrix = factory.CreateConvolveMatrix(1);

                Assert.IsNotNull(matrix);
                Assert.IsInstanceOfType(matrix, typeof(ConvolveMatrix));
            }

            [TestMethod]
            public void ShouldCreateInstanceWithValues()
            {
                var factory = new MatrixFactory();

                var matrix = factory.CreateConvolveMatrix(1, 2);

                Assert.IsNotNull(matrix);
                Assert.IsInstanceOfType(matrix, typeof(ConvolveMatrix));
                Assert.AreEqual(2, matrix.GetValue(0, 0));
            }
        }
    }
}
