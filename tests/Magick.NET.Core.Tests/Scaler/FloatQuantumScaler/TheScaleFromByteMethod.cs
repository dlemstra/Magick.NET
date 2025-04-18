// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class FloatQuantumScalerTests
{
    public class TheScaleFromByteMethod
    {
        [Theory]
        [InlineData(byte.MinValue, ushort.MinValue)]
        [InlineData(byte.MaxValue, ushort.MaxValue)]
        public void ShouldScaleByteToFloat(byte input, float output)
        {
            var scaler = new FloatQuantumScaler();
            var result = scaler.ScaleFromByte(input);

            Assert.Equal(output, result);
        }
    }
}
