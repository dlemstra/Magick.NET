// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorCMYKTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = new ColorCMYK(0, 0, 0, 0);

            color.C = 1;
            Assert.Equal(1, color.C);
            Assert.Equal(0, color.M);
            Assert.Equal(0, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.M = 2;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(0, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.Y = 3;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(0, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.K = 4;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(4, color.K);
            Assert.Equal(Quantum.Max, color.A);

            color.A = 5;
            Assert.Equal(1, color.C);
            Assert.Equal(2, color.M);
            Assert.Equal(3, color.Y);
            Assert.Equal(4, color.K);
            Assert.Equal(5, color.A);
        }
    }
}
