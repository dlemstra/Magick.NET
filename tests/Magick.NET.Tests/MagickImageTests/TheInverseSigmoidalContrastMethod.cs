// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheInverseSigmoidalContrastMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenMidpointPercentageIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("midpointPercentage", () => image.InverseSigmoidalContrast(0.0, new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldUseHalfOfQuantumForMidpointByDefault()
        {
            using var image = new MagickImage(Files.NoisePNG);
            using var other = image.Clone();
            image.InverseSigmoidalContrast(4.0);
            other.InverseSigmoidalContrast(4.0, new Percentage(50));

            var difference = other.Compare(image, ErrorMetric.RootMeanSquared);

            Assert.Equal(0.0, difference);
        }

        [Fact]
        public void ShouldAdjustTheImageContrast()
        {
            using var image = new MagickImage(Files.PictureJPG);
            using var other = image.Clone();
            other.InverseSigmoidalContrast(4.0, new Percentage(25));

            var difference = other.Compare(image, ErrorMetric.RootMeanSquared);

            Assert.InRange(difference, 0.11, 0.12);
        }

        [Fact]
        public void ShouldAdjustTheSpecifiedChannel()
        {
            using var image = new MagickImage(Files.PictureJPG);
            using var other = image.Clone();
            other.InverseSigmoidalContrast(4.0, Quantum.Max * 0.25, Channels.Blue);

            var difference = other.Compare(image, ErrorMetric.RootMeanSquared);

            Assert.InRange(difference, 0.05, 0.06);
        }
    }
}
