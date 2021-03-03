// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAutoOrientMethod
        {
            [Fact]
            public void ShouldRotateTheImage()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    Assert.Equal(600, image.Width);
                    Assert.Equal(400, image.Height);
                    Assert.Equal(OrientationType.TopLeft, image.Orientation);

                    image.Orientation = OrientationType.RightTop;

                    image.AutoOrient();

                    Assert.Equal(400, image.Width);
                    Assert.Equal(600, image.Height);
                    Assert.Equal(OrientationType.TopLeft, image.Orientation);
                }
            }
        }
    }
}
