// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheRewriteMethod : ExifProfileTests
    {
        [Fact]
        public void ShouldUpdateTheData()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();

            var before = profile.ToByteArray();
            profile.Rewrite();

            var after = profile.ToByteArray();

            Assert.NotNull(after);
            Assert.NotSame(before, after);
            Assert.Equal(4706, before.Length);
            Assert.Equal(958, after.Length);
        }
    }
}
