// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheXmpProfileProperty
    {
        [Fact]
        public void ShouldReturnNullWhenProfileHasNoIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile()!;

            Assert.Null(profile.XmpProfile);
        }

        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();
            profile.XmpProfile = image.GetXmpProfile();

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            Assert.NotNull(profile.XmpProfile);

            profile.XmpProfile = null;
            image.SetProfile(profile);

            stream.SetLength(0);
            stream.Position = 0;
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            Assert.Null(profile.XmpProfile);
        }

        [Fact]
        public void ShouldAddTheProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();

            var xmpProfile = image.GetXmpProfile();
            image.RemoveProfile(xmpProfile);

            xmpProfile = new XmpProfile([1, 2, 3, 4, 5, 6, 7, 8]);
            profile.XmpProfile = xmpProfile;

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            xmpProfile = image.GetXmpProfile();

            Assert.NotNull(profile.XmpProfile);
            Assert.NotNull(xmpProfile);
            Assert.Equal(8, xmpProfile.GetData().Length);
        }
    }
}
