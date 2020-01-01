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

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        [TestClass]
        public partial class TheConstructor
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                ExceptionAssert.Throws<ArgumentNullException>("value", () => new MagickGeometry(null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                ExceptionAssert.Throws<ArgumentException>("value", () => new MagickGeometry(string.Empty));
            }

            [TestMethod]
            public void ShouldSetIgnoreAspectRatio()
            {
                var geometry = new MagickGeometry("5x10!");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.AreEqual(true, geometry.IgnoreAspectRatio);
            }

            [TestMethod]
            public void ShouldSetLess()
            {
                var geometry = new MagickGeometry("10x5+2+1<");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(5, geometry.Height);
                Assert.AreEqual(true, geometry.Less);
            }

            [TestMethod]
            public void ShouldSetGreater()
            {
                var geometry = new MagickGeometry("5x10>");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.AreEqual(true, geometry.Greater);
            }

            [TestMethod]
            public void ShouldSetFillArea()
            {
                var geometry = new MagickGeometry("10x15^");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(15, geometry.Height);
                Assert.AreEqual(true, geometry.FillArea);
            }

            [TestMethod]
            public void ShouldSetLimitPixels()
            {
                var geometry = new MagickGeometry("10@");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(0, geometry.Height);
                Assert.AreEqual(true, geometry.LimitPixels);
            }

            [TestMethod]
            public void ShouldSetGreaterAndIsPercentage()
            {
                var geometry = new MagickGeometry("50%x0>");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(50, geometry.Width);
                Assert.AreEqual(0, geometry.Height);
                Assert.AreEqual(true, geometry.IsPercentage);
                Assert.AreEqual(true, geometry.Greater);
            }

            [TestMethod]
            public void ShouldSetAspectRatio()
            {
                var geometry = new MagickGeometry("3:2");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(3, geometry.Width);
                Assert.AreEqual(2, geometry.Height);
                Assert.AreEqual(true, geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithOnlyXOffset()
            {
                var geometry = new MagickGeometry("4:3+2");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.AreEqual(true, geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithOffset()
            {
                var geometry = new MagickGeometry("4:3+2+1");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.AreEqual(true, geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithNegativeOffset()
            {
                var geometry = new MagickGeometry("4:3-2+1");

                Assert.AreEqual(-2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.AreEqual(true, geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeightWhenSizeIsSupplied()
            {
                var geometry = new MagickGeometry(5);

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(5, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeight()
            {
                var geometry = new MagickGeometry(5, 10);

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetXAndY()
            {
                var geometry = new MagickGeometry(5, 10, 15, 20);

                Assert.AreEqual(5, geometry.X);
                Assert.AreEqual(10, geometry.Y);
                Assert.AreEqual(15, geometry.Width);
                Assert.AreEqual(20, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeightAndIsPercentage()
            {
                var geometry = new MagickGeometry(new Percentage(50.0), new Percentage(10.0));

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(50, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.AreEqual(true, geometry.IsPercentage);
            }

            [TestMethod]
            public void ShouldSetXAndYAndIsPercentage()
            {
                var geometry = new MagickGeometry(5, 10, (Percentage)15.0, (Percentage)20.0);

                Assert.AreEqual(5, geometry.X);
                Assert.AreEqual(10, geometry.Y);
                Assert.AreEqual(15, geometry.Width);
                Assert.AreEqual(20, geometry.Height);
                Assert.AreEqual(true, geometry.IsPercentage);
            }
        }
    }
}
