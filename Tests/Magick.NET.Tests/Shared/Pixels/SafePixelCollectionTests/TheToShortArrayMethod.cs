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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class SafePixelCollectionTests
    {
        [TestClass]
        public class TheToShortArrayMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            pixels.ToShortArray(-1, 0, 1, 1, "RGB");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnPixelsWhenAreaIsCorrect()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray(60, 60, 63, 58, "RGBA");
                        int length = 63 * 58 * 4;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnPixelsWhenAreaIsCorrectAndMappingIsEnum()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray(60, 60, 63, 58, PixelMapping.RGBA);
                        int length = 63 * 58 * 4;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                        {
                            pixels.ToShortArray(null, "RGB");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNullAndMappingIsEnum()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("geometry", () =>
                        {
                            pixels.ToShortArray(null, PixelMapping.RGB);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                        {
                            pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentException("mapping", () =>
                        {
                            pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), string.Empty);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenGeometryIsCorrect()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), "RG");
                        int length = 113 * 108 * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenGeometryIsCorrectAndMappingIsEnum()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), PixelMapping.RGB);
                        var length = 113 * 108 * 3;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("mapping", () =>
                        {
                            pixels.ToShortArray(null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.ThrowsArgumentException("mapping", () =>
                        {
                            pixels.ToShortArray(string.Empty);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsInvalid()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<MagickOptionErrorException>(() =>
                        {
                            pixels.ToShortArray("FOO");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenTwoChannelsAreSupplied()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray("RG");
                        int length = image.Width * image.Height * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenTwoChannelsAreSuppliedAndMappingIsEnum()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixels())
                    {
                        var values = pixels.ToShortArray(PixelMapping.RGB);
                        int length = image.Width * image.Height * 3;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }
        }
    }
}
