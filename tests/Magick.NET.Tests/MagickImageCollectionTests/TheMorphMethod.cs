// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheMorphMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => { images.Morph(10); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenCollectionContainsSingleImage()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 1, 1),
            };

            Assert.Throws<InvalidOperationException>(() => { images.Morph(10); });
        }

        [Fact]
        public void ShouldMorphTheImages()
        {
            using var images = new MagickImageCollection
            {
                Files.Builtin.Logo,
            };
            images.AddRange(Files.Builtin.Wizard);

            images.Morph(4);
            Assert.Equal(6, images.Count);
        }
    }
}
