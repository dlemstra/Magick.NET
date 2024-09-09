// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class IptcProfileTests
{
    public class TheGetValueMethod
    {
        [Fact]
        public void ShouldReturnNullWhenImageDoesNotContainValue()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var value = profile.GetValue(IptcTag.ReferenceNumber);

            Assert.Null(value);
        }

        [Fact]
        public void ShouldReturnTheValue()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var value = profile.GetValue(IptcTag.Title);

            Assert.NotNull(value);
            Assert.Equal("Communications", value.Value);
        }
    }
}
