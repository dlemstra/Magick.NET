// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFrameMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("width", () => image.Frame(-1, 100));
            }

            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("width", () => image.Frame(-1, 100, 6, 6));
            }
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("height", () => image.Frame(100, -1));
            }

            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("height", () => image.Frame(100, -1, 6, 6));
            }
        }

        [Fact]
        public void ShouldFrameTheImage()
        {
            var frameSize = 100;

            using var image = new MagickImage(Files.MagickNETIconPNG);
            var expectedWidth = frameSize + image.Width + frameSize;
            var expectedHeight = frameSize + image.Height + frameSize;

            image.Frame(frameSize, frameSize);
            Assert.Equal(expectedWidth, image.Width);
            Assert.Equal(expectedHeight, image.Height);
        }

        [Fact]
        public void ShouldNotMakeImageLargerWithBevel()
        {
            var frameSize = 100;

            using var image = new MagickImage(Files.MagickNETIconPNG);
            var expectedWidth = frameSize + image.Width + frameSize;
            var expectedHeight = frameSize + image.Height + frameSize;

            image.Frame(frameSize, frameSize, 6, 6);
            Assert.Equal(expectedWidth, image.Width);
            Assert.Equal(expectedHeight, image.Height);
        }

        [Fact]
        public void ShouldThrowExceptionWhenFrameIsLessThanImageSize()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var exception = Assert.Throws<MagickOptionErrorException>(() => { image.Frame(6, 6, 7, 7); });

            Assert.Contains("frame is less than image size", exception.Message);
        }
    }
}
