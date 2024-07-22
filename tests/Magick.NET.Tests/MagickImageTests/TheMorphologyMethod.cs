// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
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
    public class TheMorphologyMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Morphology(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenKernelCannotBeParsed()
        {
            using var image = new MagickImage();
            var settings = new MorphologySettings
            {
                Method = MorphologyMethod.Smooth,
                UserKernel = "Magick",
            };

            var exception = Assert.Throws<MagickOptionErrorException>(() => image.Morphology(settings));
            Assert.Contains("Unable to parse kernel.", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenIterationsIsLowerThanMinusOne()
        {
            using var image = new MagickImage();
            var settings = new MorphologySettings();
            settings.Iterations = -2;

            Assert.Throws<ArgumentException>("settings", () => image.Morphology(settings));
        }

        [Fact]
        public void ShouldUseTheSpecifiedSettings()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var settings = new MorphologySettings();
            settings.Method = MorphologyMethod.Convolve;
            settings.ConvolveBias = new Percentage(50);
            settings.Kernel = Kernel.DoG;
            settings.KernelArguments = "0x2";

            image.Morphology(settings);

            var half = (QuantumType)((Quantum.Max / 2.0) + 0.5);
            ColorAssert.Equal(new MagickColor(half, half, half), image, 120, 160);
        }

        [Fact]
        public void ShouldUseTheArguments()
        {
            using var original = new MagickImage(Files.Builtin.Logo);
            using var image = new MagickImage(Files.Builtin.Logo);
            var settings = new MorphologySettings
            {
                Method = MorphologyMethod.Dilate,
                Kernel = Kernel.Square,
                Iterations = 1,
            };
            image.Morphology(settings);

            var distortion = image.Compare(original, ErrorMetric.RootMeanSquared);
            Assert.InRange(distortion, 0.12, 0.13);
        }

        [Fact]
        public void ShouldUseTheKernel()
        {
            using var original = new MagickImage(Files.Builtin.Logo);
            using var image = new MagickImage(Files.Builtin.Logo);
            var settings = new MorphologySettings
            {
                Method = MorphologyMethod.Convolve,
                UserKernel = "3: 0.3,0.6,0.3 0.6,1.0,0.6 0.3,0.6,0.3",
            };

            image.Morphology(settings);
            image.Clamp();

            var distortion = image.Compare(original, ErrorMetric.RootMeanSquared);

            Assert.InRange(distortion, 0.20, 0.21);
        }
    }
}
