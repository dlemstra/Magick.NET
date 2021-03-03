// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAlphaMethod
        {
            [Fact]
            public void ShouldMakeImageTransparent()
            {
                using (var image = new MagickImage(Files.Builtin.Wizard))
                {
                    Assert.False(image.HasAlpha);

                    image.Alpha(AlphaOption.Transparent);

                    Assert.True(image.HasAlpha);
                    ColorAssert.Equal(new MagickColor("#fff0"), image, 0, 0);
                }
            }

            [Fact]
            public void ShouldUseTheBackgroundColor()
            {
                using (var image = new MagickImage(Files.Builtin.Wizard))
                {
                    image.Alpha(AlphaOption.Transparent);

                    image.BackgroundColor = new MagickColor("red");
                    image.Alpha(AlphaOption.Background);
                    image.Alpha(AlphaOption.Off);

                    Assert.False(image.HasAlpha);
                    ColorAssert.Equal(new MagickColor(Quantum.Max, 0, 0), image, 0, 0);
                }
            }
        }
    }
}
