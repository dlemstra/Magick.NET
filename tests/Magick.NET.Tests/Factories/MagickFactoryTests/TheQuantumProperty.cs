// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheQuantumInfoProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectDepthValue()
        {
            var factory = new MagickFactory();
#if Q8
            Assert.Equal(8, factory.Quantum.Depth);
#else
            Assert.Equal(16, factory.Quantum.Depth);
#endif
        }

        [Fact]
        public void ShouldHaveTheCorrectMaxValue()
        {
            var factory = new MagickFactory();
#if Q8
            Assert.Equal(byte.MaxValue, factory.Quantum.Max);
#elif Q16
            Assert.Equal(ushort.MaxValue, factory.Quantum.Max);
#else
            Assert.Equal((float)ushort.MaxValue, factory.Quantum.Max);
#endif
        }
    }
}
