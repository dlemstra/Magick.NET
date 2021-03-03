// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickColorTests
    {
        public class TheToHexStringMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectString()
            {
                var color = new MagickColor(MagickColors.Red);
                Assert.Equal("#FF0000", color.ToHexString());
            }

            [Fact]
            public void ShouldIncludeTheAlphaChannelWhenNotFullyOpquery()
            {
                var color = new MagickColor(MagickColors.Red)
                {
                    A = 0,
                };

                Assert.Equal("#FF000000", color.ToHexString());
            }

            [Fact]
            public void ShouldThrowExceptionForCmykColor()
            {
                var color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
                Assert.Throws<NotSupportedException>(() => color.ToHexString());
            }
        }
    }
}
