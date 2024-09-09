// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFloodFillMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);

            Assert.Throws<ArgumentNullException>("color", () => image.FloodFill((MagickColor)null!, 0, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenTargetColorIsNull()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);

            Assert.Throws<ArgumentNullException>("target", () => image.FloodFill(MagickColors.Purple, 0, 0, null!));
        }

        [Fact]
        public void ShouldChangeTheColors()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);
            image.FloodFill(MagickColors.Red, 0, 0);

            ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 0, 1);
            ColorAssert.Equal(MagickColors.Red, image, 1, 0);
            ColorAssert.Equal(MagickColors.Red, image, 1, 1);
        }

        [Fact]
        public void ShouldChangeTheTargetColors()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);
            using var green = new MagickImage(MagickColors.Green, 1, 1);
            image.Composite(green, 0, 0, CompositeOperator.Over);

            image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

            ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            ColorAssert.Equal(MagickColors.White, image, 0, 1);
            ColorAssert.Equal(MagickColors.White, image, 1, 0);
            ColorAssert.Equal(MagickColors.White, image, 1, 1);
        }

        [Fact]
        public void ShouldChangeTheNeighboursWithTargetColor()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);
            using var green = new MagickImage(MagickColors.Green, 1, 1);
            image.Composite(green, 0, 1, CompositeOperator.Over);

            image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.Red, image, 0, 1);
            ColorAssert.Equal(MagickColors.White, image, 1, 0);
            ColorAssert.Equal(MagickColors.White, image, 1, 1);
        }

        [Fact]
        public void ShouldNotChangeTheTargetColors()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);
            using var green = new MagickImage(MagickColors.Green, 1, 1);
            image.Composite(green, 1, 1, CompositeOperator.Over);

            image.FloodFill(MagickColors.Red, 0, 0, MagickColors.Green);

            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.White, image, 0, 1);
            ColorAssert.Equal(MagickColors.White, image, 1, 0);
            ColorAssert.Equal(MagickColors.Green, image, 1, 1);
        }

        [Fact]
        public void ShouldChangeTheColorsWithTheSameTransparency()
        {
            using var image = new MagickImage(MagickColors.White, 2, 2);
            image.HasAlpha = true;

            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(1, 1);
            pixel.SetChannel(3, 0);

            pixels.SetPixel(pixel);

            image.Settings.FillColor = MagickColors.Purple;
            image.FloodFill(0, 0, 0);

            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.White, image, 0, 1);
            ColorAssert.Equal(MagickColors.White, image, 1, 0);
            ColorAssert.Equal(new MagickColor("#ffffff00"), image, 1, 1);
        }
    }
}
