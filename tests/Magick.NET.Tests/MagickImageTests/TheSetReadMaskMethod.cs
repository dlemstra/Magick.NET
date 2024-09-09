// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetReadMaskMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.SetReadMask(null!));
        }

        [Fact]
        public void ShouldSetMaskForWholeImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var imageMask = new MagickImage(MagickColors.White, 10, 15);
            image.SetReadMask(imageMask);

            using var mask = image.GetReadMask();

            Assert.NotNull(mask);
            Assert.Equal(640U, mask.Width);
            Assert.Equal(480U, mask.Height);
            ColorAssert.Equal(MagickColors.White, mask, 9, 14);
            ColorAssert.Equal(MagickColors.Black, mask, 10, 15);
        }
    }
}
