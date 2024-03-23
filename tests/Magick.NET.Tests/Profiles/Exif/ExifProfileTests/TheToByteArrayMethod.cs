// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public partial class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldReturnOriginalDataWhenNotParsed()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();
            var bytes = profile.ToByteArray();

            Assert.Equal(4706, bytes.Length);
        }
    }
}
