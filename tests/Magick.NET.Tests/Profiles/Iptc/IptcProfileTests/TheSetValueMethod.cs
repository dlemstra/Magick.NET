// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class IptcProfileTests
{
    public class TheSetValueMethod
    {
        [Fact]
        public void ShouldChangeTheValue()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var value = profile.GetValue(IptcTag.Title);

            profile.SetValue(IptcTag.Title, "Magick.NET Title");

            Assert.NotNull(value);
            Assert.Equal("Magick.NET Title", value.Value);

            value = profile.GetValue(IptcTag.Title);

            Assert.NotNull(value);
            Assert.Equal("Magick.NET Title", value.Value);
        }

        [Fact]
        public void ShouldAddValueThatDoesNotExist()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var value = profile.GetValue(IptcTag.ReferenceNumber);

            Assert.Null(value);

            profile.SetValue(IptcTag.Title, "Magick.NET ReferenceNümber");

            value = profile.GetValue(IptcTag.Title);

            Assert.NotNull(value);
            Assert.Equal("Magick.NET ReferenceNümber", value.Value);
        }
    }
}
