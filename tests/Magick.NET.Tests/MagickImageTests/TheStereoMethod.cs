// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheStereoMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenRightImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("rightImage", () => image.Stereo(null!));
        }

        [Fact]
        public void ShouldCreateAnImageThatAppearsInStereo()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Flop();

            using var rightImage = new MagickImage(Files.Builtin.Logo);
            image.Stereo(rightImage);

            ColorAssert.Equal(new MagickColor("#2222ffffffff"), image, 250, 375);
            ColorAssert.Equal(new MagickColor("#ffff3e3e9292"), image, 380, 375);
        }
    }
}
