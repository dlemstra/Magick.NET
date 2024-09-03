// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ExifValueTests
{
    public class TheValueProperty
    {
        [Fact]
        public void ShouldReturnFalseWhenValueIsInvalidDataType1()
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.Software, "Magick.NET");

            IExifValue? value = profile.GetValue(ExifTag.Software);

            Assert.NotNull(value);
            Assert.False(value.SetValue(10.5));
        }

        [Fact]
        public void ShouldReturnFalseWhenValueIsInvalidDataType2()
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.ShutterSpeedValue, new SignedRational(75.55));

            IExifValue? value = profile.GetValue(ExifTag.ShutterSpeedValue);

            Assert.NotNull(value);
            Assert.False(value.SetValue(75));
        }

        [Fact]
        public void ShouldReturnFalseWhenValueIsInvalidDataType3()
        {
            var profile = new ExifProfile();
            profile.SetValue(ExifTag.XResolution, new Rational(150.0));

            IExifValue? value = profile.GetValue(ExifTag.XResolution);

            Assert.NotNull(value);
            Assert.Equal("150", value.ToString());
            Assert.False(value.SetValue("Magick.NET"));
        }
    }
}
