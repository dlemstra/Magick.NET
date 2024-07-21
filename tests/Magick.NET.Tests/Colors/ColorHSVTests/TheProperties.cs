// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorHSVTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorHSV(0, 0, 0);

            color.Hue = 1;
            Assert.Equal(1, color.Hue);
            Assert.Equal(0, color.Saturation);
            Assert.Equal(0, color.Value);

            color.Saturation = 2;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(0, color.Value);

            color.Value = 3;
            Assert.Equal(1, color.Hue);
            Assert.Equal(2, color.Saturation);
            Assert.Equal(3, color.Value);
        }
    }
}
