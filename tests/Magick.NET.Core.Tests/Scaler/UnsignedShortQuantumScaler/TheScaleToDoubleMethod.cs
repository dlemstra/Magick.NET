// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class UnsignedShortQuantumScalerTests
{
    public class TheScaleToDoubleMethod
    {
        [Theory]
        [InlineData(ushort.MinValue, 0.0)]
        [InlineData(ushort.MaxValue, 1.0)]
        public void ShouldScaleUnsignedShortToDouble(ushort input, double output)
        {
            var scaler = new UnsignedShortQuantumScaler();
            var result = scaler.ScaleToDouble(input);

            Assert.Equal(output, result);
        }
    }
}
