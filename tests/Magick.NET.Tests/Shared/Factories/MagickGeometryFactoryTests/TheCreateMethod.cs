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
    public partial class MagickGeometryFactoryTests
    {
        [TestClass]
        public partial class TheCreateMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                var factory = new MagickGeometryFactory();

                ExceptionAssert.Throws<ArgumentNullException>("value", () => factory.Create(null));
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                var factory = new MagickGeometryFactory();
                ExceptionAssert.Throws<ArgumentException>("value", () => factory.Create(string.Empty));
            }

            [TestMethod]
            public void ShouldSetIgnoreAspectRatio()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("5x10!");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.IsTrue(geometry.IgnoreAspectRatio);
            }

            [TestMethod]
            public void ShouldSetLess()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("10x5+2+1<");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(5, geometry.Height);
                Assert.IsTrue(geometry.Less);
            }

            [TestMethod]
            public void ShouldSetGreater()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("5x10>");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.IsTrue(geometry.Greater);
            }

            [TestMethod]
            public void ShouldSetFillArea()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("10x15^");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(15, geometry.Height);
                Assert.IsTrue(geometry.FillArea);
            }

            [TestMethod]
            public void ShouldSetLimitPixels()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("10@");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(10, geometry.Width);
                Assert.AreEqual(0, geometry.Height);
                Assert.IsTrue(geometry.LimitPixels);
            }

            [TestMethod]
            public void ShouldSetGreaterAndIsPercentage()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("50%x0>");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(50, geometry.Width);
                Assert.AreEqual(0, geometry.Height);
                Assert.IsTrue(geometry.IsPercentage);
                Assert.IsTrue(geometry.Greater);
            }

            [TestMethod]
            public void ShouldSetAspectRatio()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("3:2");

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(3, geometry.Width);
                Assert.AreEqual(2, geometry.Height);
                Assert.IsTrue(geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithOnlyXOffset()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("4:3+2");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.IsTrue(geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithOffset()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("4:3+2+1");

                Assert.AreEqual(2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.IsTrue(geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetAspectRatioWithNegativeOffset()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create("4:3-2+1");

                Assert.AreEqual(-2, geometry.X);
                Assert.AreEqual(1, geometry.Y);
                Assert.AreEqual(4, geometry.Width);
                Assert.AreEqual(3, geometry.Height);
                Assert.IsTrue(geometry.AspectRatio);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeightWhenSizeIsSupplied()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create(5);

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(5, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeight()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create(5, 10);

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(5, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetXAndY()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create(5, 10, 15, 20);

                Assert.AreEqual(5, geometry.X);
                Assert.AreEqual(10, geometry.Y);
                Assert.AreEqual(15, geometry.Width);
                Assert.AreEqual(20, geometry.Height);
            }

            [TestMethod]
            public void ShouldSetWidthAndHeightAndIsPercentage()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create(new Percentage(50.0), new Percentage(10.0));

                Assert.AreEqual(0, geometry.X);
                Assert.AreEqual(0, geometry.Y);
                Assert.AreEqual(50, geometry.Width);
                Assert.AreEqual(10, geometry.Height);
                Assert.IsTrue(geometry.IsPercentage);
            }

            [TestMethod]
            public void ShouldSetXAndYAndIsPercentage()
            {
                var factory = new MagickGeometryFactory();
                var geometry = factory.Create(5, 10, (Percentage)15.0, (Percentage)20.0);

                Assert.AreEqual(5, geometry.X);
                Assert.AreEqual(10, geometry.Y);
                Assert.AreEqual(15, geometry.Width);
                Assert.AreEqual(20, geometry.Height);
                Assert.IsTrue(geometry.IsPercentage);
            }
        }
    }
}
