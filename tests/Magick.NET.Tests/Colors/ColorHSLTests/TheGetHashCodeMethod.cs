// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorHSLTests
{
    public class TheGetHashCodeMethod
    {
        [Fact]
        public void ShouldReturnDifferentValueWhenChannelChanged()
        {
            var first = new ColorHSL(0.0, 0.0, 0.0);
            var hashCode = first.GetHashCode();

            first.Hue = first.Saturation = first.Lightness = 1.0;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }
    }
}
