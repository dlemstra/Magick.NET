// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheEmbossMethod
    {
        [Fact]
        public void ShouldUseTheCorrectDefaultValues()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                using (var other = new MagickImage(Files.Builtin.Wizard))
                {
                    image.Emboss(0.0, 1.0);

                    other.Emboss();

                    Assert.Equal(image, other);
                }
            }
        }

        [Fact]
        public void ShouldHighlightEdgesWith3dEffect()
        {
            using (var image = new MagickImage(Files.Builtin.Wizard))
            {
                image.Emboss(4, 2);

#if Q8
                ColorAssert.Equal(new MagickColor("#ff5b43"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#4344ff"), image, 99, 270);
#elif Q16
                ColorAssert.Equal(new MagickColor("#ffff597e4397"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#431f43f0ffff"), image, 99, 270);
#else
                ColorAssert.Equal(new MagickColor("#ffff59624391"), image, 325, 175);
                ColorAssert.Equal(new MagickColor("#431843e8ffff"), image, 99, 270);
#endif
            }
        }
    }
}
