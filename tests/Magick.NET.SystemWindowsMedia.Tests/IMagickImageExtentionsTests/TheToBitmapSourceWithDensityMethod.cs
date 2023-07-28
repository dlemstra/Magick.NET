// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.SystemWindowsMedia.Tests;

public partial class MagickImageTests
{
    public class TheToBitmapSourceWithDensityMethod
    {
        [Fact]
        public void ShouldNotConvertTheDpiWhenDensityIsUndefinedAndNotZero()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            image.Density = new Density(1, 2, DensityUnit.Undefined);

            var bitmapSource = image.ToBitmapSourceWithDensity();

            Assert.Equal(1, bitmapSource.DpiX);
            Assert.Equal(2, bitmapSource.DpiY);
        }

        [Fact]
        public void ShouldUseTheDefaultDensityWhenXIsZero()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(0, 1, DensityUnit.PixelsPerCentimeter);

            var bitmapSource = image.ToBitmapSourceWithDensity();
            Assert.Equal(96, bitmapSource.DpiX);
            Assert.Equal(96, bitmapSource.DpiY);
        }

        [Fact]
        public void ShouldUseTheDefaultDensityWhenYIsZero()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Density = new Density(1, 0, DensityUnit.PixelsPerCentimeter);

            var bitmapSource = image.ToBitmapSourceWithDensity();
            Assert.Equal(96, bitmapSource.DpiX);
            Assert.Equal(96, bitmapSource.DpiY);
        }

        [Fact]
        public void ShouldNotConvertTheDpiWhenDensityIsPixelsPerInch()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            image.Density = new Density(1, 2, DensityUnit.PixelsPerInch);

            var bitmapSource = image.ToBitmapSourceWithDensity();

            Assert.Equal(1, bitmapSource.DpiX);
            Assert.Equal(2, bitmapSource.DpiY);
        }

        [Fact]
        public void ShouldConvertTheDpiWhenDensityIsPixelsPerCentimeter()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            image.Density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

            var bitmapSource = image.ToBitmapSourceWithDensity();

            Assert.InRange(bitmapSource.DpiX, 2.53, 2.55);
            Assert.InRange(bitmapSource.DpiY, 5.07, 5.09);
        }

        [Fact]
        public void ShouldUseTheDensityOfTheImageWhenBitmapDensityIsSetToUse()
        {
            using var image = new MagickImage(MagickColors.Red, 200, 100);
            image.Density = new Density(300, 200);

            var bitmapSource = image.ToBitmapSourceWithDensity();

            Assert.Equal(64, (int)bitmapSource.Width);
            Assert.Equal(48, (int)bitmapSource.Height);
        }
    }
}
