// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheTextAntiAliasProperty
        {
            [Fact]
            public void ShouldDisableTextAntialiasingWhenFalse()
            {
                using (var image = new MagickImage(MagickColors.Azure, 300, 300))
                {
                    Assert.True(image.Settings.TextAntiAlias);

                    image.Settings.TextAntiAlias = false;
                    image.Settings.FontPointsize = 100;
                    image.Annotate("TEST", Gravity.Center);

                    ColorAssert.Equal(MagickColors.Azure, image, 158, 125);
                    ColorAssert.Equal(MagickColors.Black, image, 158, 126);
                    ColorAssert.Equal(MagickColors.Azure, image, 209, 127);
                    ColorAssert.Equal(MagickColors.Black, image, 209, 128);
                }
            }
        }
    }
}
