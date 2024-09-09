// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheLiquidRescaleMethod
    {
        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(128, 64);

                Assert.Equal(64U, image.Width);
                Assert.Equal(64U, image.Height);
            }
        }

        public class WithWidthAndHeightAndRigidity
        {
            [Fact]
            public void ShouldApplyTheRigidity()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(64, 64);

                using var other = new MagickImage(Files.MagickNETIconPNG);
                other.LiquidRescale(64, 64, 5.0, 0.0);

                Assert.InRange(image.Compare(other, ErrorMetric.RootMeanSquared), 0.5, 0.6);

                using var otherWithRigidity = new MagickImage(Files.MagickNETIconPNG);
                otherWithRigidity.LiquidRescale(64, 64, 5.0, 10.0);

                Assert.InRange(image.Compare(otherWithRigidity, ErrorMetric.RootMeanSquared), 0.3, 0.4);
            }
        }

        public class WithGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);

                Assert.Throws<ArgumentNullException>("geometry", () => image.LiquidRescale(null!));
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                var geometry = new MagickGeometry(128, 64)
                {
                    IgnoreAspectRatio = true,
                };
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(geometry);

                Assert.Equal(128U, image.Width);
                Assert.Equal(64U, image.Height);
            }
        }

        public class WithPercentage
        {
            [Fact]
            public void ShouldThrowExceptionWhenPercentageIsNegative()
            {
                var percentage = new Percentage(-1);
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("percentageWidth", () => image.LiquidRescale(percentage));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageWidthIsNegative()
            {
                var percentageWidth = new Percentage(-1);
                var percentageHeight = new Percentage(1);
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("percentageWidth", () => image.LiquidRescale(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageHeightIsNegative()
            {
                var percentageWidth = new Percentage(1);
                var percentageHeight = new Percentage(-1);
                using var image = new MagickImage(Files.MagickNETIconPNG);
                Assert.Throws<ArgumentException>("percentageHeight", () => image.LiquidRescale(percentageWidth, percentageHeight));
            }

            [Fact]
            public void ShouldResizeTheImage()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(new Percentage(25));

                Assert.Equal(32U, image.Width);
                Assert.Equal(32U, image.Height);
            }

            [Fact]
            public void ShouldIgnoreTheAspectRatio()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(new Percentage(25), new Percentage(10));

                Assert.Equal(32U, image.Width);
                Assert.Equal(13U, image.Height);
            }
        }

        public class WithPercentageAndRigidity
        {
            [Fact]
            public void ShouldApplyTheRigidity()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                image.LiquidRescale(new Percentage(50), new Percentage(50));

                using var other = new MagickImage(Files.MagickNETIconPNG);
                other.LiquidRescale(new Percentage(50), new Percentage(50), 5.0, 0.0);

                Assert.InRange(image.Compare(other, ErrorMetric.RootMeanSquared), 0.5, 0.6);

                using var otherWithRigidity = new MagickImage(Files.MagickNETIconPNG);
                otherWithRigidity.LiquidRescale(new Percentage(50), new Percentage(50), 5.0, 10.0);

                Assert.InRange(image.Compare(otherWithRigidity, ErrorMetric.RootMeanSquared), 0.3, 0.4);
            }
        }
    }
}
