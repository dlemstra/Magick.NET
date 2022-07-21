// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheMatteColorProperty
        {
            [Fact]
            public void ShouldBeUsedWhenFramingImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.MatteColor = MagickColors.PaleGoldenrod;
                    image.Frame();

                    ColorAssert.Equal(MagickColors.PaleGoldenrod, image, 10, 10);
                    ColorAssert.Equal(MagickColors.PaleGoldenrod, image, 680, 520);
                }
            }
        }
    }
}
