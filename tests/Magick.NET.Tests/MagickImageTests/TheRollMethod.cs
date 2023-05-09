// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheRollMethod
    {
        [Fact]
        public void ShouldRollTheImage()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.Roll(40, 60);

                var blue = new MagickColor("#a8dff8");
                ColorAssert.Equal(blue, image, 66, 103);
                ColorAssert.Equal(blue, image, 120, 86);
                ColorAssert.Equal(blue, image, 0, 82);
            }
        }
    }
}
