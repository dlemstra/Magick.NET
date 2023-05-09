// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
    public class TheGetChannelMethod
    {
        [Fact]
        public void ShouldReturnTheCorrectValueForTheSpecifiedChannel()
        {
            var half = (QuantumType)(Quantum.Max / 2.0);

            var pixel = new Pixel(0, 0, 3);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half });

            Assert.Equal(Quantum.Max, pixel.GetChannel(0));
            Assert.Equal(0, pixel.GetChannel(1));
            Assert.Equal(half, pixel.GetChannel(2));
        }
    }
}
