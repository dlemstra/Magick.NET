// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickColorTests
{
    public class TheOperators
    {
        public class WithCompare
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var color = MagickColors.Red;

                Assert.False(color == null);
                Assert.True(color != null);
                Assert.False(color < null);
                Assert.False(color <= null);
                Assert.True(color > null);
                Assert.True(color >= null);
                Assert.False(null == color);
                Assert.True(null != color);
                Assert.True(null < color);
                Assert.True(null <= color);
                Assert.False(null > color);
                Assert.False(null >= color);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
            {
                var first = MagickColors.Red;
                var second = MagickColors.Green;

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.False(first < second);
                Assert.False(first <= second);
                Assert.True(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
            {
                var first = MagickColors.Red;
                var second = new MagickColor("red");

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }
        }

        public class WithPercentage
        {
            [Fact]
            public void ShouldReturnNullWhenValueIsNull()
            {
                MagickColor color = null;
                var percentage = new Percentage(50);

                var result = color * percentage;

                Assert.Null(result);
            }

            [Fact]
            public void ShouldNotAllowValueAbove100Percent()
            {
                var color = MagickColors.White;
                var percentage = new Percentage(150);

                var result = color * percentage;

                Assert.NotNull(result);
                Assert.Equal(Quantum.Max, result.R);
                Assert.Equal(Quantum.Max, result.G);
                Assert.Equal(Quantum.Max, result.B);
                Assert.Equal(Quantum.Max, result.A);
            }

            [Fact]
            public void ShouldMultiplyAllNonAlphaChannelsForRgbColor()
            {
                var color = MagickColors.White;
                var percentage = new Percentage(50);

                var result = color * percentage;

                Assert.NotNull(result);
                Assert.Equal(Quantum.Max / 2, result.R);
                Assert.Equal(Quantum.Max / 2, result.G);
                Assert.Equal(Quantum.Max / 2, result.B);
                Assert.Equal(Quantum.Max, result.A);
            }

            [Fact]
            public void ShouldMultiplyAllNonAlphaChannelsForCmykColor()
            {
                var color = new MagickColor("cmyka(100%,100%,100%,100%)");
                var percentage = new Percentage(50);

                var result = color * percentage;

                Assert.NotNull(result);
                Assert.Equal(Quantum.Max / 2, result.R);
                Assert.Equal(Quantum.Max / 2, result.G);
                Assert.Equal(Quantum.Max / 2, result.B);
                Assert.Equal(Quantum.Max / 2, result.K);
                Assert.Equal(Quantum.Max, result.A);
            }

            [Fact]
            public void ShouldLimitChannelToQuantumMax()
            {
                var color = new MagickColor("cmyka(50%,50%,50%,50%)");
                var percentage = new Percentage(220);

                var result = color * percentage;

                Assert.NotNull(result);
                Assert.Equal(Quantum.Max, result.R);
                Assert.Equal(Quantum.Max, result.G);
                Assert.Equal(Quantum.Max, result.B);
                Assert.Equal(Quantum.Max, result.K);
                Assert.Equal(Quantum.Max, result.A);
            }
        }
    }
}
