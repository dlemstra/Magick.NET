// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorYUVTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorYUV(0, 0, 0);

            color.Y = 1;
            Assert.Equal(1, color.Y);
            Assert.Equal(0, color.U);
            Assert.Equal(0, color.V);

            color.U = 2;
            Assert.Equal(1, color.Y);
            Assert.Equal(2, color.U);
            Assert.Equal(0, color.V);

            color.V = 3;
            Assert.Equal(1, color.Y);
            Assert.Equal(2, color.U);
            Assert.Equal(3, color.V);
        }
    }
}
