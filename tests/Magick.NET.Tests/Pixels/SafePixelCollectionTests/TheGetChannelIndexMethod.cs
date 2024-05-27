// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public class TheGetChannelIndexMethod
    {
        [Fact]
        public void ShouldReturnMinusOneForInvalidChannel()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            using var pixels = image.GetPixels();
            var index = pixels.GetChannelIndex(PixelChannel.Black);

            Assert.Equal(-1, index);
        }

        [Fact]
        public void ShouldReturnIndexForValidChannel()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            using var pixels = image.GetPixels();
            var index = pixels.GetChannelIndex(PixelChannel.Green);

            Assert.Equal(1, index);
        }
    }
}
