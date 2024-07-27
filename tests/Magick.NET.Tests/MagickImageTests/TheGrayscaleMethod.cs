// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGrayscaleMethod
    {
        [Fact]
        public void ShouldUseTheDefaultPixelIntensityMethod()
        {
            using var imageA = new MagickImage(MagickColors.Purple, 1, 1);
            imageA.Grayscale();
            Assert.Equal(1U, imageA.ChannelCount);
            Assert.Equal(PixelChannel.Red, imageA.Channels.First());

            using var imageB = new MagickImage(MagickColors.Purple, 1, 1);
            imageB.Grayscale(PixelIntensityMethod.Brightness);
            Assert.Equal(1U, imageB.ChannelCount);
            Assert.Equal(PixelChannel.Red, imageB.Channels.First());

            Assert.NotEqual(0.0, imageA.Compare(imageB, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldNotRoundWhenHdriEnabled()
        {
            using var image = new MagickImage(MagickColors.Black, 1, 1);
            image.Grayscale(PixelIntensityMethod.Average);

            using var pixels = image.GetPixels();
            var pixel = pixels.GetValue(0, 0);

            Assert.Equal(0, pixel[0]);
        }
    }
}
