// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheRemoveValueMethod
    {
        [Fact]
        public void ShouldRemoveValueAndReturnTrue()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();

            Assert.NotNull(profile);
            Assert.True(profile.RemoveValue(ExifTag.FNumber));
        }

        [Fact]
        public void ShouldRemoveFalseWhenProfileDoesNotContainTag()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();

            Assert.NotNull(profile);
            Assert.False(profile.RemoveValue(ExifTag.Acceleration));
        }
    }
}
