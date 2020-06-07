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
using System.Drawing;
using System.Drawing.Imaging;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.SystemDrawing.Tests
{
    public partial class IMagickImageExtensionsTests
    {
        [TestClass]
        public class TheToBitmapWithDensityMethod
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
                using (var image = new MagickImage(ToMagickColor(Color.Red), 1, 1))
                {
                    image.ColorSpace = ColorSpace.YCbCr;

                    using (var bitmap = image.ToBitmapWithDensity())
                    {
                        ColorAssert.AreEqual(MagickColors.Red, ToMagickColor(bitmap.GetPixel(0, 0)));
                    }

                    Assert.AreEqual(ColorSpace.YCbCr, image.ColorSpace);
                }
            }

            [TestMethod]
            public void ShouldBeAbleToConvertGrayImage()
            {
                using (var image = new MagickImage(ToMagickColor(Color.Magenta), 5, 1))
                {
                    image.ColorType = ColorType.Bilevel;
                    image.ClassType = ClassType.Direct;

                    using (var bitmap = image.ToBitmapWithDensity())
                    {
                        for (int i = 0; i < image.Width; i++)
                            ColorAssert.AreEqual(MagickColors.White, ToMagickColor(bitmap.GetPixel(i, 0)));
                    }
                }
            }

            [TestMethod]
            public void ShouldSetTheDensityOfTheBitmap()
            {
                using (var image = new MagickImage(ToMagickColor(Color.Red), 1, 1))
                {
                    image.Density = new Density(300, 200);

                    using (var bitmap = image.ToBitmapWithDensity())
                    {
                        Assert.AreEqual(300, (int)bitmap.HorizontalResolution);
                        Assert.AreEqual(200, (int)bitmap.VerticalResolution);
                    }
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenImageFormatIsNull()
            {
                using (var image = new MagickImage(ToMagickColor(Color.Red), 1, 1))
                {
                    ExceptionAssert.Throws<ArgumentNullException>("imageFormat", () => image.ToBitmapWithDensity(null));
                }
            }

            [TestMethod]
            public void ShouldSetTheDensityOfTheBitmapWhenFormatIsUsed()
            {
                using (var image = new MagickImage(ToMagickColor(Color.Red), 1, 1))
                {
                    image.Density = new Density(300, 200);

                    using (var bitmap = image.ToBitmapWithDensity(ImageFormat.Jpeg))
                    {
                        Assert.AreEqual(300, (int)bitmap.HorizontalResolution);
                        Assert.AreEqual(200, (int)bitmap.VerticalResolution);
                    }
                }
            }

            private void AssertUnsupportedImageFormat(ImageFormat imageFormat)
            {
                using (var image = new MagickImage(MagickColors.Red, 10, 10))
                {
                    ExceptionAssert.Throws<NotSupportedException>(() =>
                    {
                        image.ToBitmapWithDensity(imageFormat);
                    });
                }
            }

            private void AssertSupportedImageFormat(ImageFormat imageFormat)
            {
                using (var image = new MagickImage(MagickColors.Red, 10, 10))
                {
                    using (var bitmap = image.ToBitmapWithDensity(imageFormat))
                    {
                        Assert.AreEqual(imageFormat, bitmap.RawFormat);

                        // Cannot test JPEG due to rounding issues.
                        if (imageFormat != ImageFormat.Jpeg)
                        {
                            ColorAssert.AreEqual(MagickColors.Red, ToMagickColor(bitmap.GetPixel(0, 0)));
                            ColorAssert.AreEqual(MagickColors.Red, ToMagickColor(bitmap.GetPixel(5, 5)));
                            ColorAssert.AreEqual(MagickColors.Red, ToMagickColor(bitmap.GetPixel(9, 9)));
                        }
                    }
                }
            }

            private MagickColor ToMagickColor(Color color)
            {
                var result = new MagickColor();
                result.SetFromColor(color);
                return result;
            }
        }
    }
}