// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTextureMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Texture(null!));
        }

        [Fact]
        public void ShouldAddTextureToImageBackground()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            using var canvas = new MagickImage(MagickColors.Fuchsia, 300, 300);
            canvas.Texture(image);

            ColorAssert.Equal(MagickColors.Fuchsia, canvas, 72, 68);
            ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), canvas, 299, 48);
            ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), canvas, 160, 299);
        }
    }
}
