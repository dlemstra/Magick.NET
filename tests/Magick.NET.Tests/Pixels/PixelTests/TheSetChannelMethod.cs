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

namespace Magick.NET.Tests
{
    public partial class PixelTests
    {
        public class TheSetChannelMethod
        {
            [Fact]
            public void ShouldChangeTheValueOfTheSpecifiedChannel()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);

                var pixel = new Pixel(0, 0, 3);
                pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half });

                pixel.SetChannel(0, 0);
                pixel.SetChannel(1, half);
                pixel.SetChannel(2, Quantum.Max);

                Assert.Equal(0, pixel.GetChannel(0));
                Assert.Equal(half, pixel.GetChannel(1));
                Assert.Equal(Quantum.Max, pixel.GetChannel(2));
            }
        }
    }
}
