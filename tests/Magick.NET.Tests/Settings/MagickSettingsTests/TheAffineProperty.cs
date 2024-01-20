// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheAffineProperty
    {
        [Fact]
        public void ShouldDefaultToNull()
        {
            using var image = new MagickImage();

            Assert.Null(image.Settings.Affine);
        }

        [Fact]
        public void ShouldBeUsedWhenAnnotating()
        {
            using var image = new MagickImage(MagickColors.White, 300, 300);
            image.Annotate("Magick.NET", Gravity.Center);

            ColorAssert.Equal(MagickColors.White, image, 200, 200);

            using var imageWithAffine = new MagickImage(MagickColors.White, 300, 300);
            imageWithAffine.Settings.Affine = new DrawableAffine(10, 20, 30, 40, 50, 60);
            imageWithAffine.Annotate("Magick.NET", Gravity.Center);

            ColorAssert.Equal(MagickColors.Black, imageWithAffine, 200, 200);
        }
    }
}
