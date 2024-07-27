// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheDeconstructMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Deconstruct());
        }

        [Fact]
        public void ShouldDeconstructTheImages()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 20, 20),
            };

            using var frames = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 10, 20),
                new MagickImage(MagickColors.Purple, 10, 20),
            };

            images.Add(frames.AppendHorizontally());

            Assert.Equal(20U, images[1].Width);
            Assert.Equal(20U, images[1].Height);
            Assert.Equal(new MagickGeometry(0, 0, 10, 20), images[1].Page);
            ColorAssert.Equal(MagickColors.Red, images[1], 3, 3);

            images.Deconstruct();

            Assert.Equal(10U, images[1].Width);
            Assert.Equal(20U, images[1].Height);
            Assert.Equal(new MagickGeometry(10, 0, 10, 20), images[1].Page);
            ColorAssert.Equal(MagickColors.Purple, images[1], 3, 3);
        }
    }
}
