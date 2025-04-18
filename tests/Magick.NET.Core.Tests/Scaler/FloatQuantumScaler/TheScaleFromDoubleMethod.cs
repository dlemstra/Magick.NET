// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class FloatQuantumScalerTests
{
    public class TheScaleFromDoubleMethod
    {
        [Theory]
        [InlineData(0.0, ushort.MinValue)]
        [InlineData(1.0, ushort.MaxValue)]
        public void ShouldScaleDoubleToFloat(double input, float output)
        {
            var scaler = new FloatQuantumScaler();
            var result = scaler.ScaleFromDouble(input);

            Assert.Equal(output, result);
        }
    }
}
