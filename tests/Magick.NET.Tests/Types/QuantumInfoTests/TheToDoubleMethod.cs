// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class QuantumInfoTests
    {
        public class TheToDoubleMethod
        {
            [Fact]
            public void ShouldReturnAnInstanceWithTheCorrectValues()
            {
                var quantumInfo = QuantumInfo.Instance.ToDouble();

#if Q8
                Assert.Equal(8, quantumInfo.Depth);
                Assert.Equal(255.0, quantumInfo.Max);
#else
                Assert.Equal(16, quantumInfo.Depth);
                Assert.Equal(65535.0, quantumInfo.Max);
#endif
            }
        }
    }
}
