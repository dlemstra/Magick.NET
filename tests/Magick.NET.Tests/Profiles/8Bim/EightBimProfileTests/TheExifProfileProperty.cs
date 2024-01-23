// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheExifProfileProperty
    {
        [Fact]
        public void ShouldReturnNullWhenProfileHasNoIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile()!;

            Assert.Null(profile.ExifProfile);
        }

        [Fact]
        public void ShouldReturnValueWhenProfileHasIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var profile = image.Get8BimProfile();

            Assert.NotNull(profile.ExifProfile);
            Assert.Equal(446, profile.ExifProfile.GetData().Length);
            Assert.Empty(profile.ExifProfile.Values);
        }

        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var exifProfile = image.GetExifProfile();
            image.RemoveProfile(exifProfile);

            var profile = image.Get8BimProfile();
            profile.ExifProfile = null;

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            exifProfile = image.GetExifProfile();
            Assert.Null(exifProfile);
        }

        [Fact]
        public void ShouldAddTheProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();

            IExifProfile exifProfile = new ExifProfile();
            exifProfile.SetValue(ExifTag.Copyright, "Magick.NET");
            profile.ExifProfile = exifProfile;

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            exifProfile = image.GetExifProfile();
            Assert.NotNull(exifProfile);
            Assert.Single(exifProfile.Values);
        }
    }
}
