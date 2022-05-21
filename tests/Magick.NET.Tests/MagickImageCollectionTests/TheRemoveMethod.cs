// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheRemoveMethod
        {
            [Fact]
            public void ShouldRemoveTheSpecifiedImage()
            {
                using (var images = new MagickImageCollection(Files.RoseSparkleGIF))
                {
                    var first = images[0];
                    images.Remove(first);

                    Assert.Equal(2, images.Count);
                    Assert.Equal(-1, images.IndexOf(first));
                }
            }
        }
    }
}
