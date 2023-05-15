// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheProfileNamesProperty
    {
        [Fact]
        public void ShouldReturnTheProfileNames()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var names = image.ProfileNames;

            Assert.NotNull(names);
            Assert.Equal("8bim,exif,icc,iptc,xmp", string.Join(",", names));
        }

        [Fact]
        public void ShouldReturnEmptyCollectionForImageWithoutProfiles()
        {
            using var image = new MagickImage(Files.RedPNG);
            var names = image.ProfileNames;

            Assert.NotNull(names);
            Assert.Empty(names);
        }
    }
}
