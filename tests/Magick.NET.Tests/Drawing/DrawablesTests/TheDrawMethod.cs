// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablesTests
{
    public class TheDrawMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
            => Assert.Throws<ArgumentNullException>("image", () => new Drawables().Draw(null));

        [Fact]
        public void ShouldDrawTheDrawables()
        {
            using var image = new MagickImage(MagickColors.Fuchsia, 100, 100);
            var drawables = new Drawables()
              .FillColor(MagickColors.Red)
              .Rectangle(10, 10, 90, 90);

            drawables.Draw(image);

            ColorAssert.Equal(MagickColors.Fuchsia, image, 9, 9);
            ColorAssert.Equal(MagickColors.Red, image, 10, 10);
            ColorAssert.Equal(MagickColors.Red, image, 90, 90);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 91, 91);

            image.Draw(new Drawables()
              .FillColor(MagickColors.Green)
              .Rectangle(15, 15, 85, 85));

            ColorAssert.Equal(MagickColors.Fuchsia, image, 9, 9);
            ColorAssert.Equal(MagickColors.Red, image, 10, 10);
            ColorAssert.Equal(MagickColors.Green, image, 15, 15);
            ColorAssert.Equal(MagickColors.Green, image, 85, 85);
            ColorAssert.Equal(MagickColors.Red, image, 90, 90);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 91, 91);
        }

        [Fact]
        public void ShouldUseTheDefaultDensity()
        {
            using var image = new MagickImage(MagickColors.Purple, 500, 500);
            var pointSize = new DrawableFontPointSize(20);
            var text = new DrawableText(250, 250, "Magick.NET");

            image.Draw(pointSize, text);

            image.Trim();

            Assert.Equal(108, image.Width);
            Assert.Equal(19, image.Height);
        }

        [Fact]
        public void ShouldUseTheSpecifiedDensity()
        {
            using var image = new MagickImage(MagickColors.Purple, 500, 500);
            var pointSize = new DrawableFontPointSize(20);
            var text = new DrawableText(250, 250, "Magick.NET");

            image.Draw(pointSize, new DrawableDensity(96), text);

            image.Trim();

            Assert.Equal(144, image.Width);
            Assert.Equal(24, image.Height);
        }
    }
}
