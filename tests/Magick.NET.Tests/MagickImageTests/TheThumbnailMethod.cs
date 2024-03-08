// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheThumbnailMethod
    {
        public class WithGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();
                Assert.Throws<ArgumentNullException>("geometry", () => image.Thumbnail(null));
            }

            [Fact]
            public void ShouldResizeTheImageToTheCorrectDimensions()
            {
                var geometry = new MagickGeometry("1x1+0+0>");
                using var image = new MagickImage(Files.SnakewarePNG);
                image.Thumbnail(geometry);

                Assert.Equal(1, image.Width);
                Assert.Equal(1, image.Height);
            }
        }

        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("width", () => image.Thumbnail(-1, 100));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("height", () => image.Thumbnail(100, -1));
            }

            [Fact]
            public void ShouldCreateThumbnailOfTheImage()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                image.Thumbnail(100, 100);

                Assert.Equal(100, image.Width);
                Assert.Equal(23, image.Height);
            }
        }

        public class WithPercentage
        {
            [Fact]
            public void ShouldCreateThumbnailOfTheImageWithTheSpecifiedPercentage()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                image.Thumbnail(new Percentage(50));

                Assert.Equal(320, image.Width);
                Assert.Equal(240, image.Height);
            }
        }
    }
}
