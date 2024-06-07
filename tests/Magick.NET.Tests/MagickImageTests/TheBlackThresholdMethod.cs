// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBlackThresholdMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPercentageIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("threshold", () => image.BlackThreshold(new Percentage(-1)));
        }

        [Fact]
        public void ShouldThresholdTheImage()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.BlackThreshold(new Percentage(90));

            ColorAssert.Equal(MagickColors.Black, image, 43, 74);
            ColorAssert.Equal(new MagickColor("#0000f8"), image, 60, 74);
        }

        [Fact]
        public void ShouldThresholdTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.BlackThreshold(new Percentage(90), Channels.Green);

            ColorAssert.Equal(new MagickColor("#3400bd"), image, 43, 74);
            ColorAssert.Equal(new MagickColor("#a800f8"), image, 60, 74);
        }
    }
}
