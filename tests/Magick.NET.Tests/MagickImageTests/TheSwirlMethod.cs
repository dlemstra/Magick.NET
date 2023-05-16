// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSwirlMethod
    {
        [Fact]
        public void ShouldSwirlTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Alpha(AlphaOption.Deactivate);

            ColorAssert.Equal(MagickColors.Red, image, 287, 74);
            ColorAssert.NotEqual(MagickColors.White, image, 363, 333);

            image.Swirl(60);

            ColorAssert.NotEqual(MagickColors.Red, image, 287, 74);
            ColorAssert.Equal(MagickColors.White, image, 363, 333);
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValue()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            using var other = image.Clone();
            image.Swirl(60);
            other.Swirl(other.Interpolate, 60);

            var distortion = other.Compare(image, ErrorMetric.RootMeanSquared);

            Assert.Equal(0.0, distortion);
        }
    }
}
