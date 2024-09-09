// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheColorAlphaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("color", () => image.ColorAlpha(null!));
        }

        [Fact]
        public void ShouldSetTheAlphaChannelToTheSpecifiedColor()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var purple = new MagickColor("purple");

            image.ColorAlpha(purple);

            ColorAssert.NotEqual(purple, image, 45, 75);
            ColorAssert.Equal(purple, image, 100, 60);
        }
    }
}
