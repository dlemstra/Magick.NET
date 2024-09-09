// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class IptcValueTests
{
    public class TheProperties
    {
        [Fact]
        public void ShouldSetTheCorrectValue()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var value = profile.Values[1];

            Assert.Equal(IptcTag.Caption, value.Tag);
            Assert.Equal("Communications", value.ToString());
            Assert.Equal("Communications", value.Value);
            Assert.Equal(14, value.ToByteArray().Length);
        }
    }
}
