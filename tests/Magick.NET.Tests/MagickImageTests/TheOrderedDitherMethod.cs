// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    [Collection(nameof(RunTestsSeparately))]
    public class TheOrderedDitherMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenThresholdMapIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("thresholdMap", () => image.OrderedDither(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenThresholdMapIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("thresholdMap", () => image.OrderedDither(string.Empty));
        }

        [Fact]
        public void ShouldPerformAnOrderedDither()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.OrderedDither("h4x4a");

            ColorAssert.Equal(MagickColors.Yellow, image, 299, 212);
            ColorAssert.Equal(MagickColors.Red, image, 314, 228);
            ColorAssert.Equal(MagickColors.Black, image, 448, 159);
        }
    }
}
