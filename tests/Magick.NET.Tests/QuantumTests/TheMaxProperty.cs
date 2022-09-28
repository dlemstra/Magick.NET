// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class QuantumTests
    {
        public class TheMaxProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
#if Q8
                Assert.Equal(byte.MaxValue, Quantum.Max);
#elif Q16
                Assert.Equal(ushort.MaxValue, Quantum.Max);
#else
                Assert.Equal((float)ushort.MaxValue, Quantum.Max);
#endif
            }
        }
    }
}
