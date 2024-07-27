// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheAppendAppendVerticallyMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.AppendVertically());
        }

        [Fact]
        public void ShouldAppendTheImagesVertically()
        {
            var width = 70U;
            var height = 46U;

            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            Assert.Equal(width, images[0].Width);
            Assert.Equal(height, images[0].Height);

            using var image = images.AppendVertically();

            Assert.Equal(width, image.Width);
            Assert.Equal(height * 3, image.Height);
        }
    }
}
