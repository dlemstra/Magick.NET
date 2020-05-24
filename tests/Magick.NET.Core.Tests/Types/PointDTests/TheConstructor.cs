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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Core.Tests
{
    public partial class PointDTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("value", () => { new PointD(null); });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD(string.Empty); });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD("1.0x"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD("x1.0"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD("ax1.0"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD("1.0xb"); });

                ExceptionAssert.Throws<ArgumentException>("value", () => { new PointD("1.0x6 magick"); });
            }

            [TestMethod]
            public void ShouldSetTheXAndYToZeroByDefault()
            {
                PointD point = default;
                Assert.AreEqual(0.0, point.X);
                Assert.AreEqual(0.0, point.Y);
            }

            [TestMethod]
            public void ShouldSetTheXAndYValue()
            {
                var point = new PointD(5, 10);
                Assert.AreEqual(5.0, point.X);
                Assert.AreEqual(10.0, point.Y);
            }

            [TestMethod]
            public void ShouldUseTheXValueWhenTValueIsNotSet()
            {
                var point = new PointD(5);
                Assert.AreEqual(5.0, point.X);
                Assert.AreEqual(5.0, point.Y);
            }

            [TestMethod]
            public void ShouldSetTheValuesFromString()
            {
                var point = new PointD("1.0x2.5");
                Assert.AreEqual(1.0, point.X);
                Assert.AreEqual(2.5, point.Y);
                Assert.AreEqual("1x2.5", point.ToString());
            }
        }
    }
}
