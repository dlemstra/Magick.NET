// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheRemoveAtMethod
    {
        [Fact]
        public void ShouldRemoveTheImageAtTheSpecifiedIndex()
        {
            using var images = new MagickImageCollection(Files.RoseSparkleGIF);
            var second = images[1];
            images.RemoveAt(1);

            Assert.Equal(2, images.Count);
            Assert.Equal(-1, images.IndexOf(second));
        }
    }
}
