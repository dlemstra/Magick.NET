// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheSetXmpProfileMethod
    {
        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            profile.SetXmpProfile(image.GetXmpProfile());

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();

            Assert.NotNull(profile);
            Assert.NotNull(profile.GetXmpProfile());

            profile.SetXmpProfile(null);
            image.SetProfile(profile);

            stream.SetLength(0);
            stream.Position = 0;
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            Assert.Null(profile.GetXmpProfile());
        }

        [Fact]
        public void ShouldAddTheProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            var xmpProfile = image.GetXmpProfile();
            Assert.NotNull(xmpProfile);

            image.RemoveProfile(xmpProfile);

            xmpProfile = new XmpProfile([1, 2, 3, 4, 5, 6, 7, 8]);
            profile.SetXmpProfile(xmpProfile);

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            xmpProfile = image.GetXmpProfile();

            Assert.NotNull(profile);
            Assert.NotNull(profile.GetXmpProfile());
            Assert.NotNull(xmpProfile);
            Assert.Equal(8, xmpProfile.ToByteArray().Length);
        }
    }
}
