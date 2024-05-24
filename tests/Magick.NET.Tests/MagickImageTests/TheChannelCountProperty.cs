// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChannelCountProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectNumberOfChannelsForRgbaImage()
        {
            using var image = new MagickImage(Files.RoseSparkleGIF);
            Assert.Equal(5, image.ChannelCount);
        }

        [Fact]
        public void ShouldReturnTheCorrectChannelsForCmykaImage()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            Assert.Equal(2, image.ChannelCount);

            image.ColorSpace = ColorSpace.CMYK;
            Assert.Equal(5, image.ChannelCount);
        }
    }
}
