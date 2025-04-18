// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ByteQuantumScalerTests
{
    public class TheScaleFromByteMethod
    {
        [Theory]
        [InlineData(byte.MinValue, byte.MinValue)]
        [InlineData(byte.MaxValue, byte.MaxValue)]
        public void ShouldScaleByteToByte(byte input, byte output)
        {
            var scaler = new ByteQuantumScaler();
            var result = scaler.ScaleFromByte(input);

            Assert.Equal(output, result);
        }
    }
}
