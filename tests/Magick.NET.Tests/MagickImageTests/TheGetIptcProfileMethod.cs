// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheGetIptcProfileMethod
    {
        [Fact]
        public void ShouldReturnTheIptcProfile()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

            var profile = image.GetIptcProfile();
            Assert.NotNull(profile);
        }

        [Fact]
        public void ShouldReturnNullWhenProfileEmpty()
        {
            using var image = new MagickImage(Files.PictureJPG);

            var profile = image.GetIptcProfile();
            Assert.Null(profile);
        }
    }
}
