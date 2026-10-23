// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBilateralBlurMethod
    {
        [Fact]
        public void ShouldApplyTheFilter()
        {
            using var image = new MagickImage(Files.NoisePNG);
            using var blurredImage = image.Clone();
            blurredImage.BilateralBlur(2, 2);

#if Q8
            Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.00107, 0.00109);
#else
            Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.00084, 0.00085);
#endif
        }

        [Fact]
        public void ShouldOnlyBlurTheSpecifiedChannels()
        {
            using var image = new MagickImage(Files.NoisePNG);
            using var original = image.Clone();
            image.BilateralBlur(2, 2, Channels.Red);

            Assert.NotEqual(0, original.Compare(image, ErrorMetric.RootMeanSquared, Channels.Red));
            Assert.Equal(0, original.Compare(image, ErrorMetric.RootMeanSquared, Channels.Green | Channels.Blue));
        }
    }
}
