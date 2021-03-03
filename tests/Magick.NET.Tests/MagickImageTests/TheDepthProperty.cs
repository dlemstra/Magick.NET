// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheDepthProperty
        {
            [Fact]
            public void ShouldAllowValueHigherThanQuantum()
            {
                using (var image = new MagickImage())
                {
                    var depth = Quantum.Depth * 2;

                    image.Depth = depth;
                    Assert.Equal(depth, image.Depth);
                }
            }
        }
    }
}
