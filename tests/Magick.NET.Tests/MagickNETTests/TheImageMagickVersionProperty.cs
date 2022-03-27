// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public partial class TheImageMagickVersionProperty
        {
            [Fact]
            public void ShouldContainCorrectQuantum()
            {
#if Q8
                Assert.Contains("Q8", MagickNET.ImageMagickVersion);
#elif Q16
                Assert.Contains("Q16", MagickNET.ImageMagickVersion);
#else
                Assert.Contains("Q16-HDRI", MagickNET.ImageMagickVersion);
#endif
            }

            [Fact]
            public void ShouldContainTheCorrectVersion()
            {
                Assert.Contains(" 7.", MagickNET.ImageMagickVersion);
            }
        }
    }
}
