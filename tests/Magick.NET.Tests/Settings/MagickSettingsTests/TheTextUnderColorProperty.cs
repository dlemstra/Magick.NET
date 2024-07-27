// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheTextUnderColorProperty
    {
        [Fact]
        public void ShouldDefaultToBlack()
        {
            using var image = new MagickImage();

            ColorAssert.Equal(new MagickColor(0, 0, 0, 0), image.Settings.TextUnderColor);
        }

        [Fact]
        public void ShouldUseBlackWhenSetToNull()
        {
            using var image = new MagickImage();
            image.Settings.TextUnderColor = null;
            image.Read("label:First");

            Assert.Equal(25U, image.Width);
            Assert.Equal(15U, image.Height);

            ColorAssert.Equal(MagickColors.White, image, 0, 0);
            ColorAssert.Equal(MagickColors.White, image, 23, 0);
        }

        [Fact]
        public void ShouldUseTheSpecifiedColor()
        {
            using var image = new MagickImage();
            image.Settings.TextUnderColor = MagickColors.Purple;
            image.Read("label:First");

            Assert.Equal(25U, image.Width);
            Assert.Equal(15U, image.Height);

            ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            ColorAssert.Equal(MagickColors.Purple, image, 23, 0);
        }
    }
}
