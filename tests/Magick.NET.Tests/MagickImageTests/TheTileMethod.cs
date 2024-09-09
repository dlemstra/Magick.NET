// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTileMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Tile(null!, CompositeOperator.Undefined));
        }

        [Fact]
        public void ShouldComposeAnImageRepeatedAcrossAndDownTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var checkerboard = new MagickImage(Files.Patterns.Checkerboard);
            image.Opaque(MagickColors.White, MagickColors.Transparent);
            image.Tile(checkerboard, CompositeOperator.DstOver);

            ColorAssert.Equal(new MagickColor("#66"), image, 578, 260);
        }
    }
}
