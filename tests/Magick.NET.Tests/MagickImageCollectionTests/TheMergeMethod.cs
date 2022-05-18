// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
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
                using (var collection = new MagickImageCollection())
                {
                    collection.Read(Files.RoseSparkleGIF);

                    using (var first = collection.Merge())
                    {
                        Assert.Equal(collection[0].Width, first.Width);
                        Assert.Equal(collection[0].Height, first.Height);
                    }
                }
            }
        }
    }
}
