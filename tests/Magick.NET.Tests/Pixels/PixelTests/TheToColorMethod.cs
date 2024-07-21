// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Colors;
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
    public class TheToColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenImageHasNoChannels()
        {
            using var image = new MagickImage();
            using var pixels = new UnsafePixelCollection(image);
            var pixel = Pixel.Create(pixels, 0, 0, Array.Empty<QuantumType>());

            Assert.Null(pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForOneChannel()
        {
            var pixel = new Pixel(0, 0, 1);
            pixel.SetValues(new QuantumType[] { Quantum.Max });

            ColorAssert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max), pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForTwoChannels()
        {
            var pixel = new Pixel(0, 0, 2);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0 });

            ColorAssert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0), pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForThreeChannels()
        {
            var half = (QuantumType)(Quantum.Max / 2.0);
            var pixel = new Pixel(0, 0, 3);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half });

            ColorAssert.Equal(new MagickColor(Quantum.Max, 0, half), pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForFourRgbChannels()
        {
            var half = (QuantumType)(Quantum.Max / 2.0);
            var pixel = new Pixel(0, 0, 4);
            pixel.SetValues(new QuantumType[] { 0, half, Quantum.Max, Quantum.Max });

            ColorAssert.Equal(new MagickColor(0, half, Quantum.Max, Quantum.Max), pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForFourCmykChannels()
        {
            using var image = new MagickImage("xc:cmyk(0, 127.499, 255, 255)", 1, 1);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = pixels.GetPixel(0, 0);
            var half = (QuantumType)(Quantum.Max / 2.0);
            var color = new ColorCMYK(0, half, Quantum.Max, Quantum.Max);

            ColorAssert.Equal(color.ToMagickColor(), pixel.ToColor());
        }

        [Fact]
        public void ShouldReturnTheCorrectValueForFourFiveChannels()
        {
            var half = (QuantumType)(Quantum.Max / 2.0);
            var pixel = new Pixel(0, 0, 5);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half, Quantum.Max, Quantum.Max });

            ColorAssert.Equal(new MagickColor(Quantum.Max, 0, half, Quantum.Max), pixel.ToColor());
        }
    }
}
