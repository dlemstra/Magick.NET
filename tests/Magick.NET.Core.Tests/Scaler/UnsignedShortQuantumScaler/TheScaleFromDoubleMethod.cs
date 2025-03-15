// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class UnsignedShortQuantumScalerTests
{
    public class TheScaleFromDoubleMethod
    {
        [Theory]
        [InlineData(0.0, ushort.MinValue)]
        [InlineData(1.0, ushort.MaxValue)]
        public void ShouldScaleDoubleToUnsignedShort(double input, ushort output)
        {
            var scaler = new UnsignedShortQuantumScaler();
            var result = scaler.ScaleFromDouble(input);

            Assert.Equal(output, result);
        }
    }
}
