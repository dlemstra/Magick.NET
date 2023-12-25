// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChannelsProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectChannelsForRgbaImage()
        {
            var rgb = new PixelChannel[]
            {
                PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue, PixelChannel.Index,
            };

            var rgba = new PixelChannel[]
            {
                PixelChannel.Red, PixelChannel.Green, PixelChannel.Blue, PixelChannel.Alpha, PixelChannel.Index,
            };

            using var image = new MagickImage(Files.RoseSparkleGIF);

            Assert.Equal(rgba, image.Channels.ToArray());

            image.Alpha(AlphaOption.Off);

            Assert.Equal(rgb, image.Channels.ToArray());
        }

        [Fact]
        public void ShouldReturnTheCorrectChannelsForGrayAlphaImage()
        {
            var gray = new PixelChannel[]
            {
                PixelChannel.Gray,
            };

            var grayAlpha = new PixelChannel[]
            {
                PixelChannel.Gray, PixelChannel.Alpha,
            };

            using var image = new MagickImage(Files.SnakewarePNG);

            Assert.Equal(grayAlpha, image.Channels.ToArray());

            using var redChannel = image.Separate(Channels.Red).First();

            Assert.Equal(gray, redChannel.Channels.ToArray());

            redChannel.Alpha(AlphaOption.On);

            Assert.Equal(grayAlpha, redChannel.Channels.ToArray());
        }

        [Fact]
        public void ShouldReturnTheCorrectChannelsForCmykaImage()
        {
            var cmyk = new PixelChannel[]
            {
                PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black,
            };

            var cmyka = new PixelChannel[]
            {
                PixelChannel.Cyan, PixelChannel.Magenta, PixelChannel.Yellow, PixelChannel.Black, PixelChannel.Alpha,
            };

            using var image = new MagickImage(Files.SnakewarePNG);
            image.ColorSpace = ColorSpace.CMYK;

            Assert.Equal(cmyka, image.Channels.ToArray());

            image.Alpha(AlphaOption.Off);

            Assert.Equal(cmyk, image.Channels.ToArray());
        }
    }
}
