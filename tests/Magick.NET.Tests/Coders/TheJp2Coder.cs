// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheJp2Coder
    {
        [Fact]
        public void ShouldReadTheImageWithCorrectDimensions()
        {
            using (var image = new MagickImage(Files.Coders.GrimJP2))
            {
                Assert.Equal(2155, image.Width);
                Assert.Equal(2687, image.Height);
            }

            using (var image = new MagickImage(Files.Coders.GrimJP2 + "[0]"))
            {
                Assert.Equal(2155, image.Width);
                Assert.Equal(2687, image.Height);
            }

            using (var image = new MagickImage(Files.Coders.GrimJP2 + "[1]"))
            {
                Assert.Equal(256, image.Width);
                Assert.Equal(256, image.Height);
            }
        }
    }
}