// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class IptcValueTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnTrueWhenTheObjectsAreEqual()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();
            var first = profile.Values[1];
            var second = profile.Values[1];

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }
    }
}
