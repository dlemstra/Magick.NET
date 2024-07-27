// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBorderMethod
    {
        [Fact]
        public void ShouldAddBorderOnAllSides()
        {
            using var image = new MagickImage("xc:red", 1, 1);
            image.BorderColor = MagickColors.Green;
            image.Border(3);

            Assert.Equal(7U, image.Width);
            Assert.Equal(7U, image.Height);
            ColorAssert.Equal(MagickColors.Green, image, 1, 1);
        }

        [Fact]
        public void ShouldOnlyAddVerticalBorderWhenOnlyWidthIsSpecified()
        {
            using var image = new MagickImage("xc:red", 1, 1);
            image.Border(3, 0);

            Assert.Equal(7U, image.Width);
            Assert.Equal(1U, image.Height);
        }

        [Fact]
        public void ShouldOnlyAddHorizontalBorderWhenOnlyHeightIsSpecified()
        {
            using var image = new MagickImage("xc:red", 1, 1);
            image.Border(0, 3);

            Assert.Equal(1U, image.Width);
            Assert.Equal(7U, image.Height);
        }

        [Fact]
        public void ShouldUseTheSpecifiedPercentage()
        {
            using var image = new MagickImage("xc:red", 10, 20);
            image.BorderColor = MagickColors.Green;
            image.Border(new Percentage(10));

            Assert.Equal(12U, image.Width);
            Assert.Equal(24U, image.Height);
            ColorAssert.Equal(MagickColors.Green, image, 1, 1);
            ColorAssert.Equal(MagickColors.Red, image, 1, 2);
            ColorAssert.Equal(MagickColors.Red, image, 10, 21);
            ColorAssert.Equal(MagickColors.Green, image, 10, 22);
        }
    }
}
