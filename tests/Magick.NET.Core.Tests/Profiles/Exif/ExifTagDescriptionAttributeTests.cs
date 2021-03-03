// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public class ExifTagDescriptionAttributeTests
    {
        [Fact]
        public void Test_ExifTag()
        {
            var exifProfile = new ExifProfile();

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)1);
            var value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("None", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)2);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("Inches", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)3);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("Centimeter", value.ToString());

            exifProfile.SetValue(ExifTag.ResolutionUnit, (ushort)4);
            value = exifProfile.GetValue(ExifTag.ResolutionUnit);
            Assert.Equal("4", value.ToString());

            exifProfile.SetValue(ExifTag.ImageWidth, 123);
            var imageWidth = exifProfile.GetValue(ExifTag.ImageWidth);
            Assert.Equal("123", imageWidth.ToString());
        }
    }
}
