// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheSetValueMethod
    {
        [Fact]
        public void ShouldUpdateTheDataInTheProfile()
        {
            using var input = new MagickImage(Files.ImageMagickJPG);
            var profile = input.GetExifProfile();

            Assert.Null(profile);

            profile = new ExifProfile();
            profile.SetValue(ExifTag.Copyright, "Dirk Lemstra");

            input.SetProfile(profile);
            profile = input.GetExifProfile();
            Assert.NotNull(profile);

            var bytes = input.ToByteArray();

            using var image = new MagickImage(bytes);
            profile = image.GetExifProfile();

            Assert.NotNull(profile);
            Assert.Single(profile.Values);

            var value = profile.Values.FirstOrDefault(val => val.Tag == ExifTag.Copyright);

            AssertValue(value, "Dirk Lemstra");
        }
    }
}
