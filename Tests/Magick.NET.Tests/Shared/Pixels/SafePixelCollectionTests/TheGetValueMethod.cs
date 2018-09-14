// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public partial class SafePixelCollectionTests
    {
        [TestClass]
        public class TheGetValueMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                ThrowsArgumentOutOfRangeException("x", -1, 0);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenXTooHigh()
            {
                ThrowsArgumentOutOfRangeException("x", 6, 0);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenYTooLow()
            {
                ThrowsArgumentOutOfRangeException("y", 0, -1);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenYTooHigh()
            {
                ThrowsArgumentOutOfRangeException("y", 0, 11);
            }

            [TestMethod]
            public void ShouldReturnCorrectValue()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 1, 1))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var pixel = pixels.GetValue(0, 0);

                        Assert.AreEqual(3, pixel.Length);
                        Assert.AreEqual(Quantum.Max, pixel[0]);
                        Assert.AreEqual(0, pixel[1]);
                        Assert.AreEqual(0, pixel[2]);
                    }
                }
            }

            private static void ThrowsArgumentOutOfRangeException(string paramName, int x, int y)
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentOutOfRangeException(paramName, () =>
                        {
                            pixels.GetValue(x, y);
                        });
                    }
                }
            }
        }
    }
}
