﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
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
    public class TheRangeThresholdMethod
    {
        public class WithPercentage
        {
            [Fact]
            public void ShouldChangeTheImage()
            {
                using var image = new MagickImage("gradient:", 50, 256);
                image.RangeThreshold(new Percentage(40), new Percentage(40), new Percentage(60), new Percentage(60));

                ColorAssert.Equal(MagickColors.Black, image, 22, 101);
                ColorAssert.Equal(MagickColors.White, image, 22, 103);
                ColorAssert.Equal(MagickColors.White, image, 22, 152);
                ColorAssert.Equal(MagickColors.Black, image, 22, 154);
            }
        }

        public class WithQuantum
        {
            [Fact]
            public void ShouldChangeTheImage()
            {
                using var image = new MagickImage("gradient:", 50, 256);
                var lowBlack = (QuantumType)(Quantum.Max * 0.4);
                var lowWhite = (QuantumType)(Quantum.Max * 0.4);
                var highWhite = (QuantumType)(Quantum.Max * 0.6);
                var highBlack = (QuantumType)(Quantum.Max * 0.6);
                image.RangeThreshold(lowBlack, lowWhite, highWhite, highBlack);

                ColorAssert.Equal(MagickColors.Black, image, 22, 101);
                ColorAssert.Equal(MagickColors.White, image, 22, 103);
                ColorAssert.Equal(MagickColors.White, image, 22, 152);
                ColorAssert.Equal(MagickColors.Black, image, 22, 154);
            }
        }
    }
}
