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
    public partial class SafePixelCollectionTests
    {
        [TestClass]
        public class TheGetAreaMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                ThrowsArgumentException("x", -1, 0, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenXTooHigh()
            {
                ThrowsArgumentException("x", 6, 0, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenYTooLow()
            {
                ThrowsArgumentException("y", 0, -1, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenYTooHigh()
            {
                ThrowsArgumentException("y", 0, 11, 1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthTooLow()
            {
                ThrowsArgumentException("width", 0, 0, -1, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthZero()
            {
                ThrowsArgumentException("width", 0, 0, 0, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightTooLow()
            {
                ThrowsArgumentException("height", 0, 0, 1, -1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightZero()
            {
                ThrowsArgumentException("height", 0, 0, 1, 0);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenWidthAndOffsetTooHigh()
            {
                ThrowsArgumentException("width", 4, 0, 2, 1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenHeightAndOffsetTooHigh()
            {
                ThrowsArgumentException("height", 0, 9, 1, 2);
            }

            [TestMethod]
            public void ShouldReturnAreaWhenAreaIsValid()
            {
                using (var image = new MagickImage(Files.CirclePNG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var area = pixels.GetArea(28, 28, 2, 3);
                        int length = 2 * 3 * 4; // width * height * channelCount
                        MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                        Assert.AreEqual(length, area.Length);
                        ColorAssert.AreEqual(new MagickColor("#ffffff9f"), color);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            pixels.GetArea(null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnAreaWhenGeometryIsValid()
            {
                using (var image = new MagickImage(Files.RedPNG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var area = pixels.GetArea(new MagickGeometry(0, 0, 6, 5));
                        int length = 6 * 5 * 4; // width * height * channelCount
                        MagickColor color = new MagickColor(area[0], area[1], area[2], area[3]);

                        Assert.AreEqual(length, area.Length);
                        ColorAssert.AreEqual(MagickColors.Red, color);
                    }
                }
            }

            private static void ThrowsArgumentException(string paramName, int x, int y, int width, int height)
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>(paramName, () =>
                        {
                            pixels.GetArea(x, y, width, height);
                        });
                    }
                }
            }
        }
    }
}
