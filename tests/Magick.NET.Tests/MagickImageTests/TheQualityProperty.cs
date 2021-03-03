// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheQualityProperty
        {
            [Fact]
            public void ShouldNotAllowValueBelowOne()
            {
                var image = new MagickImage
                {
                    Quality = 0,
                };

                Assert.Equal(1, image.Quality);
            }

            [Fact]
            public void ShouldNotAllowValueAbove100()
            {
                var image = new MagickImage
                {
                    Quality = 101,
                };

                Assert.Equal(100, image.Quality);
            }

            [Fact]
            public void ShouldSetTheBackgroundColorWhenReadingImage()
            {
                using (var image = new MagickImage(Files.CMYKJPG))
                {
                    Assert.Equal(91, image.Quality);
                }
            }
        }
    }
}
