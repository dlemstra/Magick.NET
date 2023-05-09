// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheReverseMethod
    {
        [Fact]
        public void ShouldReverseTheImageOrder()
        {
            using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
            {
                var first = images.First();
                images.Reverse();

                var last = images.Last();
                Assert.True(last == first);
            }
        }
    }
}
