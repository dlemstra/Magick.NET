// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheSetBytePixelsMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetBytePixels(null);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetBytePixels(new byte[] { 0, 0, 0, 0 });
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsTooLong()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new byte[(image.Width * image.Height * image.ChannelCount) + 1];
            pixels.SetBytePixels(values);
        }

        [Fact]
        public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new byte[image.Width * image.Height * image.ChannelCount];
            pixels.SetBytePixels(values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}
