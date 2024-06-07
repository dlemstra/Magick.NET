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
    public class TheRandomThresholdMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenPercentageLowIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("percentageLow", () => image.RandomThreshold(new Percentage(-1), new Percentage(1)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPercentageHighIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("percentageHigh", () => image.RandomThreshold(new Percentage(1), new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldChangeThePixelsBetweenLowAndHighValue()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RandomThreshold((QuantumType)(Quantum.Max / 4), (QuantumType)(Quantum.Max / 2));

            ColorAssert.Equal(MagickColors.Black, image, 52, 52);
            ColorAssert.Equal(MagickColors.White, image, 75, 52);
            ColorAssert.Equal(MagickColors.Red, image, 31, 90);
            ColorAssert.Equal(MagickColors.Lime, image, 69, 90);
            ColorAssert.Equal(MagickColors.Blue, image, 120, 90);
        }

        [Fact]
        public void ShouldChangeThePixelsBetweenLowAndHighPercentage()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RandomThreshold(new Percentage(25), new Percentage(50));

            ColorAssert.Equal(MagickColors.Black, image, 52, 52);
            ColorAssert.Equal(MagickColors.White, image, 75, 52);
            ColorAssert.Equal(MagickColors.Red, image, 31, 90);
            ColorAssert.Equal(MagickColors.Lime, image, 69, 90);
            ColorAssert.Equal(MagickColors.Blue, image, 120, 90);
        }

        [Fact]
        public void ShouldChangeTheSpecifiedChannelBetweenLowAndHighValue()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RandomThreshold((QuantumType)(Quantum.Max / 4), (QuantumType)(Quantum.Max / 2), Channels.Blue);

            ColorAssert.Equal(new MagickColor("#3e283e280000"), image, 52, 52);
            ColorAssert.Equal(new MagickColor("#81988198ffff"), image, 75, 52);
            ColorAssert.Equal(new MagickColor("#ffff3d560000"), image, 31, 90);
            ColorAssert.Equal(new MagickColor("#3e40ffff0000"), image, 69, 90);
            ColorAssert.Equal(new MagickColor("#000034ecffff"), image, 120, 90);
        }

        [Fact]
        public void ShouldChangeTheSpecifiedChannelBetweenLowAndHighPercentage()
        {
            using var image = new MagickImage(Files.TestPNG);
            image.RandomThreshold(new Percentage(25), new Percentage(50), Channels.Green);

            ColorAssert.Equal(new MagickColor("#3e2800003e28"), image, 52, 52);
            ColorAssert.Equal(new MagickColor("#8198ffff8198"), image, 75, 52);
            ColorAssert.Equal(MagickColors.Red, image, 31, 90);
            ColorAssert.Equal(new MagickColor("#3e40ffff0000"), image, 69, 90);
            ColorAssert.Equal(MagickColors.Blue, image, 120, 90);
        }
    }
}
