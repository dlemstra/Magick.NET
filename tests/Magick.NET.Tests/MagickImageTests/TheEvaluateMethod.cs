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
    public class TheEvaluateMethod
    {
        public class WithEvaluateFunction
        {
            [Fact]
            public void ShouldThrowExceptionWhenArgumentsIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("arguments", () => image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArgumentsIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("arguments", () => image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, Array.Empty<double>()));
            }
        }

        public class WithEvaluateOperator
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("percentage", () => image.Evaluate(Channels.Red, EvaluateOperator.Set, new Percentage(-1)));
            }
#endif

            [Fact]
            public void ShouldChangeTheSpecifiedChannels()
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                image.Evaluate(Channels.Red, EvaluateFunction.Arcsin, new double[] { 5.0 });

                ColorAssert.Equal(new MagickColor("#9068ffffffff"), image, 100, 295);

                image.Evaluate(Channels.Red, new MagickGeometry(0, 0, 100, 295), EvaluateOperator.Set, 0);

                ColorAssert.Equal(new MagickColor("#0ff"), image, 99, 195);
                ColorAssert.Equal(new MagickColor("#9068ffffffff"), image, 100, 295);

                image.Evaluate(Channels.Green, EvaluateOperator.Set, 0);

                ColorAssert.Equal(new MagickColor("#00f"), image, 99, 195);
                ColorAssert.Equal(new MagickColor("#90680000ffff"), image, 100, 295);
            }

            [Fact]
            public void ShouldUseWriteMask()
            {
                using var image = new MagickImage(MagickColors.Black, 2, 1);
                using var mask = new MagickImage(MagickColors.White, 2, 1);
                using var pixels = mask.GetPixelsUnsafe();
                pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0 });

                image.SetWriteMask(mask);

                image.Evaluate(Channels.Red, EvaluateOperator.Set, Quantum.Max);

                ColorAssert.Equal(MagickColors.Red, image, 0, 0);
                ColorAssert.Equal(MagickColors.Black, image, 1, 0);
            }
        }

        public class WithGeometry
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("geometry", () => image.Evaluate(Channels.Red, null!, EvaluateOperator.Set, 0.0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenPercentageIsNegative()
            {
                using var image = new MagickImage();
                var geometry = new MagickGeometry(new Percentage(100), new Percentage(100));

                Assert.Throws<ArgumentException>("percentage", () => image.Evaluate(Channels.Red, geometry, EvaluateOperator.Set, new Percentage(-1)));
            }
#endif

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsInvalid()
            {
                using var image = new MagickImage();
                var geometry = new MagickGeometry(new Percentage(100), new Percentage(100));

                Assert.Throws<MagickCorruptImageErrorException>(() => image.Evaluate(Channels.Red, geometry, EvaluateOperator.Set, 0.0));
            }
        }
    }
}
