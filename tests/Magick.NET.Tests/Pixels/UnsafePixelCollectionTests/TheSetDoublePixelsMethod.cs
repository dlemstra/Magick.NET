// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheSetDoublePixelsMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetDoublePixels((double[])null);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetDoublePixels(new double[] { 0, 0, 0, 0 });
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsTooLong()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new double[(image.Width * image.Height * image.ChannelCount) + 1];
            pixels.SetDoublePixels(values);
        }

        [Fact]
        public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new double[image.Width * image.Height * image.ChannelCount];
            pixels.SetDoublePixels(values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}
