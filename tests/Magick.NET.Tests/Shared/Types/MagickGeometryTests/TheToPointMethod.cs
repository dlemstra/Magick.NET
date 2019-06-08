// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public partial class MagickGeometryTests
    {
        [TestClass]
        public class TheToPointMethod
        {
            [TestMethod]
            public void ShouldReturnZeroWhenXAndYNotSet()
            {
                var point = new MagickGeometry(10, 5).ToPoint();

                Assert.AreEqual(0, point.X);
                Assert.AreEqual(0, point.Y);
            }

            [TestMethod]
            public void ShouldReturnCorrectValue()
            {
                var point = new MagickGeometry(1, 2, 3, 4).ToPoint();

                Assert.AreEqual(1, point.X);
                Assert.AreEqual(2, point.Y);
            }
        }
    }
}
