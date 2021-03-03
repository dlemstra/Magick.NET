// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        public class TheFontProperty
        {
            [Fact]
            public void ShouldSetTheFontWhenReadingImage()
            {
                using (var image = new MagickImage())
                {
                    Assert.Null(image.Settings.Font);

                    image.Settings.Font = "Courier New";
                    image.Settings.FontPointsize = 40;
                    image.Read("pango:Test");

                    // Different result on MacOS
                    if (image.Width != 40)
                    {
                        Assert.Equal(128, image.Width);
                        Assert.Contains(image.Height, new[] { 58, 62 });
                        ColorAssert.Equal(MagickColors.Black, image, 26, 22);
                    }
                }
            }
        }
    }
}
