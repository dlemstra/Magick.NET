// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChopVerticalMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            Assert.Throws<ArgumentException>("height", () => image.ChopVertical(-1, -1));
        }

        [Fact]
        public void ShouldChopTheImageVertically()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.ChopVertical(10, 200);

            Assert.Equal(480, image.Width);
            Assert.Equal(440, image.Height);
        }
    }
}
