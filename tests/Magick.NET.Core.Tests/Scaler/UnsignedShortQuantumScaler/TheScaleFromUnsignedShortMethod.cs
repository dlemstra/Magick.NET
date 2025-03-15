// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class UnsignedShortQuantumScalerTests
{
    public class TheScaleFromUnsignedShortMethod
    {
        [Theory]
        [InlineData(ushort.MinValue, ushort.MinValue)]
        [InlineData(ushort.MaxValue, ushort.MaxValue)]
        public void ShouldScaleUnsignedShortToByte(ushort input, ushort output)
        {
            var scaler = new UnsignedShortQuantumScaler();
            var result = scaler.ScaleFromUnsignedShort(input);

            Assert.Equal(output, result);
        }
    }
}
