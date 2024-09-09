// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheSetExifProfileMethod
    {
        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var exifProfile = image.GetExifProfile();
            Assert.NotNull(exifProfile);

            image.RemoveProfile(exifProfile);

            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            profile.SetExifProfile(null);

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
            Assert.NotNull(profile);

            IExifProfile? exifProfile = new ExifProfile();
            exifProfile.SetValue(ExifTag.Copyright, "Magick.NET");
            profile.SetExifProfile(exifProfile);

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            exifProfile = image.GetExifProfile();

            Assert.NotNull(profile);
            Assert.NotNull(profile.GetExifProfile());
            Assert.NotNull(exifProfile);
            Assert.Single(exifProfile.Values);
        }
    }
}
