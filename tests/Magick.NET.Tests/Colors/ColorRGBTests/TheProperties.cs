// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorRGBTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorRGB(0, 0, 0);

            color.R = 1;
            Assert.Equal(1, color.R);
            Assert.Equal(0, color.G);
            Assert.Equal(0, color.B);

            color.G = 2;
            Assert.Equal(1, color.R);
            Assert.Equal(2, color.G);
            Assert.Equal(0, color.B);

            color.B = 3;
            Assert.Equal(1, color.R);
            Assert.Equal(2, color.G);
            Assert.Equal(3, color.B);
        }
    }
}
