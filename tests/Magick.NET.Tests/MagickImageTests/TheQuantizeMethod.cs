// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheQuantizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSettingsAreNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Quantize(null!));
        }

        [Fact]
        public void ShouldReduceNumberOfColors()
        {
            var settings = new QuantizeSettings();
            settings.Colors = 8;

            Assert.Equal(DitherMethod.Riemersma, settings.DitherMethod);

            settings.DitherMethod = DitherMethod.No;
            settings.MeasureErrors = true;

            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var errorInfo = image.Quantize(settings);
            Assert.NotNull(errorInfo);

#if Q8
            Assert.InRange(errorInfo.MeanErrorPerPixel, 7.066, 7.067);
#else
            Assert.InRange(errorInfo.MeanErrorPerPixel, 1827.8, 1827.9);
#endif
            Assert.InRange(errorInfo.NormalizedMaximumError, 0.352, 0.354);
            Assert.InRange(errorInfo.NormalizedMeanError, 0.001, 0.002);
        }
    }
}
