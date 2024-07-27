// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheShearMethod
    {
        [Fact]
        public void ShouldShearTheImage()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.BackgroundColor = MagickColors.Firebrick;
            image.VirtualPixelMethod = VirtualPixelMethod.Background;
            image.Shear(20, 40);

            Assert.Equal(186U, image.Width);
            Assert.Equal(195U, image.Height);
            ColorAssert.Equal(MagickColors.Red, image, 14, 68);
            ColorAssert.Equal(MagickColors.Firebrick, image, 45, 6);
            ColorAssert.Equal(MagickColors.Blue, image, 150, 171);
            ColorAssert.Equal(MagickColors.Firebrick, image, 158, 181);
        }
    }
}
