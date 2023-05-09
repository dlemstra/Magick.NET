// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheMergeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using (var images = new MagickImageCollection())
            {
                Assert.Throws<InvalidOperationException>(() => images.Merge());
            }
        }

        [Fact]
        public void ShouldMergeTheImages()
        {
            using (var images = new MagickImageCollection())
            {
                images.Read(Files.RoseSparkleGIF);

                using (var first = images.Merge())
                {
                    Assert.Equal(images[0].Width, first.Width);
                    Assert.Equal(images[0].Height, first.Height);
                }
            }
        }
    }
}
