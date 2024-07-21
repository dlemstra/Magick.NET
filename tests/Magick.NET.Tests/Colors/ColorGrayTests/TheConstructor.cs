// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorGrayTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenShadeIsTooLow()
        {
            Assert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(-0.99);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenShadeIsTooHigh()
        {
            Assert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(1.01);
            });
        }
    }
}
