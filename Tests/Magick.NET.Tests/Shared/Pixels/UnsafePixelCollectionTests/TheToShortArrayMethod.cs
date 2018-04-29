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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Pixels
{
    public partial class UnsafePixelCollectionTests
    {
        [TestClass]
        public class TheToShortArrayMethod
        {
            private static bool Is64Bit => IntPtr.Size == 8;

            [TestMethod]
            public void ShouldThrowExceptionWhenXTooLow()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        if (Is64Bit)
                        {
                            pixels.ToShortArray(-1, 0, 1, 1, "RGB");
                        }
                        else
                        {
                            ExceptionAssert.Throws<OverflowException>(() =>
                            {
                                pixels.ToShortArray(-1, 0, 1, 1, "RGB");
                            });
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnPixelsWhenAreaIsCorrecy()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray(60, 60, 63, 58, "RGBA");
                        int length = 63 * 58 * 4;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenGeometryIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray(null, "RGB");
                        Assert.IsNull(values);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenGeometryIsSpecifiedAndMappingIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), null);
                        Assert.IsNull(values);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenGeometryIsSpecifiedAndMappingIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        ExceptionAssert.Throws<MagickResourceLimitErrorException> (() =>
                        {
                            var values = pixels.ToShortArray(new MagickGeometry(1, 2, 3, 4), string.Empty);
                        });
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnArrayWhenGeometryIsCorrect()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray(new MagickGeometry(10, 10, 113, 108), "RG");
                        int length = 113 * 108 * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }

            [TestMethod]
            public void ShouldReturnNullWhenMappingIsNull()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray(null);
                        Assert.IsNull(values);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenMappingIsEmpty()
            {
                using (IMagickImage image = new MagickImage(Files.ImageMagickJPG))
                {
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        ExceptionAssert.Throws<MagickResourceLimitErrorException>(() =>
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
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
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
                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var values = pixels.ToShortArray("RG");
                        int length = image.Width * image.Height * 2;

                        Assert.AreEqual(length, values.Length);
                    }
                }
            }
        }
    }
}
