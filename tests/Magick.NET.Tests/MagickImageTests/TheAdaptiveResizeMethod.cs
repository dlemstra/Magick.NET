// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAdaptiveResizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            Assert.Throws<ArgumentException>("width", () => image.AdaptiveResize(-1, 512));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            Assert.Throws<ArgumentException>("height", () => image.AdaptiveResize(512, -1));
        }

        [Fact]
        public void ShouldNotEnlargeTheImage()
        {
            using var image = new MagickImage(MagickColors.Black, 512, 1);
            image.AdaptiveResize(512, 512);

            Assert.Equal(1, image.Height);
        }

        [Fact]
        public void ShouldEnlargeTheImageWhenAspectRatioIsIgnored()
        {
            var geometry = new MagickGeometry(512, 512)
            {
                IgnoreAspectRatio = true,
            };
            using var image = new MagickImage(MagickColors.Black, 512, 1);

            image.AdaptiveResize(geometry);

            Assert.Equal(512, image.Height);
        }

        [Fact]
        public void ShouldResizeTheImage()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.AdaptiveResize(100, 80);

            Assert.Equal(80, image.Width);
            Assert.Equal(80, image.Height);

            ColorAssert.Equal(new MagickColor("#347bbd"), image, 23, 42);
            ColorAssert.Equal(new MagickColor("#a8dff8"), image, 42, 42);
        }
    }
}
