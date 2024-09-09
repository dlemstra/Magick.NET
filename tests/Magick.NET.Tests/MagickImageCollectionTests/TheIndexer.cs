// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheIndexer
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNull()
        {
            using var images = new MagickImageCollection(Files.CirclePNG);

            Assert.Throws<InvalidOperationException>(() => images[0] = null!);
        }

        [Fact]
        public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
        {
            var imageA = new MagickImage();
            var imageB = new MagickImage();

            using var images = new MagickImageCollection(new[] { imageA, imageB });

            Assert.Throws<InvalidOperationException>(() => images[0] = imageB);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenImageIsSameImage()
        {
            var image = new MagickImage();

            using var images = new MagickImageCollection(new[] { image })
            {
                [0] = image,
            };
        }
    }
}
