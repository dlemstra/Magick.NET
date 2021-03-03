// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ColorCMYKTests : ColorBaseTests<ColorCMYK>
    {
        public class TheNativeInstance
        {
            [Fact]
            public void ShouldHaveTheCorrectColorspace()
            {
                using (var image = new MagickImage(MagickColors.Black, 1, 1))
                {
                    image.ColorSpace = ColorSpace.CMYK;
                    image.Opaque(MagickColors.Black, new MagickColor("cmyk(128,23,250,156)"));

                    using (var pixels = image.GetPixelsUnsafe())
                    {
                        var color = pixels.GetPixel(0, 0).ToColor();
#if Q8
                        Assert.Equal("cmyka(128,23,250,156,1.0)", color.ToString());
#else
                        Assert.Equal("cmyka(32896,5911,64250,40092,1.0)", color.ToString());
#endif
                    }
                }
            }
        }
    }
}
