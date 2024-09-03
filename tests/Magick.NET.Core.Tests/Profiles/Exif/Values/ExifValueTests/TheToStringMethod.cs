// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ExifValueTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheValueAsString()
        {
            var value = new ExifShort(ExifTag.GPSDifferential);
            value.Value = 42;

            Assert.Equal("42", value.ToString());
        }

        [Theory]
        [InlineData(1, "None")]
        [InlineData(2, "Inches")]
        [InlineData(3, "Centimeter")]
        [InlineData(4, "4")]
        public void ShouldReturnTheExifTagDescriptionWhenItIsPresent(ushort input, string expected)
        {
            var exifProfile = new ExifProfile();

            exifProfile.SetValue(ExifTag.ResolutionUnit, input);
            var value = exifProfile.GetValue(ExifTag.ResolutionUnit);

            Assert.NotNull(value);
            Assert.Equal(expected, value.ToString());
        }
    }
}
