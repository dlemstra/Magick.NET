// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleToDoubleMethod
    {
        [Theory]
        [InlineData(byte.MinValue, 0.0)]
        [InlineData(byte.MaxValue, 1.0)]
        public void ShouldScaleByteToDouble(byte input, double output)
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleToDouble(input);

            Assert.Equal(output, result);
        }
    }
}
