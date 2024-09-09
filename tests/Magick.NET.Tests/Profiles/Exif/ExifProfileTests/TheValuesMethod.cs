// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ExifProfileTests
{
    public class TheValuesMethod
    {
        [Fact]
        public void ShouldReturnTheValuesOfTheProfile()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetExifProfile();
            Assert.NotNull(profile);

            AssertProfile(profile);

            using var emptyImage = new MagickImage(Files.ImageMagickJPG);
            Assert.Null(emptyImage.GetExifProfile());
            emptyImage.SetProfile(profile);

            profile = emptyImage.GetExifProfile();
            AssertProfile(profile);
        }

        private static void AssertProfile(IExifProfile? profile)
        {
            Assert.NotNull(profile);

            Assert.Equal(44, profile.Values.Count());

            foreach (var value in profile.Values)
            {
                Assert.NotNull(value.GetValue());

                if (value.Tag == ExifTag.Software)
                    Assert.Equal("Adobe Photoshop 7.0", value.ToString());

                if (value.Tag == ExifTag.XResolution)
                    Assert.Equal(new Rational(300, 1), (Rational)value.GetValue());

                if (value.Tag == ExifTag.GPSLatitude)
                {
                    var pos = (Rational[])value.GetValue();
                    Assert.Equal(54, pos[0].ToDouble());
                    Assert.Equal(59.38, pos[1].ToDouble());
                    Assert.Equal(0, pos[2].ToDouble());
                }

                if (value.Tag == ExifTag.ShutterSpeedValue)
                    Assert.Equal(9.5, ((SignedRational)value.GetValue()).ToDouble());
            }
        }
    }
}
