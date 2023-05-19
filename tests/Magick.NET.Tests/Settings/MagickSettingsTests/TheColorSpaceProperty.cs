// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheColorSpaceProperty
    {
        [Fact]
        public void ShouldDefaultToUndefined()
        {
            using var image = new MagickImage();

            Assert.Equal(ColorSpace.Undefined, image.Settings.ColorSpace);
        }

        [Fact]
        public void ShouldBeUsedWhenReadingTheImage()
        {
            using var image = new MagickImage();

            Assert.Equal(ColorSpace.Undefined, image.Settings.ColorSpace);

            image.Read(Files.ImageMagickJPG);

            ColorAssert.Equal(MagickColors.White, image, 0, 0);

            image.Settings.ColorSpace = ColorSpace.Rec601YCbCr;
            image.Read(Files.ImageMagickJPG);

            ColorAssert.Equal(new MagickColor("#ff8080"), image, 0, 0);
        }
    }
}
