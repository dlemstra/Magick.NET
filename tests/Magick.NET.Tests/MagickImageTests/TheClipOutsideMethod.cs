// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheClipOutsideMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPathNameIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("pathName", () => image.ClipOutside(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPathNameIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("pathName", () => image.ClipOutside(string.Empty));
        }

        [Fact]
        public void ShouldSetTheCorrectColors()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            image.Alpha(AlphaOption.Transparent);
            image.ClipOutside("Pad A");
            image.Alpha(AlphaOption.Opaque);

            using var mask = image.GetWriteMask();

            Assert.NotNull(mask);
            Assert.False(mask.HasAlpha);

            using var pixels = mask.GetPixels();
            var pixelA = pixels.GetPixel(0, 0).ToColor();
            var pixelB = pixels.GetPixel((int)mask.Width - 1, (int)mask.Height - 1).ToColor();

            Assert.Equal(pixelA, pixelB);
            Assert.Equal(Quantum.Max, pixelA.R);
            Assert.Equal(Quantum.Max, pixelA.G);
            Assert.Equal(Quantum.Max, pixelA.B);

            var pixelC = pixels.GetPixel((int)mask.Width / 2, (int)mask.Height / 2).ToColor();
            Assert.Equal(0, pixelC.R);
            Assert.Equal(0, pixelC.G);
            Assert.Equal(0, pixelC.B);
        }
    }
}
