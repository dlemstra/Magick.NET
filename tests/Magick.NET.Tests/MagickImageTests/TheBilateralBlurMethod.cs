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
    }
}
