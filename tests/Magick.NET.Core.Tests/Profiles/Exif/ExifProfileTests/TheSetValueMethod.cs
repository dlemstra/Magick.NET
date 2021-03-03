// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifProfileTests
    {
        public class TheSetValueMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenExifTagIsInvalid()
            {
                var exifProfile = new ExifProfile();

                Assert.Throws<NotSupportedException>(() =>
                {
                    exifProfile.SetValue(new ExifTag<int>((ExifTagValue)42), 42);
                });
            }

            [Fact]
            public void ShouldCorrectlyHandleFraction()
            {
                var profile = new ExifProfile();
                profile.SetValue(ExifTag.ShutterSpeedValue, new SignedRational(75.55));

                var value = profile.GetValue(ExifTag.ShutterSpeedValue);

                Assert.NotNull(value);
                Assert.Equal("1511/20", value.ToString());
            }

            [Fact]
            public void ShouldCorrectlyHandleArray()
            {
                Rational[] latitude = new Rational[] { new Rational(12.3), new Rational(4.56), new Rational(789.0) };

                var profile = new ExifProfile();
                profile.SetValue(ExifTag.GPSLatitude, latitude);

                var value = profile.GetValue(ExifTag.GPSLatitude);

                Assert.NotNull(value);
                Rational[] values = (Rational[])value.GetValue();
                Assert.NotNull(values);
                Assert.Equal(latitude, values);
            }
        }
    }
}
