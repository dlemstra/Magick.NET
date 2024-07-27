// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheSmushVerticalMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.SmushVertical(1));
        }

        [Fact]
        public void ShouldSmushTheImagesHorizontally()
        {
            using var images = new MagickImageCollection();
            images.AddRange(Files.RoseSparkleGIF);

            using var image = images.SmushVertical(40);

            Assert.Equal(70U, image.Width);
            Assert.Equal((46U * 3U) + (40U * 2U), image.Height);
        }
    }
}
