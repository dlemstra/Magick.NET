// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Runtime.InteropServices;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheComplexMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();
            var settings = new ComplexSettings(ComplexOperator.RealImaginary);

            Assert.Throws<InvalidOperationException>(() => images.Complex(settings));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentNullException>("complexSettings", () => images.Complex(null!));
        }

        [Fact]
        public void ShouldApplyTheOperatorToTheImages()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            var settings = new ComplexSettings(ComplexOperator.MagnitudePhase);
            images.Complex(settings);

            Assert.Equal(2, images.Count);

#if Q8
            ColorAssert.Equal(new MagickColor("#a4a4a39f"), images[1], 10, 10);

#elif Q16
            ColorAssert.Equal(new MagickColor("#a4cda471a3ce9fff"), images[1], 10, 10);
#else
            ColorAssert.Equal(new MagickColor("#a4cca470a3ce9fff"), images[1], 10, 10);
#endif
        }

        [Fact]
        public void ShouldClampTheValuesOfThePixel()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            var settings = new ComplexSettings(ComplexOperator.Conjugate);
            images.Complex(settings);

            Assert.Equal(2, images.Count);

#if Q16HDRI
            ColorAssert.Equal(new MagickColor("#00000000"), images[1], 39, 10);
#else
            ColorAssert.Equal(new MagickColor("#00000000"), images[1], 10, 10);
#endif
        }
    }
}
