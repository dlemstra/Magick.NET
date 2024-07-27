// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheBoundingBoxProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var boundingBox = image.BoundingBox;

            Assert.Equal(458U, boundingBox.Width);
            Assert.Equal(473U, boundingBox.Height);
            Assert.Equal(92, boundingBox.X);
            Assert.Equal(0, boundingBox.Y);
        }

        [Fact]
        public void ShouldReturnNullWhenThereIsNoBoundingBox()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.ColorFuzz = new Percentage(2);
            image.InverseOpaque(new MagickColor("#19FF8c"), MagickColors.Black);

            var boundingBox = image.BoundingBox;

            Assert.Null(boundingBox);
        }
    }
}
