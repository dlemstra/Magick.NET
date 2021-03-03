// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheCoalesceMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Coalesce();
                    });
                }
            }

            [Fact]
            public void ShouldMergeTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Read(Files.RoseSparkleGIF);

                    using (var pixels = collection[1].GetPixels())
                    {
                        var color = pixels.GetPixel(53, 3).ToColor();
                        Assert.Equal(0, color.A);
                    }

                    collection.Coalesce();

                    using (var pixels = collection[1].GetPixels())
                    {
                        var color = pixels.GetPixel(53, 3).ToColor();
                        Assert.Equal(Quantum.Max, color.A);
                    }
                }
            }
        }
    }
}
