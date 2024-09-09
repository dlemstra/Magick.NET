// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSampleMethod
    {
        public class WithGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("geometry", () => image.Sample(null!));
            }

            [Fact]
            public void ShouldUseTheSpecifiedGeometry()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                image.Sample(new MagickGeometry(300, 300));

                Assert.Equal(300U, image.Width);
                Assert.Equal(225U, image.Height);
            }
        }

        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                image.Sample(400, 400);

                Assert.Equal(400U, image.Width);
                Assert.Equal(300U, image.Height);
            }
        }

        public class WithPercentage
        {
            [Fact]
            public void ShouldThrowExceptionWhenPercentageIsNegative()
            {
                var percentage = new Percentage(-0.5);
                using var image = new MagickImage(Files.Builtin.Logo);

                Assert.Throws<ArgumentException>("percentageWidth", () => image.Sample(percentage));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageWidthIsNegative()
            {
                var percentageWidth = new Percentage(-0.5);
                var percentageHeight = new Percentage(10);
                using var image = new MagickImage(Files.Builtin.Logo);

                Assert.Throws<ArgumentException>("percentageWidth", () => image.Sample(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageHeightIsNegative()
            {
                var percentageWidth = new Percentage(10);
                var percentageHeight = new Percentage(-0.5);
                using var image = new MagickImage(Files.Builtin.Logo);

                Assert.Throws<ArgumentException>("percentageHeight", () => image.Sample(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldUseTheSpecifiedPercentage()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                image.Sample(new Percentage(50));

                Assert.Equal(320U, image.Width);
                Assert.Equal(240U, image.Height);
            }
        }
    }
}
