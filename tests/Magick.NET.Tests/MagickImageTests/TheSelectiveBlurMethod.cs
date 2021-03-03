// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSelectiveBlurMethod
        {
            [Fact]
            public void ShouldBlurTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Rose))
                {
                    image.SelectiveBlur(0, 5, new Percentage(20));

#if Q8
                    ColorAssert.Equal(new MagickColor("#df3a39ff"), image, 37, 20);
#else
                    ColorAssert.Equal(new MagickColor("#df003a7738aeffff"), image, 37, 20);
#endif
                }
            }
        }
    }
}
