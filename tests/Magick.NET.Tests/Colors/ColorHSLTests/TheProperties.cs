// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorHSLTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorHSL(0, 0, 0);

            color.Hue = 1;
            Assert.Equal(1, color.Hue);
            Assert.Equal(0, color.Saturation);
            Assert.Equal(0, color.Lightness);

            color.Saturation = 2;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(0, color.Lightness);

            color.Lightness = 3;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(3, color.Lightness);
        }
    }
}
