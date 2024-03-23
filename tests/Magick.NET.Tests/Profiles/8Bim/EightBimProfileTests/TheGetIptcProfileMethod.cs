// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheGetIptcProfileMethod
    {
        [Fact]
        public void ShouldReturnNullWhenProfileHasNoIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile()!;

            Assert.Null(profile.GetIptcProfile());
        }

        [Fact]
        public void ShouldReturnValueWhenProfileHasIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var profile = image.Get8BimProfile();
            var iptcProfile = profile.GetIptcProfile();

            Assert.NotNull(profile.GetIptcProfile());
            Assert.Equal(15, iptcProfile.ToByteArray().Length);
            Assert.Single(iptcProfile.Values);
        }
    }
}
