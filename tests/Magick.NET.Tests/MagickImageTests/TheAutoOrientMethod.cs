// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAutoOrientMethod
    {
        [Fact]
        public void ShouldRotateTheImage()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG);

            Assert.Equal(600U, image.Width);
            Assert.Equal(400U, image.Height);
            Assert.Equal(OrientationType.TopLeft, image.Orientation);

            image.Orientation = OrientationType.RightTop;
            image.AutoOrient();

            Assert.Equal(400U, image.Width);
            Assert.Equal(600U, image.Height);
            Assert.Equal(OrientationType.TopLeft, image.Orientation);
        }
    }
}
