// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorTests
{
    public class TheToHexStringMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectString()
        {
            var color = MagickColors.PowderBlue;
            Assert.Equal("#B0E0E6", color.ToHexString());
        }

        [Fact]
        public void ShouldIncludeTheAlphaChannelWhenNotFullyOpquery()
        {
            var color = new MagickColor("#b0e0e680");
            Assert.Equal("#B0E0E680", color.ToHexString());
        }

        [Fact]
        public void ShouldThrowExceptionForCmykColor()
        {
            var color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
            Assert.Throws<NotSupportedException>(() => color.ToHexString());
        }
    }
}
