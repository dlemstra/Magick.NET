// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDetermineColorTypeMethod
    {
        [Fact]
        public void ShouldDetermineTheColorTypeOfTheImage()
        {
            using (var image = new MagickImage(Files.WireframeTIF))
            {
                Assert.Equal(ColorType.TrueColor, image.ColorType);

                var colorType = image.DetermineColorType();
                Assert.Equal(ColorType.Grayscale, colorType);
            }
        }
    }
}
