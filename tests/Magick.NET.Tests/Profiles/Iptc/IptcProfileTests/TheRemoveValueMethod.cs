// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class IptcProfileTests
{
    public class TheRemoveValueMethod
    {
        [Fact]
        public void ShouldRemoveTheValueAndReturnTrueWhenValueWasFound()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var result = profile.RemoveValue(IptcTag.Title);

            Assert.True(result);

            var value = profile.GetValue(IptcTag.Title);

            Assert.Null(value);
        }

        [Fact]
        public void ShouldReturnFalseWhenProfileDoesNotContainTag()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var result = profile.RemoveValue(IptcTag.ReferenceNumber);

            Assert.False(result);
        }
    }
}
