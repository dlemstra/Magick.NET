// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBilateralBlurMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.NoisePNG);
            Assert.Throws<ArgumentException>("width", () => image.BilateralBlur(-1, 2));
        }

        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegativeThanOneWithLowAndHigh()
        {
            using var image = new MagickImage(Files.NoisePNG);
            Assert.Throws<ArgumentException>("width", () => image.BilateralBlur(-1, 2, 0.1, 0.1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.NoisePNG);
            Assert.Throws<ArgumentException>("height", () => image.BilateralBlur(2, -1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegativeWithLowAndHigh()
        {
            using var image = new MagickImage(Files.NoisePNG);
            Assert.Throws<ArgumentException>("height", () => image.BilateralBlur(2, -1, 0.1, 0.1));
        }

        [Fact]
        public void ShouldApplyTheFilter()
        {
            using var image = new MagickImage(Files.NoisePNG);
            using var blurredImage = image.Clone();
            blurredImage.BilateralBlur(2, 2);
#if Q8
            Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.0008, 0.00081);
#else
            Assert.InRange(image.Compare(blurredImage, ErrorMetric.RootMeanSquared), 0.00069, 0.0007);
#endif
        }
    }
}
