// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using NSubstitute;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCompareMethod
    {
        [Fact]
        public void ShouldThrowAnExceptionWhenImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenImageIsNullAndErrorMetricIsSpecified()
        {
            using var image = new MagickImage();
            using var diff = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenImageIsNullAndSettingsAreNotNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null, new CompareSettings(), out var distortion));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Compare(image, null, out var distortion));
        }

        [Fact]
        public void ShouldReturnEmptyErrorInfoWhenTheImagesAreEqual()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = new MagickImage(Files.Builtin.Logo);
            var errorInfo = image.Compare(other);

            Assert.NotNull(errorInfo);
            Assert.Equal(0, errorInfo.MeanErrorPerPixel);
            Assert.Equal(0, errorInfo.NormalizedMaximumError);
            Assert.Equal(0, errorInfo.NormalizedMeanError);
        }

        [Fact]
        public void ShouldReturnZeroWhenTheImagesAreEqual()
        {
            var settings = new CompareSettings
            {
                Metric = ErrorMetric.RootMeanSquared,
            };

            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = new MagickImage(Files.Builtin.Logo);
            using var diff = image.Compare(other, settings, out var result);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldReturnZeroWhenTheImagesAreEqualAndErrorMetricIsRootMeanSquared()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = new MagickImage(Files.Builtin.Logo);
            using var diff = image.Compare(other, ErrorMetric.RootMeanSquared, out var result);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldReturnErrorInfoWhenTheImagesAreNotEqual()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = new MagickImage(Files.Builtin.Logo);
            other.Rotate(180);

            var errorInfo = image.Compare(other);

#if Q8
            Assert.InRange(errorInfo.MeanErrorPerPixel, 44.55, 44.56);
#else
            Assert.InRange(errorInfo.MeanErrorPerPixel, 11450.85, 11450.86);
#endif
            Assert.Equal(1, errorInfo.NormalizedMaximumError);
            Assert.InRange(errorInfo.NormalizedMeanError, 0.13, 0.14);
        }

        [Fact]
        public void ShouldReturnNonZeroValueWhenTheImagesAreNotEqual()
        {
            var settings = new CompareSettings
            {
                Metric = ErrorMetric.RootMeanSquared,
                HighlightColor = MagickColors.Yellow,
                LowlightColor = MagickColors.Red,
                MasklightColor = MagickColors.Magenta,
            };

            using var image = new MagickImage(Files.Builtin.Logo);
            using var mask = new MagickImage("xc:white", image.Width, image.Height - 100);
            image.SetReadMask(mask);

            using var other = new MagickImage(Files.Builtin.Logo);
            other.Rotate(180);

            using var diff = image.Compare(other, settings, out var result);

            Assert.InRange(result, 0.36, 0.37);
            ColorAssert.Equal(MagickColors.Yellow, diff, 150, 50);
            ColorAssert.Equal(MagickColors.Red, diff, 150, 250);
            ColorAssert.Equal(MagickColors.Magenta, diff, 150, 450);
        }

        [Fact]
        public void ShouldUseTheColorFuzz()
        {
            using var image = new MagickImage(new MagickColor("#f1d3bc"), 1, 1);
            using var other = new MagickImage(new MagickColor("#24292e"), 1, 1);
            image.ColorFuzz = new Percentage(81);
            using var diff = image.Compare(other, ErrorMetric.Absolute, out var result);

            Assert.Equal(0, result);
            ColorAssert.Equal(new MagickColor("#fd2ff729f28b"), diff, 0, 0);
        }
    }
}
