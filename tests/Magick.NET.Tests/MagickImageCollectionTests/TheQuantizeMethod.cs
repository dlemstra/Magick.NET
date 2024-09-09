// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheQuantizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Quantize());
        }

        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var images = new MagickImageCollection();
            images.Add(Files.FujiFilmFinePixS1ProJPG);

            Assert.Throws<ArgumentNullException>("settings", () => images.Quantize(null!));
        }

        [Fact]
        public void ShouldReturnNullWhenMeasureErrorsIsFalse()
        {
            using var images = new MagickImageCollection();
            images.Add(Files.FujiFilmFinePixS1ProJPG);

            var settings = new QuantizeSettings
            {
                Colors = 1,
                MeasureErrors = false,
            };

            var errorInfo = images.Quantize(settings);

            Assert.Null(errorInfo);
        }

        [Fact]
        public void ShouldReduceTheColors()
        {
            using var images = new MagickImageCollection();
            images.Add(Files.FujiFilmFinePixS1ProJPG);

            var settings = new QuantizeSettings
            {
                Colors = 3,
            };

            images.Quantize(settings);

#if Q8
            ColorAssert.Equal(new MagickColor("#2b414f"), images[0], 120, 140);
            ColorAssert.Equal(new MagickColor("#7b929f"), images[0], 95, 140);
            ColorAssert.Equal(new MagickColor("#44739f"), images[0], 300, 150);
#else
            ColorAssert.Equal(new MagickColor("#2af841624f09"), images[0], 120, 140);
            ColorAssert.Equal(new MagickColor("#7b3c92b69f5a"), images[0], 95, 140);
            ColorAssert.Equal(new MagickColor("#44bc73059f70"), images[0], 300, 150);
#endif
        }

        [Fact]
        public void ShouldReturnErrorInfoWhenMeasureErrorsIsTrue()
        {
            using var images = new MagickImageCollection();
            images.Add(Files.FujiFilmFinePixS1ProJPG);

            var settings = new QuantizeSettings
            {
                Colors = 3,
                MeasureErrors = true,
            };

            var errorInfo = images.Quantize(settings);

            Assert.NotNull(errorInfo);

#if Q8
            Assert.InRange(errorInfo.MeanErrorPerPixel, 11.54, 11.59);
#else
            Assert.InRange(errorInfo.MeanErrorPerPixel, 2967, 2979);
#endif
            Assert.InRange(errorInfo.NormalizedMaximumError, 0.46, 0.47);
            Assert.InRange(errorInfo.NormalizedMeanError, 0.004, 0.005);
        }
    }
}
