// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheModulateMethod
    {
        [Fact]
        public void ShouldDefaultTo100PercentForSaturationAndHue()
        {
            using var image = new MagickImage(Files.TestPNG);
            using var other = image.Clone();
            image.Modulate(new Percentage(50));
            other.Modulate(new Percentage(50), new Percentage(100), new Percentage(100));

            var difference = image.Compare(other, ErrorMetric.RootMeanSquared);

            Assert.Equal(0, difference);
        }

        [Fact]
        public void ShouldDefaultTo100PercentForSaturation()
        {
            using var image = new MagickImage(Files.TestPNG);
            using var other = image.Clone();
            image.Modulate(new Percentage(50), new Percentage(25));
            other.Modulate(new Percentage(50), new Percentage(25), new Percentage(100));

            var difference = image.Compare(other, ErrorMetric.RootMeanSquared);

            Assert.Equal(0, difference);
        }

        [Fact]
        public void ShouldModulateImage()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.Modulate(new Percentage(70), new Percentage(30));

#if Q8
            ColorAssert.Equal(new MagickColor("#743e3e"), image, 25, 70);
            ColorAssert.Equal(new MagickColor("#0b0b0b"), image, 25, 40);
            ColorAssert.Equal(new MagickColor("#1f3a1f"), image, 75, 70);
            ColorAssert.Equal(new MagickColor("#5a5a5a"), image, 75, 40);
            ColorAssert.Equal(new MagickColor("#3e3e74"), image, 125, 70);
            ColorAssert.Equal(new MagickColor("#a8a8a8"), image, 125, 40);
#else
            ColorAssert.Equal(new MagickColor("#747a3eb83eb8"), image, 25, 70);
            ColorAssert.Equal(new MagickColor("#0b5f0b5f0b5f"), image, 25, 40);
            ColorAssert.Equal(new MagickColor("#1f7c3a781f7c"), image, 75, 70);
            ColorAssert.Equal(new MagickColor("#5ab75ab75ab7"), image, 75, 40);
            ColorAssert.Equal(new MagickColor("#3eb83eb8747a"), image, 125, 70);
            ColorAssert.Equal(new MagickColor("#a88ba88ba88b"), image, 125, 40);
#endif
        }
    }
}
