// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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

        [Fact]
        public void ShouldStoreRationalCorrectly()
        {
            var exposureTime = 1.0 / 1600;

            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var value = SetValueAndReturnValueFromNewImage(image, ExifTag.ExposureTime, new Rational(exposureTime));

            Assert.NotNull(value);
            Assert.NotEqual(exposureTime, value.Value.ToDouble());
        }

        [Fact]
        public void ShouldUsePrecisionOfValue()
        {
            var exposureTime = 1.0 / 1600;

            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var value = SetValueAndReturnValueFromNewImage(image, ExifTag.ExposureTime, new Rational(exposureTime, true));

            Assert.NotNull(value);
            Assert.Equal(exposureTime, value.Value.ToDouble());
        }

        [Fact]
        public void ShouldStorePositiveInfinityCorrectly()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var value = SetValueAndReturnValueFromNewImage(image, ExifTag.ExposureBiasValue, new SignedRational(double.PositiveInfinity));

            Assert.NotNull(value);
            Assert.Equal(double.PositiveInfinity, value.Value.ToDouble());
        }

        [Fact]
        public void ShouldStoreNegativeInfinityCorrectly()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var value = SetValueAndReturnValueFromNewImage(image, ExifTag.ExposureBiasValue, new SignedRational(double.NegativeInfinity));

            Assert.NotNull(value);
            Assert.Equal(double.NegativeInfinity, value.Value.ToDouble());
        }

        [Fact]
        public void ShouldSavePositiveInfinityWhenSettingNegativeInfinityWhenValueIsNotSigned()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var value = SetValueAndReturnValueFromNewImage(image, ExifTag.FlashEnergy, new Rational(double.NegativeInfinity));

            Assert.NotNull(value);
            Assert.Equal(double.PositiveInfinity, value.Value.ToDouble());
        }

        private static IExifValue<TValueType>? SetValueAndReturnValueFromNewImage<TValueType>(MagickImage image, ExifTag<TValueType> tag, TValueType value)
        {
            var profile = image.GetExifProfile();
            Assert.NotNull(profile);

            profile.SetValue(tag, value);
            image.SetProfile(profile);

            var bytes = image.ToByteArray();
            using var output = new MagickImage(bytes);
            profile = output.GetExifProfile();
            Assert.NotNull(profile);

            return profile.GetValue(tag);
        }
    }
}
