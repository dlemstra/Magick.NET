// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheShadowMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("color", () => image.Shadow(null!));
        }

        [Fact]
        public void ShouldAddShadowToImage()
        {
            using var image = new MagickImage();
            image.Settings.BackgroundColor = MagickColors.Transparent;
            image.Settings.FontPointsize = 60;
            image.Read("label:Magick.NET");

            var width = image.Width;
            var height = image.Height;

            image.Shadow(2, 2, 5, new Percentage(50), MagickColors.Red);

            Assert.Equal(width + 20U, image.Width);
            Assert.Equal(height + 20U, image.Height);

            using var pixels = image.GetPixels();
            var pixel = pixels.GetPixel(90, 9);

            var color = pixel.ToColor();
            Assert.NotNull(color);

            Assert.Equal(0, color.A);

            pixel = pixels.GetPixel(34, 55);

            color = pixel.ToColor();
            Assert.NotNull(color);

#if Q8
            Assert.Equal(68, color.A);
#else
            Assert.InRange(color.A, 17057, 17058);
#endif
        }
    }
}
