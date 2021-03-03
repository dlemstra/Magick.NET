// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickFactoryTests
    {
        public class TheQuantumInfoProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectDephValue()
            {
                var factory = new MagickFactory();
#if Q8
                Assert.Equal(8, factory.QuantumInfo.Depth);
#else
                Assert.Equal(16, factory.QuantumInfo.Depth);
#endif
            }

            [Fact]
            public void ShouldHaveTheCorrectMaxValue()
            {
                var factory = new MagickFactory();
#if Q8
                Assert.Equal(byte.MaxValue, factory.QuantumInfo.Max);
#elif Q16
                Assert.Equal(ushort.MaxValue, factory.QuantumInfo.Max);
#else
                Assert.Equal((float)ushort.MaxValue, factory.QuantumInfo.Max);
#endif
            }
        }
    }
}
