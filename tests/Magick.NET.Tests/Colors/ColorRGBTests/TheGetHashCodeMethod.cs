// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorRGBTests
{
    public class TheGetHashCodeMethod
    {
        [Fact]
        public void ShouldReturnDifferentValueWhenChannelChanged()
        {
            var first = new ColorRGB(0, 0, 0);
            var hashCode = first.GetHashCode();

            first.B = Quantum.Max;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }
    }
}
