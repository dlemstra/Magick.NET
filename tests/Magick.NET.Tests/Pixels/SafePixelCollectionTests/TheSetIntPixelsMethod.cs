// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheSetIntPixelsMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("values", () => pixels.SetIntPixels(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("values", () => pixels.SetIntPixels(new int[] { 0, 0, 0, 0 }));
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayIsTooLong()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new int[(image.Width * image.Height * image.ChannelCount) + 1];

            Assert.Throws<ArgumentException>("values", () => pixels.SetIntPixels(values));
        }

        [Fact]
        public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new int[image.Width * image.Height * image.ChannelCount];
            pixels.SetIntPixels(values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}
