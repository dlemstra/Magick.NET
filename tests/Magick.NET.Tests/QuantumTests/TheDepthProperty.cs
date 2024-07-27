// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class QuantumTests
{
    public class TheDepthProperty
    {
        [Fact]
        public void ShouldHaveTheCorrectValue()
        {
#if Q8
            Assert.Equal(8U, Quantum.Depth);
#else
            Assert.Equal(16U, Quantum.Depth);
#endif
        }
    }
}
