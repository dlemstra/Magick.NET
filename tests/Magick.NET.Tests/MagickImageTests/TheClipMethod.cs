// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheClipMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPathNameIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("pathName", () => image.Clip(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPathNameIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("pathName", () => image.Clip(string.Empty));
        }

        [Fact]
        public void ShouldSetTheCorrectColors()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            image.Alpha(AlphaOption.Transparent);
            image.Clip("Pad A");
            image.Alpha(AlphaOption.Opaque);

            using var mask = image.GetWriteMask();

            Assert.NotNull(mask);
            Assert.False(mask.HasAlpha);

            using var pixels = mask.GetPixels();
            var pixelA = pixels.GetPixel(0, 0).ToColor();
            var pixelB = pixels.GetPixel((int)mask.Width - 1, (int)mask.Height - 1).ToColor();

            Assert.Equal(pixelA, pixelB);
            Assert.Equal(0, pixelA.R);
            Assert.Equal(0, pixelA.G);
            Assert.Equal(0, pixelA.B);

            var pixelC = pixels.GetPixel((int)mask.Width / 2, (int)mask.Height / 2).ToColor();
            Assert.Equal(Quantum.Max, pixelC.R);
            Assert.Equal(Quantum.Max, pixelC.G);
            Assert.Equal(Quantum.Max, pixelC.B);
        }
    }
}
