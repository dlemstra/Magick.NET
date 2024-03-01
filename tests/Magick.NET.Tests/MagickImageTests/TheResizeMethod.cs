// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheResizeMethod
    {
        public class WithMagickGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("geometry", () => image.Resize(null));
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.RedPNG);
                image.Resize(new MagickGeometry(64, 64));

                Assert.Equal(64, image.Width);
                Assert.Equal(21, image.Height);
            }

            [Fact]
            public void ShouldIgnoreTheAspectRatioWhenSpecified()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize(new MagickGeometry("5x10!"));

                Assert.Equal(5, image.Width);
                Assert.Equal(10, image.Height);
            }

            [Fact]
            public void ShouldNotResizeTheImageWhenLarger()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize(new MagickGeometry("32x32<"));

                Assert.Equal(128, image.Width);
                Assert.Equal(128, image.Height);
            }

            [Fact]
            public void ShouldResizeTheImageWhenSmaller()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize(new MagickGeometry("256x256<"));

                Assert.Equal(256, image.Width);
                Assert.Equal(256, image.Height);
            }

            [Fact]
            public void ShouldNotResizeTheImageWhenSmaller()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize(new MagickGeometry("256x256>"));

                Assert.Equal(128, image.Width);
                Assert.Equal(128, image.Height);
            }

            [Fact]
            public void ShouldResizeTheImageWhenLarger()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize(new MagickGeometry("32x32>"));

                Assert.Equal(32, image.Width);
                Assert.Equal(32, image.Height);
            }

            [Fact]
            public void ShouldResizeToSmallerArea()
            {
                using var image = new MagickImage(Files.SnakewarePNG);
                image.Resize(new MagickGeometry("4096@"));

                Assert.True((image.Width * image.Height) < 4096);
            }
        }

        public class WithPercentage
        {
            [Fact]
            public void ShouldThrowExceptionWhenPercentageIsNegative()
            {
                var percentage = new Percentage(-0.5);
                using var image = new MagickImage(Files.MagickNETIconPNG);

                Assert.Throws<ArgumentException>("percentageWidth", () => image.Resize(percentage));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageWidthIsNegative()
            {
                var percentageWidth = new Percentage(-0.5);
                var percentageHeight = new Percentage(10);
                using var image = new MagickImage(Files.MagickNETIconPNG);

                Assert.Throws<ArgumentException>("percentageWidth", () => image.Resize(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageHeightIsNegative()
            {
                var percentageWidth = new Percentage(10);
                var percentageHeight = new Percentage(-0.5);
                using var image = new MagickImage(Files.MagickNETIconPNG);

                Assert.Throws<ArgumentException>("percentageHeight", () => image.Resize(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.Resize((Percentage)200);

                Assert.Equal(256, image.Width);
                Assert.Equal(256, image.Height);
            }
        }

        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                using var image = new MagickImage(Files.RedPNG);
                Assert.Throws<ArgumentException>("width", () => image.Resize(-1, 32));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                using var image = new MagickImage(Files.RedPNG);
                Assert.Throws<ArgumentException>("height", () => image.Resize(32, -1));
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.RedPNG);
                image.Resize(32, 32);

                Assert.Equal(32, image.Width);
                Assert.Equal(11, image.Height);
            }
        }
    }
}
