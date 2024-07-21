// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorRGBTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            Assert.Throws<ArgumentNullException>("color", () =>
            {
                new ColorRGB(null);
            });
        }
    }
}
