// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokePatternProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.StrokePattern);
        }

        [Fact]
        public void ShouldBeUsedWhenDrawing()
        {
            using var image = new MagickImage(MagickColors.Red, 250, 100);
            image.Settings.StrokeWidth = 40;
            image.Settings.StrokePattern = new MagickImage(Files.Builtin.Logo);
            image.Draw(new DrawableLine(50, 50, 200, 50));

            ColorAssert.Equal(MagickColors.Red, image, 49, 50);
            ColorAssert.Equal(MagickColors.White, image, 50, 50);
            ColorAssert.Equal(MagickColors.White, image, 50, 70);
            ColorAssert.Equal(MagickColors.White, image, 200, 50);
            ColorAssert.Equal(MagickColors.White, image, 200, 70);
            ColorAssert.Equal(MagickColors.Red, image, 201, 50);
        }
    }
}
