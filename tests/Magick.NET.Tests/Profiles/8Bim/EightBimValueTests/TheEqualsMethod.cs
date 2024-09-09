// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimValueTests
{
    public class TheEqualsMethod
    {
        [Fact]
        public void ShouldReturnTrueWhenTheObjectsAreEqual()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            var first = profile.Values.First();
            var second = profile.Values.First();

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }
    }
}
