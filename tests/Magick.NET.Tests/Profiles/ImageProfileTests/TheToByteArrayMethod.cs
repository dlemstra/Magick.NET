// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageProfileTests
{
    public class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldReturnArrayWithCorrectLength()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var bytes = profile.ToByteArray();

            Assert.NotNull(bytes);
            Assert.Equal(281, bytes.Length);
        }
    }
}
