// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTintMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenOpacityIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("opacity", () => image.Tint(null!, MagickColors.Red));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("color", () => image.Tint(new MagickGeometry("2x2"), null!));
        }

        [Fact]
        public void ShouldApplyColorVectorToPixels()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Tint(new MagickGeometry("1x2"), MagickColors.Gold);
            image.Clamp();

            ColorAssert.Equal(new MagickColor("#dee500000000"), image, 400, 205);
            ColorAssert.Equal(MagickColors.Black, image, 400, 380);
        }
    }
}
