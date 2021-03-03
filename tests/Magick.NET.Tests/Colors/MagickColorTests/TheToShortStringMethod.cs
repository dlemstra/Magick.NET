// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickColorTests
    {
        public class TheToShortStringMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectString()
            {
                var color = new MagickColor(MagickColors.Red);
#if Q8
                Assert.Equal("#FF0000", color.ToShortString());
#else
                Assert.Equal("#FFFF00000000", color.ToShortString());
#endif
            }

            [Fact]
            public void ShouldIncludeTheAlphaChannelWhenNotFullyOpquery()
            {
                var color = new MagickColor(MagickColors.Red)
                {
                    A = 0,
                };

#if Q8
                Assert.Equal("#FF000000", color.ToShortString());
#else
                Assert.Equal("#FFFF000000000000", color.ToShortString());
#endif
            }

            [Fact]
            public void ShouldReturnTheCorrectStringForCmykColor()
            {
                var color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
                Assert.Equal("cmyk(0," + Quantum.Max + ",0,0)", color.ToShortString());
            }
        }
    }
}
