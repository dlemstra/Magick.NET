// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheAllowedIfdsProperty
    {
        [Fact]
        public void ShouldFilterTheTagsWhenWritten()
        {
            using var memStream = new MemoryStream();
            using var input = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = input.GetExifProfile();

            Assert.NotNull(profile);

            Assert.Equal(44, profile.Values.Count);

            profile.AllowedIfds = ExifIfds.Exif;
            input.SetProfile(profile);
            input.Write(memStream);

            memStream.Position = 0;
            using var output = new MagickImage(memStream);
            profile = output.GetExifProfile();

            Assert.NotNull(profile);
            Assert.Equal(24, profile.Values.Count);
        }
    }
}
