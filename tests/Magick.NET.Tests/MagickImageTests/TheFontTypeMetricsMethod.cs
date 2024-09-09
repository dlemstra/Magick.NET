// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFontTypeMetricsMethod
    {
        public class WithText
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.FontTypeMetrics(null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.FontTypeMetrics(string.Empty));
            }

            [Fact]
            public void ShouldReturnTheCorrectValues()
            {
                using var image = new MagickImage();
                image.Settings.Font = "Arial";
                image.Settings.FontPointsize = 15;

                var typeMetric = image.FontTypeMetrics("Magick.NET");

                Assert.NotNull(typeMetric);
                Assert.Equal(14, typeMetric.Ascent);
                Assert.Equal(-4, typeMetric.Descent);
                Assert.Equal(30, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(18, typeMetric.TextHeight);
                Assert.Equal(82, typeMetric.TextWidth);
                Assert.Equal(-2.138671875, typeMetric.UnderlinePosition);
                Assert.Equal(1.0986328125, typeMetric.UnderlineThickness);
            }

            [Fact]
            public void ShouldUseTheFontPointsize()
            {
                using var image = new MagickImage();
                image.Settings.Font = "Arial";
                image.Settings.FontPointsize = 150;

                var typeMetric = image.FontTypeMetrics("Magick.NET");

                Assert.NotNull(typeMetric);
                Assert.Equal(136, typeMetric.Ascent);
                Assert.Equal(-32, typeMetric.Descent);
                Assert.Equal(300, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(168, typeMetric.TextHeight);
                Assert.Equal(817, typeMetric.TextWidth);
                Assert.Equal(-21.38671875, typeMetric.UnderlinePosition);
                Assert.Equal(10.986328125, typeMetric.UnderlineThickness);
            }
        }

        public class WithTextAndIgnoreNewlines
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.FontTypeMetrics(null!, true));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.FontTypeMetrics(string.Empty, true));
            }

            [Fact]
            public void ShouldNotIgnoreTheNewlinesWhenOptionIsFalse()
            {
                using var image = new MagickImage();
                image.Settings.Font = "Arial";
                image.Settings.FontPointsize = 42;

                var typeMetric = image.FontTypeMetrics("Magick.NET\nIs the best", false);

                Assert.NotNull(typeMetric);
                Assert.Equal(39, typeMetric.Ascent);
                Assert.Equal(-9, typeMetric.Descent);
                Assert.Equal(84, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(96, typeMetric.TextHeight);
                Assert.Equal(229, typeMetric.TextWidth);
                Assert.Equal(-5.98828125, typeMetric.UnderlinePosition);
                Assert.Equal(3.076171875, typeMetric.UnderlineThickness);
            }

            [Fact]
            public void ShouldIgnoreTheNewlinesWhenOptionIsTrue()
            {
                using var image = new MagickImage();
                image.Settings.Font = "Arial";
                image.Settings.FontPointsize = 42;

                var typeMetric = image.FontTypeMetrics("Magick.NET\nIs the best", true);

                Assert.NotNull(typeMetric);
                Assert.Equal(39, typeMetric.Ascent);
                Assert.Equal(-9, typeMetric.Descent);
                Assert.Equal(84, typeMetric.MaxHorizontalAdvance);
                Assert.Equal(48, typeMetric.TextHeight);
                Assert.Equal(454, typeMetric.TextWidth);
                Assert.Equal(-5.98828125, typeMetric.UnderlinePosition);
                Assert.Equal(3.076171875, typeMetric.UnderlineThickness);
            }
        }
    }
}
