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
    public class TheLevelMethod
    {
        public class WithPercentage
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenBlackPointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("blackPointPercentage", () => image.Level(new Percentage(-1), new Percentage(1)));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWhitePointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("whitePointPercentage", () => image.Level(new Percentage(1), new Percentage(-1)));
            }
#endif

            [Fact]
            public void ShouldUseCompositeAsDefaultChannels()
            {
                using var first = new MagickImage(Files.MagickNETIconPNG);
                using var second = first.Clone();
                first.Level(new Percentage(50), new Percentage(10));
                second.Level(new Percentage(50), new Percentage(10), Channels.Composite);

                Assert.Equal(first.Signature, second.Signature);
            }

            [Fact]
            public void ShouldUseOneAsGammaDefault()
            {
                using var first = new MagickImage(Files.MagickNETIconPNG);
                using var second = first.Clone();
                first.Level(new Percentage(50), new Percentage(10));
                second.Level(new Percentage(50), new Percentage(10), 1.0, Channels.Composite);

                Assert.Equal(first.Signature, second.Signature);
            }

            [Fact]
            public void ShouldScaleTheColors()
            {
                using var image = new MagickImage(Files.MagickNETIconPNG);
                using var second = image.Clone();
                image.Level(new Percentage(50.0), new Percentage(10.0));

                var fifty = (QuantumType)(Quantum.Max * 0.5);
                var ten = (QuantumType)(Quantum.Max * 0.1);
                second.Level(fifty, ten, Channels.Red);
                second.Level(fifty, ten, Channels.Green | Channels.Blue);
                second.Level(fifty, ten, Channels.Alpha);

                Assert.Equal(image.Signature, second.Signature);
            }
        }

        public class WithPercentageAndChannel
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenBlackPointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("blackPointPercentage", () => image.Level(new Percentage(-1), new Percentage(1), Channels.Red));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWhitePointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("whitePointPercentage", () => image.Level(new Percentage(1), new Percentage(-1), Channels.Red));
            }
#endif
        }

        public class WithPercentageAndGamma
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenBlackPointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("blackPointPercentage", () => image.Level(new Percentage(-1), new Percentage(1), 2.0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWhitePointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("whitePointPercentage", () => image.Level(new Percentage(1), new Percentage(-1), 2.0));
            }
#endif
        }

        public class WithPercentageGammaAndChannel
        {
#if !Q16HDRI
            [Fact]
            public void ShouldThrowExceptionWhenBlackPointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("blackPointPercentage", () => image.Level(new Percentage(-1), new Percentage(1), 2.0, Channels.Red));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWhitePointPercentageIsNegative()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("whitePointPercentage", () => image.Level(new Percentage(1), new Percentage(-1), 2.0, Channels.Red));
            }
#endif
        }
    }
}
