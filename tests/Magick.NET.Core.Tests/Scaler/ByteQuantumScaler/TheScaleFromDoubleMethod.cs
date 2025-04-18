// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleFromDoubleMethod
    {
        [Theory]
        [InlineData(0.0, byte.MinValue)]
        [InlineData(1.0, byte.MaxValue)]
        public void ShouldScaleDoubleToByte(double input, byte output)
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleFromDouble(input);

            Assert.Equal(output, result);
        }
    }
}
