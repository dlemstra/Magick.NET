// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorMonoTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            var color = ColorMono.White;

            color.IsBlack = true;
            Assert.True(color.IsBlack);
        }
    }
}
