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
        public class TheToByteArrayMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentOutOfRangeException>("x", () =>
                        {
                            pixels.ToByteArray(-1, 0, 1, 1, "RGB");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnPixelsWhenAreaIsCorrect()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray(60, 60, 63, 58, "RGBA");
                        int length = 63 * 58 * 4;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnPixelsWhenAreaIsCorrectAndMappingIsEnum()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray(60, 60, 63, 58, PixelMapping.RGBA);
                        int length = 63 * 58 * 4;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            pixels.ToByteArray(null, "RGB");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsNullAndMappingIsEnum()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("geometry", () =>
                        {
                            pixels.ToByteArray(null, PixelMapping.RGB);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("mapping", () =>
                        {
                            pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("mapping", () =>
                        {
                            pixels.ToByteArray(new MagickGeometry(1, 2, 3, 4), string.Empty);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenGeometryIsCorrect()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray(new MagickGeometry(10, 10, 113, 108), "RG");
                        int length = 113 * 108 * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenGeometryIsCorrectAndMappingIsEnum()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray(new MagickGeometry(10, 10, 113, 108), PixelMapping.RGB);
                        int length = 113 * 108 * 3;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsNull()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("mapping", () =>
                        {
                            pixels.ToByteArray(null);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<ArgumentException>("mapping", () =>
                        {
                            pixels.ToByteArray(string.Empty);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsInvalid()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        ExceptionAssert.Throws<MagickOptionErrorException>(() =>
                        {
                            pixels.ToByteArray("FOO");
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenTwoChannelsAreSupplied()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray("RG");
                        int length = image.Width * image.Height * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenTwoChannelsAreSuppliedAndMappingIsEnum()
            {
                using (var image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (var pixels = image.GetPixels())
                    {
                        var values = pixels.ToByteArray(PixelMapping.RGB);
                        int length = image.Width * image.Height * 3;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }
        }
    }
}
