// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTransverseMethod
    {
        [Fact]
        public void ShouldCreateVerticalMirrorImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Transverse();

            Assert.Equal(480U, image.Width);
            Assert.Equal(640U, image.Height);

            ColorAssert.Equal(MagickColors.Red, image, 330, 508);
            ColorAssert.Equal(new MagickColor("#f5f5eeee3636"), image, 288, 474);
            ColorAssert.Equal(new MagickColor("#cdcd20202727"), image, 30, 123);
        }
    }
}
