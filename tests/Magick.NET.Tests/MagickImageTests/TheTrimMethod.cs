// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTrimMethod
    {
        [Fact]
        public void ShouldTrimTheBackground()
        {
            using var image = new MagickImage("xc:fuchsia", 50, 50);

            ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 49, 49);

            image.Extent(100, 60, Gravity.Center, MagickColors.Gold);

            Assert.Equal(100U, image.Width);
            Assert.Equal(60U, image.Height);
            ColorAssert.Equal(MagickColors.Gold, image, 0, 0);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 50, 30);

            image.Trim();

            Assert.Equal(50U, image.Width);
            Assert.Equal(50U, image.Height);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 49, 49);
        }

        [Fact]
        public void ShouldTrimTheBackgroundWithThePercentage()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);
            image.BackgroundColor = MagickColors.Black;
            image.Rotate(10);

            image.Trim(new Percentage(5));

#if Q8 || Q16
            Assert.Equal(558U, image.Width);
            Assert.Equal(318U, image.Height);
#else
            Assert.Equal(560U, image.Width);
            Assert.Equal(320U, image.Height);
#endif
        }

        [Fact]
        public void ShouldTrimTheBackgroundHorizontally()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Extent(3, 3, Gravity.Center, MagickColors.White);

            image.Trim(Gravity.East, Gravity.West);

            Assert.Equal(1U, image.Width);
            Assert.Equal(3U, image.Height);
            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 0, 1);
            ColorAssert.Equal(MagickColors.White, image, 0, 2);
        }

        [Fact]
        public void ShouldTrimTheBackgroundVertically()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Extent(3, 3, Gravity.Center, MagickColors.White);

            image.Trim(Gravity.North, Gravity.South);

            Assert.Equal(3U, image.Width);
            Assert.Equal(1U, image.Height);
            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 1, 0);
            ColorAssert.Equal(MagickColors.White, image, 2, 0);
        }

        [Theory]
        [InlineData(Gravity.North, 3, 2, 1, 0)]
        [InlineData(Gravity.Northeast, 2, 2, 1, 0)]
        [InlineData(Gravity.East, 2, 3, 1, 1)]
        [InlineData(Gravity.Southeast, 2, 2, 1, 1)]
        [InlineData(Gravity.South, 3, 2, 1, 1)]
        [InlineData(Gravity.Southwest, 2, 2, 0, 1)]
        [InlineData(Gravity.West, 2, 3, 0, 1)]
        [InlineData(Gravity.Northwest, 2, 2, 0, 0)]
        public void ShouldTrimTheSpecifiedEdge(Gravity edge, uint width, uint height, int redX, int redY)
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            image.Extent(3, 3, Gravity.Center, MagickColors.White);

            image.Trim(edge);

            Assert.Equal(width, image.Width);
            Assert.Equal(height, image.Height);
            ColorAssert.Equal(MagickColors.Red, image, redX, redY);
        }
    }
}
