// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheSetIntAreaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("values", () => pixels.SetIntArea(10, 10, 1000, 1000, null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("values", () => pixels.SetIntArea(10, 10, 1000, 1000, new int[] { 0, 0, 0, 0 }));
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayHasTooManyValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new int[(113 * 108 * image.ChannelCount) + image.ChannelCount];

            Assert.Throws<ArgumentException>("values", () => pixels.SetIntArea(10, 10, 113, 108, values));
        }

        [Fact]
        public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new int[113 * 108 * image.ChannelCount];
            pixels.SetIntArea(10, 10, 113, 108, values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayIsSpecifiedAndGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.SetIntArea(null!, new int[] { 0 }));
        }

        [Fact]
        public void ShouldChangePixelsWhenGeometryAndArrayAreSpecified()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new int[113 * 108 * image.ChannelCount];
            pixels.SetIntArea(new MagickGeometry(10, 10, 113, 108), values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}
