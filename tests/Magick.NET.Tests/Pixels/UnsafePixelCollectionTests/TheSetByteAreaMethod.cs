// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheSetByteAreaMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetByteArea(10, 10, 1000, 1000, null);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetByteArea(10, 10, 1000, 1000, new byte[] { 0, 0, 0, 0 });
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayHasTooManyValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new byte[(113 * 108 * image.ChannelCount) + image.ChannelCount];
            pixels.SetByteArea(10, 10, 113, 108, values);
        }

        [Fact]
        public void ShouldChangePixelsWhenArrayHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new byte[113 * 108 * image.ChannelCount];
            pixels.SetByteArea(10, 10, 113, 108, values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsSpecifiedAndGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetByteArea(null, new byte[] { 0 });
        }

        [Fact]
        public void ShouldChangePixelsWhenGeometryAndArrayAreSpecified()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new byte[113 * 108 * image.ChannelCount];
            pixels.SetByteArea(new MagickGeometry(10, 10, 113, 108), values);

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}
