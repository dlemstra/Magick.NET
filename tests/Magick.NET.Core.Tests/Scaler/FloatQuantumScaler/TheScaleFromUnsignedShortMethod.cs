// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class FloatQuantumScalerTests
{
    public class TheScaleFromUnsignedShortMethod
    {
        [Theory]
        [InlineData(ushort.MinValue, ushort.MinValue)]
        [InlineData(ushort.MaxValue, ushort.MaxValue)]
        public void ShouldScaleUnsignedShortToFloat(ushort input, float output)
        {
            var scaler = new FloatQuantumScaler();
            var result = scaler.ScaleFromUnsignedShort(input);

            Assert.Equal(output, result);
        }
    }
}
