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
                Assert.Equal(Quantum.Max, byte.MaxValue);
#elif Q16
                Assert.Equal(Quantum.Max, ushort.MaxValue);
#else
                Assert.Equal(Quantum.Max, (float)ushort.MaxValue);
#endif
            }
        }
    }
}
