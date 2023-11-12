// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheDisposeMethod
    {
        [Fact]
        public void ShouldRemoveAndDisposeTheImages()
        {
            var image = new MagickImage(MagickColors.Red, 10, 10);
            var collection = new MagickImageCollection
            {
                image,
            };
            collection.Dispose();

            Assert.Empty(collection);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenCalledTwice()
        {
            var collection = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 10, 10),
            };

            collection.Dispose();
            collection.Dispose();
        }
    }
}
