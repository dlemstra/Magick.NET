// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheWhiteThresholdMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPercentageIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("threshold", () => image.BlackThreshold(new Percentage(-1)));
        }

        [Fact]
        public void ShouldMakePixelsAboveThresholdWhite()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.WhiteThreshold(new Percentage(10));

            ColorAssert.Equal(MagickColors.White, image, 43, 74);
            ColorAssert.Equal(MagickColors.White, image, 60, 74);
        }

        [Fact]
        public void ShouldThresholdTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.BlackThreshold(new Percentage(10), Channels.Green);

            ColorAssert.Equal(new MagickColor("#347bbd"), image, 43, 74);
            ColorAssert.Equal(new MagickColor("#a8dff8"), image, 60, 74);
        }
    }
}
