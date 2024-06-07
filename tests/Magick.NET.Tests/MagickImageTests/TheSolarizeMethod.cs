// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSolarizeMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenFactorPercentageIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("factorPercentage", () => image.Solarize(new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldSolarizeTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Solarize();

            ColorAssert.Equal(MagickColors.Black, image, 125, 125);
            ColorAssert.Equal(new MagickColor("#007f7f"), image, 122, 143);
            ColorAssert.Equal(new MagickColor("#2e6935"), image, 435, 240);
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValue()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            using var other = image.Clone();
            image.Solarize();
            other.Solarize(new Percentage(50));

            var distortion = other.Compare(image, ErrorMetric.RootMeanSquared);

            Assert.Equal(0.0, distortion);
        }
    }
}
