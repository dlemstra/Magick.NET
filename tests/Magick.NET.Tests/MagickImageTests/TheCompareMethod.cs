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

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null!));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenImageIsNullAndErrorMetricIsSpecified()
        {
            using var image = new MagickImage();
            using var diff = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null!, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenImageIsNullAndSettingsAreNotNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.Compare(null!, new CompareSettings(ErrorMetric.PeakSignalToNoiseRatio), out var distortion));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Compare(image, null!, out var distortion));
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
            var settings = new CompareSettings(ErrorMetric.RootMeanSquared);

            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = new MagickImage(Files.Builtin.Logo);
            using var diff = image.Compare(other, settings, out var result);

            Assert.Equal(0, result);
        }

        [Fact]
        public void ShouldReturnTheDistortion()
        {
            using var image = new MagickImage(new MagickColor("#f1d3bc"), 1, 1);
            using var other = new MagickImage(new MagickColor("#24292e"), 1, 1);
            var distortion = image.Compare(other, ErrorMetric.RootMeanSquared);

            Assert.InRange(distortion, 0.68, 0.69);
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
            Assert.InRange(errorInfo.NormalizedMeanError, 0.40, 0.41);
        }

        [Fact]
        public void ShouldReturnNonZeroValueWhenTheImagesAreNotEqual()
        {
            var settings = new CompareSettings(ErrorMetric.RootMeanSquared)
            {
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

        [Theory]
        [InlineData(ErrorMetric.Undefined, 0.3653)]
        [InlineData(ErrorMetric.Absolute, 0.2905)]
        [InlineData(ErrorMetric.DotProductCorrelation, 0.4668)]
        [InlineData(ErrorMetric.Fuzz, 0.4662)]
        [InlineData(ErrorMetric.MeanAbsolute, 0.1747)]
        [InlineData(ErrorMetric.MeanErrorPerPixel, 0.1747)]
        [InlineData(ErrorMetric.MeanSquared, 0.1334)]
        [InlineData(ErrorMetric.NormalizedCrossCorrelation, 0.4668)]
        [InlineData(ErrorMetric.PeakAbsolute, 1)]
        [InlineData(ErrorMetric.PeakSignalToNoiseRatio, 0.0728)]
        [InlineData(ErrorMetric.PerceptualHash, 0)]
        [InlineData(ErrorMetric.RootMeanSquared, 0.3653)]
        [InlineData(ErrorMetric.StructuralSimilarity, 0.1546)]
        [InlineData(ErrorMetric.StructuralDissimilarity, 0.4226)]
        public void ShouldReturnTheCorrectValueForEachErrorMetric(ErrorMetric errorMetric, double expectedResult)
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var other = image.CloneAndMutate(image => image.Rotate(180));

            var result = image.Compare(other, errorMetric);
            Assert.InRange(result, expectedResult, expectedResult + 0.0001);
        }

        [Theory]
        [InlineData(ErrorMetric.Undefined, 0.4726)]
        [InlineData(ErrorMetric.Absolute, 0.3944, 0.3945)]
        [InlineData(ErrorMetric.DotProductCorrelation, 0.4748)]
        [InlineData(ErrorMetric.Fuzz, 0.5677, 0.5676)]
        [InlineData(ErrorMetric.MeanAbsolute, 0.2714)]
        [InlineData(ErrorMetric.MeanErrorPerPixel, 0.2714)]
        [InlineData(ErrorMetric.MeanSquared, 0.2233)]
        [InlineData(ErrorMetric.NormalizedCrossCorrelation, 0.4748)]
        [InlineData(ErrorMetric.PeakAbsolute, 1)]
        [InlineData(ErrorMetric.PeakSignalToNoiseRatio, 0.0542)]
        [InlineData(ErrorMetric.PerceptualHash, 0)]
        [InlineData(ErrorMetric.RootMeanSquared, 0.4726)]
        [InlineData(ErrorMetric.StructuralSimilarity, 0.2889)]
        [InlineData(ErrorMetric.StructuralDissimilarity, 0.3555)]
        public void ShouldReturnTheCorrectValueForEachErrorMetricForImageWithAlphaChannel(ErrorMetric errorMetric, double expectedResult, double? expectedArm64Result = null)
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            using var other = image.CloneAndMutate(image => image.Rotate(180));

            var result = image.Compare(other, errorMetric);
            if (expectedArm64Result != null && TestRuntime.IsArm64)
                Assert.InRange(result, expectedArm64Result.Value, expectedArm64Result.Value + 0.0001);
            else
                Assert.InRange(result, expectedResult, expectedResult + 0.0001);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForPhaseCorrelationErrorMetric()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Resize(64, 64);
            using var other = image.CloneAndMutate(image => image.Rotate(180));

            var result = image.Compare(other, ErrorMetric.PhaseCorrelation);
            Assert.InRange(result, 0.1871, 0.1872);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForPhaseCorrelationErrorMetricWithAlphaChannel()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.Resize(64, 64);
            using var other = image.CloneAndMutate(image => image.Rotate(180));

            var result = image.Compare(other, ErrorMetric.PhaseCorrelation);
            Assert.InRange(result, 0.0085, 0.0086);
        }
    }
}
