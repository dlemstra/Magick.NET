// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public partial class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldPreserveTheThumbnail()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();

            Assert.NotNull(profile);

            var bytes = profile.ToByteArray();

            profile = new ExifProfile(bytes);

            using var thumbnail = profile.CreateThumbnail();

            Assert.NotNull(thumbnail);
            Assert.Equal(128U, thumbnail.Width);
            Assert.Equal(85U, thumbnail.Height);
            Assert.Equal(MagickFormat.Jpeg, thumbnail.Format);
        }
    }
}

#endif
