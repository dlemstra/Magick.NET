// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFormatInfoTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnTrueWhenTheObjectsAreEqual()
        {
            var first = MagickFormatInfo.Create(MagickFormat.Png);
            var second = MagickFormatInfo.Create(Files.SnakewarePNG);

            Assert.True(first == second);
            Assert.NotNull(first);
            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }
    }
}
