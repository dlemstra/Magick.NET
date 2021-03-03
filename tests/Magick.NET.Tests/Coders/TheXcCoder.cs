// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheXcCoder
    {
        [Fact]
        public void ShouldHandleSrgbaColor()
        {
            using (var image = new MagickImage("xc:srgba(255,0,0,1)", 1, 1))
            {
                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            }
        }

        [Fact]
        public void ShouldHandleRgbColor()
        {
            using (var image = new MagickImage("xc:rgb(0,50%,0)", 1, 1))
            {
                ColorAssert.Equal(new MagickColor("#000080000000"), image, 0, 0);
            }
        }

        [Fact]
        public void ShouldHandleHslaColor()
        {
            using (var image = new MagickImage("xc:hsla(180,255,127.5,0.5)", 1, 1))
            {
                ColorAssert.Equal(new MagickColor("#0000ffffffff8000"), image, 0, 0);
            }
        }
    }
}