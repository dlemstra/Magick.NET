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

public partial class PixelTests
{
    public class TheEqualsMethod
    {
        public class WithObject
        {
            [Fact]
            public void ShouldReturnFalseWhenValueIsNull()
            {
                var pixel = new Pixel(0, 0, 3);

                Assert.False(pixel.Equals((object)null!));
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsTheSame()
            {
                var pixel = new Pixel(0, 0, 3);

                Assert.True(pixel.Equals((object)pixel));
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsEqual()
            {
                var first = new Pixel(0, 0, 3);
                first.SetChannel(0, 100);
                first.SetChannel(1, 150);
                first.SetChannel(2, 200);

                var second = new Pixel(0, 0, 3);
                second.SetChannel(0, 100);
                second.SetChannel(1, 150);
                second.SetChannel(2, 200);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenValueIsNotEqual()
            {
                var first = new Pixel(0, 0, 1);
                first.SetChannel(0, 100);

                var second = new Pixel(0, 0, 1);
                second.SetChannel(0, 50);

                Assert.False(first.Equals((object)second));
            }
        }

        public class WithPixel
        {
            [Fact]
            public void ShouldReturnFalseWhenValueIsNull()
            {
                var pixel = new Pixel(0, 0, 3);

                Assert.False(pixel.Equals((Pixel)null!));
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsTheSame()
            {
                var pixel = new Pixel(0, 0, 3);

                Assert.True(pixel.Equals(pixel));
            }

            [Fact]
            public void ShouldReturnTrueWhenValueIsEqual()
            {
                var first = new Pixel(0, 0, 3);
                first.SetChannel(0, 100);
                first.SetChannel(1, 150);
                first.SetChannel(2, 200);

                var second = new Pixel(0, 0, 3);
                second.SetChannel(0, 100);
                second.SetChannel(1, 150);
                second.SetChannel(2, 200);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenValueIsNotEqual()
            {
                var first = new Pixel(0, 0, 1);
                first.SetChannel(0, 100);

                var second = new Pixel(0, 0, 1);
                second.SetChannel(0, 50);

                Assert.False(first.Equals(second));
            }
        }

        public class WithMagickColor
        {
            [Fact]
            public void ShouldReturnFalseWhenValueIsNull()
            {
                var pixel = new Pixel(0, 0, 3);

                Assert.False(pixel.Equals((MagickColor)null!));
            }

            [Fact]
            public void ShouldReturnTrueWhenImageHasNoChannelsAndValueIsNull()
            {
                using var image = new MagickImage();
                using var pixels = new UnsafePixelCollection(image);
                var pixel = Pixel.Create(pixels, 0, 0, Array.Empty<QuantumType>());

                Assert.True(pixel.Equals((MagickColor)null!));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForOneChannel()
            {
                var pixel = new Pixel(0, 0, 1);
                pixel.SetValues(new QuantumType[] { Quantum.Max });

                Assert.True(pixel.Equals(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max)));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForTwoChannels()
            {
                var pixel = new Pixel(0, 0, 2);
                pixel.SetValues(new QuantumType[] { Quantum.Max, 0 });

                Assert.True(pixel.Equals(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0)));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForThreeChannels()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var pixel = new Pixel(0, 0, 3);
                pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half });

                Assert.True(pixel.Equals(new MagickColor(Quantum.Max, 0, half)));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForFourRgbChannels()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var pixel = new Pixel(0, 0, 4);
                pixel.SetValues(new QuantumType[] { 0, half, Quantum.Max, Quantum.Max });

                Assert.True(pixel.Equals(new MagickColor(0, half, Quantum.Max, Quantum.Max)));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForFourCmykChannels()
            {
                var color = new MagickColor("cmyk(0, 128, 255, 255)");
                using var image = new MagickImage(color, 1, 1);
                using var pixels = image.GetPixelsUnsafe();
                var pixel = pixels.GetPixel(0, 0);

                Assert.True(pixel.Equals(color));
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForFourFiveChannels()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var pixel = new Pixel(0, 0, 5);
                pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half, Quantum.Max, Quantum.Max });

                Assert.True(pixel.Equals(new MagickColor(Quantum.Max, 0, half, Quantum.Max)));
            }
        }
    }
}
