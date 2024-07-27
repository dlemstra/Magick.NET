// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheRemoveThumbnailMethod
    {
        [Fact]
        public void ShouldRemoveTheThumbnail()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();

            Assert.NotNull(profile);

            profile.RemoveThumbnail();

            Assert.Equal(0U, profile.ThumbnailLength);
            Assert.Equal(0U, profile.ThumbnailOffset);
        }
    }
}
