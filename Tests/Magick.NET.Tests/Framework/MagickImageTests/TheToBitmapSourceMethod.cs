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

#if !NETCORE

using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheToBitmapSourceMethod
        {
            [TestMethod]
            public void ShouldReturnImageWithRgb24FormatForRgbImage()
            {
                byte[] pixels = new byte[150];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    var bitmapSource = image.ToBitmapSource();

                    Assert.AreEqual(PixelFormats.Rgb24, bitmapSource.Format);
                    Assert.AreEqual(5, bitmapSource.Width);
                    Assert.AreEqual(10, bitmapSource.Height);
                    Assert.AreEqual(96, bitmapSource.DpiX);
                    Assert.AreEqual(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 15, 0);

                    Assert.AreEqual(255, pixels[0]);
                    Assert.AreEqual(0, pixels[1]);
                    Assert.AreEqual(0, pixels[2]);
                }
            }

            [TestMethod]
            public void ShouldReturnImageWithCmyk32FormatForCmykImage()
            {
                byte[] pixels = new byte[200];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 5))
                {
                    image.ColorSpace = ColorSpace.CMYK;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.AreEqual(PixelFormats.Cmyk32, bitmapSource.Format);
                    Assert.AreEqual(10, bitmapSource.Width);
                    Assert.AreEqual(5, bitmapSource.Height);
                    Assert.AreEqual(96, bitmapSource.DpiX);
                    Assert.AreEqual(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 40, 0);

                    Assert.AreEqual(0, pixels[0]);
                    Assert.AreEqual(255, pixels[1]);
                    Assert.AreEqual(255, pixels[2]);
                    Assert.AreEqual(0, pixels[3]);
                }
            }

            [TestMethod]
            public void ShouldReturnImageWithBgra32FormatForRgbaImage()
            {
                byte[] pixels = new byte[200];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.HasAlpha = true;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.AreEqual(PixelFormats.Bgra32, bitmapSource.Format);
                    Assert.AreEqual(5, bitmapSource.Width);
                    Assert.AreEqual(10, bitmapSource.Height);
                    Assert.AreEqual(96, bitmapSource.DpiX);
                    Assert.AreEqual(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 20, 0);

                    Assert.AreEqual(0, pixels[0]);
                    Assert.AreEqual(0, pixels[1]);
                    Assert.AreEqual(255, pixels[2]);
                    Assert.AreEqual(255, pixels[3]);
                }
            }

            [TestMethod]
            public void ShouldReturnImageWithRgb24FormatForYCbCrImage()
            {
                byte[] pixels = new byte[150];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.ColorSpace = ColorSpace.YCbCr;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.AreEqual(PixelFormats.Rgb24, bitmapSource.Format);
                    Assert.AreEqual(5, bitmapSource.Width);
                    Assert.AreEqual(10, bitmapSource.Height);
                    Assert.AreEqual(96, bitmapSource.DpiX);
                    Assert.AreEqual(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 15, 0);

                    Assert.AreEqual(255, pixels[0]);
                    Assert.AreEqual(0, pixels[1]);
                    Assert.AreEqual(0, pixels[2]);
                }
            }

            [TestMethod]
            public void ShouldNotConvertTheDpiWhenDensityIsUndefinedAndNotZero()
            {
                byte[] pixels = new byte[150];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.Undefined);

                    var bitmapSource = image.ToBitmapSource(BitmapDensity.Use);

                    Assert.AreEqual(1, bitmapSource.DpiX);
                    Assert.AreEqual(2, bitmapSource.DpiY);
                }
            }

            [TestMethod]
            public void ShouldNotConvertTheDpiWhenDensityIsPixelsPerInch()
            {
                byte[] pixels = new byte[150];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.PixelsPerInch);

                    var bitmapSource = image.ToBitmapSource(BitmapDensity.Use);

                    Assert.AreEqual(1, bitmapSource.DpiX);
                    Assert.AreEqual(2, bitmapSource.DpiY);
                }
            }

            [TestMethod]
            public void ShouldConvertTheDpiWhenDensityIsPixelsPerCentimeter()
            {
                byte[] pixels = new byte[150];

                using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                    var bitmapSource = image.ToBitmapSource(BitmapDensity.Use);

                    Assert.AreEqual(2.54, bitmapSource.DpiX, 0.01);
                    Assert.AreEqual(5.08, bitmapSource.DpiY, 0.01);
                }
            }

            [TestMethod]
            public void ShouldIgnoreTheDensityOfTheImage()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 200, 100))
                {
                    image.Density = new Density(300);

                    var bitmapSource = image.ToBitmapSource();

                    Assert.AreEqual(200, (int)bitmapSource.Width);
                    Assert.AreEqual(100, (int)bitmapSource.Height);
                }
            }

            [TestMethod]
            public void ShouldUseTheDensityOfTheImageWhenBitmapDensityIsSetToUse()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Red, 200, 100))
                {
                    image.Density = new Density(300, 200);

                    var bitmapSource = image.ToBitmapSource(BitmapDensity.Use);

                    Assert.AreEqual(64, (int)bitmapSource.Width);
                    Assert.AreEqual(48, (int)bitmapSource.Height);
                }
            }
        }
    }
}

#endif