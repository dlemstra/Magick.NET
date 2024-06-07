// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColorFuzzProperty
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNegative()
        {
            using var image = new MagickImage();
            Assert.Throws<ArgumentException>("value", () => image.ColorFuzz = new Percentage(-1));
        }
#endif
    }
}
