// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSpliceMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => image.Splice(null!));
        }

        [Fact]
        public void ShouldSpliceTheBackgroundColorIntoTheImage()
        {
            using var image = new MagickImage(Files.SnakewarePNG);
            image.BackgroundColor = MagickColors.Fuchsia;
            image.Splice(new MagickGeometry(105, 50, 10, 20));

            Assert.Equal(296U, image.Width);
            Assert.Equal(87U, image.Height);
            ColorAssert.Equal(MagickColors.Fuchsia, image, 105, 50);
            ColorAssert.Equal(new MagickColor("#0000"), image, 115, 70);
        }
    }
}
