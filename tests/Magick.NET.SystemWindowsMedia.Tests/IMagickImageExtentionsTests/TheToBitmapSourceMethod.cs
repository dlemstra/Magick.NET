// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Windows.Media;
using System.Windows.Media.Imaging;
using ImageMagick;
using Xunit;

namespace Magick.NET.SystemWindowsMedia.Tests
{
    public partial class MagickImageTests
    {
        public class TheToBitmapSourceMethod
        {
            [Fact]
            public void ShouldReturnImageWithRgb24FormatForRgbImage()
            {
                var pixels = new byte[150];

                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    var bitmapSource = image.ToBitmapSource();

                    Assert.Equal(PixelFormats.Rgb24, bitmapSource.Format);
                    Assert.Equal(5, bitmapSource.Width);
                    Assert.Equal(10, bitmapSource.Height);
                    Assert.Equal(96, bitmapSource.DpiX);
                    Assert.Equal(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 15, 0);

                    Assert.Equal(255, pixels[0]);
                    Assert.Equal(0, pixels[1]);
                    Assert.Equal(0, pixels[2]);
                }
            }

            [Fact]
            public void ShouldReturnImageWithCmyk32FormatForCmykImage()
            {
                var pixels = new byte[200];

                using (var image = new MagickImage(MagickColors.Red, 10, 5))
                {
                    image.ColorSpace = ColorSpace.CMYK;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.Equal(PixelFormats.Cmyk32, bitmapSource.Format);
                    Assert.Equal(10, bitmapSource.Width);
                    Assert.Equal(5, bitmapSource.Height);
                    Assert.Equal(96, bitmapSource.DpiX);
                    Assert.Equal(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 40, 0);

                    Assert.Equal(0, pixels[0]);
                    Assert.Equal(255, pixels[1]);
                    Assert.Equal(255, pixels[2]);
                    Assert.Equal(0, pixels[3]);
                }
            }

            [Fact]
            public void ShouldReturnImageWithBgra32FormatForRgbaImage()
            {
                byte[] pixels = new byte[200];

                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.HasAlpha = true;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.Equal(PixelFormats.Bgra32, bitmapSource.Format);
                    Assert.Equal(5, bitmapSource.Width);
                    Assert.Equal(10, bitmapSource.Height);
                    Assert.Equal(96, bitmapSource.DpiX);
                    Assert.Equal(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 20, 0);

                    Assert.Equal(0, pixels[0]);
                    Assert.Equal(0, pixels[1]);
                    Assert.Equal(255, pixels[2]);
                    Assert.Equal(255, pixels[3]);
                }
            }

            [Fact]
            public void ShouldReturnImageWithRgb24FormatForYCbCrImage()
            {
                byte[] pixels = new byte[150];

                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.ColorSpace = ColorSpace.YCbCr;

                    BitmapSource bitmapSource = image.ToBitmapSource();

                    Assert.Equal(PixelFormats.Rgb24, bitmapSource.Format);
                    Assert.Equal(5, bitmapSource.Width);
                    Assert.Equal(10, bitmapSource.Height);
                    Assert.Equal(96, bitmapSource.DpiX);
                    Assert.Equal(96, bitmapSource.DpiY);

                    bitmapSource.CopyPixels(pixels, 15, 0);

                    Assert.Equal(255, pixels[0]);
                    Assert.Equal(0, pixels[1]);
                    Assert.Equal(0, pixels[2]);
                }
            }

            [Fact]
            public void ShouldNotConvertTheDpiWhenDensityIsUndefinedAndNotZero()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.Undefined);

                    var bitmapSource = image.ToBitmapSourceWithDensity();

                    Assert.Equal(1, bitmapSource.DpiX);
                    Assert.Equal(2, bitmapSource.DpiY);
                }
            }

            [Fact]
            public void ShouldNotConvertTheDpiWhenDensityIsPixelsPerInch()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.PixelsPerInch);

                    var bitmapSource = image.ToBitmapSourceWithDensity();

                    Assert.Equal(1, bitmapSource.DpiX);
                    Assert.Equal(2, bitmapSource.DpiY);
                }
            }

            [Fact]
            public void ShouldConvertTheDpiWhenDensityIsPixelsPerCentimeter()
            {
                using (var image = new MagickImage(MagickColors.Red, 5, 10))
                {
                    image.Density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                    var bitmapSource = image.ToBitmapSourceWithDensity();

                    Assert.InRange(bitmapSource.DpiX, 2.53, 2.55);
                    Assert.InRange(bitmapSource.DpiY, 5.07, 5.09);
                }
            }

            [Fact]
            public void ShouldIgnoreTheDensityOfTheImage()
            {
                using (var image = new MagickImage(MagickColors.Red, 200, 100))
                {
                    image.Density = new Density(300);

                    var bitmapSource = image.ToBitmapSource();

                    Assert.Equal(200, (int)bitmapSource.Width);
                    Assert.Equal(100, (int)bitmapSource.Height);
                }
            }

            [Fact]
            public void ShouldUseTheDensityOfTheImageWhenBitmapDensityIsSetToUse()
            {
                using (var image = new MagickImage(MagickColors.Red, 200, 100))
                {
                    image.Density = new Density(300, 200);

                    var bitmapSource = image.ToBitmapSourceWithDensity();

                    Assert.Equal(64, (int)bitmapSource.Width);
                    Assert.Equal(48, (int)bitmapSource.Height);
                }
            }
        }
    }
}