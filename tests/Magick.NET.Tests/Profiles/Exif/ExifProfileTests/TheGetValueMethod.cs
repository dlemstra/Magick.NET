// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheGetValueMethod
    {
        [Fact]
        public void ShouldReturnStringWhenValueIsString()
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.Software, "Magick.NET");
            var value = profile.GetValue(ExifTag.Software);

            AssertValue(value, "Magick.NET");
        }

        [Fact]
        public void ShouldReturnByteArrayWhenDataTypeIsUndefined()
        {
            var image = new MagickImage(Files.ExifUndefTypeJPG);
            var profile = image.GetExifProfile();

            Assert.NotNull(profile);

            var value = profile.GetValue(ExifTag.ExifVersion);

            Assert.NotNull(value);
            Assert.Equal(ExifDataType.Undefined, value.DataType);

            var data = value.GetValue() as byte[];
            Assert.NotNull(data);
            Assert.Equal(4, data.Length);
        }
    }
}
