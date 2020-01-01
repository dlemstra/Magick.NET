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

#if !NETCORE

using System;
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheToBitmapMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenImageFormatIsExif()
            {
                AssertUnsupportedImageFormat(ImageFormat.Exif);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageFormatIsEmf()
            {
                AssertUnsupportedImageFormat(ImageFormat.Emf);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageFormatIsWmf()
            {
                AssertUnsupportedImageFormat(ImageFormat.Wmf);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsBmp()
            {
                AssertSupportedImageFormat(ImageFormat.Bmp);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsGif()
            {
                AssertSupportedImageFormat(ImageFormat.Gif);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsIcon()
            {
                AssertSupportedImageFormat(ImageFormat.Icon);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsJpeg()
            {
                AssertSupportedImageFormat(ImageFormat.Jpeg);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsPng()
            {
                AssertSupportedImageFormat(ImageFormat.Png);
            }

            [TestMethod]
            public void ShouldReturnBitmapWhenFormatIsTiff()
            {
                AssertSupportedImageFormat(ImageFormat.Tiff);
            }

            [TestMethod]
            public void ShouldChangeTheColorSpaceToSrgb()
            {
                using (IMagickImage image = new MagickImage(Color.Red, 1, 1))
                {
                    image.ColorSpace = ColorSpace.YCbCr;

                    using (Bitmap bitmap = image.ToBitmap())
                    {
                        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(0, 0));
                    }

                    Assert.AreEqual(ColorSpace.YCbCr, image.ColorSpace);
                }
            }

            [TestMethod]
            public void ShouldSetTheDensityOfTheBitmapWhenBitmapDensityIsSetToUse()
            {
                using (IMagickImage image = new MagickImage(Color.Red, 1, 1))
                {
                    image.Density = new Density(300, 200);

                    using (Bitmap bitmap = image.ToBitmap(BitmapDensity.Use))
                    {
                        Assert.AreEqual(300, (int)bitmap.HorizontalResolution);
                        Assert.AreEqual(200, (int)bitmap.VerticalResolution);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageFormatIsNull()
            {
                using (IMagickImage image = new MagickImage(Color.Red, 1, 1))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("imageFormat", () => image.ToBitmap(null, BitmapDensity.Use));
                }
            }

            [TestMethod]
            public void ShouldSetTheDensityOfTheBitmapWhenBitmapDensityIsSetToUseAndFormatIsSet()
            {
                using (IMagickImage image = new MagickImage(Color.Red, 1, 1))
                {
                    image.Density = new Density(300, 200);

                    using (Bitmap bitmap = image.ToBitmap(ImageFormat.Jpeg, BitmapDensity.Use))
                    {
                        Assert.AreEqual(300, (int)bitmap.HorizontalResolution);
                        Assert.AreEqual(200, (int)bitmap.VerticalResolution);
                    }
                }
            }

            private void AssertUnsupportedImageFormat(ImageFormat imageFormat)
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 10))
                {
                    ExceptionAssert.Throws<NotSupportedException>(() =>
                    {
                        image.ToBitmap(imageFormat);
                    });
                }
            }

            private void AssertSupportedImageFormat(ImageFormat imageFormat)
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 10))
                {
                    using (Bitmap bitmap = image.ToBitmap(imageFormat))
                    {
                        Assert.AreEqual(imageFormat, bitmap.RawFormat);

                        // Cannot test JPEG due to rounding issues.
                        if (imageFormat != ImageFormat.Jpeg)
                        {
                            ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(0, 0));
                            ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(5, 5));
                            ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(9, 9));
                        }
                    }
                }
            }
        }
    }
}

#endif