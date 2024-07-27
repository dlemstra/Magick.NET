// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheTrimBoundsMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.TrimBounds());
        }

        [Fact]
        public void ShouldAdjustTheCanvas()
        {
            using var images = new MagickImageCollection
            {
                Files.Builtin.Logo,
                Files.Builtin.Wizard,
            };

            images.TrimBounds();

            Assert.Equal(640U, images[0].Page.Width);
            Assert.Equal(640U, images[0].Page.Height);
            Assert.Equal(0, images[0].Page.X);
            Assert.Equal(0, images[0].Page.Y);

            Assert.Equal(640U, images[1].Page.Width);
            Assert.Equal(640U, images[1].Page.Height);
            Assert.Equal(0, images[0].Page.X);
            Assert.Equal(0, images[0].Page.Y);
        }
    }
}
