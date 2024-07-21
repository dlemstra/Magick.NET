// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorGrayTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorGray(0);

            color.Shade = 1;
            Assert.Equal(1, color.Shade);

            color.Shade = -0.99;
            Assert.Equal(1, color.Shade);

            color.Shade = 1.01;
            Assert.Equal(1, color.Shade);
        }
    }
}
